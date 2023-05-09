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
DECIMAL: 'decimal';
NOTHING: 'nothing';
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
PRINT: 'output';
SCAN: 'input';
COMMENT: 'comment:';

LISTADD: 'Add';
LISTIDXOF: 'IndexOf';
LISTREPLACE: 'Replace';
LISTVALOF: 'ValueOf';

COMM: 'comment:'~(';')*;
STR: '"' (~'"')* '"';
DEC: [0-9]+'.'[0-9]+; 
INT: [0-9]+ ; 
ID: [a-zA-Z_][a-zA-Z_0-9]* ; 
WS: [ \t\n\r\f]+ -> skip ; 