using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class TryCastToRuntimeVariable
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_TRY_CAST_RTM";

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

			if (environment.GetRuntimeVariable(
				variableNameString,
				out var runtimeVariable))
			{
				stack.Push(
					runtimeVariable);

				return true;
			}

			stack.Push(
				variableName);

			return true;
		}

		#endregion
	}
}