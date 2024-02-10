using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public abstract class AStanleyOperation
		: IStanleyOperation
	{
		#region IStanleyOperation

		public abstract string Opcode { get; }

		public virtual string[] Aliases => null;

		public abstract bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment);

		public abstract Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token);

		#endregion

		protected bool AssertOpcode(string[] instructionTokens)
		{
			return instructionTokens[0] == Opcode;
		}

		protected bool AssertMinInstructionLength(
			string[] instructionTokens,
			int targetLength)
		{
			return instructionTokens.Length >= targetLength;
		}

		protected bool AssertInstructionNotEmpty(
			string[] instructionTokens,
			int instructionIndex)
		{
			return !string.IsNullOrEmpty(instructionTokens[instructionIndex]);
		}

		protected bool AssertStackVariableType<TVariable>(
			IStackMachine stack,
			int offsetFromTop)
		{
			if (!stack.PeekFromTop(
				offsetFromTop,
				out var variable))
			{
				return false;
			}

			if (variable.VariableType != typeof(TVariable))
			{
				return false;
			}

			return true;
		}

		protected bool AssertStackVariable<TVariable>(
			IStackMachine stack,
			int offsetFromTop,
			TVariable expectedValue)
		{
			if (!stack.PeekFromTop(
				offsetFromTop,
				out var variable))
			{
				return false;
			}

			if (variable.VariableType != typeof(TVariable))
			{
				return false;
			}

			if (!variable.GetValue<TVariable>().Equals(expectedValue))
			{
				return false;
			}

			return true;
		}

		protected bool AssertVariable(
			IStanleyVariable variable,
			ILoggable logger)
		{
			if (variable == null)
			{
				logger.Log("INVALID STACK VARIABLE");

				return false;
			}

			return true;
		}

		protected bool AssertVariable<TVariable>(
			IStanleyVariable variable,
			ILoggable logger)
		{
			if (variable == null)
			{
				logger.Log("INVALID STACK VARIABLE");

				return false;
			}

			if (variable.VariableType != typeof(TVariable))
			{
				logger.Log($"INVALID STACK VARIABLE TYPE. EXPECTED: {typeof(string).Name} ACTUAL: {variable.VariableType.Name}");

				return false;
			}

			return true;
		}

		protected bool AssertValueNotEmpty(
			string value,
			ILoggable logger)
		{
			if (string.IsNullOrEmpty(value))
			{
				logger.Log("INVALID VARIABLE VALUE");

				return false;
			}

			return true;
		}
	}
}