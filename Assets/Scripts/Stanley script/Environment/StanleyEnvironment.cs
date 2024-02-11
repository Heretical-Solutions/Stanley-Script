using System.Collections.Generic;

using System.Text.RegularExpressions;

using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyEnvironment
		: IRuntimeEnvironment,
		  IExecutable,
		  IStackMachine,
		  IREPL,
		  IReportable
	{
		private const string REGEX_SPLIT_BY_WHITESPACE_UNLESS_WITHIN_QUOTES = "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";

		private readonly Dictionary<string, IStanleyVariable> inputVariables;

		private readonly Dictionary<string, List<IStanleyOperation>> operationsChainOfResponsibility;

		private readonly Dictionary<string, IStanleyVariable> runtimeVariables;

		private readonly Stack<IStanleyVariable> stack;

		private readonly List<string> report;

		private EExecutionStatus executionStatus;

		private int programCounter = 0;

		private string[] instructions;

		private int currentLine = -1;

		private CancellationTokenSource cancellationTokenSource;

		public StanleyEnvironment(
			Dictionary<string, IStanleyVariable> inputVariables,
			Dictionary<string, List<IStanleyOperation>> operationsChainOfResponsibility,
			Dictionary<string, IStanleyVariable> runtimeVariables,
			Stack<IStanleyVariable> stack,
			List<string> report)
		{
			this.inputVariables = inputVariables;

			this.operationsChainOfResponsibility = operationsChainOfResponsibility;

			this.runtimeVariables = runtimeVariables;

			this.stack = stack;

			this.report = report;

			executionStatus = EExecutionStatus.IDLE;

			instructions = null;

			currentLine = -1;

			programCounter = 0;

			cancellationTokenSource = new CancellationTokenSource();
		}

		#region IRuntimeEnvironment

		public bool LoadInputVariable(
			IStanleyVariable variable)
		{
			string name = variable.Name;

			if (inputVariables.ContainsKey(name))
			{
				return false;
			}

			inputVariables.Add(
				name,
				variable);

			return true;
		}

		public bool LoadOperation(
			IStanleyOperation operation)
		{
			string opcode = operation.Opcode;

			if (operationsChainOfResponsibility.ContainsKey(opcode))
			{
				operationsChainOfResponsibility[opcode].Insert(
					0,
					operation);
			}
			else
			{
				operationsChainOfResponsibility.Add(
					opcode,
					new List<IStanleyOperation>
					{
						operation
					});
			}

			if (operation.Aliases != null)
			{
				foreach (string alias in operation.Aliases)
				{
					if (operationsChainOfResponsibility.ContainsKey(alias))
					{
						operationsChainOfResponsibility[alias].Insert(
							0,
							operation);
					}
					else
					{
						operationsChainOfResponsibility.Add(
							alias,
							new List<IStanleyOperation>
							{
								operation
							});
					}
				}
			}

			return true;
		}

		public bool AddRuntimeVariable(
			IStanleyVariable variable)
		{
			string name = variable.Name;

			if (runtimeVariables.ContainsKey(name))
			{
				return false;
			}

			runtimeVariables.Add(
				name,
				variable);

			return true;
		}

		public void SetCurrentLine(
			int line)
		{
			currentLine = line;
		}

		public bool GetRuntimeVariable(
			string name,
			out IStanleyVariable variable)
		{
			return runtimeVariables.TryGetValue(
				name,
				out variable);
		}

		public bool GetImportVariable(
			string name,
			out IStanleyVariable variable)
		{
			return inputVariables.TryGetValue(
				name,
				out variable);
		}

		public bool GetOperation(
			string opcode,
			out IEnumerable<IStanleyOperation> operations)
		{
			bool reult = operationsChainOfResponsibility.TryGetValue(
				opcode,
				out var operationsList);

			operations = (IEnumerable<IStanleyOperation>)operationsList;

			return reult;
		}

		#endregion

		#region IExecutable

		public EExecutionStatus Status { get => executionStatus; }

		public int CurrentLine { get => currentLine; }

		public void LoadProgram(
			string[] instructions)
		{
			Stop();

			this.instructions = instructions;

			currentLine = 0;

			programCounter = 0;

			report.Clear();
		}

		public void Start()
		{
			switch (executionStatus)
			{
				case EExecutionStatus.IDLE:
				case EExecutionStatus.PAUSED:
				case EExecutionStatus.STOPPED:
				{
					executionStatus = EExecutionStatus.RUNNING;

					RunInternal();

					//Task.Run(async () =>
					//{
					//	RunInternal();
					//});
				}

				break;
			}
		}

		public void Step()
		{
			if (executionStatus == EExecutionStatus.PAUSED)
			{
				ExecuteInternal(cancellationTokenSource.Token);
			}
		}

		public void Pause()
		{
			executionStatus = EExecutionStatus.PAUSED;
		}

		public void Resume()
		{
			executionStatus = EExecutionStatus.RUNNING;
		}

		public void Stop()
		{
			executionStatus = EExecutionStatus.STOPPED;

			cancellationTokenSource.Cancel();
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

		#region IReportable

		public void Log(
			string message)
		{
			report.Add(message);
		}

		public string[] GetReport()
		{
			return report.ToArray();
		}

		#endregion

		#region IREPL

		public async Task<bool> Execute(
			string instruction,
			CancellationToken cancellationToken)
		{
			bool result = false;

			string[] instructionTokens = Regex.Split(
				instruction,
				REGEX_SPLIT_BY_WHITESPACE_UNLESS_WITHIN_QUOTES);

			//string[] instructionTokens = instruction.Split(' ');

			string opcode = instructionTokens[0];

			if (operationsChainOfResponsibility.ContainsKey(opcode))
			{
				bool handled = false;

				foreach (IStanleyOperation operation in operationsChainOfResponsibility[opcode])
				{
					if (operation.WillHandle(
						instructionTokens,
						this))
					{
						result = await operation.Handle(
							instructionTokens,
							this,
							cancellationToken);

						handled = true;

						break;
					}
				}

				if (!handled)
				{
					Log("OPERATION NOT HANDLED: " + opcode);

					return false;
				}
			}
			else
			{
				Log("UNKNOWN OPERATION: " + opcode);

				return false;
			}

			return result;
		}

		#endregion

		private async Task<bool> RunInternal()
		{
			while (executionStatus == EExecutionStatus.RUNNING)
			{
				if (programCounter < instructions.Length)
				{
					var result = await ExecuteInternal(cancellationTokenSource.Token);

					if (!result)
					{
						StopInternal();

						return false;
					}
				}
				else
				{
					StopInternal();

					return true;
				}
			}

			return true;
		}

		private void StopInternal()
		{
			executionStatus = EExecutionStatus.STOPPED;

			UnityEngine.Debug.Log("--------");

			UnityEngine.Debug.Log("REPORT:");

			UnityEngine.Debug.Log("--------");

			for (int i = 0; i < report.Count; i++)
			{
				UnityEngine.Debug.Log($"{i}: {report[i]}");
			}

			UnityEngine.Debug.Log("--------");

			cancellationTokenSource.Cancel();
		}

		private async Task<bool> ExecuteInternal(CancellationToken cancellationToken)
		{
			UnityEngine.Debug.Log("Executing line " + programCounter);

			string instruction = instructions[programCounter];

			bool result = await Execute(
				instruction,
				cancellationToken);

			programCounter++;

			return result;
		}
	}
}