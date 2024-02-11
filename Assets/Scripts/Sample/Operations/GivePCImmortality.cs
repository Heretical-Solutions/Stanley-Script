using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//give WHOM "immortality"
	public class GivePCImmortality
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "give";

		public override string[] Aliases => new string[] { "given" };

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = environment as IStackMachine;

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

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var reportable = environment as IReportable;

			//Get pc
			if (!stack.Pop(
				out var pc))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(pc, reportable))
				return false;

			//Get "immortality"
			if (!stack.Pop(
				out var _))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			pc.GetValue<PlayerCharacter>().Immortalize();

			return true;
		}

		#endregion
	}
}