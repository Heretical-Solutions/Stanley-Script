using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class GivePCImmortality
		: ATwoArgOperation
	{
		public override string Opcode => "give";

		public override string[] Aliases => new string[] { "given" };

		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = context as IStackMachine;

			if (!AssertStackVariableType<PlayerCharacter>(
				stack,
				0))
				return false;

			if (!AssertStackVariable<string>(
				stack,
				1,
				"immortality"))
				return false;

			return true;
		}

		protected override async Task<bool> HandleInternal(
			IStanleyVariable whom,
			IStanleyVariable what,

			IStackMachine stack,
			IReportable reportable,

			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			whom.GetValue<PlayerCharacter>().Immortalize();

			return true;
		}
	}
}