// DELETE THIS CONTENT IF YOU PUT COMBINED GRAMMAR IN Parser TAB 

lexer grammar ExprLexer; 

OR: 'or'; 
AND: 'and'; 
EQUAL: 'is'; 
NOT: 'is not'; 
GREAT: '>'; 
LESS: '<'; 
GREATEQL: '>='; 
LESSEQL: '<='; 

ASSIGN : '=' ; 
COMMA : ',' ; 
SEMI : ';' ;
COLON: ':';
LPAREN : '(' ; 
RPAREN : ')' ; 
LCURLY : '{' ; 
RCURLY : '}' ; 
TRUE: 'true'; 
FALSE: 'false';

ADD: '+'; 
SUB: '-'; 
MUL: '*'; 
DIV: '/'; 
BOOL: 'boolean'; 
TEXT: 'text'; 
NUM: 'number';
LIST: 'list';
QUOTE: '"'; 
IF: 'if'; 
ELSE: 'else';
REPEAT: 'repeat';
TIMES: 'times';
WHILE: 'while';
FOREACH: 'for each';
IN: 'in';
FUNCTION: 'function';
RETURN: 'return';
CALL: 'call';

LISTADD: 'Add';
LISTIDXOF: 'IndexOf';
LISTDEL: 'Delete';
LISTVALOF: 'ValueOf';


STR: '"'[a-zA-Z_0-9]*'"'; 
INT: [0-9]+ ; 
ID: [a-zA-Z_][a-zA-Z_0-9]* ; 
WS: [ \t\n\r\f]+ -> skip ; 