using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class MaxOutPerk
		: AOneArgOperation
	{
		public override string Opcode => "max";

		public override string[] Aliases => new string[] { "maxed" };

		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = context as IStackMachine;

			if (!AssertStackVariableType<Perk>(
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
			whom.GetValue<Perk>().MaxOut();

			return true;
		}
	}
}