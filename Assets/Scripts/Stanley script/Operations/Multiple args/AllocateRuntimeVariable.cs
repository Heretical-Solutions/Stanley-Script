using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class AllocateRuntimeVariable
		: IStanleyOperation
	{
		#region IStanleyOperation

		public string Opcode => "OP_ALLOC_RTM";

		public string[] Aliases => null;

		public virtual bool WillHandle(
			string[] instructionTokens,
			StanleyEnvironment environment)
		{
			if (instructionTokens[0] != Opcode)
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

			//REMEMBER: when popping from the stack, the order is reversed

			//Get variable to clone
			var variableToClone = stack.Pop();

			if (variableToClone == null)
			{
				logger.Log("INVALID STACK VARIABLE");

				return false;
			}

			//Get variable name
			var variableName = stack.Pop();

			if (variableName == null)
			{
				logger.Log("INVALID STACK VARIABLE");

				return false;
			}

			if (variableName.VariableType != typeof(string))
			{
				logger.Log($"INVALID STACK VARIABLE TYPE. EXPECTED: {typeof(string).Name} ACTUAL: {variableName.VariableType.Name}");

				return false;
			}

			var variableNameString = variableName.GetValue<string>();

			if (string.IsNullOrEmpty(variableNameString))
			{
				logger.Log("INVALID RUNTIME VARIABLE NAME");

				return false;
			}

			if (!environment.AddRuntimeVariable(
				variableNameString,
				new StanleyCachedVariable(
					variableNameString,
					variableToClone.VariableType,
					variableToClone.GetValue())))
			{
				logger.Log($"COULD NOT ADD RUNTIME VARIABLE: {variableNameString}");

				return false;
			}

			return true;
		}

		#endregion
	}
}