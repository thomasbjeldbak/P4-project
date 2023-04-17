# Generated from C:\Github Repos\P4-project\ExprParser.g4 by ANTLR 4.12.0
# encoding: utf-8
from antlr4 import *
from io import StringIO
import sys
if sys.version_info[1] > 5:
	from typing import TextIO
else:
	from typing.io import TextIO

def serializedATN():
    return [
        4,1,45,358,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
        6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,
        2,14,7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,
        7,20,2,21,7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,
        2,27,7,27,2,28,7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,
        7,33,2,34,7,34,2,35,7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,
        2,40,7,40,1,0,1,0,1,1,1,1,1,1,1,1,3,1,89,8,1,1,2,1,2,3,2,93,8,2,
        1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,3,4,103,8,4,1,5,1,5,1,5,1,5,1,5,
        1,5,1,5,1,5,1,5,1,5,1,5,1,5,1,5,3,5,118,8,5,1,6,1,6,1,6,1,7,1,7,
        1,7,1,7,1,7,3,7,128,8,7,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,9,3,9,138,
        8,9,1,10,1,10,1,10,1,11,1,11,1,11,1,11,1,11,1,11,1,11,1,11,1,11,
        3,11,152,8,11,1,12,1,12,1,12,1,13,1,13,1,13,1,13,1,13,1,13,1,13,
        1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,3,13,174,8,13,
        1,14,1,14,1,14,1,15,1,15,1,15,1,15,1,15,1,15,1,15,1,15,1,15,3,15,
        188,8,15,1,16,1,16,1,16,1,17,1,17,1,17,1,17,1,17,1,17,1,17,1,17,
        1,17,3,17,202,8,17,1,18,1,18,1,18,1,18,1,18,1,18,1,18,1,18,3,18,
        212,8,18,1,19,1,19,1,19,1,19,1,20,1,20,3,20,220,8,20,1,21,1,21,1,
        21,1,21,1,21,1,21,1,21,1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,22,1,
        22,1,22,3,22,239,8,22,1,23,1,23,1,23,3,23,244,8,23,1,24,1,24,1,24,
        1,25,1,25,1,25,3,25,252,8,25,1,26,1,26,1,26,1,26,1,26,1,26,1,27,
        1,27,1,27,1,27,1,27,1,27,1,28,1,28,1,28,1,28,1,28,1,28,1,28,1,28,
        1,28,1,29,1,29,1,29,1,29,1,30,1,30,1,30,1,30,1,30,1,30,1,30,1,30,
        1,30,1,30,1,30,1,30,1,30,1,30,1,30,1,30,1,30,1,30,1,30,1,30,3,30,
        299,8,30,1,31,1,31,1,31,1,31,1,31,1,31,1,32,1,32,1,32,1,32,1,32,
        1,32,1,32,1,33,1,33,1,33,3,33,317,8,33,1,34,1,34,1,34,1,34,3,34,
        323,8,34,1,35,1,35,1,35,1,35,1,35,3,35,330,8,35,1,36,1,36,1,36,1,
        37,1,37,1,37,3,37,338,8,37,1,38,1,38,1,38,1,38,3,38,344,8,38,1,39,
        1,39,1,40,1,40,1,40,1,40,1,40,1,40,1,40,1,40,3,40,356,8,40,1,40,
        0,0,41,0,2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32,34,36,38,40,
        42,44,46,48,50,52,54,56,58,60,62,64,66,68,70,72,74,76,78,80,0,1,
        1,0,17,18,356,0,82,1,0,0,0,2,88,1,0,0,0,4,92,1,0,0,0,6,94,1,0,0,
        0,8,102,1,0,0,0,10,117,1,0,0,0,12,119,1,0,0,0,14,127,1,0,0,0,16,
        129,1,0,0,0,18,137,1,0,0,0,20,139,1,0,0,0,22,151,1,0,0,0,24,153,
        1,0,0,0,26,173,1,0,0,0,28,175,1,0,0,0,30,187,1,0,0,0,32,189,1,0,
        0,0,34,201,1,0,0,0,36,211,1,0,0,0,38,213,1,0,0,0,40,219,1,0,0,0,
        42,221,1,0,0,0,44,238,1,0,0,0,46,243,1,0,0,0,48,245,1,0,0,0,50,251,
        1,0,0,0,52,253,1,0,0,0,54,259,1,0,0,0,56,265,1,0,0,0,58,274,1,0,
        0,0,60,298,1,0,0,0,62,300,1,0,0,0,64,306,1,0,0,0,66,316,1,0,0,0,
        68,322,1,0,0,0,70,329,1,0,0,0,72,331,1,0,0,0,74,337,1,0,0,0,76,343,
        1,0,0,0,78,345,1,0,0,0,80,355,1,0,0,0,82,83,3,2,1,0,83,1,1,0,0,0,
        84,85,3,4,2,0,85,86,3,2,1,0,86,89,1,0,0,0,87,89,1,0,0,0,88,84,1,
        0,0,0,88,87,1,0,0,0,89,3,1,0,0,0,90,93,3,10,5,0,91,93,3,6,3,0,92,
        90,1,0,0,0,92,91,1,0,0,0,93,5,1,0,0,0,94,95,3,80,40,0,95,96,5,44,
        0,0,96,97,3,8,4,0,97,98,5,11,0,0,98,7,1,0,0,0,99,100,5,9,0,0,100,
        103,3,12,6,0,101,103,1,0,0,0,102,99,1,0,0,0,102,101,1,0,0,0,103,
        9,1,0,0,0,104,105,5,44,0,0,105,106,5,9,0,0,106,107,3,12,6,0,107,
        108,5,11,0,0,108,118,1,0,0,0,109,118,3,40,20,0,110,111,3,58,29,0,
        111,112,5,11,0,0,112,118,1,0,0,0,113,118,3,64,32,0,114,115,3,62,
        31,0,115,116,5,11,0,0,116,118,1,0,0,0,117,104,1,0,0,0,117,109,1,
        0,0,0,117,110,1,0,0,0,117,113,1,0,0,0,117,114,1,0,0,0,118,11,1,0,
        0,0,119,120,3,16,8,0,120,121,3,14,7,0,121,13,1,0,0,0,122,123,5,1,
        0,0,123,124,3,16,8,0,124,125,3,14,7,0,125,128,1,0,0,0,126,128,1,
        0,0,0,127,122,1,0,0,0,127,126,1,0,0,0,128,15,1,0,0,0,129,130,3,20,
        10,0,130,131,3,18,9,0,131,17,1,0,0,0,132,133,5,2,0,0,133,134,3,20,
        10,0,134,135,3,18,9,0,135,138,1,0,0,0,136,138,1,0,0,0,137,132,1,
        0,0,0,137,136,1,0,0,0,138,19,1,0,0,0,139,140,3,24,12,0,140,141,3,
        22,11,0,141,21,1,0,0,0,142,143,5,3,0,0,143,144,3,24,12,0,144,145,
        3,22,11,0,145,152,1,0,0,0,146,147,5,4,0,0,147,148,3,24,12,0,148,
        149,3,22,11,0,149,152,1,0,0,0,150,152,1,0,0,0,151,142,1,0,0,0,151,
        146,1,0,0,0,151,150,1,0,0,0,152,23,1,0,0,0,153,154,3,28,14,0,154,
        155,3,26,13,0,155,25,1,0,0,0,156,157,5,5,0,0,157,158,3,28,14,0,158,
        159,3,26,13,0,159,174,1,0,0,0,160,161,5,6,0,0,161,162,3,28,14,0,
        162,163,3,26,13,0,163,174,1,0,0,0,164,165,5,7,0,0,165,166,3,28,14,
        0,166,167,3,26,13,0,167,174,1,0,0,0,168,169,5,8,0,0,169,170,3,28,
        14,0,170,171,3,26,13,0,171,174,1,0,0,0,172,174,1,0,0,0,173,156,1,
        0,0,0,173,160,1,0,0,0,173,164,1,0,0,0,173,168,1,0,0,0,173,172,1,
        0,0,0,174,27,1,0,0,0,175,176,3,32,16,0,176,177,3,30,15,0,177,29,
        1,0,0,0,178,179,5,19,0,0,179,180,3,32,16,0,180,181,3,30,15,0,181,
        188,1,0,0,0,182,183,5,20,0,0,183,184,3,32,16,0,184,185,3,30,15,0,
        185,188,1,0,0,0,186,188,1,0,0,0,187,178,1,0,0,0,187,182,1,0,0,0,
        187,186,1,0,0,0,188,31,1,0,0,0,189,190,3,36,18,0,190,191,3,34,17,
        0,191,33,1,0,0,0,192,193,5,21,0,0,193,194,3,36,18,0,194,195,3,34,
        17,0,195,202,1,0,0,0,196,197,5,22,0,0,197,198,3,36,18,0,198,199,
        3,34,17,0,199,202,1,0,0,0,200,202,1,0,0,0,201,192,1,0,0,0,201,196,
        1,0,0,0,201,200,1,0,0,0,202,35,1,0,0,0,203,204,5,13,0,0,204,205,
        3,12,6,0,205,206,5,14,0,0,206,212,1,0,0,0,207,212,5,43,0,0,208,212,
        5,42,0,0,209,212,5,44,0,0,210,212,3,78,39,0,211,203,1,0,0,0,211,
        207,1,0,0,0,211,208,1,0,0,0,211,209,1,0,0,0,211,210,1,0,0,0,212,
        37,1,0,0,0,213,214,5,15,0,0,214,215,3,2,1,0,215,216,5,16,0,0,216,
        39,1,0,0,0,217,220,3,42,21,0,218,220,3,48,24,0,219,217,1,0,0,0,219,
        218,1,0,0,0,220,41,1,0,0,0,221,222,5,28,0,0,222,223,5,13,0,0,223,
        224,3,12,6,0,224,225,5,14,0,0,225,226,3,38,19,0,226,227,3,44,22,
        0,227,43,1,0,0,0,228,229,5,29,0,0,229,230,5,28,0,0,230,231,5,13,
        0,0,231,232,3,12,6,0,232,233,5,14,0,0,233,234,3,38,19,0,234,235,
        3,44,22,0,235,239,1,0,0,0,236,239,3,46,23,0,237,239,1,0,0,0,238,
        228,1,0,0,0,238,236,1,0,0,0,238,237,1,0,0,0,239,45,1,0,0,0,240,241,
        5,29,0,0,241,244,3,38,19,0,242,244,1,0,0,0,243,240,1,0,0,0,243,242,
        1,0,0,0,244,47,1,0,0,0,245,246,5,30,0,0,246,247,3,50,25,0,247,49,
        1,0,0,0,248,252,3,52,26,0,249,252,3,54,27,0,250,252,3,56,28,0,251,
        248,1,0,0,0,251,249,1,0,0,0,251,250,1,0,0,0,252,51,1,0,0,0,253,254,
        5,13,0,0,254,255,3,12,6,0,255,256,5,14,0,0,256,257,5,31,0,0,257,
        258,3,38,19,0,258,53,1,0,0,0,259,260,5,32,0,0,260,261,5,13,0,0,261,
        262,3,12,6,0,262,263,5,14,0,0,263,264,3,38,19,0,264,55,1,0,0,0,265,
        266,5,33,0,0,266,267,5,13,0,0,267,268,3,80,40,0,268,269,5,44,0,0,
        269,270,5,34,0,0,270,271,5,44,0,0,271,272,5,14,0,0,272,273,3,38,
        19,0,273,57,1,0,0,0,274,275,5,44,0,0,275,276,5,12,0,0,276,277,3,
        60,30,0,277,59,1,0,0,0,278,279,5,38,0,0,279,280,5,13,0,0,280,281,
        3,12,6,0,281,282,5,14,0,0,282,299,1,0,0,0,283,284,5,40,0,0,284,285,
        5,13,0,0,285,286,3,12,6,0,286,287,5,14,0,0,287,299,1,0,0,0,288,289,
        5,39,0,0,289,290,5,13,0,0,290,291,3,12,6,0,291,292,5,14,0,0,292,
        299,1,0,0,0,293,294,5,41,0,0,294,295,5,13,0,0,295,296,3,12,6,0,296,
        297,5,14,0,0,297,299,1,0,0,0,298,278,1,0,0,0,298,283,1,0,0,0,298,
        288,1,0,0,0,298,293,1,0,0,0,299,61,1,0,0,0,300,301,5,37,0,0,301,
        302,5,44,0,0,302,303,5,13,0,0,303,304,3,74,37,0,304,305,5,14,0,0,
        305,63,1,0,0,0,306,307,5,35,0,0,307,308,5,13,0,0,308,309,3,68,34,
        0,309,310,5,14,0,0,310,311,3,66,33,0,311,312,3,38,19,0,312,65,1,
        0,0,0,313,314,5,36,0,0,314,317,3,80,40,0,315,317,1,0,0,0,316,313,
        1,0,0,0,316,315,1,0,0,0,317,67,1,0,0,0,318,319,3,72,36,0,319,320,
        3,70,35,0,320,323,1,0,0,0,321,323,1,0,0,0,322,318,1,0,0,0,322,321,
        1,0,0,0,323,69,1,0,0,0,324,325,5,10,0,0,325,326,3,72,36,0,326,327,
        3,70,35,0,327,330,1,0,0,0,328,330,1,0,0,0,329,324,1,0,0,0,329,328,
        1,0,0,0,330,71,1,0,0,0,331,332,3,80,40,0,332,333,5,44,0,0,333,73,
        1,0,0,0,334,335,5,44,0,0,335,338,3,76,38,0,336,338,1,0,0,0,337,334,
        1,0,0,0,337,336,1,0,0,0,338,75,1,0,0,0,339,340,5,10,0,0,340,341,
        5,44,0,0,341,344,3,76,38,0,342,344,1,0,0,0,343,339,1,0,0,0,343,342,
        1,0,0,0,344,77,1,0,0,0,345,346,7,0,0,0,346,79,1,0,0,0,347,356,5,
        23,0,0,348,356,5,24,0,0,349,356,5,25,0,0,350,351,5,26,0,0,351,352,
        5,13,0,0,352,353,3,80,40,0,353,354,5,14,0,0,354,356,1,0,0,0,355,
        347,1,0,0,0,355,348,1,0,0,0,355,349,1,0,0,0,355,350,1,0,0,0,356,
        81,1,0,0,0,22,88,92,102,117,127,137,151,173,187,201,211,219,238,
        243,251,298,316,322,329,337,343,355
    ]

