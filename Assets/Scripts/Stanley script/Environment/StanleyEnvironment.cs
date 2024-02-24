using System.Collections.Generic;

using System.Text.RegularExpressions;

using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyEnvironment
		: IRuntimeEnvironment,
		  IContextManager,
		  IREPL,
		  IExecutable,
		  IReportable
	{
		private const string REGEX_SPLIT_BY_WHITESPACE_UNLESS_WITHIN_QUOTES = "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";


		private readonly Dictionary<string, IStanleyVariable> inputVariables;

		private readonly Dictionary<string, List<IStanleyOperation>> operationsChainOfResponsibility;

		private readonly Dictionary<string, IStanleyVariable> runtimeVariables;


		private readonly IStanleyContext defaultContext;

		private readonly List<IStanleyContext> genericContexts;


		private readonly List<string> report;


		private CancellationTokenSource cancellationTokenSource;


		public StanleyEnvironment(
			Dictionary<string, IStanleyVariable> inputVariables,
			Dictionary<string, List<IStanleyOperation>> operationsChainOfResponsibility,
			Dictionary<string, IStanleyVariable> runtimeVariables,

			IStanleyContext defaultContext,
			List<IStanleyContext> genericContexts,

			List<string> report)
		{
			this.inputVariables = inputVariables;

			this.operationsChainOfResponsibility = operationsChainOfResponsibility;

			this.runtimeVariables = runtimeVariables;


			this.defaultContext = defaultContext;

			this.genericContexts = genericContexts;


			this.report = report;


			cancellationTokenSource = new CancellationTokenSource();

			defaultContext.Initialize(
				StanleyConsts.DEFAULT_CONTEXT_ID,
				this,
				this,
				cancellationTokenSource.Token);
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

		public void RemoveAllRuntimeVariables()
		{
			runtimeVariables.Clear();
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

		#region IContextManager

		public IStanleyContext DefaultContext { get => defaultContext; }

		public IStanleyContext AllocateContext()
		{
			IStanleyContext context = StanleyFactory.BuildContext();

			int index = genericContexts.Count;

			context.Initialize(
				$"{StanleyConsts.CONTEXT_PREFIX}{index}",
				this,
				this,
				cancellationTokenSource.Token);

			genericContexts.Add(context);

			return context;
		}

		public void ReleaseContext(
			IStanleyContext context)
		{
			genericContexts.Remove(context);

			context.Cleanup();
		}

		#endregion

		#region IREPL

		public async Task<bool> Execute(
			string instruction,
			IStanleyContext context = null,
			CancellationToken cancellationToken = default)
		{
			bool result = false;

			string[] instructionTokens = Regex.Split(
				instruction,
				REGEX_SPLIT_BY_WHITESPACE_UNLESS_WITHIN_QUOTES);

			//string[] instructionTokens = instruction.Split(' ');

			string opcode = instructionTokens[0];

			if (context == null)
			{
				context = defaultContext;
			}

			if (operationsChainOfResponsibility.ContainsKey(opcode))
			{
				bool handled = false;

				foreach (IStanleyOperation operation in operationsChainOfResponsibility[opcode])
				{
					if (operation.WillHandle(
						instructionTokens,
						context,
						this))
					{
						result = await operation.Handle(
							instructionTokens,
							context,
							this,
							cancellationToken);
						//.ThrowExceptions();

						handled = true;

						break;
					}
				}

				if (!handled)
				{
					Log(
						StanleyConsts.ENVIRONMENT_CONDEXT_ID,
						$"OPERATION NOT HANDLED: {opcode}");

					return false;
				}
			}
			else
			{
				Log(
					StanleyConsts.ENVIRONMENT_CONDEXT_ID,
					$"UNKNOWN OPERATION: {opcode}");

				return false;
			}

			return result;
		}

		#endregion

		#region IReportable

		public void Log(
			string contextID,
			string message)
		{
			report.Add($"[{contextID}] {message}");
		}

		public string[] GetReport()
		{
			return report.ToArray();
		}

		public void ClearReport()
		{
			report.Clear();
		}

		#endregion

		#region IExecutable

		public void Start()
		{
			switch (defaultContext.Status)
			{
				case EExecutionStatus.IDLE:
				case EExecutionStatus.PAUSED:
				case EExecutionStatus.STOPPED:
					{
						ClearReport();
					}

					break;
			}

			var defaultContextAsExecutable = defaultContext as IExecutable;

			defaultContextAsExecutable.Start();
		}

		public void Step()
		{
			switch (defaultContext.Status)
			{
				case EExecutionStatus.IDLE:
				case EExecutionStatus.STOPPED:
					{
						ClearReport();
					}

					break;
			}


			var defaultContextAsExecutable = defaultContext as IExecutable;

			defaultContextAsExecutable.Step();
		}

		public void Pause()
		{
			var defaultContextAsExecutable = defaultContext as IExecutable;

			defaultContextAsExecutable.Pause();
		}

		public void Resume()
		{
			var defaultContextAsExecutable = defaultContext as IExecutable;

			defaultContextAsExecutable.Resume();
		}

		public void Stop()
		{
			//cancellationTokenSource?.Dispose();

			cancellationTokenSource?.Cancel();


			var defaultContextAsExecutable = defaultContext as IExecutable;

			defaultContextAsExecutable.Stop();
		}

		#endregion
	}
}