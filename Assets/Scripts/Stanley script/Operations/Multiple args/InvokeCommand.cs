using System;

using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class InvokeCommand
		: IStanleyOperation
	{
		#region IStanleyOperation

		public string Opcode => "OP_INVOKE";

		public string[] Aliases => null;

		public virtual bool WillHandle(
			string[] instructionTokens,
			StanleyEnvironment environment)
		{
			if (instructionTokens[0] != Opcode)
				return false;

			if (instructionTokens.Length < 3)
				return false;

			if (string.IsNullOrEmpty(instructionTokens[1]))
				return false;

			if (string.IsNullOrEmpty(instructionTokens[2]))
				return false;

			return true;
		}

		public virtual async Task<bool> Handle(
			string[] instructionTokens,
			StanleyEnvironment environment,
			CancellationToken token)
		{
			var REPL = environment as IREPL;

			//REMEMBER: when popping from the stack, the order is reversed

			string opcode = instructionTokens[1];

			int argumentsAmount = Convert.ToInt32(instructionTokens[2]);

			string instruction = $"{opcode} {argumentsAmount}";

			bool result = await REPL.Execute(
				instruction,
				token);

			return result;
		}

		#endregion
	}
}