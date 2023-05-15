﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/eshes/Desktop/Software/VSCode/P4-project/P4-project/CobraCompiler/CobraCompiler/ExprParser.g4 by ANTLR 4.6.6

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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IExprParserListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class ExprParserBaseListener : IExprParserListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgram([NotNull] ExprParser.ProgramContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgram([NotNull] ExprParser.ProgramContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.cmds"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCmds([NotNull] ExprParser.CmdsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.cmds"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCmds([NotNull] ExprParser.CmdsContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.cmd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCmd([NotNull] ExprParser.CmdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.cmd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCmd([NotNull] ExprParser.CmdContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.dcl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDcl([NotNull] ExprParser.DclContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.dcl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDcl([NotNull] ExprParser.DclContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.ass"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAss([NotNull] ExprParser.AssContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.ass"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAss([NotNull] ExprParser.AssContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStmt([NotNull] ExprParser.StmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.stmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStmt([NotNull] ExprParser.StmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr([NotNull] ExprParser.ExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr([NotNull] ExprParser.ExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprOr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOprOr([NotNull] ExprParser.OprOrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprOr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOprOr([NotNull] ExprParser.OprOrContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.logicOr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLogicOr([NotNull] ExprParser.LogicOrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.logicOr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLogicOr([NotNull] ExprParser.LogicOrContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprAnd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOprAnd([NotNull] ExprParser.OprAndContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprAnd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOprAnd([NotNull] ExprParser.OprAndContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.logicAnd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLogicAnd([NotNull] ExprParser.LogicAndContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.logicAnd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLogicAnd([NotNull] ExprParser.LogicAndContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprEql"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOprEql([NotNull] ExprParser.OprEqlContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprEql"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOprEql([NotNull] ExprParser.OprEqlContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.equal"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEqual([NotNull] ExprParser.EqualContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.equal"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEqual([NotNull] ExprParser.EqualContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprBool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOprBool([NotNull] ExprParser.OprBoolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprBool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOprBool([NotNull] ExprParser.OprBoolContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.bool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBool([NotNull] ExprParser.BoolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.bool"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBool([NotNull] ExprParser.BoolContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOprExpr([NotNull] ExprParser.OprExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOprExpr([NotNull] ExprParser.OprExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTerm([NotNull] ExprParser.TermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTerm([NotNull] ExprParser.TermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.oprTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOprTerm([NotNull] ExprParser.OprTermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.oprTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOprTerm([NotNull] ExprParser.OprTermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFactor([NotNull] ExprParser.FactorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFactor([NotNull] ExprParser.FactorContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock([NotNull] ExprParser.BlockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock([NotNull] ExprParser.BlockContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.commentStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCommentStmt([NotNull] ExprParser.CommentStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.commentStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCommentStmt([NotNull] ExprParser.CommentStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.ctrlStrct"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCtrlStrct([NotNull] ExprParser.CtrlStrctContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.ctrlStrct"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCtrlStrct([NotNull] ExprParser.CtrlStrctContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.ifStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIfStmt([NotNull] ExprParser.IfStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.ifStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIfStmt([NotNull] ExprParser.IfStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.elseIfStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.elseIfStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.else"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterElse([NotNull] ExprParser.ElseContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.else"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitElse([NotNull] ExprParser.ElseContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.loop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLoop([NotNull] ExprParser.LoopContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.loop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLoop([NotNull] ExprParser.LoopContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.loops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLoops([NotNull] ExprParser.LoopsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.loops"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLoops([NotNull] ExprParser.LoopsContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.loopStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLoopStmt([NotNull] ExprParser.LoopStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.loopStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLoopStmt([NotNull] ExprParser.LoopStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.whileStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWhileStmt([NotNull] ExprParser.WhileStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.whileStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWhileStmt([NotNull] ExprParser.WhileStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.foreachStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterForeachStmt([NotNull] ExprParser.ForeachStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.foreachStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitForeachStmt([NotNull] ExprParser.ForeachStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.listStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterListStmt([NotNull] ExprParser.ListStmtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.listStmt"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitListStmt([NotNull] ExprParser.ListStmtContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.listOpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterListOpr([NotNull] ExprParser.ListOprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.listOpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitListOpr([NotNull] ExprParser.ListOprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.listOprExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterListOprExpr([NotNull] ExprParser.ListOprExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.listOprExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitListOprExpr([NotNull] ExprParser.ListOprExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcCall"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFuncCall([NotNull] ExprParser.FuncCallContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcCall"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFuncCall([NotNull] ExprParser.FuncCallContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcDef"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFuncDef([NotNull] ExprParser.FuncDefContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcDef"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFuncDef([NotNull] ExprParser.FuncDefContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcReturn"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFuncReturn([NotNull] ExprParser.FuncReturnContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcReturn"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFuncReturn([NotNull] ExprParser.FuncReturnContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.funcReturnType"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFuncReturnType([NotNull] ExprParser.FuncReturnTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.funcReturnType"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFuncReturnType([NotNull] ExprParser.FuncReturnTypeContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.paramList"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParamList([NotNull] ExprParser.ParamListContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.paramList"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParamList([NotNull] ExprParser.ParamListContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.paramTail"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParamTail([NotNull] ExprParser.ParamTailContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.paramTail"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParamTail([NotNull] ExprParser.ParamTailContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParam([NotNull] ExprParser.ParamContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.param"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParam([NotNull] ExprParser.ParamContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.argList"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArgList([NotNull] ExprParser.ArgListContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.argList"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArgList([NotNull] ExprParser.ArgListContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.argTail"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArgTail([NotNull] ExprParser.ArgTailContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.argTail"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArgTail([NotNull] ExprParser.ArgTailContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.boolean"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBoolean([NotNull] ExprParser.BooleanContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.boolean"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBoolean([NotNull] ExprParser.BooleanContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="ExprParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterType([NotNull] ExprParser.TypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ExprParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitType([NotNull] ExprParser.TypeContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace CobraCompiler
