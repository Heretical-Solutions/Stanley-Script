using System.Threading;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyContext
		: IStanleyContext,
		  IStackMachine,
		  IExecutable
	{
		private readonly Stack<IStanleyVariable> stack;

		private IReportable reportable;

		private IREPL repl;


		private string contextID;


		private EExecutionStatus executionStatus;

		private int programCounter = 0;

		private string[] instructions;


		private int currentLine = -1;

		private bool stepMode = false;

		public StanleyContext(
			Stack<IStanleyVariable> stack)
		{
			this.stack = stack;

			Cleanup();
		}

		public void Initialize(
			string contextID,
			IReportable reportable,
			IREPL repl,
			CancellationToken cancellationToken)
		{
			this.contextID = contextID;

			this.reportable = reportable;

			this.repl = repl;

			this.CancellationToken = cancellationToken;


			executionStatus = EExecutionStatus.IDLE;

			instructions = null;

			currentLine = -1;

			programCounter = 0;

			stepMode = false;
		}


		#region IStanleyContext

		public string ContextID => contextID;

		public EExecutionStatus Status { get => executionStatus; }

		public int CurrentLine { get => currentLine; set => currentLine = value; }

		public int ProgramCounter { get => programCounter; set => programCounter = value; }

		public string[] Instructions { get => instructions; }

		public void LoadProgram(
			string[] instructions)
		{
			Stop();

			this.instructions = instructions;

			currentLine = -1;

			programCounter = 0;

			stepMode = false;

			//report.Clear();
		}

		public CancellationToken CancellationToken { get; set; }

		public void Cleanup()
		{
			contextID = null;

			reportable = null;

			repl = null;


			executionStatus = EExecutionStatus.IDLE;

			instructions = null;

			currentLine = -1;

			programCounter = 0;

			stepMode = false;

			CancellationToken = CancellationToken.None;
		}

		#endregion

		#region IStackMachine

		public int StackSize => stack.Count;

		public void Push(
			IStanleyVariable variable)
		{
			stack.Push(variable);
		}

		public bool Pop(
			out IStanleyVariable variable)
		{
			if (stack.Count > 0)
			{
				variable = stack.Pop();

				return true;
			}

			variable = null;

			return false;
		}

		public bool Peek(
			out IStanleyVariable variable)
		{
			if (stack.Count > 0)
			{
				variable = stack.Peek();

				return true;
			}

			variable = null;

			return false;
		}

		public bool PeekFromTop(
			int relativeIndex,
			out IStanleyVariable variable)
		{
			if (stack.Count > relativeIndex)
			{
				variable = stack.ToArray()[relativeIndex];

				return true;
			}

			variable = null;

			return false;
		}

		public bool PeekFromBottom(
			int relativeIndex,
			out IStanleyVariable variable)
		{
			if (stack.Count > relativeIndex)
			{
				variable = stack.ToArray()[stack.Count - relativeIndex - 1];

				return true;
			}

			variable = null;

			return false;
		}

		#endregion

		#region IExecutable

		public void Start()
		{
			switch (executionStatus)
			{
				case EExecutionStatus.RUNNING:
					break;

				case EExecutionStatus.IDLE:
				case EExecutionStatus.PAUSED:
				case EExecutionStatus.STOPPED:
				case EExecutionStatus.FINISHED:
				{
					if (instructions == null
						|| instructions.Length == 0)
						return;

					executionStatus = EExecutionStatus.RUNNING;

					programCounter = 0;

					currentLine = -1;

					stepMode = false;

					RunContextInternal(CancellationToken);

					//TaskExtensions.RunSync<bool>(
					//	() => RunContextInternal(CancellationToken));
				}

					break;
			}
		}

		public void Step()
		{
			switch (executionStatus)
			{
				case EExecutionStatus.RUNNING:
					break;

				case EExecutionStatus.PAUSED:
				{
					stepMode = true;

					executionStatus = EExecutionStatus.RUNNING;
				}

					break;

				case EExecutionStatus.IDLE:
				case EExecutionStatus.STOPPED:
				case EExecutionStatus.FINISHED:
				{
					if (instructions == null
						|| instructions.Length == 0)
						return;

					executionStatus = EExecutionStatus.RUNNING;

					programCounter = 0;

					currentLine = -1;

					stepMode = true;

					RunContextInternal(CancellationToken);

					//TaskExtensions.RunSync<bool>(
					//	() => RunContextInternal(CancellationToken));
				}

					break;
			}
		}

		public void Pause()
		{
			switch (executionStatus)
			{
				case EExecutionStatus.IDLE:
				case EExecutionStatus.STOPPED:
				case EExecutionStatus.FINISHED:
				case EExecutionStatus.PAUSED:
					break;

				case EExecutionStatus.RUNNING:
					executionStatus = EExecutionStatus.PAUSED;

					break;
			}
		}

		public void Resume()
		{
			switch (executionStatus)
			{
				case EExecutionStatus.IDLE:
				case EExecutionStatus.STOPPED:
				case EExecutionStatus.FINISHED:
				case EExecutionStatus.RUNNING:
					break;

				case EExecutionStatus.PAUSED:

					stepMode = false;

					executionStatus = EExecutionStatus.RUNNING;

					break;
			}
		}

		public void Stop()
		{
			executionStatus = EExecutionStatus.STOPPED;

			stepMode = false;
		}

		#endregion

		private async Task<bool> RunContextInternal(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				switch (executionStatus)
				{
					case EExecutionStatus.RUNNING:
						{
							if (programCounter < instructions.Length)
							{
								var result = await ExecuteInstructionInternal(cancellationToken);
								//.ThrowExceptions();

								if (!result)
								{
									executionStatus = EExecutionStatus.STOPPED;

									reportable.Log(
										contextID,
										"SCRIPT FINISHED DUE TO INTERNAL ERROR. CHECK LOGS FOR DETAILS");

									return false;
								}

								if (PauseBeforeNextLine())
								{
									executionStatus = EExecutionStatus.PAUSED;

									continue;
								}
							}
							else
							{
								stepMode = false;

								executionStatus = EExecutionStatus.FINISHED;

								reportable.Log(
									contextID,
									"SCRIPT FINISHED WITH EXECUTION STATUS \"FINISHED\"");

								return true;
							}
						}

						break;

					case EExecutionStatus.PAUSED:

						await Task.Yield();

						continue;

					case EExecutionStatus.IDLE:
					case EExecutionStatus.STOPPED:
					case EExecutionStatus.FINISHED:
						reportable.Log(
							contextID,
							$"SCRIPT INTERRUPTED WITH EXECUTION STATUS \"{executionStatus}\"");

						stepMode = false;

						return false;
				}
			}

			reportable.Log(
				contextID,
				$"SCRIPT INTERRUPTED WITH EXECUTION STATUS \"{executionStatus}\"");

			stepMode = false;

			return false;
		}

		private async Task<bool> ExecuteInstructionInternal(CancellationToken cancellationToken)
		{
			string instruction = instructions[programCounter];

			bool result = await repl.Execute(
				instruction,
				this,
				cancellationToken);
			//.ThrowExceptions();

			programCounter++;

			return result;
		}

		private bool PauseBeforeNextLine()
		{
			if (!stepMode)
				return false;

			if ((programCounter + 1) >= instructions.Length)
				return false;

			if (instructions[programCounter + 1].StartsWith("OP_LINE"))
				return true;

			return false;
		}
	}
}