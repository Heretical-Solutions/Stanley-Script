using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class PauseGame
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "pause";

		public override string[] Aliases => new string[] { "paused" };

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = environment as IStackMachine;

			if (!AssertStackVariableType<Game>(
				stack,
				0))
				return false;

			return true;
		}

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var reportable = environment as IReportable;

			//Get game
			if (!stack.Pop(
				out var gameVariable))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(gameVariable, reportable))
				return false;

			gameVariable.GetValue<Game>().StartGame();

			return true;
		}

		#endregion
	}
}