﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\GithubRepos\P4-project\CobraCompiler\CobraCompiler\ExprParser.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace CobraCompiler {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="ExprParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IExprParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] ExprParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.cmds"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCmds([NotNull] ExprParser.CmdsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.cmd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCmd([NotNull] ExprParser.CmdContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.dcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDcl([NotNull] ExprParser.DclContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.ass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAss([NotNull] ExprParser.AssContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmt([NotNull] ExprParser.StmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] ExprParser.ExprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOprOr([NotNull] ExprParser.OprOrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.logicOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicOr([NotNull] ExprParser.LogicOrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOprAnd([NotNull] ExprParser.OprAndContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.logicAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicAnd([NotNull] ExprParser.LogicAndContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprEql"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOprEql([NotNull] ExprParser.OprEqlContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.equal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqual([NotNull] ExprParser.EqualContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprBool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOprBool([NotNull] ExprParser.OprBoolContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBool([NotNull] ExprParser.BoolContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOprExpr([NotNull] ExprParser.OprExprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] ExprParser.TermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOprTerm([NotNull] ExprParser.OprTermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFactor([NotNull] ExprParser.FactorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] ExprParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.commentStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCommentStmt([NotNull] ExprParser.CommentStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.ctrlStrct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCtrlStrct([NotNull] ExprParser.CtrlStrctContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStmt([NotNull] ExprParser.IfStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.elseIfStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.else"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElse([NotNull] ExprParser.ElseContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoop([NotNull] ExprParser.LoopContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.loops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoops([NotNull] ExprParser.LoopsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.loopStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopStmt([NotNull] ExprParser.LoopStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.whileStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStmt([NotNull] ExprParser.WhileStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.foreachStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForeachStmt([NotNull] ExprParser.ForeachStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.listStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListStmt([NotNull] ExprParser.ListStmtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.listOpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListOpr([NotNull] ExprParser.ListOprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.listOprExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListOprExpr([NotNull] ExprParser.ListOprExprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncCall([NotNull] ExprParser.FuncCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncDef([NotNull] ExprParser.FuncDefContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcReturn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncReturn([NotNull] ExprParser.FuncReturnContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcReturnType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncReturnType([NotNull] ExprParser.FuncReturnTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.paramList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParamList([NotNull] ExprParser.ParamListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.paramTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParamTail([NotNull] ExprParser.ParamTailContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParam([NotNull] ExprParser.ParamContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.argList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgList([NotNull] ExprParser.ArgListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.argTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgTail([NotNull] ExprParser.ArgTailContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.boolean"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolean([NotNull] ExprParser.BooleanContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] ExprParser.TypeContext context);
}
} // namespace CobraCompiler
