using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//give WHOM WHAT
	public class AccelerateGame
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "accelerate";

		public override string[] Aliases => new string[] { "accelerated" };

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			IStackMachine stack = environment as IStackMachine;

			if (!AssertStackVariableType<Game>(
				stack,
				0))
				return false;

			if (!AssertStackVariableType<StanleyScalarPropertyObject>(
				stack,
				1))
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

			//Get game
			if (!stack.Pop(
				out var game))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(game, reportable))
				return false;

			//Get amount
			if (!stack.Pop(
				out var amountVariable))
			{
				reportable.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<StanleyScalarPropertyObject>(amountVariable, reportable))
				return false;

			//Get the values
			var scalar = amountVariable.GetValue<StanleyScalarPropertyObject>();

			var property = scalar.Property;

			var amount = scalar.Amount;

			switch (property)
			{
				case "speed":

					game.GetValue<Game>().SetSpeed((float)amount);

					break;

				default:
				
					reportable.Log($"Property {property} not found");

					return false;
			}

			return true;
		}

		#endregion
	}
}