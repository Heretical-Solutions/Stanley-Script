using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ConcatenateToScalarObject
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_SCAL";

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			return true;
		}

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var logger = environment as ILoggable;

			//REMEMBER: when popping from the stack, the order is reversed

			//Get variable to multiply
			if (!stack.Pop(
				out var variableToMultiply))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(variableToMultiply, logger))
				return false;

			//Get amount
			if (!stack.Pop(
				out var amount))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(amount, logger))
				return false;

			double amountValue;

			if (amount.VariableType == typeof(int))
			{
				amountValue = amount.GetValue<int>();
			}
			else if (amount.VariableType == typeof(float))
			{
				amountValue = amount.GetValue<float>();
			}
			else
			{
				logger.Log("INVALID AMOUNT VARIABLE TYPE");

				return false;
			}

			if (variableToMultiply.VariableType == typeof(string))
			{
				stack.Push(
					new StanleyCachedVariable(
						"TEMPVAR",
						typeof(StanleyScalarPropertyObject),
						new StanleyScalarPropertyObject
						{
							Amount = amountValue,

							Property = variableToMultiply.GetValue<string>()
						}));
			}
			else
			{
				stack.Push(
					new StanleyCachedVariable(
						"TEMPVAR",
						typeof(StanleyScalarVariableObject),
						new StanleyScalarVariableObject
						{
							Amount = amountValue,

							Variable = variableToMultiply
						}));
			}

			return true;
		}

		#endregion
	}
}