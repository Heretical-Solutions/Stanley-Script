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
		  ILoggable
	{
		private const string REGEX_SPLIT_BY_WHITESPACE_UNLESS_WITHIN_QUOTES = "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";

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

		public bool LoadInputVariable(
			string name,
			IStanleyVariable variable)
		{
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
			string name,
			IStanleyVariable variable)
		{
			if (runtimeVariables.ContainsKey(name))
			{
				return false;
			}

			runtimeVariables.Add(
				name,
				variable);

			return true;
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
					Log("NO OPERATION HANDLED: " + opcode);

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

		private async Task<bool> ExecuteInternal(CancellationToken cancellationToken)
		{
			string instruction = instructions[programCounter];

			bool result = await Execute(
				instruction,
				cancellationToken);

			programCounter++;

			return result;
		}
	}
}