# Generated from C:\Github Repos\P4-project\ExprParser.g4 by ANTLR 4.12.0
from antlr4 import *
if __name__ is not None and "." in __name__:
    from .ExprParser import ExprParser
else:
    from ExprParser import ExprParser

# This class defines a complete listener for a parse tree produced by ExprParser.
class ExprParserListener(ParseTreeListener):

    # Enter a parse tree produced by ExprParser#program.
    def enterProgram(self, ctx:ExprParser.ProgramContext):
        pass

    # Exit a parse tree produced by ExprParser#program.
    def exitProgram(self, ctx:ExprParser.ProgramContext):
        pass


    # Enter a parse tree produced by ExprParser#cmds.
    def enterCmds(self, ctx:ExprParser.CmdsContext):
        pass

    # Exit a parse tree produced by ExprParser#cmds.
    def exitCmds(self, ctx:ExprParser.CmdsContext):
        pass


    # Enter a parse tree produced by ExprParser#cmd.
    def enterCmd(self, ctx:ExprParser.CmdContext):
        pass

    # Exit a parse tree produced by ExprParser#cmd.
    def exitCmd(self, ctx:ExprParser.CmdContext):
        pass


    # Enter a parse tree produced by ExprParser#dcl.
    def enterDcl(self, ctx:ExprParser.DclContext):
        pass

    # Exit a parse tree produced by ExprParser#dcl.
    def exitDcl(self, ctx:ExprParser.DclContext):
        pass


    # Enter a parse tree produced by ExprParser#ass.
    def enterAss(self, ctx:ExprParser.AssContext):
        pass

    # Exit a parse tree produced by ExprParser#ass.
    def exitAss(self, ctx:ExprParser.AssContext):
        pass


    # Enter a parse tree produced by ExprParser#stmt.
    def enterStmt(self, ctx:ExprParser.StmtContext):
        pass

    # Exit a parse tree produced by ExprParser#stmt.
    def exitStmt(self, ctx:ExprParser.StmtContext):
        pass


    # Enter a parse tree produced by ExprParser#expr.
    def enterExpr(self, ctx:ExprParser.ExprContext):
        pass

    # Exit a parse tree produced by ExprParser#expr.
    def exitExpr(self, ctx:ExprParser.ExprContext):
        pass


    # Enter a parse tree produced by ExprParser#oprOr.
    def enterOprOr(self, ctx:ExprParser.OprOrContext):
        pass

    # Exit a parse tree produced by ExprParser#oprOr.
    def exitOprOr(self, ctx:ExprParser.OprOrContext):
        pass


    # Enter a parse tree produced by ExprParser#logicOr.
    def enterLogicOr(self, ctx:ExprParser.LogicOrContext):
        pass

    # Exit a parse tree produced by ExprParser#logicOr.
    def exitLogicOr(self, ctx:ExprParser.LogicOrContext):
        pass


    # Enter a parse tree produced by ExprParser#oprAnd.
    def enterOprAnd(self, ctx:ExprParser.OprAndContext):
        pass

    # Exit a parse tree produced by ExprParser#oprAnd.
    def exitOprAnd(self, ctx:ExprParser.OprAndContext):
        pass


    # Enter a parse tree produced by ExprParser#logicAnd.
    def enterLogicAnd(self, ctx:ExprParser.LogicAndContext):
        pass

    # Exit a parse tree produced by ExprParser#logicAnd.
    def exitLogicAnd(self, ctx:ExprParser.LogicAndContext):
        pass


    # Enter a parse tree produced by ExprParser#oprEql.
    def enterOprEql(self, ctx:ExprParser.OprEqlContext):
        pass

    # Exit a parse tree produced by ExprParser#oprEql.
    def exitOprEql(self, ctx:ExprParser.OprEqlContext):
        pass


    # Enter a parse tree produced by ExprParser#equal.
    def enterEqual(self, ctx:ExprParser.EqualContext):
        pass

    # Exit a parse tree produced by ExprParser#equal.
    def exitEqual(self, ctx:ExprParser.EqualContext):
        pass


    # Enter a parse tree produced by ExprParser#oprBool.
    def enterOprBool(self, ctx:ExprParser.OprBoolContext):
        pass

    # Exit a parse tree produced by ExprParser#oprBool.
    def exitOprBool(self, ctx:ExprParser.OprBoolContext):
        pass


    # Enter a parse tree produced by ExprParser#bool.
    def enterBool(self, ctx:ExprParser.BoolContext):
        pass

    # Exit a parse tree produced by ExprParser#bool.
    def exitBool(self, ctx:ExprParser.BoolContext):
        pass


    # Enter a parse tree produced by ExprParser#oprExpr.
    def enterOprExpr(self, ctx:ExprParser.OprExprContext):
        pass

    # Exit a parse tree produced by ExprParser#oprExpr.
    def exitOprExpr(self, ctx:ExprParser.OprExprContext):
        pass


    # Enter a parse tree produced by ExprParser#term.
    def enterTerm(self, ctx:ExprParser.TermContext):
        pass

    # Exit a parse tree produced by ExprParser#term.
    def exitTerm(self, ctx:ExprParser.TermContext):
        pass


    # Enter a parse tree produced by ExprParser#oprTerm.
    def enterOprTerm(self, ctx:ExprParser.OprTermContext):
        pass

    # Exit a parse tree produced by ExprParser#oprTerm.
    def exitOprTerm(self, ctx:ExprParser.OprTermContext):
        pass


    # Enter a parse tree produced by ExprParser#factor.
    def enterFactor(self, ctx:ExprParser.FactorContext):
        pass

    # Exit a parse tree produced by ExprParser#factor.
    def exitFactor(self, ctx:ExprParser.FactorContext):
        pass


    # Enter a parse tree produced by ExprParser#block.
    def enterBlock(self, ctx:ExprParser.BlockContext):
        pass

    # Exit a parse tree produced by ExprParser#block.
    def exitBlock(self, ctx:ExprParser.BlockContext):
        pass


    # Enter a parse tree produced by ExprParser#ctrlStrct.
    def enterCtrlStrct(self, ctx:ExprParser.CtrlStrctContext):
        pass

    # Exit a parse tree produced by ExprParser#ctrlStrct.
    def exitCtrlStrct(self, ctx:ExprParser.CtrlStrctContext):
        pass


    # Enter a parse tree produced by ExprParser#ifStmt.
    def enterIfStmt(self, ctx:ExprParser.IfStmtContext):
        pass

    # Exit a parse tree produced by ExprParser#ifStmt.
    def exitIfStmt(self, ctx:ExprParser.IfStmtContext):
        pass


    # Enter a parse tree produced by ExprParser#elseIfStmt.
    def enterElseIfStmt(self, ctx:ExprParser.ElseIfStmtContext):
        pass

    # Exit a parse tree produced by ExprParser#elseIfStmt.
    def exitElseIfStmt(self, ctx:ExprParser.ElseIfStmtContext):
        pass


    # Enter a parse tree produced by ExprParser#else.
    def enterElse(self, ctx:ExprParser.ElseContext):
        pass

    # Exit a parse tree produced by ExprParser#else.
    def exitElse(self, ctx:ExprParser.ElseContext):
        pass


    # Enter a parse tree produced by ExprParser#loop.
    def enterLoop(self, ctx:ExprParser.LoopContext):
        pass

    # Exit a parse tree produced by ExprParser#loop.
    def exitLoop(self, ctx:ExprParser.LoopContext):
        pass


    # Enter a parse tree produced by ExprParser#loops.
    def enterLoops(self, ctx:ExprParser.LoopsContext):
        pass

    # Exit a parse tree produced by ExprParser#loops.
    def exitLoops(self, ctx:ExprParser.LoopsContext):
        pass


    # Enter a parse tree produced by ExprParser#loopStmt.
    def enterLoopStmt(self, ctx:ExprParser.LoopStmtContext):
        pass

    # Exit a parse tree produced by ExprParser#loopStmt.
    def exitLoopStmt(self, ctx:ExprParser.LoopStmtContext):
        pass


    # Enter a parse tree produced by ExprParser#whileStmt.
    def enterWhileStmt(self, ctx:ExprParser.WhileStmtContext):
        pass

    # Exit a parse tree produced by ExprParser#whileStmt.
    def exitWhileStmt(self, ctx:ExprParser.WhileStmtContext):
        pass


    # Enter a parse tree produced by ExprParser#foreachStmt.
    def enterForeachStmt(self, ctx:ExprParser.ForeachStmtContext):
        pass

    # Exit a parse tree produced by ExprParser#foreachStmt.
    def exitForeachStmt(self, ctx:ExprParser.ForeachStmtContext):
        pass


    # Enter a parse tree produced by ExprParser#listStmt.
    def enterListStmt(self, ctx:ExprParser.ListStmtContext):
        pass

    # Exit a parse tree produced by ExprParser#listStmt.
    def exitListStmt(self, ctx:ExprParser.ListStmtContext):
        pass


    # Enter a parse tree produced by ExprParser#listOpr.
    def enterListOpr(self, ctx:ExprParser.ListOprContext):
        pass

    # Exit a parse tree produced by ExprParser#listOpr.
    def exitListOpr(self, ctx:ExprParser.ListOprContext):
        pass


    # Enter a parse tree produced by ExprParser#funcCall.
    def enterFuncCall(self, ctx:ExprParser.FuncCallContext):
        pass

    # Exit a parse tree produced by ExprParser#funcCall.
    def exitFuncCall(self, ctx:ExprParser.FuncCallContext):
        pass


    # Enter a parse tree produced by ExprParser#funcDef.
    def enterFuncDef(self, ctx:ExprParser.FuncDefContext):
        pass

    # Exit a parse tree produced by ExprParser#funcDef.
    def exitFuncDef(self, ctx:ExprParser.FuncDefContext):
        pass


    # Enter a parse tree produced by ExprParser#funcReturn.
    def enterFuncReturn(self, ctx:ExprParser.FuncReturnContext):
        pass

    # Exit a parse tree produced by ExprParser#funcReturn.
    def exitFuncReturn(self, ctx:ExprParser.FuncReturnContext):
        pass


    # Enter a parse tree produced by ExprParser#paramList.
    def enterParamList(self, ctx:ExprParser.ParamListContext):
        pass

    # Exit a parse tree produced by ExprParser#paramList.
    def exitParamList(self, ctx:ExprParser.ParamListContext):
        pass


    # Enter a parse tree produced by ExprParser#paramTail.
    def enterParamTail(self, ctx:ExprParser.ParamTailContext):
        pass

    # Exit a parse tree produced by ExprParser#paramTail.
    def exitParamTail(self, ctx:ExprParser.ParamTailContext):
        pass


    # Enter a parse tree produced by ExprParser#param.
    def enterParam(self, ctx:ExprParser.ParamContext):
        pass

    # Exit a parse tree produced by ExprParser#param.
    def exitParam(self, ctx:ExprParser.ParamContext):
        pass


    # Enter a parse tree produced by ExprParser#argList.
    def enterArgList(self, ctx:ExprParser.ArgListContext):
        pass

    # Exit a parse tree produced by ExprParser#argList.
    def exitArgList(self, ctx:ExprParser.ArgListContext):
        pass


    # Enter a parse tree produced by ExprParser#argTail.
    def enterArgTail(self, ctx:ExprParser.ArgTailContext):
        pass

    # Exit a parse tree produced by ExprParser#argTail.
    def exitArgTail(self, ctx:ExprParser.ArgTailContext):
        pass


    # Enter a parse tree produced by ExprParser#boolean.
    def enterBoolean(self, ctx:ExprParser.BooleanContext):
        pass

    # Exit a parse tree produced by ExprParser#boolean.
    def exitBoolean(self, ctx:ExprParser.BooleanContext):
        pass


    # Enter a parse tree produced by ExprParser#type.
    def enterType(self, ctx:ExprParser.TypeContext):
        pass

    # Exit a parse tree produced by ExprParser#type.
    def exitType(self, ctx:ExprParser.TypeContext):
        pass



del ExprParser