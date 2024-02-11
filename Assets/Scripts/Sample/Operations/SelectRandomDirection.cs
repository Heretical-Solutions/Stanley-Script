using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//select "random" "direcions" ARGUMENT ASSERT_AMOUNT ASSERT? ASSERT?
	public class SelectRandomDirection
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => StanleyConsts.SELECTION_OPCODE;

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = environment as IStackMachine;

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

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var reportable = environment as IReportable;

			//Get "random"
			if (!stack.Pop(
				out var _))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			//Get "directions"
			if (!stack.Pop(
				out var __))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			//Get argument (unused, still pop)
			if (!stack.Pop(
				out var argument))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(argument, reportable))
				return false;

			//Get asserts amount (unused, still pop)
			if (!stack.Pop(
				out var assertsAmount))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(assertsAmount, reportable))
				return false;

			//Get asserts (unused, still pop)
			var asserts = new string[assertsAmount.GetValue<int>()];

			for (int i = 0; i < asserts.Length; i++)
			{
				if (!stack.Pop(
					out var assert))
				{
					reportable.Log("STACK VARIABLE NOT FOUND");

					return false;
				}

				if (!AssertVariable(assert, reportable))
					return false;

				asserts[i] = assert.GetValue<string>();
			}

			float direction = UnityEngine.Random.Range(0, 2 * Mathf.PI);

			stack.Push(
				new StanleyCachedVariable(
					StanleyConsts.TEMPORARY_VARIABLE,
					typeof(float),
					direction));

			return true;
		}

		#endregion
	}
}