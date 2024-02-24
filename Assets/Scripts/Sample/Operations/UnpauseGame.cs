using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class UnpauseGame
		: AOneArgOperation
	{
		public override string Opcode => "unpause";

		public override string[] Aliases => new string[] { "unpaused" };

		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = context as IStackMachine;

			if (!AssertStackVariableType<Game>(
				stack,
				0))
				return false;

			return true;
		}

		protected override async Task<bool> HandleInternal(
			IStanleyVariable whom,

			IStackMachine stack,
			IReportable reportable,

			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			whom.GetValue<Game>().UnpauseGame();

			return true;
		}
	}
}