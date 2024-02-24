//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /SSD1/Repositories/Unity/Heretical Solutions/Stanley Script Unity/Assets/Scripts/Stanley script/Grammars/StanleyParser.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace HereticalSolutions.StanleyScript.Grammars {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="StanleyParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IStanleyParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.script"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScript([NotNull] StanleyParser.ScriptContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.storyHeader"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStoryHeader([NotNull] StanleyParser.StoryHeaderContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] StanleyParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.defineStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefineStatement([NotNull] StanleyParser.DefineStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.defineSubject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefineSubject([NotNull] StanleyParser.DefineSubjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.commandStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCommandStatement([NotNull] StanleyParser.CommandStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.timeStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeStatement([NotNull] StanleyParser.TimeStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.eventStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEventStatement([NotNull] StanleyParser.EventStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.subscriptionStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubscriptionStatement([NotNull] StanleyParser.SubscriptionStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.unsubscriptionStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnsubscriptionStatement([NotNull] StanleyParser.UnsubscriptionStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.unsubscriptionStatementWithSubject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnsubscriptionStatementWithSubject([NotNull] StanleyParser.UnsubscriptionStatementWithSubjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.actionStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionStatement([NotNull] StanleyParser.ActionStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.actionExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionExpression([NotNull] StanleyParser.ActionExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.actionWithArguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionWithArguments([NotNull] StanleyParser.ActionWithArgumentsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.objectArgument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectArgument([NotNull] StanleyParser.ObjectArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.pluralSubjectsExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPluralSubjectsExpression([NotNull] StanleyParser.PluralSubjectsExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.subjectExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubjectExpression([NotNull] StanleyParser.SubjectExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.selectedSubject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelectedSubject([NotNull] StanleyParser.SelectedSubjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.subjectSelectedByQuality"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubjectSelectedByQuality([NotNull] StanleyParser.SubjectSelectedByQualityContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.subjectSelectedInRelation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubjectSelectedInRelation([NotNull] StanleyParser.SubjectSelectedInRelationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.pluralObjectsExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPluralObjectsExpression([NotNull] StanleyParser.PluralObjectsExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.objectExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectExpression([NotNull] StanleyParser.ObjectExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.selectedObject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelectedObject([NotNull] StanleyParser.SelectedObjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.objectSelectedByQuality"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectSelectedByQuality([NotNull] StanleyParser.ObjectSelectedByQualityContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.objectSelectedInRelation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectSelectedInRelation([NotNull] StanleyParser.ObjectSelectedInRelationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.selectionAdjective"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelectionAdjective([NotNull] StanleyParser.SelectionAdjectiveContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.relativeSelectionAdjective"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelativeSelectionAdjective([NotNull] StanleyParser.RelativeSelectionAdjectiveContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.assertAdjective"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssertAdjective([NotNull] StanleyParser.AssertAdjectiveContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.timeExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeExpression([NotNull] StanleyParser.TimeExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.timeStep"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeStep([NotNull] StanleyParser.TimeStepContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.subject"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubject([NotNull] StanleyParser.SubjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.object"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObject([NotNull] StanleyParser.ObjectContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAction([NotNull] StanleyParser.ActionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.importVariableLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImportVariableLiteral([NotNull] StanleyParser.ImportVariableLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.eventVariableLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEventVariableLiteral([NotNull] StanleyParser.EventVariableLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.runtimeVariableLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRuntimeVariableLiteral([NotNull] StanleyParser.RuntimeVariableLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.integer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInteger([NotNull] StanleyParser.IntegerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StanleyParser.float"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFloat([NotNull] StanleyParser.FloatContext context);
}
} // namespace HereticalSolutions.StanleyScript.Grammars
