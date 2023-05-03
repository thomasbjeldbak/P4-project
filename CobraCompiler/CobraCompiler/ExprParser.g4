parser grammar ExprParser; 
options { tokenVocab=ExprLexer; } 

program: cmds; 

cmds: cmd cmds | /*epsilon*/; 
cmd: stmt | dcl; 

dcl: type ID ass SEMI; 
ass: ASSIGN expr | /*epsilon*/;  
stmt: ID ASSIGN expr SEMI | 
      ctrlStrct | 
      listStmt SEMI |
      funcDef |
      funcCall SEMI |
	commentStmt SEMI |
	RETURN type SEMI;  
      
expr: logicOr oprOr; 

oprOr: OR logicOr oprOr | /*epsilon*/; 
logicOr: logicAnd oprAnd; 

oprAnd: AND logicAnd oprAnd | /*epsilon*/; 
logicAnd: equal oprEql; 

oprEql: EQUAL equal oprEql | NOT equal oprEql | /*epsilon*/; 
equal: bool oprBool; 

oprBool: GREAT bool oprBool | LESS bool oprBool |  
         GREATEQL bool oprBool| LESSEQL bool oprBool | 
         /*epsilon*/; 
bool: term oprExpr; 

oprExpr: ADD term oprExpr | SUB term oprExpr | /*epsilon*/; 
term: factor oprTerm;

oprTerm: MUL factor oprTerm | DIV factor oprTerm | /*epsilon*/; 
factor: LPAREN expr RPAREN | funcCall | listOprExpr | INT | DEC | STR | ID | boolean; 

block: LCURLY cmds RCURLY; 

commentStmt: COMM;

ctrlStrct: ifStmt | loop;

ifStmt: IF LPAREN expr RPAREN block elseIfStmt; 
elseIfStmt: ELSE IF LPAREN expr RPAREN block elseIfStmt |  
            else | 
            /*epsilon*/; 
else: ELSE block;  

loop: REPEAT loops;
loops: loopStmt | whileStmt | foreachStmt;

loopStmt: LPAREN expr RPAREN TIMES block;
whileStmt: WHILE LPAREN expr RPAREN block;
foreachStmt: FOREACH LPAREN type ID IN ID RPAREN block;

listStmt: listOpr | listOprExpr;
listOpr: ID COLON LISTADD LPAREN argList RPAREN | 
         ID COLON LISTDEL LPAREN argList RPAREN;

listOprExpr: ID COLON LISTIDXOF LPAREN argList RPAREN | 
             ID COLON LISTVALOF LPAREN argList RPAREN;

funcCall: CALL ID LPAREN argList RPAREN | 
	    CALL PRINT LPAREN argList RPAREN | 
	    CALL type SCAN LPAREN argList RPAREN;
funcDef: FUNCTION ID LPAREN paramList RPAREN funcReturn block;
funcReturn: RETURN funcReturnType;
funcReturnType: type | NOTHING;

paramList: param paramTail |
           /*epsilon*/;
paramTail: COMMA param paramTail | /*epsilon*/;
param: type ID;

argList: expr argTail |
           /*epsilon*/;
argTail: COMMA expr argTail | /*epsilon*/;

boolean: TRUE | FALSE; 
type: BOOL | TEXT | NUM | LIST LPAREN type RPAREN | DECIMAL;

