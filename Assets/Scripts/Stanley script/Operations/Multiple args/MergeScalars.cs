using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class MergeScalars
		: AStanleyOperation
	{
		private Func<string, double> calculateMultiplierDelegate;

		private Func<string> getPropertyDelegate;

		public MergeScalars(
			Func<string, double> calculateMultiplierDelegate,
			Func<string> getPropertyDelegate)
		{
			this.calculateMultiplierDelegate = calculateMultiplierDelegate;

			this.getPropertyDelegate = getPropertyDelegate;
		}

		#region IStanleyOperation

		public override string Opcode => "OP_MERGE_SCLR";

		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
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

			//REMEMBER: when popping from the stack, the order is reversed

			//Get variables amount
			if (!stack.Pop(
				out var variablesAmount))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(
				variablesAmount,
				context,
				reportable))
				return false;

			int scalarsAmount = variablesAmount.GetValue<int>();


			double totalAmount = 0;

			string property = getPropertyDelegate.Invoke();


			for (int i = scalarsAmount - 1; i > -1; i--)
			{
				if (!stack.Pop(
					out var scalarVariable))
				{
					reportable.Log(
						context.ContextID,
						"STACK VARIABLE NOT FOUND");

					return false;
				}

				if (!AssertVariable<StanleyScalarPropertyObject>(
					scalarVariable,
					context,
					reportable))
					return false;

				var currentScalar = scalarVariable.GetValue<StanleyScalarPropertyObject>();

				string currentProperty = currentScalar.Property;

				double currentAmount = currentScalar.Amount;

				double multiplier = calculateMultiplierDelegate.Invoke(currentProperty);

				totalAmount += currentAmount * multiplier;
			}

			stack.Push(
				new StanleyCachedVariable(
					StanleyConsts.TEMPORARY_VARIABLE,
					typeof(StanleyScalarPropertyObject),
					new StanleyScalarPropertyObject
					{
						Amount = totalAmount,

						Property = property
					}));

			return true;
		}

		#endregion
	}
}