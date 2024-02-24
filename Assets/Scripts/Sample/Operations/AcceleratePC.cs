using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class AcceleratePC
		: ATwoArgOperation
	{
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
			if (!AssertVariable<StanleyScalarPropertyObject>(
				what,
				context,
				reportable))
				return false;

			//Get the values
			var scalar = what.GetValue<StanleyScalarPropertyObject>();

			var property = scalar.Property;

			var amount = scalar.Amount;

			switch (property)
			{
				case "speed":

					whom.GetValue<PlayerCharacter>().Accelerate((float)amount);

					break;

				default:

					reportable.Log(
						context.ContextID,
						$"Property {property} not found");

					return false;
			}
			
			return true;
		}
	}
}