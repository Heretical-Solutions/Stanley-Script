using System;
using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	//The signature should look like this:
	//opcode WHOM
	public class LeftJoinConcatenationVariables
		: AStanleyOperation
	{
		private readonly string opcode;

		private readonly string[] aliases;

		public LeftJoinConcatenationVariables(
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
			IStanleyContext context,
			IRuntimeEnvironment environment)
		{
			if (!AssertOpcodeOrAlias(instructionTokens))
				return false;

			var stack = context as IStackMachine;

			if (!AssertStackVariableType<Array>(stack, 0))
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

			var REPL = environment as IREPL;

			//Get target variable
			if (!stack.Pop(
				out var target))
			{
				reportable.Log(
					context.ContextID,
					"STACK VARIABLE NOT FOUND");

				return false;
			}

			if (!AssertVariable(
				target,
				context,
				reportable))
				return false;

			//Unwrap targets
			IStanleyVariable[] targets;

			if (target.VariableType == typeof(Array))
			{
				targets = target.GetValue<IStanleyVariable[]>();
			}
			else
			{
				targets = new IStanleyVariable[] { target };
			}

			//Invoke for each
			foreach (var targetVariable in targets)
			{
				stack.Push(targetVariable);

				bool result = await REPL.Execute(
					opcode,
					context,
					token);
					//.ThrowExceptions();

				if (!result)
					return false;
			}

			return true;
		}

		#endregion
	}
}