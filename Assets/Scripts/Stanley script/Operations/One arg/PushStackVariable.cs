using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class PushStackVariable
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_PUSH_STK";

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
				out var stackIndex))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(stackIndex, logger))
				return false;

			var stackIndexValue = stackIndex.GetValue<int>();

			if (!stack.PeekAt(
				stackIndexValue,
				out var stackVariable))
			{
				logger.Log($"STACK VARIABLE NOT FOUND AT: {stackIndexValue}");

				return false;
			}

			stack.Push(
				stackVariable);

			return true;
		}

		#endregion
	}
}