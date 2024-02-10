using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class FlushStackVariable
		: AStanleyOperation
	{
		#region  IStanleyOperation

		public override string Opcode => "OP_FLUSH";

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
				out var _))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			return true;
		}

		#endregion
	}
}