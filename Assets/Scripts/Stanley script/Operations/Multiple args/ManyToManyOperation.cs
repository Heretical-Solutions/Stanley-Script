using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public abstract class ManyToManyOperation
		: AStanleyOperation
	{
		private readonly string opcode;

		private readonly string[] aliases;

		public ManyToManyOperation(
			string opcode,
			string[] aliases)
		{
			this.opcode = opcode;

			this.aliases = aliases;
		}

		#region IStanleyOperation

		public override string Opcode => opcode;

		public override string[] Aliases => aliases;

		public override bool WillHandle(
			string[] instructionTokens,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcode(instructionTokens))
				return false;

			var stack = environment as IStackMachine;

			if (!AssertStackVariableType<Array>(stack, 0)
				&& !AssertStackVariableType<Array>(stack, 1))
				return false;

			return true;
		}

		public override async Task<bool> Handle(
			string[] instructionTokens,
			IRuntimeEnvironment environment,
			CancellationToken token)
		{
			var stack = environment as IStackMachine;

			var logger = environment as ILoggable;

			var REPL = environment as IREPL;

			if (!stack.Pop(
				out var target))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(target, logger))
				return false;

			if (!stack.Pop(
				out var argument))
			{
				logger.Log("STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(argument, logger))
				return false;

			IStanleyVariable[] targets;

			if (target.VariableType == typeof(Array))
			{
				targets = target.GetValue<IStanleyVariable[]>();
			}
			else
			{
				targets = new IStanleyVariable[] { target };
			}

			IStanleyVariable[] arguments;

			if (argument.VariableType == typeof(Array))
			{
				arguments = argument.GetValue<IStanleyVariable[]>();
			}
			else
			{
				arguments = new IStanleyVariable[] { argument };
			}

			foreach (var targetVariable in targets)
			{
				foreach (var argumentVariable in arguments)
				{
					stack.Push(targetVariable);

					stack.Push(argumentVariable);

					bool result = await REPL.Execute(
						opcode,
						token);
						
					if (!result)
						return false;
				}
			}

			return true;
		}

		#endregion
	}
}