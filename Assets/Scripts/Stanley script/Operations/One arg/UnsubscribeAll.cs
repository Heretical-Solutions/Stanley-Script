using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class UnsubscribeAll
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_UNSUB_ALL";

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

			//Get event variable
			if (!stack.Pop(
				out var eventDelegateVariable))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<Delegate>(
				eventDelegateVariable,
				context,
				reportable))
				return false;

			IStanleyEvent stanleyEvent = eventDelegateVariable as IStanleyEvent;

			//Unsubscribe
			stanleyEvent.UnsubscribeAll();

			return true;
		}

		#endregion
	}
}