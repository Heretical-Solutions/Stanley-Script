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
				out var lineIndex))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(
				lineIndex,
				context,
				reportable))
				return false;

			var lineIndexValue = lineIndex.GetValue<int>();

			reportable.Log(
				context.ContextID,
				$"STARTING LINE: {lineIndexValue}");

			context.CurrentLine = lineIndexValue;

			return true;
		}

		#endregion
	}
}