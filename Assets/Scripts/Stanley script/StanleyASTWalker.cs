using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using HereticalSolutions.StanleyScript.Grammars;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyASTWalker
		: StanleyParserBaseVisitor<object>
	{
		private IRuntimeEnvironment runtimeEnvironment;

		private List<string> instructions;

		public StanleyASTWalker(
			IRuntimeEnvironment runtimeEnvironment,
			List<string> instructions)
		{
			this.runtimeEnvironment = runtimeEnvironment;

			this.instructions = instructions;
		}

		public string[] GetInstructions()
		{
			return instructions.ToArray();
		}

		public override object VisitScript(StanleyParser.ScriptContext context)
		{
			//UnityEngine.Debug.Log("Visiting script");

			//UnityEngine.Debug.Log($"Text: {context.GetText()}");

			base.VisitScript(context);

			return null;
		}

		public override object VisitStatement(StanleyParser.StatementContext context)
		{
			//UnityEngine.Debug.Log("Visiting statement");

			//UnityEngine.Debug.Log($"Text: {context.GetText()}");

			base.VisitStatement(context);

			return null;
		}

        public override object VisitStoryHeader([NotNull] StanleyParser.StoryHeaderContext context)
        {
			//UnityEngine.Debug.Log("Visiting header");

			//UnityEngine.Debug.Log($"Text: {context.GetText()}");

			//UnityEngine.Debug.Log($"Child count: {context.ChildCount}");

			//for (int i = 0; i < context.ChildCount; i++)
			//{
			//	UnityEngine.Debug.Log($"-- {context.children[i].GetText()}");
			//}

			base.VisitStoryHeader(context);

			instructions.Add(
				$"OP_READ_STORY");

			return null;
        }

        public override object VisitSubject([NotNull] StanleyParser.SubjectContext context)
        {
			//UnityEngine.Debug.Log("Visiting subject");

			base.VisitSubject(context);

			instructions.Add(
				$"OP_PUSH_STR {context.GetText()}");

            return null;
        }

        public override object VisitObject([NotNull] StanleyParser.ObjectContext context)
        {
			//UnityEngine.Debug.Log("Visiting object");

			base.VisitObject(context);

			instructions.Add(
				$"OP_PUSH_STR {context.GetText()}");

			return null;
        }
    }
}