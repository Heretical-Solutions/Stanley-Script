using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ReadStory
		: AStanleyOperation
	{
		#region  IStanleyOperation

		public override string Opcode => "OP_STORY";

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
				out var storyName))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<string>(
				storyName,
				context,
				reportable))
				return false;

			var storyNameString = storyName.GetValue<string>();

			if (!AssertValueNotEmpty(
				storyNameString,
				context,
				reportable))
				return false;

			reportable.Log(
				context.ContextID,
				$"STARTING STORY: {storyNameString}");

			return true;
		}

		#endregion
	}
}