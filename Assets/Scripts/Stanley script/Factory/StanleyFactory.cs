using System;
using System.Collections.Generic;

namespace HereticalSolutions.StanleyScript
{
	public static class StanleyFactory
	{
		public static StanleyContext BuildContext()
		{
			return new StanleyContext(
				new Stack<IStanleyVariable>());
		}

		public static StanleyInterpreter BuildInterpreter()
		{
			var defaultContext = BuildContext();

			StanleyASTWalker walker = new StanleyASTWalker(
				BuildEnvironment(),
				new List<string>());

			return new StanleyInterpreter(
				walker);
		}

		public static StanleyEnvironment BuildEnvironment()
		{
			var defaultContext = BuildContext();

			var result = new StanleyEnvironment(
				new Dictionary<string, IStanleyVariable>(),
				new Dictionary<string, IStanleyVariable>(),
				new Dictionary<string, List<IStanleyOperation>>(),
				new Dictionary<string, IStanleyVariable>(),
				defaultContext,
				new List<IStanleyContext>(),
				new List<string>());

			IRuntimeEnvironment environment = result;

			#region No args

			environment.LoadOperation(
				new PushString());

			environment.LoadOperation(
				new PushInt());

			environment.LoadOperation(
				new PushFloat());

			environment.LoadOperation(
				new ClearRuntimeVariables());

			environment.LoadOperation(
				new ClearAllEvents());

			#endregion

			#region One arg

			environment.LoadOperation(
				new SetLineIndex());

			environment.LoadOperation(
				new PushImportVariable());

			environment.LoadOperation(
				new PushEventVariable());

			environment.LoadOperation(
				new PushRuntimeVariable());

			environment.LoadOperation(
				new PushStackVariable());

			environment.LoadOperation(
				new FlushStackVariable());

			environment.LoadOperation(
				new TryCastToRuntimeVariable());

			environment.LoadOperation(
				new UnsubscribeAll());

			//Adding this one with the intent for end user to add his own override
			//We need the variable popped from the stack and unless there's any real, good operation to handle waiting,
			//we're calling it a day immediately
			environment.LoadOperation(
				new Wait(
					(duration) =>
					{
						return null;
					},
					(state) => { return false; }));

			environment.LoadOperation(
				new ReadStory());

			#endregion

			#region Multiple args

			environment.LoadOperation(
				new AllocateRuntimeVariable());

			environment.LoadOperation(
				new ConcatenateToScalarObject());

			environment.LoadOperation(
				new ConcatenateVariables());

			environment.LoadOperation(
				new MergeScalars(
					(timeStep) =>
					{ 
						switch (timeStep)
						{
							case "SECONDS":
								return 1.0;
							case "MINUTES":
								return 60.0;
							//case "HOURS":
							//	return 3600.0;
							//case "DAYS":
							//	return 86400.0;
							default:
								throw new Exception($"UNKNOWN TIME STEP: {timeStep}");
						}
					},
					() => { return "SECONDS"; }));

			environment.LoadOperation(
				new Subscribe());

			environment.LoadOperation(
				new Unsubscribe());

			environment.LoadOperation(
				new InvokeCommand());

			#endregion

			//To unwrap concatenated subscriber variables
			environment.LoadOperation(
				new CrossJoinConcatenationVariables(
					"OP_SUB",
					new string [0]));

			//To unwrap subscriber arrays
			environment.LoadOperation(
				new CrossJoinArrayValues(
					"OP_SUB",
					new string[0]));

			return result;
		}
	}
}