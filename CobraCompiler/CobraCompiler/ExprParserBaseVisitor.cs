//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\chris\OneDrive\Dokumenter\GitHub\P4-project\ExprParser.txt by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CobraCompiler;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IExprParserVisitor{Result}"/>,
/// which can be extended to create a visitor which only needs to handle a subset
/// of the available methods.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class ExprParserBaseVisitor<Result> : AbstractParseTreeVisitor<Result>, CobraCompiler.IExprParserVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.program"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitProgram([NotNull] ExprParser.ProgramContext context)
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.cmds"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCmds([NotNull] ExprParser.CmdsContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.cmd"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCmd([NotNull] ExprParser.CmdContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.dcl"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitDcl([NotNull] ExprParser.DclContext context) 
	{
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.ass"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitAss([NotNull] ExprParser.AssContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.stmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStmt([NotNull] ExprParser.StmtContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr([NotNull] ExprParser.ExprContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprOr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitOprOr([NotNull] ExprParser.OprOrContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.logicOr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLogicOr([NotNull] ExprParser.LogicOrContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprAnd"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitOprAnd([NotNull] ExprParser.OprAndContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.logicAnd"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLogicAnd([NotNull] ExprParser.LogicAndContext context)
	{
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprEql"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitOprEql([NotNull] ExprParser.OprEqlContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.equal"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitEqual([NotNull] ExprParser.EqualContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprBool"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitOprBool([NotNull] ExprParser.OprBoolContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.bool"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBool([NotNull] ExprParser.BoolContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprExpr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitOprExpr([NotNull] ExprParser.OprExprContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.term"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTerm([NotNull] ExprParser.TermContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.oprTerm"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitOprTerm([NotNull] ExprParser.OprTermContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFactor([NotNull] ExprParser.FactorContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.block"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBlock([NotNull] ExprParser.BlockContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.ctrlStrct"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCtrlStrct([NotNull] ExprParser.CtrlStrctContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.ifStmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitIfStmt([NotNull] ExprParser.IfStmtContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.elseIfStmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitElseIfStmt([NotNull] ExprParser.ElseIfStmtContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.else"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitElse([NotNull] ExprParser.ElseContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.loop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLoop([NotNull] ExprParser.LoopContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.loops"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLoops([NotNull] ExprParser.LoopsContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.loopStmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLoopStmt([NotNull] ExprParser.LoopStmtContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.whileStmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitWhileStmt([NotNull] ExprParser.WhileStmtContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.foreachStmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitForeachStmt([NotNull] ExprParser.ForeachStmtContext context)
	{
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.listStmt"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitListStmt([NotNull] ExprParser.ListStmtContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.listOpr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitListOpr([NotNull] ExprParser.ListOprContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcCall"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFuncCall([NotNull] ExprParser.FuncCallContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcDef"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFuncDef([NotNull] ExprParser.FuncDefContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.funcReturn"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFuncReturn([NotNull] ExprParser.FuncReturnContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.paramList"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitParamList([NotNull] ExprParser.ParamListContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.paramTail"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitParamTail([NotNull] ExprParser.ParamTailContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.param"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitParam([NotNull] ExprParser.ParamContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.argList"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitArgList([NotNull] ExprParser.ArgListContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.argTail"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitArgTail([NotNull] ExprParser.ArgTailContext context) 
	{ 
		return VisitChildren(context); 
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.boolean"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBoolean([NotNull] ExprParser.BooleanContext context) 
	{ 
		return VisitChildren(context);
	}
	/// <summary>
	/// Visit a parse tree produced by <see cref="ExprParser.type"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitType([NotNull] ExprParser.TypeContext context) 
	{ 
		return VisitChildren(context); 
	}
}