class ExprParser ( Parser ):

    grammarFileName = "ExprParser.g4"

    atn = ATNDeserializer().deserialize(serializedATN())

    decisionsToDFA = [ DFA(ds, i) for i, ds in enumerate(atn.decisionToState) ]

    sharedContextCache = PredictionContextCache()

    literalNames = [ "<INVALID>", "'or'", "'and'", "'is'", "'is not'", "'>'", 
                     "'<'", "'>='", "'<='", "'='", "','", "';'", "':'", 
                     "'('", "')'", "'{'", "'}'", "'true'", "'false'", "'+'", 
                     "'-'", "'*'", "'/'", "'boolean'", "'text'", "'number'", 
                     "'list'", "'\"'", "'if'", "'else'", "'repeat'", "'times'", 
                     "'while'", "'for each'", "'in'", "'function'", "'return'", 
                     "'call'", "'Add'", "'IndexOf'", "'Delete'", "'ValueOf'" ]

    symbolicNames = [ "<INVALID>", "OR", "AND", "EQUAL", "NOT", "GREAT", 
                      "LESS", "GREATEQL", "LESSEQL", "ASSIGN", "COMMA", 
                      "SEMI", "COLON", "LPAREN", "RPAREN", "LCURLY", "RCURLY", 
                      "TRUE", "FALSE", "ADD", "SUB", "MUL", "DIV", "BOOL", 
                      "TEXT", "NUM", "LIST", "QUOTE", "IF", "ELSE", "REPEAT", 
                      "TIMES", "WHILE", "FOREACH", "IN", "FUNCTION", "RETURN", 
                      "CALL", "LISTADD", "LISTIDXOF", "LISTDEL", "LISTVALOF", 
                      "STR", "INT", "ID", "WS" ]

    RULE_program = 0
    RULE_cmds = 1
    RULE_cmd = 2
    RULE_dcl = 3
    RULE_ass = 4
    RULE_stmt = 5
    RULE_expr = 6
    RULE_oprOr = 7
    RULE_logicOr = 8
    RULE_oprAnd = 9
    RULE_logicAnd = 10
    RULE_oprEql = 11
    RULE_equal = 12
    RULE_oprBool = 13
    RULE_bool = 14
    RULE_oprExpr = 15
    RULE_term = 16
    RULE_oprTerm = 17
    RULE_factor = 18
    RULE_block = 19
    RULE_ctrlStrct = 20
    RULE_ifStmt = 21
    RULE_elseIfStmt = 22
    RULE_else = 23
    RULE_loop = 24
    RULE_loops = 25
    RULE_loopStmt = 26
    RULE_whileStmt = 27
    RULE_foreachStmt = 28
    RULE_listStmt = 29
    RULE_listOpr = 30
    RULE_funcCall = 31
    RULE_funcDef = 32
    RULE_funcReturn = 33
    RULE_paramList = 34
    RULE_paramTail = 35
    RULE_param = 36
    RULE_argList = 37
    RULE_argTail = 38
    RULE_boolean = 39
    RULE_type = 40

    ruleNames =  [ "program", "cmds", "cmd", "dcl", "ass", "stmt", "expr", 
                   "oprOr", "logicOr", "oprAnd", "logicAnd", "oprEql", "equal", 
                   "oprBool", "bool", "oprExpr", "term", "oprTerm", "factor", 
                   "block", "ctrlStrct", "ifStmt", "elseIfStmt", "else", 
                   "loop", "loops", "loopStmt", "whileStmt", "foreachStmt", 
                   "listStmt", "listOpr", "funcCall", "funcDef", "funcReturn", 
                   "paramList", "paramTail", "param", "argList", "argTail", 
                   "boolean", "type" ]

    EOF = Token.EOF
    OR=1
    AND=2
    EQUAL=3
    NOT=4
    GREAT=5
    LESS=6
    GREATEQL=7
    LESSEQL=8
    ASSIGN=9
    COMMA=10
    SEMI=11
    COLON=12
    LPAREN=13
    RPAREN=14
    LCURLY=15
    RCURLY=16
    TRUE=17
    FALSE=18
    ADD=19
    SUB=20
    MUL=21
    DIV=22
    BOOL=23
    TEXT=24
    NUM=25
    LIST=26
    QUOTE=27
    IF=28
    ELSE=29
    REPEAT=30
    TIMES=31
    WHILE=32
    FOREACH=33
    IN=34
    FUNCTION=35
    RETURN=36
    CALL=37
    LISTADD=38
    LISTIDXOF=39
    LISTDEL=40
    LISTVALOF=41
    STR=42
    INT=43
    ID=44
    WS=45

    def __init__(self, input:TokenStream, output:TextIO = sys.stdout):
        super().__init__(input, output)
        self.checkVersion("4.12.0")
        self._interp = ParserATNSimulator(self, self.atn, self.decisionsToDFA, self.sharedContextCache)
        self._predicates = None




    class ProgramContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def cmds(self):
            return self.getTypedRuleContext(ExprParser.CmdsContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_program

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterProgram" ):
                listener.enterProgram(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitProgram" ):
                listener.exitProgram(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitProgram" ):
                return visitor.visitProgram(self)
            else:
                return visitor.visitChildren(self)




    def program(self):

        localctx = ExprParser.ProgramContext(self, self._ctx, self.state)
        self.enterRule(localctx, 0, self.RULE_program)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 82
            self.cmds()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class CmdsContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def cmd(self):
            return self.getTypedRuleContext(ExprParser.CmdContext,0)


        def cmds(self):
            return self.getTypedRuleContext(ExprParser.CmdsContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_cmds

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterCmds" ):
                listener.enterCmds(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitCmds" ):
                listener.exitCmds(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitCmds" ):
                return visitor.visitCmds(self)
            else:
                return visitor.visitChildren(self)




    def cmds(self):

        localctx = ExprParser.CmdsContext(self, self._ctx, self.state)
        self.enterRule(localctx, 2, self.RULE_cmds)
        try:
            self.state = 88
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [23, 24, 25, 26, 28, 30, 35, 37, 44]:
                self.enterOuterAlt(localctx, 1)
                self.state = 84
                self.cmd()
                self.state = 85
                self.cmds()
                pass
            elif token in [-1, 16]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class CmdContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def stmt(self):
            return self.getTypedRuleContext(ExprParser.StmtContext,0)


        def dcl(self):
            return self.getTypedRuleContext(ExprParser.DclContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_cmd

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterCmd" ):
                listener.enterCmd(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitCmd" ):
                listener.exitCmd(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitCmd" ):
                return visitor.visitCmd(self)
            else:
                return visitor.visitChildren(self)




    def cmd(self):

        localctx = ExprParser.CmdContext(self, self._ctx, self.state)
        self.enterRule(localctx, 4, self.RULE_cmd)
        try:
            self.state = 92
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [28, 30, 35, 37, 44]:
                self.enterOuterAlt(localctx, 1)
                self.state = 90
                self.stmt()
                pass
            elif token in [23, 24, 25, 26]:
                self.enterOuterAlt(localctx, 2)
                self.state = 91
                self.dcl()
                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class DclContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def type_(self):
            return self.getTypedRuleContext(ExprParser.TypeContext,0)


        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def ass(self):
            return self.getTypedRuleContext(ExprParser.AssContext,0)


        def SEMI(self):
            return self.getToken(ExprParser.SEMI, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_dcl

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterDcl" ):
                listener.enterDcl(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitDcl" ):
                listener.exitDcl(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitDcl" ):
                return visitor.visitDcl(self)
            else:
                return visitor.visitChildren(self)




    def dcl(self):

        localctx = ExprParser.DclContext(self, self._ctx, self.state)
        self.enterRule(localctx, 6, self.RULE_dcl)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 94
            self.type_()
            self.state = 95
            self.match(ExprParser.ID)
            self.state = 96
            self.ass()
            self.state = 97
            self.match(ExprParser.SEMI)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class AssContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ASSIGN(self):
            return self.getToken(ExprParser.ASSIGN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_ass

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterAss" ):
                listener.enterAss(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitAss" ):
                listener.exitAss(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitAss" ):
                return visitor.visitAss(self)
            else:
                return visitor.visitChildren(self)




    def ass(self):

        localctx = ExprParser.AssContext(self, self._ctx, self.state)
        self.enterRule(localctx, 8, self.RULE_ass)
        try:
            self.state = 102
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [9]:
                self.enterOuterAlt(localctx, 1)
                self.state = 99
                self.match(ExprParser.ASSIGN)
                self.state = 100
                self.expr()
                pass
            elif token in [11]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class StmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def ASSIGN(self):
            return self.getToken(ExprParser.ASSIGN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def SEMI(self):
            return self.getToken(ExprParser.SEMI, 0)

        def ctrlStrct(self):
            return self.getTypedRuleContext(ExprParser.CtrlStrctContext,0)


        def listStmt(self):
            return self.getTypedRuleContext(ExprParser.ListStmtContext,0)


        def funcDef(self):
            return self.getTypedRuleContext(ExprParser.FuncDefContext,0)


        def funcCall(self):
            return self.getTypedRuleContext(ExprParser.FuncCallContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_stmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterStmt" ):
                listener.enterStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitStmt" ):
                listener.exitStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitStmt" ):
                return visitor.visitStmt(self)
            else:
                return visitor.visitChildren(self)




    def stmt(self):

        localctx = ExprParser.StmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 10, self.RULE_stmt)
        try:
            self.state = 117
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,3,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 104
                self.match(ExprParser.ID)
                self.state = 105
                self.match(ExprParser.ASSIGN)
                self.state = 106
                self.expr()
                self.state = 107
                self.match(ExprParser.SEMI)
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 109
                self.ctrlStrct()
                pass

            elif la_ == 3:
                self.enterOuterAlt(localctx, 3)
                self.state = 110
                self.listStmt()
                self.state = 111
                self.match(ExprParser.SEMI)
                pass

            elif la_ == 4:
                self.enterOuterAlt(localctx, 4)
                self.state = 113
                self.funcDef()
                pass

            elif la_ == 5:
                self.enterOuterAlt(localctx, 5)
                self.state = 114
                self.funcCall()
                self.state = 115
                self.match(ExprParser.SEMI)
                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ExprContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def logicOr(self):
            return self.getTypedRuleContext(ExprParser.LogicOrContext,0)


        def oprOr(self):
            return self.getTypedRuleContext(ExprParser.OprOrContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_expr

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterExpr" ):
                listener.enterExpr(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitExpr" ):
                listener.exitExpr(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitExpr" ):
                return visitor.visitExpr(self)
            else:
                return visitor.visitChildren(self)




    def expr(self):

        localctx = ExprParser.ExprContext(self, self._ctx, self.state)
        self.enterRule(localctx, 12, self.RULE_expr)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 119
            self.logicOr()
            self.state = 120
            self.oprOr()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class OprOrContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def OR(self):
            return self.getToken(ExprParser.OR, 0)

        def logicOr(self):
            return self.getTypedRuleContext(ExprParser.LogicOrContext,0)


        def oprOr(self):
            return self.getTypedRuleContext(ExprParser.OprOrContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_oprOr

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterOprOr" ):
                listener.enterOprOr(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitOprOr" ):
                listener.exitOprOr(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitOprOr" ):
                return visitor.visitOprOr(self)
            else:
                return visitor.visitChildren(self)




    def oprOr(self):

        localctx = ExprParser.OprOrContext(self, self._ctx, self.state)
        self.enterRule(localctx, 14, self.RULE_oprOr)
        try:
            self.state = 127
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [1]:
                self.enterOuterAlt(localctx, 1)
                self.state = 122
                self.match(ExprParser.OR)
                self.state = 123
                self.logicOr()
                self.state = 124
                self.oprOr()
                pass
            elif token in [11, 14]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LogicOrContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def logicAnd(self):
            return self.getTypedRuleContext(ExprParser.LogicAndContext,0)


        def oprAnd(self):
            return self.getTypedRuleContext(ExprParser.OprAndContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_logicOr

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLogicOr" ):
                listener.enterLogicOr(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLogicOr" ):
                listener.exitLogicOr(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitLogicOr" ):
                return visitor.visitLogicOr(self)
            else:
                return visitor.visitChildren(self)




    def logicOr(self):

        localctx = ExprParser.LogicOrContext(self, self._ctx, self.state)
        self.enterRule(localctx, 16, self.RULE_logicOr)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 129
            self.logicAnd()
            self.state = 130
            self.oprAnd()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class OprAndContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def AND(self):
            return self.getToken(ExprParser.AND, 0)

        def logicAnd(self):
            return self.getTypedRuleContext(ExprParser.LogicAndContext,0)


        def oprAnd(self):
            return self.getTypedRuleContext(ExprParser.OprAndContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_oprAnd

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterOprAnd" ):
                listener.enterOprAnd(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitOprAnd" ):
                listener.exitOprAnd(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitOprAnd" ):
                return visitor.visitOprAnd(self)
            else:
                return visitor.visitChildren(self)




    def oprAnd(self):

        localctx = ExprParser.OprAndContext(self, self._ctx, self.state)
        self.enterRule(localctx, 18, self.RULE_oprAnd)
        try:
            self.state = 137
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [2]:
                self.enterOuterAlt(localctx, 1)
                self.state = 132
                self.match(ExprParser.AND)
                self.state = 133
                self.logicAnd()
                self.state = 134
                self.oprAnd()
                pass
            elif token in [1, 11, 14]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LogicAndContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def equal(self):
            return self.getTypedRuleContext(ExprParser.EqualContext,0)


        def oprEql(self):
            return self.getTypedRuleContext(ExprParser.OprEqlContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_logicAnd

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLogicAnd" ):
                listener.enterLogicAnd(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLogicAnd" ):
                listener.exitLogicAnd(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitLogicAnd" ):
                return visitor.visitLogicAnd(self)
            else:
                return visitor.visitChildren(self)




    def logicAnd(self):

        localctx = ExprParser.LogicAndContext(self, self._ctx, self.state)
        self.enterRule(localctx, 20, self.RULE_logicAnd)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 139
            self.equal()
            self.state = 140
            self.oprEql()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class OprEqlContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def EQUAL(self):
            return self.getToken(ExprParser.EQUAL, 0)

        def equal(self):
            return self.getTypedRuleContext(ExprParser.EqualContext,0)


        def oprEql(self):
            return self.getTypedRuleContext(ExprParser.OprEqlContext,0)


        def NOT(self):
            return self.getToken(ExprParser.NOT, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_oprEql

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterOprEql" ):
                listener.enterOprEql(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitOprEql" ):
                listener.exitOprEql(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitOprEql" ):
                return visitor.visitOprEql(self)
            else:
                return visitor.visitChildren(self)




    def oprEql(self):

        localctx = ExprParser.OprEqlContext(self, self._ctx, self.state)
        self.enterRule(localctx, 22, self.RULE_oprEql)
        try:
            self.state = 151
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [3]:
                self.enterOuterAlt(localctx, 1)
                self.state = 142
                self.match(ExprParser.EQUAL)
                self.state = 143
                self.equal()
                self.state = 144
                self.oprEql()
                pass
            elif token in [4]:
                self.enterOuterAlt(localctx, 2)
                self.state = 146
                self.match(ExprParser.NOT)
                self.state = 147
                self.equal()
                self.state = 148
                self.oprEql()
                pass
            elif token in [1, 2, 11, 14]:
                self.enterOuterAlt(localctx, 3)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class EqualContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def bool_(self):
            return self.getTypedRuleContext(ExprParser.BoolContext,0)


        def oprBool(self):
            return self.getTypedRuleContext(ExprParser.OprBoolContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_equal

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterEqual" ):
                listener.enterEqual(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitEqual" ):
                listener.exitEqual(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitEqual" ):
                return visitor.visitEqual(self)
            else:
                return visitor.visitChildren(self)




    def equal(self):

        localctx = ExprParser.EqualContext(self, self._ctx, self.state)
        self.enterRule(localctx, 24, self.RULE_equal)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 153
            self.bool_()
            self.state = 154
            self.oprBool()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class OprBoolContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def GREAT(self):
            return self.getToken(ExprParser.GREAT, 0)

        def bool_(self):
            return self.getTypedRuleContext(ExprParser.BoolContext,0)


        def oprBool(self):
            return self.getTypedRuleContext(ExprParser.OprBoolContext,0)


        def LESS(self):
            return self.getToken(ExprParser.LESS, 0)

        def GREATEQL(self):
            return self.getToken(ExprParser.GREATEQL, 0)

        def LESSEQL(self):
            return self.getToken(ExprParser.LESSEQL, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_oprBool

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterOprBool" ):
                listener.enterOprBool(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitOprBool" ):
                listener.exitOprBool(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitOprBool" ):
                return visitor.visitOprBool(self)
            else:
                return visitor.visitChildren(self)




    def oprBool(self):

        localctx = ExprParser.OprBoolContext(self, self._ctx, self.state)
        self.enterRule(localctx, 26, self.RULE_oprBool)
        try:
            self.state = 173
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [5]:
                self.enterOuterAlt(localctx, 1)
                self.state = 156
                self.match(ExprParser.GREAT)
                self.state = 157
                self.bool_()
                self.state = 158
                self.oprBool()
                pass
            elif token in [6]:
                self.enterOuterAlt(localctx, 2)
                self.state = 160
                self.match(ExprParser.LESS)
                self.state = 161
                self.bool_()
                self.state = 162
                self.oprBool()
                pass
            elif token in [7]:
                self.enterOuterAlt(localctx, 3)
                self.state = 164
                self.match(ExprParser.GREATEQL)
                self.state = 165
                self.bool_()
                self.state = 166
                self.oprBool()
                pass
            elif token in [8]:
                self.enterOuterAlt(localctx, 4)
                self.state = 168
                self.match(ExprParser.LESSEQL)
                self.state = 169
                self.bool_()
                self.state = 170
                self.oprBool()
                pass
            elif token in [1, 2, 3, 4, 11, 14]:
                self.enterOuterAlt(localctx, 5)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class BoolContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def term(self):
            return self.getTypedRuleContext(ExprParser.TermContext,0)


        def oprExpr(self):
            return self.getTypedRuleContext(ExprParser.OprExprContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_bool

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterBool" ):
                listener.enterBool(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitBool" ):
                listener.exitBool(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitBool" ):
                return visitor.visitBool(self)
            else:
                return visitor.visitChildren(self)




    def bool_(self):

        localctx = ExprParser.BoolContext(self, self._ctx, self.state)
        self.enterRule(localctx, 28, self.RULE_bool)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 175
            self.term()
            self.state = 176
            self.oprExpr()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class OprExprContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ADD(self):
            return self.getToken(ExprParser.ADD, 0)

        def term(self):
            return self.getTypedRuleContext(ExprParser.TermContext,0)


        def oprExpr(self):
            return self.getTypedRuleContext(ExprParser.OprExprContext,0)


        def SUB(self):
            return self.getToken(ExprParser.SUB, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_oprExpr

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterOprExpr" ):
                listener.enterOprExpr(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitOprExpr" ):
                listener.exitOprExpr(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitOprExpr" ):
                return visitor.visitOprExpr(self)
            else:
                return visitor.visitChildren(self)




    def oprExpr(self):

        localctx = ExprParser.OprExprContext(self, self._ctx, self.state)
        self.enterRule(localctx, 30, self.RULE_oprExpr)
        try:
            self.state = 187
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [19]:
                self.enterOuterAlt(localctx, 1)
                self.state = 178
                self.match(ExprParser.ADD)
                self.state = 179
                self.term()
                self.state = 180
                self.oprExpr()
                pass
            elif token in [20]:
                self.enterOuterAlt(localctx, 2)
                self.state = 182
                self.match(ExprParser.SUB)
                self.state = 183
                self.term()
                self.state = 184
                self.oprExpr()
                pass
            elif token in [1, 2, 3, 4, 5, 6, 7, 8, 11, 14]:
                self.enterOuterAlt(localctx, 3)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class TermContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def factor(self):
            return self.getTypedRuleContext(ExprParser.FactorContext,0)


        def oprTerm(self):
            return self.getTypedRuleContext(ExprParser.OprTermContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_term

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterTerm" ):
                listener.enterTerm(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitTerm" ):
                listener.exitTerm(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitTerm" ):
                return visitor.visitTerm(self)
            else:
                return visitor.visitChildren(self)




    def term(self):

        localctx = ExprParser.TermContext(self, self._ctx, self.state)
        self.enterRule(localctx, 32, self.RULE_term)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 189
            self.factor()
            self.state = 190
            self.oprTerm()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class OprTermContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def MUL(self):
            return self.getToken(ExprParser.MUL, 0)

        def factor(self):
            return self.getTypedRuleContext(ExprParser.FactorContext,0)


        def oprTerm(self):
            return self.getTypedRuleContext(ExprParser.OprTermContext,0)


        def DIV(self):
            return self.getToken(ExprParser.DIV, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_oprTerm

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterOprTerm" ):
                listener.enterOprTerm(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitOprTerm" ):
                listener.exitOprTerm(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitOprTerm" ):
                return visitor.visitOprTerm(self)
            else:
                return visitor.visitChildren(self)




    def oprTerm(self):

        localctx = ExprParser.OprTermContext(self, self._ctx, self.state)
        self.enterRule(localctx, 34, self.RULE_oprTerm)
        try:
            self.state = 201
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [21]:
                self.enterOuterAlt(localctx, 1)
                self.state = 192
                self.match(ExprParser.MUL)
                self.state = 193
                self.factor()
                self.state = 194
                self.oprTerm()
                pass
            elif token in [22]:
                self.enterOuterAlt(localctx, 2)
                self.state = 196
                self.match(ExprParser.DIV)
                self.state = 197
                self.factor()
                self.state = 198
                self.oprTerm()
                pass
            elif token in [1, 2, 3, 4, 5, 6, 7, 8, 11, 14, 19, 20]:
                self.enterOuterAlt(localctx, 3)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FactorContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def INT(self):
            return self.getToken(ExprParser.INT, 0)

        def STR(self):
            return self.getToken(ExprParser.STR, 0)

        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def boolean(self):
            return self.getTypedRuleContext(ExprParser.BooleanContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_factor

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFactor" ):
                listener.enterFactor(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFactor" ):
                listener.exitFactor(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitFactor" ):
                return visitor.visitFactor(self)
            else:
                return visitor.visitChildren(self)




    def factor(self):

        localctx = ExprParser.FactorContext(self, self._ctx, self.state)
        self.enterRule(localctx, 36, self.RULE_factor)
        try:
            self.state = 211
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [13]:
                self.enterOuterAlt(localctx, 1)
                self.state = 203
                self.match(ExprParser.LPAREN)
                self.state = 204
                self.expr()
                self.state = 205
                self.match(ExprParser.RPAREN)
                pass
            elif token in [43]:
                self.enterOuterAlt(localctx, 2)
                self.state = 207
                self.match(ExprParser.INT)
                pass
            elif token in [42]:
                self.enterOuterAlt(localctx, 3)
                self.state = 208
                self.match(ExprParser.STR)
                pass
            elif token in [44]:
                self.enterOuterAlt(localctx, 4)
                self.state = 209
                self.match(ExprParser.ID)
                pass
            elif token in [17, 18]:
                self.enterOuterAlt(localctx, 5)
                self.state = 210
                self.boolean()
                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class BlockContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LCURLY(self):
            return self.getToken(ExprParser.LCURLY, 0)

        def cmds(self):
            return self.getTypedRuleContext(ExprParser.CmdsContext,0)


        def RCURLY(self):
            return self.getToken(ExprParser.RCURLY, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_block

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterBlock" ):
                listener.enterBlock(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitBlock" ):
                listener.exitBlock(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitBlock" ):
                return visitor.visitBlock(self)
            else:
                return visitor.visitChildren(self)




    def block(self):

        localctx = ExprParser.BlockContext(self, self._ctx, self.state)
        self.enterRule(localctx, 38, self.RULE_block)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 213
            self.match(ExprParser.LCURLY)
            self.state = 214
            self.cmds()
            self.state = 215
            self.match(ExprParser.RCURLY)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class CtrlStrctContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ifStmt(self):
            return self.getTypedRuleContext(ExprParser.IfStmtContext,0)


        def loop(self):
            return self.getTypedRuleContext(ExprParser.LoopContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_ctrlStrct

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterCtrlStrct" ):
                listener.enterCtrlStrct(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitCtrlStrct" ):
                listener.exitCtrlStrct(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitCtrlStrct" ):
                return visitor.visitCtrlStrct(self)
            else:
                return visitor.visitChildren(self)




    def ctrlStrct(self):

        localctx = ExprParser.CtrlStrctContext(self, self._ctx, self.state)
        self.enterRule(localctx, 40, self.RULE_ctrlStrct)
        try:
            self.state = 219
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [28]:
                self.enterOuterAlt(localctx, 1)
                self.state = 217
                self.ifStmt()
                pass
            elif token in [30]:
                self.enterOuterAlt(localctx, 2)
                self.state = 218
                self.loop()
                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class IfStmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def IF(self):
            return self.getToken(ExprParser.IF, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def elseIfStmt(self):
            return self.getTypedRuleContext(ExprParser.ElseIfStmtContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_ifStmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterIfStmt" ):
                listener.enterIfStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitIfStmt" ):
                listener.exitIfStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitIfStmt" ):
                return visitor.visitIfStmt(self)
            else:
                return visitor.visitChildren(self)




    def ifStmt(self):

        localctx = ExprParser.IfStmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 42, self.RULE_ifStmt)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 221
            self.match(ExprParser.IF)
            self.state = 222
            self.match(ExprParser.LPAREN)
            self.state = 223
            self.expr()
            self.state = 224
            self.match(ExprParser.RPAREN)
            self.state = 225
            self.block()
            self.state = 226
            self.elseIfStmt()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ElseIfStmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ELSE(self):
            return self.getToken(ExprParser.ELSE, 0)

        def IF(self):
            return self.getToken(ExprParser.IF, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def elseIfStmt(self):
            return self.getTypedRuleContext(ExprParser.ElseIfStmtContext,0)


        def else_(self):
            return self.getTypedRuleContext(ExprParser.ElseContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_elseIfStmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterElseIfStmt" ):
                listener.enterElseIfStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitElseIfStmt" ):
                listener.exitElseIfStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitElseIfStmt" ):
                return visitor.visitElseIfStmt(self)
            else:
                return visitor.visitChildren(self)




    def elseIfStmt(self):

        localctx = ExprParser.ElseIfStmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 44, self.RULE_elseIfStmt)
        try:
            self.state = 238
            self._errHandler.sync(self)
            la_ = self._interp.adaptivePredict(self._input,12,self._ctx)
            if la_ == 1:
                self.enterOuterAlt(localctx, 1)
                self.state = 228
                self.match(ExprParser.ELSE)
                self.state = 229
                self.match(ExprParser.IF)
                self.state = 230
                self.match(ExprParser.LPAREN)
                self.state = 231
                self.expr()
                self.state = 232
                self.match(ExprParser.RPAREN)
                self.state = 233
                self.block()
                self.state = 234
                self.elseIfStmt()
                pass

            elif la_ == 2:
                self.enterOuterAlt(localctx, 2)
                self.state = 236
                self.else_()
                pass

            elif la_ == 3:
                self.enterOuterAlt(localctx, 3)

                pass


        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ElseContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ELSE(self):
            return self.getToken(ExprParser.ELSE, 0)

        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_else

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterElse" ):
                listener.enterElse(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitElse" ):
                listener.exitElse(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitElse" ):
                return visitor.visitElse(self)
            else:
                return visitor.visitChildren(self)




    def else_(self):

        localctx = ExprParser.ElseContext(self, self._ctx, self.state)
        self.enterRule(localctx, 46, self.RULE_else)
        try:
            self.state = 243
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [29]:
                self.enterOuterAlt(localctx, 1)
                self.state = 240
                self.match(ExprParser.ELSE)
                self.state = 241
                self.block()
                pass
            elif token in [-1, 16, 23, 24, 25, 26, 28, 30, 35, 37, 44]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LoopContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def REPEAT(self):
            return self.getToken(ExprParser.REPEAT, 0)

        def loops(self):
            return self.getTypedRuleContext(ExprParser.LoopsContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_loop

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLoop" ):
                listener.enterLoop(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLoop" ):
                listener.exitLoop(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitLoop" ):
                return visitor.visitLoop(self)
            else:
                return visitor.visitChildren(self)




    def loop(self):

        localctx = ExprParser.LoopContext(self, self._ctx, self.state)
        self.enterRule(localctx, 48, self.RULE_loop)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 245
            self.match(ExprParser.REPEAT)
            self.state = 246
            self.loops()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LoopsContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def loopStmt(self):
            return self.getTypedRuleContext(ExprParser.LoopStmtContext,0)


        def whileStmt(self):
            return self.getTypedRuleContext(ExprParser.WhileStmtContext,0)


        def foreachStmt(self):
            return self.getTypedRuleContext(ExprParser.ForeachStmtContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_loops

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLoops" ):
                listener.enterLoops(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLoops" ):
                listener.exitLoops(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitLoops" ):
                return visitor.visitLoops(self)
            else:
                return visitor.visitChildren(self)




    def loops(self):

        localctx = ExprParser.LoopsContext(self, self._ctx, self.state)
        self.enterRule(localctx, 50, self.RULE_loops)
        try:
            self.state = 251
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [13]:
                self.enterOuterAlt(localctx, 1)
                self.state = 248
                self.loopStmt()
                pass
            elif token in [32]:
                self.enterOuterAlt(localctx, 2)
                self.state = 249
                self.whileStmt()
                pass
            elif token in [33]:
                self.enterOuterAlt(localctx, 3)
                self.state = 250
                self.foreachStmt()
                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class LoopStmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def TIMES(self):
            return self.getToken(ExprParser.TIMES, 0)

        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_loopStmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterLoopStmt" ):
                listener.enterLoopStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitLoopStmt" ):
                listener.exitLoopStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitLoopStmt" ):
                return visitor.visitLoopStmt(self)
            else:
                return visitor.visitChildren(self)




    def loopStmt(self):

        localctx = ExprParser.LoopStmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 52, self.RULE_loopStmt)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 253
            self.match(ExprParser.LPAREN)
            self.state = 254
            self.expr()
            self.state = 255
            self.match(ExprParser.RPAREN)
            self.state = 256
            self.match(ExprParser.TIMES)
            self.state = 257
            self.block()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class WhileStmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def WHILE(self):
            return self.getToken(ExprParser.WHILE, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_whileStmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterWhileStmt" ):
                listener.enterWhileStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitWhileStmt" ):
                listener.exitWhileStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitWhileStmt" ):
                return visitor.visitWhileStmt(self)
            else:
                return visitor.visitChildren(self)




    def whileStmt(self):

        localctx = ExprParser.WhileStmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 54, self.RULE_whileStmt)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 259
            self.match(ExprParser.WHILE)
            self.state = 260
            self.match(ExprParser.LPAREN)
            self.state = 261
            self.expr()
            self.state = 262
            self.match(ExprParser.RPAREN)
            self.state = 263
            self.block()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ForeachStmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def FOREACH(self):
            return self.getToken(ExprParser.FOREACH, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def type_(self):
            return self.getTypedRuleContext(ExprParser.TypeContext,0)


        def ID(self, i:int=None):
            if i is None:
                return self.getTokens(ExprParser.ID)
            else:
                return self.getToken(ExprParser.ID, i)

        def IN(self):
            return self.getToken(ExprParser.IN, 0)

        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_foreachStmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterForeachStmt" ):
                listener.enterForeachStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitForeachStmt" ):
                listener.exitForeachStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitForeachStmt" ):
                return visitor.visitForeachStmt(self)
            else:
                return visitor.visitChildren(self)




    def foreachStmt(self):

        localctx = ExprParser.ForeachStmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 56, self.RULE_foreachStmt)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 265
            self.match(ExprParser.FOREACH)
            self.state = 266
            self.match(ExprParser.LPAREN)
            self.state = 267
            self.type_()
            self.state = 268
            self.match(ExprParser.ID)
            self.state = 269
            self.match(ExprParser.IN)
            self.state = 270
            self.match(ExprParser.ID)
            self.state = 271
            self.match(ExprParser.RPAREN)
            self.state = 272
            self.block()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ListStmtContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def COLON(self):
            return self.getToken(ExprParser.COLON, 0)

        def listOpr(self):
            return self.getTypedRuleContext(ExprParser.ListOprContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_listStmt

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterListStmt" ):
                listener.enterListStmt(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitListStmt" ):
                listener.exitListStmt(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitListStmt" ):
                return visitor.visitListStmt(self)
            else:
                return visitor.visitChildren(self)




    def listStmt(self):

        localctx = ExprParser.ListStmtContext(self, self._ctx, self.state)
        self.enterRule(localctx, 58, self.RULE_listStmt)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 274
            self.match(ExprParser.ID)
            self.state = 275
            self.match(ExprParser.COLON)
            self.state = 276
            self.listOpr()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ListOprContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def LISTADD(self):
            return self.getToken(ExprParser.LISTADD, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def expr(self):
            return self.getTypedRuleContext(ExprParser.ExprContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def LISTDEL(self):
            return self.getToken(ExprParser.LISTDEL, 0)

        def LISTIDXOF(self):
            return self.getToken(ExprParser.LISTIDXOF, 0)

        def LISTVALOF(self):
            return self.getToken(ExprParser.LISTVALOF, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_listOpr

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterListOpr" ):
                listener.enterListOpr(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitListOpr" ):
                listener.exitListOpr(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitListOpr" ):
                return visitor.visitListOpr(self)
            else:
                return visitor.visitChildren(self)




    def listOpr(self):

        localctx = ExprParser.ListOprContext(self, self._ctx, self.state)
        self.enterRule(localctx, 60, self.RULE_listOpr)
        try:
            self.state = 298
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [38]:
                self.enterOuterAlt(localctx, 1)
                self.state = 278
                self.match(ExprParser.LISTADD)
                self.state = 279
                self.match(ExprParser.LPAREN)
                self.state = 280
                self.expr()
                self.state = 281
                self.match(ExprParser.RPAREN)
                pass
            elif token in [40]:
                self.enterOuterAlt(localctx, 2)
                self.state = 283
                self.match(ExprParser.LISTDEL)
                self.state = 284
                self.match(ExprParser.LPAREN)
                self.state = 285
                self.expr()
                self.state = 286
                self.match(ExprParser.RPAREN)
                pass
            elif token in [39]:
                self.enterOuterAlt(localctx, 3)
                self.state = 288
                self.match(ExprParser.LISTIDXOF)
                self.state = 289
                self.match(ExprParser.LPAREN)
                self.state = 290
                self.expr()
                self.state = 291
                self.match(ExprParser.RPAREN)
                pass
            elif token in [41]:
                self.enterOuterAlt(localctx, 4)
                self.state = 293
                self.match(ExprParser.LISTVALOF)
                self.state = 294
                self.match(ExprParser.LPAREN)
                self.state = 295
                self.expr()
                self.state = 296
                self.match(ExprParser.RPAREN)
                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FuncCallContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def CALL(self):
            return self.getToken(ExprParser.CALL, 0)

        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def argList(self):
            return self.getTypedRuleContext(ExprParser.ArgListContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_funcCall

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFuncCall" ):
                listener.enterFuncCall(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFuncCall" ):
                listener.exitFuncCall(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitFuncCall" ):
                return visitor.visitFuncCall(self)
            else:
                return visitor.visitChildren(self)




    def funcCall(self):

        localctx = ExprParser.FuncCallContext(self, self._ctx, self.state)
        self.enterRule(localctx, 62, self.RULE_funcCall)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 300
            self.match(ExprParser.CALL)
            self.state = 301
            self.match(ExprParser.ID)
            self.state = 302
            self.match(ExprParser.LPAREN)
            self.state = 303
            self.argList()
            self.state = 304
            self.match(ExprParser.RPAREN)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FuncDefContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def FUNCTION(self):
            return self.getToken(ExprParser.FUNCTION, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def paramList(self):
            return self.getTypedRuleContext(ExprParser.ParamListContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def funcReturn(self):
            return self.getTypedRuleContext(ExprParser.FuncReturnContext,0)


        def block(self):
            return self.getTypedRuleContext(ExprParser.BlockContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_funcDef

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFuncDef" ):
                listener.enterFuncDef(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFuncDef" ):
                listener.exitFuncDef(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitFuncDef" ):
                return visitor.visitFuncDef(self)
            else:
                return visitor.visitChildren(self)




    def funcDef(self):

        localctx = ExprParser.FuncDefContext(self, self._ctx, self.state)
        self.enterRule(localctx, 64, self.RULE_funcDef)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 306
            self.match(ExprParser.FUNCTION)
            self.state = 307
            self.match(ExprParser.LPAREN)
            self.state = 308
            self.paramList()
            self.state = 309
            self.match(ExprParser.RPAREN)
            self.state = 310
            self.funcReturn()
            self.state = 311
            self.block()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class FuncReturnContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def RETURN(self):
            return self.getToken(ExprParser.RETURN, 0)

        def type_(self):
            return self.getTypedRuleContext(ExprParser.TypeContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_funcReturn

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterFuncReturn" ):
                listener.enterFuncReturn(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitFuncReturn" ):
                listener.exitFuncReturn(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitFuncReturn" ):
                return visitor.visitFuncReturn(self)
            else:
                return visitor.visitChildren(self)




    def funcReturn(self):

        localctx = ExprParser.FuncReturnContext(self, self._ctx, self.state)
        self.enterRule(localctx, 66, self.RULE_funcReturn)
        try:
            self.state = 316
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [36]:
                self.enterOuterAlt(localctx, 1)
                self.state = 313
                self.match(ExprParser.RETURN)
                self.state = 314
                self.type_()
                pass
            elif token in [15]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ParamListContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def param(self):
            return self.getTypedRuleContext(ExprParser.ParamContext,0)


        def paramTail(self):
            return self.getTypedRuleContext(ExprParser.ParamTailContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_paramList

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterParamList" ):
                listener.enterParamList(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitParamList" ):
                listener.exitParamList(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitParamList" ):
                return visitor.visitParamList(self)
            else:
                return visitor.visitChildren(self)




    def paramList(self):

        localctx = ExprParser.ParamListContext(self, self._ctx, self.state)
        self.enterRule(localctx, 68, self.RULE_paramList)
        try:
            self.state = 322
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [23, 24, 25, 26]:
                self.enterOuterAlt(localctx, 1)
                self.state = 318
                self.param()
                self.state = 319
                self.paramTail()
                pass
            elif token in [14]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ParamTailContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def COMMA(self):
            return self.getToken(ExprParser.COMMA, 0)

        def param(self):
            return self.getTypedRuleContext(ExprParser.ParamContext,0)


        def paramTail(self):
            return self.getTypedRuleContext(ExprParser.ParamTailContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_paramTail

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterParamTail" ):
                listener.enterParamTail(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitParamTail" ):
                listener.exitParamTail(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitParamTail" ):
                return visitor.visitParamTail(self)
            else:
                return visitor.visitChildren(self)




    def paramTail(self):

        localctx = ExprParser.ParamTailContext(self, self._ctx, self.state)
        self.enterRule(localctx, 70, self.RULE_paramTail)
        try:
            self.state = 329
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [10]:
                self.enterOuterAlt(localctx, 1)
                self.state = 324
                self.match(ExprParser.COMMA)
                self.state = 325
                self.param()
                self.state = 326
                self.paramTail()
                pass
            elif token in [14]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ParamContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def type_(self):
            return self.getTypedRuleContext(ExprParser.TypeContext,0)


        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_param

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterParam" ):
                listener.enterParam(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitParam" ):
                listener.exitParam(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitParam" ):
                return visitor.visitParam(self)
            else:
                return visitor.visitChildren(self)




    def param(self):

        localctx = ExprParser.ParamContext(self, self._ctx, self.state)
        self.enterRule(localctx, 72, self.RULE_param)
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 331
            self.type_()
            self.state = 332
            self.match(ExprParser.ID)
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ArgListContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def argTail(self):
            return self.getTypedRuleContext(ExprParser.ArgTailContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_argList

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterArgList" ):
                listener.enterArgList(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitArgList" ):
                listener.exitArgList(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitArgList" ):
                return visitor.visitArgList(self)
            else:
                return visitor.visitChildren(self)




    def argList(self):

        localctx = ExprParser.ArgListContext(self, self._ctx, self.state)
        self.enterRule(localctx, 74, self.RULE_argList)
        try:
            self.state = 337
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [44]:
                self.enterOuterAlt(localctx, 1)
                self.state = 334
                self.match(ExprParser.ID)
                self.state = 335
                self.argTail()
                pass
            elif token in [14]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class ArgTailContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def COMMA(self):
            return self.getToken(ExprParser.COMMA, 0)

        def ID(self):
            return self.getToken(ExprParser.ID, 0)

        def argTail(self):
            return self.getTypedRuleContext(ExprParser.ArgTailContext,0)


        def getRuleIndex(self):
            return ExprParser.RULE_argTail

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterArgTail" ):
                listener.enterArgTail(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitArgTail" ):
                listener.exitArgTail(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitArgTail" ):
                return visitor.visitArgTail(self)
            else:
                return visitor.visitChildren(self)




    def argTail(self):

        localctx = ExprParser.ArgTailContext(self, self._ctx, self.state)
        self.enterRule(localctx, 76, self.RULE_argTail)
        try:
            self.state = 343
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [10]:
                self.enterOuterAlt(localctx, 1)
                self.state = 339
                self.match(ExprParser.COMMA)
                self.state = 340
                self.match(ExprParser.ID)
                self.state = 341
                self.argTail()
                pass
            elif token in [14]:
                self.enterOuterAlt(localctx, 2)

                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class BooleanContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def TRUE(self):
            return self.getToken(ExprParser.TRUE, 0)

        def FALSE(self):
            return self.getToken(ExprParser.FALSE, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_boolean

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterBoolean" ):
                listener.enterBoolean(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitBoolean" ):
                listener.exitBoolean(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitBoolean" ):
                return visitor.visitBoolean(self)
            else:
                return visitor.visitChildren(self)




    def boolean(self):

        localctx = ExprParser.BooleanContext(self, self._ctx, self.state)
        self.enterRule(localctx, 78, self.RULE_boolean)
        self._la = 0 # Token type
        try:
            self.enterOuterAlt(localctx, 1)
            self.state = 345
            _la = self._input.LA(1)
            if not(_la==17 or _la==18):
                self._errHandler.recoverInline(self)
            else:
                self._errHandler.reportMatch(self)
                self.consume()
        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx


    class TypeContext(ParserRuleContext):
        __slots__ = 'parser'

        def __init__(self, parser, parent:ParserRuleContext=None, invokingState:int=-1):
            super().__init__(parent, invokingState)
            self.parser = parser

        def BOOL(self):
            return self.getToken(ExprParser.BOOL, 0)

        def TEXT(self):
            return self.getToken(ExprParser.TEXT, 0)

        def NUM(self):
            return self.getToken(ExprParser.NUM, 0)

        def LIST(self):
            return self.getToken(ExprParser.LIST, 0)

        def LPAREN(self):
            return self.getToken(ExprParser.LPAREN, 0)

        def type_(self):
            return self.getTypedRuleContext(ExprParser.TypeContext,0)


        def RPAREN(self):
            return self.getToken(ExprParser.RPAREN, 0)

        def getRuleIndex(self):
            return ExprParser.RULE_type

        def enterRule(self, listener:ParseTreeListener):
            if hasattr( listener, "enterType" ):
                listener.enterType(self)

        def exitRule(self, listener:ParseTreeListener):
            if hasattr( listener, "exitType" ):
                listener.exitType(self)

        def accept(self, visitor:ParseTreeVisitor):
            if hasattr( visitor, "visitType" ):
                return visitor.visitType(self)
            else:
                return visitor.visitChildren(self)




    def type_(self):

        localctx = ExprParser.TypeContext(self, self._ctx, self.state)
        self.enterRule(localctx, 80, self.RULE_type)
        try:
            self.state = 355
            self._errHandler.sync(self)
            token = self._input.LA(1)
            if token in [23]:
                self.enterOuterAlt(localctx, 1)
                self.state = 347
                self.match(ExprParser.BOOL)
                pass
            elif token in [24]:
                self.enterOuterAlt(localctx, 2)
                self.state = 348
                self.match(ExprParser.TEXT)
                pass
            elif token in [25]:
                self.enterOuterAlt(localctx, 3)
                self.state = 349
                self.match(ExprParser.NUM)
                pass
            elif token in [26]:
                self.enterOuterAlt(localctx, 4)
                self.state = 350
                self.match(ExprParser.LIST)
                self.state = 351
                self.match(ExprParser.LPAREN)
                self.state = 352
                self.type_()
                self.state = 353
                self.match(ExprParser.RPAREN)
                pass
            else:
                raise NoViableAltException(self)

        except RecognitionException as re:
            localctx.exception = re
            self._errHandler.reportError(self, re)
            self._errHandler.recover(self, re)
        finally:
            self.exitRule()
        return localctx





