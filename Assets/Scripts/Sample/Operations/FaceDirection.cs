using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//face WHOM WHAT
	public class FaceDirection
		: AStanleyOperation
	{
		#region IStanleyOperation

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

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = context as IStackMachine;

			var reportable = environment as IReportable;

			//Get target
			if (!stack.Pop(
				out var targetVariable))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				targetVariable,
				context,
				reportable))
				return false;

			//Get direction
			if (!stack.Pop(
				out var direction))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<float>(
				direction,
				context,
				reportable))
				return false;

			var directionValue = direction.GetValue<float>();

			if (targetVariable.VariableType == typeof(PlayerCharacter))
			{
				targetVariable
					.GetValue<PlayerCharacter>()
					.transform
					.rotation = Quaternion.Euler(0, directionValue * Mathf.Rad2Deg, 0);
			}

			return true;
		}

		#endregion
	}
}