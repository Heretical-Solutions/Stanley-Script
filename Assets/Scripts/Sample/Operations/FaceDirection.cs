using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class FaceDirection
		: ATwoArgOperation
	{
		public override string Opcode => "face";

		public override string[] Aliases => new string[] { "faced" };

		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = context as IStackMachine;

			if (!AssertStackVariableType<float>(
				stack,
				1))
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
			if (!AssertVariable<float>(
				what,
				context,
				reportable))
				return false;

			var directionValue = what.GetValue<float>();

			if (whom.VariableType == typeof(PlayerCharacter))
			{
				whom
					.GetValue<PlayerCharacter>()
					.transform
					.rotation = Quaternion.Euler(0, directionValue * Mathf.Rad2Deg, 0);
			}

			return true;
		}
	}
}