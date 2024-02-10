/*
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ConcatenateToScalarObject<TValue>
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_MUL";

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			var stack = environment as IStackMachine;

			if (!AssertStackVariableType<TValue>(stack, 0))
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

			if (!AssertVariable<int>(amount, logger))
				return false;

			stack.Push(
				new StanleyCachedVariable(
					"TEMPVAR",
					typeof(StanleyScalarObject),
					new StanleyScalarObject
					{
						Amount = amount.GetValue<int>(),

						Unit 
					}));

			return true;
		}

		#endregion
	}
}
*/