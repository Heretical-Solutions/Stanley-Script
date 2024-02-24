using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class ClearAllEvents
		: AStanleyOperation
	{
		#region  IStanleyOperation

		public override string Opcode => "OP_CLR_EVNT";

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
			environment.UnsubscribeAllEvents();

			return true;
		}

		#endregion
	}
}