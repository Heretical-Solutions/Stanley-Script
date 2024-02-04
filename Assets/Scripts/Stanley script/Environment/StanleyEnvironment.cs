using System.Collections.Generic;
using System.Threading;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyEnvironment
		: IRuntimeEnvironment,
		  IExecutable,
		  IStackMachine,
		  ILoggable
	{
		private Dictionary<string, IStanleyVariable> inputVariables;

		private Dictionary<string, List<IStanleyOperation>> operationsChainOfResponsibility;

		private Dictionary<string, IStanleyVariable> runtimeVariables;

		private Stack<IStanleyVariable> stack;

		private List<string> logs;

		private EExecutionStatus executionStatus;

		private int programCounter = 0;

		private string[] instructions;

		private CancellationTokenSource cancellationTokenSource;

		public StanleyEnvironment(
			Dictionary<string, IStanleyVariable> inputVariables,
			Dictionary<string, List<IStanleyOperation>> operationsChainOfResponsibility,
			Dictionary<string, IStanleyVariable> runtimeVariables,
			Stack<IStanleyVariable> stack,
			List<string> logs)
		{
			this.inputVariables = inputVariables;

			this.operationsChainOfResponsibility = operationsChainOfResponsibility;

			this.runtimeVariables = runtimeVariables;

			this.stack = stack;

			this.logs = logs;

			executionStatus = EExecutionStatus.IDLE;

			instructions = null;

			programCounter = 0;

			cancellationTokenSource = new CancellationTokenSource();
		}

		#region IRuntimeEnvironment

		public void LoadInputVariable(
			string name,
			IStanleyVariable variable)
		{
			inputVariables.Add(
				name,
				variable);
		}

		public void LoadOperation(
			string name,
			IStanleyOperation operation)
		{
			if (operationsChainOfResponsibility.ContainsKey(name))
			{
				operationsChainOfResponsibility[name].Insert(
					0,
					operation);
			}
			else
			{
				operationsChainOfResponsibility.Add(
					name,
					new List<IStanleyOperation>
					{
						operation
					});
			}
		}

		#endregion

		#region IExecutable

		public EExecutionStatus Status { get => executionStatus; }

		public void LoadProgram(
			string[] instructions)
		{
			this.instructions = instructions;

			programCounter = 0;
		}

		public void Start()
		{

		}

		public void Step()
		{

		}

		public void Pause()
		{

		}

		public void Resume()
		{

		}

		public void Stop()
		{

		}

		#endregion

		#region Stack machine

		public void AddRuntimeVariable(
			string name,
			IStanleyVariable variable)
		{
			runtimeVariables.Add(
				name,
				variable);
		}

		public void Push(
			IStanleyVariable variable)
		{
			stack.Push(variable);
		}

		public IStanleyVariable Pop()
		{
			return stack.Pop();
		}

		#endregion

		#region Logging

		public void Log(
			string message)
		{
			logs.Add(message);
		}

		public string[] GetLogs()
		{
			return logs.ToArray();
		}

		#endregion

		private async void Execute()
		{
			string instruction = instructions[programCounter];

			string[] instructionTokens = instruction.Split(' ');

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
						await operation.Handle(
							instructionTokens,
							this,
							cancellationTokenSource.Token);

						handled = true;

						break;
					}
				}

				if (!handled)
				{
					Log("NO OPERATION HANDLED: " + opcode);
				}
			}
			else
			{
				Log("UNKNOWN OPERATION: " + opcode);
			}

			programCounter++;
		}
	}
}