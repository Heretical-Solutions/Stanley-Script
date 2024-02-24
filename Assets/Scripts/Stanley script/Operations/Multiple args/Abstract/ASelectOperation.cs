using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript.Sample
{
	//The signature should look like this:
	//select SELECTOR FROM_WHAT ARGUMENT ASSERT_AMOUNT ASSERT? ASSERT?
	public abstract class ASelectOperation
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => StanleyConsts.SELECTION_OPCODE;

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = context as IStackMachine;

			var reportable = environment as IReportable;

			//Get selector
			if (!stack.Pop(
				out var selector))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				selector,
				context,
				reportable))
				return false;

			//Get from what
			if (!stack.Pop(
				out var fromWhat))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				fromWhat,
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

			return await HandleInternal(
				selector,
				fromWhat,
				argument,
				asserts,

				stack,
				reportable,

				instructionTokens,
				context,
				environment,
				token);
		}

		#endregion

		protected abstract Task<bool> HandleInternal(
			IStanleyVariable selector,
			IStanleyVariable fromWhat,
			IStanleyVariable argument,
			string[] asserts,

			IStackMachine stack,
			IReportable reportable,

			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token);
	}
}