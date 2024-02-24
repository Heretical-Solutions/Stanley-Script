using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public class Subscribe
		: AStanleyOperation
	{
		#region IStanleyOperation

		public override string Opcode => "OP_SUB";

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

			//Get target
			if (!stack.Pop(
				out var targetVariable))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				targetVariable,
				context,
				reportable))
				return false;

			var targetValue = targetVariable.GetValue<object>();

			//Get instructions amount
			if (!stack.Pop(
				out var instructionsAmount))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable<int>(
				instructionsAmount,
				context,
				reportable))
				return false;

			int instructionsAmountValue = instructionsAmount.GetValue<int>();

			string[] instructions = new string[instructionsAmountValue];

			for (int i = 0; i < instructionsAmountValue; i++)
			{
				instructions[i] = context.Instructions[context.ProgramCounter + i + 1];
			}

			//Subscribe
			stanleyEvent.Subscribe(
				new StanleyEventSubscriptionDescriptor(
					targetValue,
					(eventTarget, eventArgs) => 
						ExecuteSubroutineIfTargetMatches(
							eventTarget,
							eventArgs,

							targetValue,
							instructions,
							environment,
							token)));

			context.ProgramCounter += instructionsAmountValue;

			return true;
		}

		#endregion

		private void ExecuteSubroutineIfTargetMatches(
			object eventTarget,
			object[] eventArguments,
			
			object opTarget,
			string[] instructions,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			bool targetMatches =
				eventTarget == null
				|| eventTarget == opTarget;

			if (targetMatches)
			{
				ExecuteSubroutineInGenericContext(
					instructions,
					environment,
					token);
			}
		}

		private void ExecuteSubroutineInGenericContext(
			string[] instructions,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var contextManager = environment as IContextManager;

			var newContext = contextManager.AllocateContext();
			
			newContext.LoadProgram(
				instructions);

			var newExecutable = newContext as IExecutable;

			newExecutable.Start();
		}
	}
}