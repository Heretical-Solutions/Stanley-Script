using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class AllocateRuntimeVariable
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_ALLOC_RTM";

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

			//Get variable name
			if (!stack.Pop(
				out var variableName))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<string>(
				variableName,
				context,
				reportable))
				return false;

			var variableNameString = variableName.GetValue<string>();

			if (!AssertValueNotEmpty(
				variableNameString,
				context,
				reportable))
				return false;

			//Get variable to clone
			if (!stack.Pop(
				out var variableToClone))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				variableToClone,
				context,
				reportable))
				return false;

			//Add new runtime variable
			if (!environment.AddRuntimeVariable(
				new StanleyCachedVariable(
					variableNameString,
					variableToClone.VariableType,
					variableToClone.GetValue())))
			{
				reportable.Log(
					context.ContextID,
					$"COULD NOT ADD RUNTIME VARIABLE: {variableNameString}");

				return false;
			}

			return true;
		}

		#endregion
	}
}