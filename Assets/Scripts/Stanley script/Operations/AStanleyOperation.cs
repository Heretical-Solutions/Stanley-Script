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
			IStanleyContext context,
			IRuntimeEnvironment environment);

		public abstract Task<bool> Handle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token);

		#endregion

		protected bool AssertOpcode(string[] instructionTokens)
		{
			return instructionTokens[0] == Opcode;
		}

		protected bool AssertOpcodeOrAlias(string[] instructionTokens)
		{
			if (instructionTokens[0] == Opcode)
			{
				return true;
			}

			if (Aliases == null)
			{
				return false;
			}

			foreach (var alias in Aliases)
			{
				if (instructionTokens[0] == alias)
				{
					return true;
				}
			}

			return false;
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

		protected bool AssertMinStackSize(
			IStackMachine stack,
			int targetLength)
		{
			return stack.StackSize >= targetLength;
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

		protected bool AssertStackVariableTypeIsArray(
			IStackMachine stack,
			int offsetFromTop)
		{
			if (!stack.PeekFromTop(
				offsetFromTop,
				out var variable))
			{
				return false;
			}

			if (variable.VariableType.IsArray
				&& variable.VariableType != typeof(IStanleyVariable)) //Just to tell the difference
			{
				return true;
			}

			return false;
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
			IStanleyContext context,
			IReportable reportable)
		{
			if (variable == null)
			{
				reportable.Log(
					context.ContextID,
					"INVALID STACK VARIABLE");

				return false;
			}

			return true;
		}

		protected bool AssertVariable<TVariable>(
			IStanleyVariable variable,
			IStanleyContext context,
			IReportable reportable)
		{
			if (variable == null)
			{
				reportable.Log(
					context.ContextID,
					"INVALID STACK VARIABLE");

				return false;
			}

			if (variable.VariableType != typeof(TVariable))
			{
				reportable.Log(
					context.ContextID,
					$"INVALID STACK VARIABLE TYPE. EXPECTED: {typeof(string).Name} ACTUAL: {variable.VariableType.Name}");

				return false;
			}

			return true;
		}

		protected bool AssertValueNotEmpty(
			string value,
			IStanleyContext context,
			IReportable reportable)
		{
			if (string.IsNullOrEmpty(value))
			{
				reportable.Log(
					context.ContextID,
					"INVALID VARIABLE VALUE");

				return false;
			}

			return true;
		}
	}
}