using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//select "random" FROM_WHAT ARGUMENT ASSERT_AMOUNT ASSERT? ASSERT?
	public class SelectRandomPerk
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

			if (!AssertStackVariableType<Perk[]>(
				stack,
				1))
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
			if (!AssertVariable<Perk[]>(
				fromWhat,
				context,
				reportable))
				return false;

			//Select random perk that matches asserts
			var perksToChoseFrom = fromWhat.GetValue<Perk[]>().ToList();

			while (perksToChoseFrom.Count > 0)
			{
				var index = UnityEngine.Random.Range(0, perksToChoseFrom.Count);

				var perk = perksToChoseFrom[index];

				perksToChoseFrom.RemoveAt(index);

				if (PerkAsserts(
					perk,
					argument,
					asserts))
				{
					stack.Push(
						new StanleyCachedVariable(
							StanleyConsts.TEMPORARY_VARIABLE,
							typeof(Perk),
							perk));

					return true;
				}
			}

			//No perk found
			reportable.Log(
				context.ContextID,
				"COULD NOT FIND PERK THAT MATCHES ASSERTS");

			return false;
		}

		//Assert the perk
		private bool PerkAsserts(
			Perk perk,
			IStanleyVariable argument,
			string[] asserts)
		{
			if (perk == null)
				return false;

			if (asserts == null)
				return true;

			foreach (var assert in asserts)
			{
				switch (assert)
				{
					case "unpicked":
						if (argument == null)
							break;

						if (argument.VariableType != typeof(PlayerCharacter))
							break;

						if (argument.GetValue<PlayerCharacter>().HasPerk(perk))
							return false;

						break;
				}	
			}

			return true;
		}
	}
}