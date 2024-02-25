using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class Wait
		: AStanleyOperation
	{
		Func<double, object> startWaitingDelegate;

		Func<object, bool> areWeThereYetDelegate;

		public Wait(
			Func<double, object> startWaitingDelegate,
			Func<object, bool> areWeThereYetDelegate)
		{
			this.startWaitingDelegate = startWaitingDelegate;

			this.areWeThereYetDelegate = areWeThereYetDelegate;
		}

		#region  IStanleyOperation

		public override string Opcode => "OP_WAIT";

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
				out var durationScalar))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<StanleyScalarPropertyObject>(
				durationScalar,
				context,
				reportable))
				return false;

			var durationValue = durationScalar.GetValue<StanleyScalarPropertyObject>().Amount;

			var state = startWaitingDelegate.Invoke(durationValue);

			while (!areWeThereYetDelegate.Invoke(state))
			{
				await Task.Yield();
			}

			return true;
		}

		#endregion
	}
}