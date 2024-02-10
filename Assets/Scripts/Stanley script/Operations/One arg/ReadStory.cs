using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ReadStory
		: AStanleyOperation
	{
		#region  IStanleyOperation

		public override string Opcode => "OP_READ_STORY";

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
				out var storyName))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<string>(storyName, logger))
				return false;

			var storyNameString = storyName.GetValue<string>();

			if (!AssertValueNotEmpty(storyNameString, logger))
				return false;

			logger.Log($"STARTING STORY: {storyNameString}");

			return true;
		}

		#endregion
	}
}