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
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			return true;
		}

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = context as IStackMachine;

			var reportable = environment as IReportable;

			if (!stack.Pop(
				out var stackIndex))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(
				stackIndex,
				context,
				reportable))
				return false;

			var stackIndexValue = stackIndex.GetValue<int>();

			if (!stack.PeekFromBottom(
				stackIndexValue,
				out var stackVariable))
			{
				reportable.Log(
					context.ContextID,
					$"STACK VARIABLE NOT FOUND AT: {stackIndexValue}");

				return false;
			}

			stack.Push(
				stackVariable);

			return true;
		}

		#endregion
	}
}