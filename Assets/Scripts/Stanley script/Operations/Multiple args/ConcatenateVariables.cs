using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ConcatenateVariables
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_CONCAT";

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			if (!AssertMinInstructionLength(instructionTokens, 2))
				return false;

			if (AssertInstructionNotEmpty(instructionTokens, 1))
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

			//int arrayLength = Convert.ToInt32(instructionTokens[1]);

			//REMEMBER: when popping from the stack, the order is reversed

			//Get variables amount
			if (!stack.Pop(
				out var variablesAmount))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(variablesAmount, logger))
				return false;

			int arrayLength = variablesAmount.GetValue<int>();

			IStanleyVariable[] variables = new IStanleyVariable[arrayLength];

			//REMEMBER: when popping from the stack, the order is reversed

			for (int i = arrayLength - 1; i > -1; i--)
			{
				if (!stack.Pop(
					out var arrayElement))
				{
					logger.Log("STACK VARIABLE NOT FOUND");

					return false;
				}

				if (!AssertVariable(arrayElement, logger))
					return false;

				variables[i] = arrayElement;
			}

			stack.Push(
				new StanleyCachedVariable(
					"TEMPVAR",
					typeof(Array),
					variables));

			return true;
		}

		#endregion
	}
}