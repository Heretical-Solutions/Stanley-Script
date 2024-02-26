using System;

using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class InvokeCommand
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_INVOKE";

		public string[] Aliases => null;

		public override bool WillHandle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			//if (!AssertMinInstructionLength(instructionTokens, 2))
			//	return false;

			//if (!AssertInstructionNotEmpty(instructionTokens, 1))
			//	return false;

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

			var REPL = environment as IREPL;

			//REMEMBER: when popping from the stack, the order is reversed

			if (!stack.Pop(
				out var opcode))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<string>(
				opcode,
				context,
				reportable))
				return false;

			var opcodeString = opcode.GetValue<string>();

			if (!AssertValueNotEmpty(
				opcodeString,
				context,
				reportable))
				return false;

			bool result = await REPL.Execute(
				opcodeString,
				context,
				token);

			return result;
		}

		#endregion
	}
}