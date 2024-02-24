using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//select "random" "direcions" ARGUMENT ASSERT_AMOUNT ASSERT? ASSERT?
	public class SelectRandomDirection
		: ASelectOperation
	{
		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = context as IStackMachine;

			if (!AssertStackVariable<string>(
				stack,
				0,
				"random"))
				return false;

			if (!AssertStackVariable<string>(
				stack,
				1,
				"directions"))
				return false;

			return true;
		}

		protected override async Task<bool> HandleInternal(
			IStanleyVariable selector,
			IStanleyVariable fromWhat,
			IStanleyVariable argument,
			string[] asserts,

			IStackMachine stack,
			IReportable reportable,

			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			float direction = UnityEngine.Random.Range(0, 2 * Mathf.PI);

			stack.Push(
				new StanleyCachedVariable(
					StanleyConsts.TEMPORARY_VARIABLE,
					typeof(float),
					direction));

			return true;
		}
	}
}