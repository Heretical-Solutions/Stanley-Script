using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ReadStory
		: IStanleyOperation
	{
		#region  IStanleyOperation

		public string Opcode => "OP_READ_STORY";

		public string[] Aliases => null;

		public virtual bool WillHandle(
			string[] instructionTokens,
			StanleyEnvironment environment)
		{
			if (instructionTokens[0] != Opcode)
				return false;

			return true;
		}

		public virtual async Task<bool> Handle(
			string[] instructionTokens,
			StanleyEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var logger = environment as ILoggable;

			var storyName = stack.Pop();

			if (storyName == null)
			{
				logger.Log("INVALID STACK VARIABLE");

				return false;
			}

			if (storyName.VariableType != typeof(string))
			{
				logger.Log($"INVALID STACK VARIABLE TYPE. EXPECTED: {typeof(string).Name} ACTUAL: {storyName.VariableType.Name}");

				return false;
			}

			var storyNameString = storyName.GetValue<string>();

			if (string.IsNullOrEmpty(storyNameString))
			{
				logger.Log("INVALID STORY NAME");

				return false;
			}

			logger.Log($"STARTING STORY: {storyNameString}");

			return true;
		}

		#endregion
	}
}