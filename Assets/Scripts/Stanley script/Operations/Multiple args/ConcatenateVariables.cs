using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ConcatenateVariables
		: IStanleyOperation
	{
		#region IStanleyOperation

		public string Opcode => "OP_CONCAT";

		public string[] Aliases => null;

		public virtual bool WillHandle(
			string[] instructionTokens,
			StanleyEnvironment environment)
		{
			if (instructionTokens[0] != Opcode)
				return false;

			if (instructionTokens.Length < 2)
				return false;

			if (string.IsNullOrEmpty(instructionTokens[1]))
				return false;

			return true;
		}

		public virtual async Task<bool> Handle(
			string[] instructionTokens,
			StanleyEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var logger = environment as ILoggable;

			int arrayLength = Convert.ToInt32(instructionTokens[1]);

			IStanleyVariable[] variables = new IStanleyVariable[arrayLength];

			//REMEMBER: when popping from the stack, the order is reversed

			for (int i = arrayLength - 1; i > -1; i--)
			{
				var arrayElement = stack.Pop();

				if (arrayElement == null)
				{
					logger.Log("INVALID STACK VARIABLE");

					return false;
				}

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