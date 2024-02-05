using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class PushImportVariable
		: IStanleyOperation
	{
		#region IStanleyOperation

		public string Opcode => "OP_PUSH_IMP";

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
				logger.Log("INVALID IMPORT VARIABLE NAME");

				return false;
			}

			if (!environment.GetImportVariable(
				variableNameString,
				out var importVariable))
			{
				logger.Log($"IMPORT VARIABLE NOT FOUND: {variableNameString}");

				return false;
			}

			stack.Push(
				importVariable);

			return true;
		}

		#endregion
	}
}