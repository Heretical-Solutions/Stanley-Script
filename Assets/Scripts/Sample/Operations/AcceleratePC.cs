using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//give WHOM WHAT
	public class AcceleratePC
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "accelerate";

		public override string[] Aliases => new string[] { "accelerated" };

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

			if (!AssertStackVariableType<StanleyScalarPropertyObject>(
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

			//Get amount
			if (!stack.Pop(
				out var amountVariable))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<StanleyScalarPropertyObject>(
				amountVariable,
				context,
				reportable))
				return false;

			//Get the values
			var scalar = amountVariable.GetValue<StanleyScalarPropertyObject>();

			var property = scalar.Property;

			var amount = scalar.Amount;

			switch (property)
			{
				case "speed":

					pc.GetValue<PlayerCharacter>().Accelerate((float)amount);

					break;

				default:

					reportable.Log(
						context.ContextID,
						$"Property {property} not found");

					return false;
			}
			
			return true;
		}

		#endregion
	}
}