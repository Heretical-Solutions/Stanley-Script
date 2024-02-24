using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	//The signature should look like this:
	//opcode WHOM
	public abstract class AOneArgOperation
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = context as IStackMachine;

			var reportable = environment as IReportable;

			//Get whom
			if (!stack.Pop(
				out var whom))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				whom,
				context,
				reportable))
				return false;

			return await HandleInternal(
				whom,
				stack,
				reportable,
				instructionTokens,
				context,
				environment,
				token);
		}

		#endregion

		protected abstract Task<bool> HandleInternal(
			IStanleyVariable whom,

			IStackMachine stack,
			IReportable reportable,

			string[] instructionTokens,
			IStanleyContext context,
			IRuntimeEnvironment environment,
			CancellationToken token);
	}
}