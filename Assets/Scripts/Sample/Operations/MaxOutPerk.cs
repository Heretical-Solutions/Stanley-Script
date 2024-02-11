using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class MaxOutPerk
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "max";

		public override string[] Aliases => new string[] { "maxed" };

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = environment as IStackMachine;

			if (!AssertStackVariableType<Perk>(
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

			//Get perk
			if (!stack.Pop(
				out var perk))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(perk, reportable))
				return false;

			perk.GetValue<Perk>().MaxOut();

			return true;
		}

		#endregion
	}
}