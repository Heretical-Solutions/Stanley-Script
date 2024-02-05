using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class PushString
		: IStanleyOperation
	{
		#region  IStanleyOperation

		public string Opcode => "OP_PUSH_STR";

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

			stack.Push(
				new StanleyCachedVariable(
					"TEMPVAR",
					typeof(string),
					instructionTokens[1]));

			return true;
		}

		#endregion
	}
}