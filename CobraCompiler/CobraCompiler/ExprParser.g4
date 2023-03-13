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
      funcCall SEMI;  
      
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
factor: LPAREN expr RPAREN | INT | STR | ID | boolean; 

block: LCURLY cmds RCURLY; 

ctrlStrct: ifStmt | loop;

ifStmt: IF LPAREN expr RPAREN block elseIfStmt; 
elseIfStmt: ELSE IF LPAREN expr RPAREN block elseIfStmt |  
            else | 
            /*epsilon*/; 
else: ELSE block | /*epsilon*/;  

loop: REPEAT loops;
loops: loopStmt | whileStmt | foreachStmt;

loopStmt: LPAREN expr RPAREN TIMES block;
whileStmt: WHILE LPAREN expr RPAREN block;
foreachStmt: FOREACH LPAREN type ID IN ID RPAREN block;

listStmt: ID COLON listOpr;
listOpr: LISTADD LPAREN expr RPAREN | 
         LISTDEL LPAREN expr RPAREN | 
         LISTIDXOF LPAREN expr RPAREN | 
         LISTVALOF LPAREN expr RPAREN;

funcCall: CALL ID LPAREN argList RPAREN;
funcDef: FUNCTION LPAREN paramList RPAREN funcReturn block;
funcReturn: RETURN type | /*epsilon*/;

paramList: param paramTail |
           /*epsilon*/;
paramTail: COMMA param paramTail | /*epsilon*/;
param: type ID;

argList: ID argTail |
           /*epsilon*/;
argTail: COMMA ID argTail | /*epsilon*/;

boolean: TRUE | FALSE; 
type: BOOL | TEXT | NUM | LIST LPAREN type RPAREN;

