﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using Antlr4.Runtime.Atn;
using static ASTNodes;
using static ASTNodes.ASTNode;
using static CobraCompiler.Symbol;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CobraCompiler
{

    public class Symbol //Entry in a scope
    {
        public string Name { get; set; } //ID (name) of the variable
        public TypeEnum Type { get; set; } //Type of the variable
        public ASTNode Reference { get; set; } //Reference to the declared node
    }

    public class Scope //Scope in the symbol table
    {
        public Scope() 
        {
            Symbols = new Dictionary<string, Symbol>();
        }
        public Dictionary<string, Symbol> Symbols { get; set; } //Key = ID (name), Value = Symbol
        public Scope Parent { get; set; } //Outter Scope (parent scope)
        public BlockNode Block { get; set; }
    }

    public class SymbolTable : ASTVisitor<ASTNode?>
    {
        public Dictionary<BlockNode, Scope> _scopes; //Key = BlockNode belonging to the Scope, Value = Scope
        public Stack<Scope> _stackScopes; //Stack of scopes for building _scopes
        private ErrorHandler symbolErrorhandler;
        public BlockNode _currentBlock;

        List<string> _reservedKeywords = new List<string>()
            {
                "auto", "break", "case", "char", "const", "continue", "default", "do", "double",
                "else", "enum", "extern", "float", "for", "goto", "if", "int", "long", "register",
                "return", "short", "signed", "sizeof", "static", "struct", "switch", "typedef",
                "union", "unsigned", "void", "volatile", "while", "concat", "AddToList", "ReplaceInList", 
                "IndexOfList", "ValueOfList", "input", "print", "printf", "scanf", "strcmp", "strlen", 
                "malloc", "calloc", "realloc", "free", "abs", "abort", "exit", "system", "memchr", 
                "memcmp", "memcpy", "memmove", "memset", "strcat", "strncat", "strcmp", "strcpy", "strlen", 
                "strcoll", "strerror"
            };
        public SymbolTable(ErrorHandler errorHandler)
        {
            symbolErrorhandler = errorHandler;
            _scopes = new Dictionary<BlockNode, Scope>();
            _stackScopes = new Stack<Scope>();
        }
        public SymbolTable BuildSymbolTable(ASTNode astRoot)
        {
            Visit((ProgramNode)astRoot);
            return this;
        }

        //Add a new scope on the stack and add the scope to _scopes
        //Also update the currentBlock
        public void NewScope(BlockNode blockNode)
        {
            var scope = new Scope();
            scope.Block = blockNode;

            if (_stackScopes.Count > 0)
                scope.Parent= _stackScopes.Peek();

            _scopes.Add(blockNode, scope);
            _stackScopes.Push(scope);
            _currentBlock = _stackScopes.Peek().Block;
        }

        //Pop the stack of scopes
        public void ExitScope()
        {
            _stackScopes.Pop();
            if (_currentBlock is not ProgramNode)
                _currentBlock = _stackScopes.Peek().Block;
        }

        //Insert ID (name) and Type for a variable into the
        //scope at the top of the stack
        public void Insert(string name, TypeEnum type, ASTNode node)
        {
            if (_stackScopes.Peek().Symbols.ContainsKey(name))
            {
                SymbolError(node, $"The variable '{name}' is defined twice within the same scope.");
                return;
            }

            _stackScopes.Peek().Symbols.Add(name, new Symbol 
            { 
                Name = name, Type = type, Reference = node
            });
        }

        //Given a ID (name) and a BlockNode, check outwards from
        //the scope belonging to the blockNode until the variable is found
        public Symbol? Lookup(string name, BlockNode blockNode)
        {
            var scope = _scopes[blockNode];

            while (scope != null)
            {
                foreach (var symbol in scope.Symbols.Values)
                {
                    if (symbol.Name == name)
                    {
                        return symbol;
                    }
                }
                
                scope = scope.Parent;
            }
            return null;
        }

        //Function for adding used variables to a functionBlock (used for the emitter)
        public void AddIDToFunctionBlock(Symbol symbol, BlockNode blockNode)
        {
            //Only add the ID if the ID is contained in a functionBlock
            //and has not already been declared within this functionBlock

            FunctionBlockNode? fBlockNode = null;

            var scope = _scopes[blockNode];

            while (scope != null)
            {
                //Has functionBlock:
                if (scope.Block is FunctionBlockNode)
                {
                    fBlockNode = scope.Block as FunctionBlockNode;
                }

                //If the name is declared within the current scope, we don't add it
                if (scope.Symbols.ContainsKey(symbol.Name))
                    break;

                //If has functionBlock and has not been declared yet in any scopes,
                //The ID must've been an identifier from outside the function
                if (fBlockNode != null)
                {

                    if (!fBlockNode.UsedVariables.Keys.Contains(symbol.Name))
                        fBlockNode.UsedVariables.Add(symbol.Name, symbol.Type);

                    //Exit because we've now met a functionBlockNode
                    break;
                }

                scope = scope.Parent;
            }
        }

        public override ASTNode? Visit(ProgramNode node)
        {
            NewScope(node);

            if (node.Commands == null)
            {
                ExitScope();
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                }
            }

            ExitScope();
            
            return null;
        }

        public override ASTNode? Visit(BlockNode node)
        {
            NewScope(node);

            if (node.Commands == null)
            {
                ExitScope();
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                }
            }

            ExitScope();
            return null;
        }

        public override ASTNode? Visit(DeclarationNode node)
        {
            if (node.Identifier.Name.Contains("___________________________________"))
                SymbolError(node, "Invalid variable name");  

            if (_reservedKeywords.Contains(node.Identifier.Name))
                node.Identifier.Name = $"{node.Identifier.Name}___________________________________";

            //var sym = Lookup(node.Identifier.Name, _currentBlock);

            //string underscores = "";
            //while (sym != null)
            //{
            //    underscores += "_";
            //    sym = Lookup($"{node.Identifier.Name}{underscores}", _currentBlock);
            //}

            //node.Identifier.Name += underscores;
            Insert(node.Identifier.Name, node.Identifier.TypeNode.Type, node);
            Visit(node.Expression);
            return null;
        }

        public override ASTNode? Visit(StatementNode node)
        {
            switch (node)
            {
                case IfNode ifNode:
                    Visit(ifNode);
                    break;
                case RepeatNode repeatNode:
                    Visit(repeatNode);
                    break;
                case WhileNode whileNode:
                    Visit(whileNode);
                    break;
                case ListOprStatementNode listOprStatementNode:
                    Visit(listOprStatementNode);
                    break;
                case ForeachNode foreachNode:
                    Visit(foreachNode);
                    break;
                case FunctionDeclarationNode functionDeclarationNode: 
                    Visit(functionDeclarationNode);
                    break;
                case CommentNode commentNode:
                    Visit(commentNode);
                    break;
                case InputStmtNode inputStmtNode:
                    Visit(inputStmtNode);
                    break;
                case OutputStmtNode outputNode:
                    Visit(outputNode);
                    break;
                case FunctionCallStmtNode functionCallStmtNode:
                    Visit(functionCallStmtNode);
                    break;
                case ReturnNode returnNode:
                    Visit(returnNode);
                    break;
            }
            return null;
        }

        public override ASTNode? Visit(AssignNode node)
        {
            Visit(node.Identifier);
            Visit(node.Expression);
            return null;
        }

        public override ASTNode? Visit(ExpressionNode node)
        {
            //Visits based on the type of ExpressionNode
            switch (node)
            {
                case InfixExpressionNode infixExpressionNode:
                    Visit(infixExpressionNode);
                    break;
                case IdentifierNode identifierNode:
                    Visit(identifierNode);
                    break;
                case ListOprExpressionNode listOprExpressionNode:
                    Visit(listOprExpressionNode);
                    break;
                case InputExprNode inputExprNode:
                    Visit(inputExprNode);
                    break;
                case OutputExprNode outputExprNode:
                    Visit(outputExprNode);
                    break;
                case FunctionCallExprNode functionCallExprNode:
                    Visit(functionCallExprNode);
                    break;
            }

            return null;
        }

        public override ASTNode? Visit(InfixExpressionNode node)
        {
            //Visits based on the type of InfixExpressionNode
            switch (node)
            {
                case AdditionNode additionNode:
                    Visit(additionNode);
                    break;
                case SubtractionNode subtractionNode:
                    Visit(subtractionNode);
                    break;
                case MultiplicationNode multiplicationNode:
                    Visit(multiplicationNode);
                    break;
                case DivisionNode divideNode:
                    Visit(divideNode);
                    break;
                case AndNode andNode:
                    Visit(andNode);
                    break;
                case OrNode orNode:
                    Visit(orNode);
                    break;
                case EqualNode equalNode:
                    Visit(equalNode);
                    break;
                case NotEqualNode notEqualNode:
                    Visit(notEqualNode);
                    break;
                case GreaterNode greaterNode:
                    Visit(greaterNode);
                    break;
                case LessNode lessNode:
                    Visit(lessNode);
                    break;
                case GreaterEqualNode greaterEqualNode:
                    Visit(greaterEqualNode);
                    break;
                case LessEqualNode lessEqualNode:
                    Visit(lessEqualNode);
                    break;
            }

            return null;
        }

        public override ASTNode? Visit(IfNode node)
        {
            //Visit Condition & Block
            //If any - visit ElseIfs and/Or Else
            Visit(node.Condition);
            Visit(node.Block);

            foreach (var @else in node.ElseIfs)
            {
                switch (@else)
                {
                    case ElseIfNode elseIf:
                        Visit(elseIf);
                        break;
                    case ElseNode:
                        Visit(@else);
                        break;
                }

            }
            return null;
        }

        public override ASTNode? Visit(ElseIfNode node)
        {
            //Visit Condition & Block
            Visit(node.Condition);
            Visit(node.Block);
            return null;
        }

        public override ASTNode? Visit(ElseNode node)
        {
            Visit(node.Block);
            return null;
        }

        public override ASTNode? Visit(RepeatNode node)
        {
            Visit(node.Expression);
            Visit(node.Block);
            return null;
        }

        public override ASTNode? Visit(WhileNode node)
        {
            Visit(node.Condition);
            Visit(node.Block);
            return null;
        }

        public override ASTNode? Visit(ForeachNode node)
        {
            var sym = Lookup(node.List.Name, _currentBlock);

            if (sym != null)
                AddIDToFunctionBlock(sym, _currentBlock);

            Visit(node.Block);
            return null;
        }

        public override ASTNode? Visit(ListOprStatementNode node)
        {
            //Visits based on type of ListOprStatementNode
            switch (node)
            {
                case ListAddNode listAddNode:
                    Visit(listAddNode);
                    break;
                case ListReplaceNode listDeleteNode:
                    Visit(listDeleteNode);
                    break;
            }
            return null;
        }

        public override ASTNode? Visit(ListOprExpressionNode node)
        {
            //Visits based on type of ListOprExpressionNode
            switch (node)
            {
                case ListValueOfNode listValueOfNode:
                    Visit(listValueOfNode);
                    break;
                case ListIndexOfNode listIndexOfNode:
                    Visit(listIndexOfNode);
                    break;
            }
            return null;
        }

        public override ASTNode? Visit(ListAddNode node)
        {
            Visit(node.Identifier);
            Visit(node.Arguments);
            return null;
        }

        public override ASTNode? Visit(ListReplaceNode node)
        {
            Visit(node.Identifier);
            Visit(node.Arguments);
            return null;
        }

        public override ASTNode? Visit(ListIndexOfNode node)
        {
            Visit(node.Identifier);
            Visit(node.Arguments);
            return null;
        }

        public override ASTNode? Visit(ListValueOfNode node)
        {
            Visit(node.Identifier);
            Visit(node.Arguments);
            return null;
        }

        public override ASTNode? Visit(AdditionNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(SubtractionNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(MultiplicationNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(DivisionNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(AndNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(OrNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(GreaterNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(LessNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(GreaterEqualNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(LessEqualNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(EqualNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode? Visit(NotEqualNode node)
        {
            Visit(node.Left);
            Visit(node.Right);
            return null;
        }

        public override ASTNode Visit(CommentNode node)
        {
            return null;
        }

        public override ASTNode Visit(FunctionCallExprNode node)
        {
            if (node.Name.Contains("___________________________________"))
                SymbolError(node, "Invalid function name");

            if (_reservedKeywords.Contains(node.Name))
                node.Name = $"{node.Name}___________________________________";

            var sym = Lookup(node.Name, _currentBlock);

            if (sym == null)
            {
                SymbolError(node, $"{node.Name} is not found. Declare your function before calling. Recursive functions are not supported");
                return null;
            }

            var declaration = (FunctionDeclarationNode)sym.Reference;

            foreach (var expr in node.Arguments.Expressions)
            {
                if (expr is IdentifierNode)
                {
                    var identifier = (IdentifierNode)expr;
                    if (declaration.Block.UsedVariables.Keys.Contains(identifier.Name))
                        declaration.Block.UsedVariables.Remove(identifier.Name);
                }

                Visit(expr);
            }

            return null;
        }

        public override ASTNode Visit(FunctionDeclarationNode node)
        {
            if (node.Name.Contains("___________________________________"))
                SymbolError(node, "Invalid function name");

            if (_reservedKeywords.Contains(node.Name))
                node.Name = $"{node.Name}___________________________________";

            if (_currentBlock is not ProgramNode)
            {
                SymbolError(node, $"The function '{node.Name}' is declared within a scope");
            }

            Insert(node.Name, node.ReturnType, node);

            Visit(node.Block);

            return null;
        }

        public override ASTNode Visit(InputExprNode node)
        {
            foreach (var expr in node.Arguments.Expressions)
                Visit(expr);

            return null;
        }

        public override ASTNode Visit(OutputExprNode node)
        {
            foreach (var expr in node.Arguments.Expressions)
                Visit(expr);

            return null;
        }

        public override ASTNode? Visit(FunctionCallStmtNode node)
        {
            if (node.Name.Contains("___________________________________"))
                SymbolError(node, "Invalid function name");

            if (_reservedKeywords.Contains(node.Name))
                node.Name = $"{node.Name}___________________________________";

            var sym = Lookup(node.Name, _currentBlock);

            if (sym == null)
            {
                SymbolError(node, $"{node.Name} is not found. Declare your function before calling. Recursive functions are not supported");
                return null;
            }

            var declaration = (FunctionDeclarationNode)sym.Reference;

            if (_currentBlock is FunctionBlockNode)
            {
                var functionBlock = (FunctionBlockNode)_currentBlock;
                foreach (var usedVariable in declaration.Block.UsedVariables)
                {
                    functionBlock.UsedVariables.Add(usedVariable.Key, usedVariable.Value);
                }
            }

            foreach (var expr in node.Arguments.Expressions)
            {
                if (expr is IdentifierNode)
                {
                    var identifier = (IdentifierNode)expr;
                    if (declaration.Block.UsedVariables.Keys.Contains(identifier.Name))
                        declaration.Block.UsedVariables.Remove(identifier.Name);
                }

                Visit(expr);
            }
            return null;
        }
        
        public override ASTNode? Visit(InputStmtNode node)
        {
            foreach (var expr in node.Arguments.Expressions)
                Visit(expr);
            return null;
        }
        
        public override ASTNode? Visit(OutputStmtNode node)
        {
            foreach (var expr in node.Arguments.Expressions)
                Visit(expr);

            return null;
        }

        public override ASTNode? Visit(ReturnNode node)
        {
            return null;
        }

        public override ASTNode? Visit(FunctionBlockNode node)
        {
            NewScope(node);

            foreach (var decl in node.Parameters.Declarations)
                Visit(decl);

            if (node.Commands == null)
            {
                ExitScope();
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                }
            }

            Visit(node.ReturnExpression);

            ExitScope();
            return null;
        }

        public override ASTNode? Visit(ForeachBlockNode node)
        {
            NewScope(node);

            Visit(node.LocalVariable);

            if (node.Commands == null)
            {
                ExitScope();
                return null;
            }

            foreach (var cmd in node.Commands)
            {
                switch (cmd)
                {
                    case DeclarationNode declarationNode:
                        Visit(declarationNode);
                        break;
                    case AssignNode assignNode:
                        Visit(assignNode);
                        break;
                    case StatementNode statementNode:
                        Visit(statementNode);
                        break;
                }
            }

            ExitScope();
            return null;
        }
        public void Visit(IdentifierNode node)
        {
            if (node.Name.Contains("___________________________________"))
                SymbolError(node, "Invalid variable name");

            if (_reservedKeywords.Contains(node.Name))
                node.Name = $"{node.Name}___________________________________";

            var sym = Lookup(node.Name, _currentBlock);

            if (sym == null)
            {
                SymbolError(node, $"{node.Name} is not found. Declare your variable before use.");
            }
            else
            {
                AddIDToFunctionBlock(sym, _currentBlock);
            }

            //var prevSym = Lookup(node.Name, _currentBlock);
            //var currSym = Lookup($"{node.Name}_", _currentBlock);

            //string underscores = "_";
            //while (currSym != null)
            //{
            //    prevSym = currSym;
            //    currSym = Lookup($"{node.Name}{underscores}", _currentBlock);
            //    underscores += "_";
            //}

            //if (prevSym == null)
            //{
            //    SymbolError(node, $"{node.Name} is not found. Declare your variable before use.");
            //}

            //if (prevSym != null)
            //{
            //    node.Name = prevSym.Name;
            //    AddIDToFunctionBlock(prevSym, _currentBlock);
            //}

        }

        public void SymbolError(ASTNode node, string error)
        {
            symbolErrorhandler.SymbolErrorMessages.Add($"Error line {node.Line}: {error}");
        }

    }
}

