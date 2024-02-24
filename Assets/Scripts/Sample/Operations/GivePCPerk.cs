using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//give WHOM WHAT
	public class GivePCPerk
		: AStanleyOperation
	{
		#region IStanleyOperation

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

			if (!AssertStackVariableType<Perk>(
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

			//Get pc
			if (!stack.Pop(
				out var pc))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				pc,
				context,
				reportable))
				return false;

			//Get perk
			if (!stack.Pop(
				out var perk))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<Perk>(
				perk,
				context,
				reportable))
				return false;

			pc.GetValue<PlayerCharacter>().Receive(
				perk.GetValue<Perk>());

			return true;
		}

		#endregion
	}
}