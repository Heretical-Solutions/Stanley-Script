using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class SetLineIndex
		: AStanleyOperation
	{
		#region  IStanleyOperation

		public override string Opcode => "OP_LINE";

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			return true;
		}

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var logger = environment as ILoggable;

			if (!stack.Pop(
				out var lineIndex))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<string>(lineIndex, logger))
				return false;

			var lineIndexValue = lineIndex.GetValue<int>();

			logger.Log($"STARTING LINE: {lineIndexValue}");

			environment.SetCurrentLine(lineIndexValue);

			return true;
		}

		#endregion
	}
}