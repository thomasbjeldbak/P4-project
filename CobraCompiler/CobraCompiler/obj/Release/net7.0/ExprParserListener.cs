﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Github Repos\P4-project\CobraCompiler\CobraCompiler\ExprParser.g4 by ANTLR 4.6.6

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
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="ExprParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IExprParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] ExprParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] ExprParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.cmds"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCmds([NotNull] ExprParser.CmdsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.cmds"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCmds([NotNull] ExprParser.CmdsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.cmd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCmd([NotNull] ExprParser.CmdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.cmd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCmd([NotNull] ExprParser.CmdContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.dcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDcl([NotNull] ExprParser.DclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.dcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDcl([NotNull] ExprParser.DclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.ass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAss([NotNull] ExprParser.AssContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.ass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAss([NotNull] ExprParser.AssContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmt([NotNull] ExprParser.StmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmt([NotNull] ExprParser.StmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] ExprParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] ExprParser.ExprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOprOr([NotNull] ExprParser.OprOrContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOprOr([NotNull] ExprParser.OprOrContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.logicOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogicOr([NotNull] ExprParser.LogicOrContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.logicOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogicOr([NotNull] ExprParser.LogicOrContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOprAnd([NotNull] ExprParser.OprAndContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOprAnd([NotNull] ExprParser.OprAndContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.logicAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogicAnd([NotNull] ExprParser.LogicAndContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.logicAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogicAnd([NotNull] ExprParser.LogicAndContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprEql"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOprEql([NotNull] ExprParser.OprEqlContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprEql"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOprEql([NotNull] ExprParser.OprEqlContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.equal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEqual([NotNull] ExprParser.EqualContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.equal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEqual([NotNull] ExprParser.EqualContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprBool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOprBool([NotNull] ExprParser.OprBoolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprBool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOprBool([NotNull] ExprParser.OprBoolContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBool([NotNull] ExprParser.BoolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBool([NotNull] ExprParser.BoolContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOprExpr([NotNull] ExprParser.OprExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOprExpr([NotNull] ExprParser.OprExprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] ExprParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] ExprParser.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOprTerm([NotNull] ExprParser.OprTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOprTerm([NotNull] ExprParser.OprTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] ExprParser.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] ExprParser.FactorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] ExprParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] ExprParser.BlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.ctrlStrct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCtrlStrct([NotNull] ExprParser.CtrlStrctContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.ctrlStrct"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCtrlStrct([NotNull] ExprParser.CtrlStrctContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStmt([NotNull] ExprParser.IfStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStmt([NotNull] ExprParser.IfStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.elseIfStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.elseIfStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.else"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElse([NotNull] ExprParser.ElseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.else"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElse([NotNull] ExprParser.ElseContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoop([NotNull] ExprParser.LoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoop([NotNull] ExprParser.LoopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.loops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoops([NotNull] ExprParser.LoopsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.loops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoops([NotNull] ExprParser.LoopsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.loopStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoopStmt([NotNull] ExprParser.LoopStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.loopStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoopStmt([NotNull] ExprParser.LoopStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.whileStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileStmt([NotNull] ExprParser.WhileStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.whileStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileStmt([NotNull] ExprParser.WhileStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.foreachStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForeachStmt([NotNull] ExprParser.ForeachStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.foreachStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForeachStmt([NotNull] ExprParser.ForeachStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.listStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListStmt([NotNull] ExprParser.ListStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.listStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListStmt([NotNull] ExprParser.ListStmtContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.listOpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListOpr([NotNull] ExprParser.ListOprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.listOpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListOpr([NotNull] ExprParser.ListOprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncCall([NotNull] ExprParser.FuncCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncCall([NotNull] ExprParser.FuncCallContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncDef([NotNull] ExprParser.FuncDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncDef([NotNull] ExprParser.FuncDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcReturn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFuncReturn([NotNull] ExprParser.FuncReturnContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcReturn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFuncReturn([NotNull] ExprParser.FuncReturnContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.paramList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParamList([NotNull] ExprParser.ParamListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.paramList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParamList([NotNull] ExprParser.ParamListContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.paramTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParamTail([NotNull] ExprParser.ParamTailContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.paramTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParamTail([NotNull] ExprParser.ParamTailContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParam([NotNull] ExprParser.ParamContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.param"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParam([NotNull] ExprParser.ParamContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.argList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgList([NotNull] ExprParser.ArgListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.argList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgList([NotNull] ExprParser.ArgListContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.argTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgTail([NotNull] ExprParser.ArgTailContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.argTail"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgTail([NotNull] ExprParser.ArgTailContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.boolean"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoolean([NotNull] ExprParser.BooleanContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.boolean"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoolean([NotNull] ExprParser.BooleanContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] ExprParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] ExprParser.TypeContext context);
}
} // namespace CobraCompiler
