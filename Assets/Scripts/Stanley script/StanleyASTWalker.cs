using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using HereticalSolutions.StanleyScript.Grammars;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyASTWalker
		: StanleyParserBaseVisitor<object>
	{
		private IRuntimeEnvironment runtimeEnvironment;

		//Walking runtime

		private List<string> instructions;

		private bool pushingRuntimeVariablesAllowed = true;

		/*
		private string commandOpcode = string.Empty;

		private string commandTarget = string.Empty;

		private int commandArgumentsCount = 0;

		private List<string> commandArguments = new List<string>();
		*/

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

			pushingRuntimeVariablesAllowed = false;

			base.VisitStoryHeader(context);

			pushingRuntimeVariablesAllowed = true;

			instructions.Add(
				$"OP_READ_STORY");

			return null;
        }

        public override object VisitDefineStatement([NotNull] StanleyParser.DefineStatementContext context)
        {
            base.VisitDefineStatement(context);

			instructions.Add(
				$"OP_ALLOC_RTM");

			return null;
        }

        public override object VisitRelatablePluralSubjectsExpression([NotNull] StanleyParser.RelatablePluralSubjectsExpressionContext context)
        {
            base.VisitRelatablePluralSubjectsExpression(context);

			var subjects = context.relatableSingleSubjectExpression();

			var subjectsAmount = subjects.Length;

			instructions.Add(
				$"OP_CONCAT {subjectsAmount}");

			return null;
        }

        public override object VisitImportVariableLiteral([NotNull] StanleyParser.ImportVariableLiteralContext context)
        {
            base.VisitImportVariableLiteral(context);

			instructions.Add(
				$"OP_PUSH_IMP");

			return null;
        }

        public override object VisitDefineSubject([NotNull] StanleyParser.DefineSubjectContext context)
        {
			pushingRuntimeVariablesAllowed = false;

			base.VisitDefineSubject(context);

			pushingRuntimeVariablesAllowed = true;

			return null;
        }

        public override object VisitSubject([NotNull] StanleyParser.SubjectContext context)
        {
			//UnityEngine.Debug.Log("Visiting subject");

			base.VisitSubject(context);

			var text = context.GetText();

			instructions.Add($"OP_PUSH_STR {text}");

			//I think it's better to leave reserved symbols in the variable names

			//instructions.Add(
			//	$"OP_PUSH_STR {text.Substring(1, text.Length - 2)}");

			if (pushingRuntimeVariablesAllowed)
			{
				instructions.Add(
					$"OP_PUSH_RTM");
			}

            return null;
        }

        public override object VisitObject([NotNull] StanleyParser.ObjectContext context)
        {
			//UnityEngine.Debug.Log("Visiting object");

			base.VisitObject(context);

			var text = context.GetText();

			instructions.Add($"OP_PUSH_STR {text}");

			//I think it's better to leave reserved symbols in the variable names

			//instructions.Add(
			//	$"OP_PUSH_STR {text.Substring(1, text.Length - 2)}");

			if (pushingRuntimeVariablesAllowed)
			{
				instructions.Add(
					$"OP_PUSH_RTM");
			}

			return null;
        }

        public override object VisitAction([NotNull] StanleyParser.ActionContext context)
        {
            base.VisitAction(context);



			return null;
        }

        public override object VisitIdLiteral([NotNull] StanleyParser.IdLiteralContext context)
        {
            base.VisitIdLiteral(context);

			var text = context.GetText();

			instructions.Add(
				$"OP_PUSH_STR {text}");

			return null;
        }
    }
}