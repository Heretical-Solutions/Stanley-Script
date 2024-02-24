using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//select "random" FROM_WHAT ARGUMENT ASSERT_AMOUNT ASSERT? ASSERT?
	public class SelectRandomPerk
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => StanleyConsts.SELECTION_OPCODE;

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

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = context as IStackMachine;

			var reportable = environment as IReportable;

			//Get "random"
			if (!stack.Pop(
				out var _))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			//Get perks to chose from
			if (!stack.Pop(
				out var perks))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<Perk[]>(
				perks,
				context,
				reportable))
				return false;

			//Get argument
			if (!stack.Pop(
				out var argument))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				argument,
				context,
				reportable))
				return false;

			//Get asserts amount
			if (!stack.Pop(
				out var assertsAmount))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(
				assertsAmount,
				context,
				reportable))
				return false;

			//Get asserts
			var asserts = new string[assertsAmount.GetValue<int>()];

			for (int i = 0; i < asserts.Length; i++)
			{
				if (!stack.Pop(
					out var assert))
				{
					reportable.Log(
						context.ContextID,
						"STACK VARIABLE NOT FOUND");

					return false;
				}

				if (!AssertVariable(
					assert,
					context,
					reportable))
					return false;

				asserts[i] = assert.GetValue<string>();
			}

			//Select random perk that matches asserts
			var perksToChoseFrom = perks.GetValue<Perk[]>().ToList();

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

		#endregion
	}
}