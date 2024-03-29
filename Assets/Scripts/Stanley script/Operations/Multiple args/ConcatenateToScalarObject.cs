using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ConcatenateToScalarObject
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_PUSH_SCLR";

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

			//Get variable to concatenate
			if (!stack.Pop(
				out var variableToMakeScalar))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				variableToMakeScalar,
				context,
				reportable))
				return false;

			//Get amount
			if (!stack.Pop(
				out var amount))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				amount,
				context,
				reportable))
				return false;

			double amountValue;

			if (amount.VariableType == typeof(int))
			{
				amountValue = Convert.ToDouble(amount.GetValue<int>());
			}
			else if (amount.VariableType == typeof(float))
			{
				amountValue = Convert.ToDouble(amount.GetValue<float>());
			}
			else
			{
				reportable.Log(
					context.ContextID,
					"INVALID AMOUNT VARIABLE TYPE");

				return false;
			}

			if (variableToMakeScalar.VariableType == typeof(string))
			{
				stack.Push(
					new StanleyCachedVariable(
						StanleyConsts.TEMPORARY_VARIABLE,
						typeof(StanleyScalarPropertyObject),
						new StanleyScalarPropertyObject
						{
							Amount = amountValue,

							Property = variableToMakeScalar.GetValue<string>()
						}));
			}
			else
			{
				stack.Push(
					new StanleyCachedVariable(
						StanleyConsts.TEMPORARY_VARIABLE,
						typeof(StanleyScalarVariableObject),
						new StanleyScalarVariableObject
						{
							Amount = amountValue,

							Variable = variableToMakeScalar
						}));
			}

			return true;
		}

		#endregion
	}
}