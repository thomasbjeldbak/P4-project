# Generated from C:\Github Repos\P4-project\ExprParser.g4 by ANTLR 4.12.0
from antlr4 import *
if __name__ is not None and "." in __name__:
    from .ExprParser import ExprParser
else:
    from ExprParser import ExprParser

# This class defines a complete generic visitor for a parse tree produced by ExprParser.

class ExprParserVisitor(ParseTreeVisitor):

    # Visit a parse tree produced by ExprParser#program.
    def visitProgram(self, ctx:ExprParser.ProgramContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#cmds.
    def visitCmds(self, ctx:ExprParser.CmdsContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#cmd.
    def visitCmd(self, ctx:ExprParser.CmdContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#dcl.
    def visitDcl(self, ctx:ExprParser.DclContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#ass.
    def visitAss(self, ctx:ExprParser.AssContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#stmt.
    def visitStmt(self, ctx:ExprParser.StmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#expr.
    def visitExpr(self, ctx:ExprParser.ExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#oprOr.
    def visitOprOr(self, ctx:ExprParser.OprOrContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#logicOr.
    def visitLogicOr(self, ctx:ExprParser.LogicOrContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#oprAnd.
    def visitOprAnd(self, ctx:ExprParser.OprAndContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#logicAnd.
    def visitLogicAnd(self, ctx:ExprParser.LogicAndContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#oprEql.
    def visitOprEql(self, ctx:ExprParser.OprEqlContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#equal.
    def visitEqual(self, ctx:ExprParser.EqualContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#oprBool.
    def visitOprBool(self, ctx:ExprParser.OprBoolContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#bool.
    def visitBool(self, ctx:ExprParser.BoolContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#oprExpr.
    def visitOprExpr(self, ctx:ExprParser.OprExprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#term.
    def visitTerm(self, ctx:ExprParser.TermContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#oprTerm.
    def visitOprTerm(self, ctx:ExprParser.OprTermContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#factor.
    def visitFactor(self, ctx:ExprParser.FactorContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#block.
    def visitBlock(self, ctx:ExprParser.BlockContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#ctrlStrct.
    def visitCtrlStrct(self, ctx:ExprParser.CtrlStrctContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#ifStmt.
    def visitIfStmt(self, ctx:ExprParser.IfStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#elseIfStmt.
    def visitElseIfStmt(self, ctx:ExprParser.ElseIfStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#else.
    def visitElse(self, ctx:ExprParser.ElseContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#loop.
    def visitLoop(self, ctx:ExprParser.LoopContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#loops.
    def visitLoops(self, ctx:ExprParser.LoopsContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#loopStmt.
    def visitLoopStmt(self, ctx:ExprParser.LoopStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#whileStmt.
    def visitWhileStmt(self, ctx:ExprParser.WhileStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#foreachStmt.
    def visitForeachStmt(self, ctx:ExprParser.ForeachStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#listStmt.
    def visitListStmt(self, ctx:ExprParser.ListStmtContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#listOpr.
    def visitListOpr(self, ctx:ExprParser.ListOprContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#funcCall.
    def visitFuncCall(self, ctx:ExprParser.FuncCallContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#funcDef.
    def visitFuncDef(self, ctx:ExprParser.FuncDefContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#funcReturn.
    def visitFuncReturn(self, ctx:ExprParser.FuncReturnContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#paramList.
    def visitParamList(self, ctx:ExprParser.ParamListContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#paramTail.
    def visitParamTail(self, ctx:ExprParser.ParamTailContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#param.
    def visitParam(self, ctx:ExprParser.ParamContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#argList.
    def visitArgList(self, ctx:ExprParser.ArgListContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#argTail.
    def visitArgTail(self, ctx:ExprParser.ArgTailContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#boolean.
    def visitBoolean(self, ctx:ExprParser.BooleanContext):
        return self.visitChildren(ctx)


    # Visit a parse tree produced by ExprParser#type.
    def visitType(self, ctx:ExprParser.TypeContext):
        return self.visitChildren(ctx)



del ExprParser