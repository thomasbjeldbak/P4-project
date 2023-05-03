using System;
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

namespace CobraCompiler
{

    public class Symbol //Entry in a scope
    {
        public string Name { get; set; } //ID (name) of the variable
        public TypeEnum Type { get; set; } //Type of the variable
    }

    public class Scope //Scope in the symbol table
    {
        public Scope() 
        {
            Symbols = new Dictionary<string, Symbol>();
        }
        public Dictionary<string, Symbol> Symbols { get; set; } //Key = ID (name), Value = Symbol
        public Scope Parent { get; set; } //Outter Scope (parent scope)
    }

    internal class SymbolTable : ASTVisitor<ASTNode?>
    {
        private Dictionary<BlockNode, Scope> _scopes; //Key = BlockNode belonging to the Scope, Value = Scope
        private Stack<Scope> _stackScopes; //Stack of scopes for building _scopes
        private BlockNode _currentBlock; //The current Block (used for look-up)
        private ErrorHandler symbolErrorhandler;

        public SymbolTable(ErrorHandler errorHandler)
        {
            symbolErrorhandler = errorHandler;
        }
        public SymbolTable BuildSymbolTable(ASTNode astRoot)
        {
            _scopes = new Dictionary<BlockNode, Scope>();
            _stackScopes = new Stack<Scope>();
            Visit((ProgramNode)astRoot);
            return this;
        }

        //Add a new scope on the stack and add the scope to _scopes
        //Also update the currentBlock
        private void NewScope(BlockNode blockNode)
        {
            var scope = new Scope();

            if (_stackScopes.Count > 0)
                scope.Parent= _stackScopes.Peek();

            _scopes.Add(blockNode, scope);
            _stackScopes.Push(scope);
            _currentBlock = blockNode;
        }

        //Pop the stack of scopes
        private void ExitScope()
        {
            _stackScopes.Pop();
        }

        //Insert ID (name) and Type for a variable into the
        //scope at the top of the stack
        private void Insert(IdentifierNode node)
        {
            if (_stackScopes.Peek().Symbols.ContainsKey(node.Name))
            {
                SymbolError(node, $"The variable '{node.Name}' is defined twice within the same scope.");
                return;
            }

            _stackScopes.Peek().Symbols.Add(node.Name, new Symbol 
            { 
                Name = node.Name, Type = node.TypeNode.Type
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

        public override ASTNode? Visit(ProgramNode node)
        {
            NewScope(node);

            if (node.Commands == null)
                return null;

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
                return null;

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
            Insert(node.Identifier);
            Visit(node.Identifier);
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
                case ListOperationNode listOperationNode:
                    Visit(listOperationNode);
                    break;
                case ForeachNode foreachNode:
                    Visit(foreachNode);
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
            Visit(node.LocalVariable);
            Visit(node.List);
            Visit(node.Block);
            return null;
        }

        public override ASTNode? Visit(ListOperationNode node)
        {
            //Visits based on type of ListOperationNode
            switch (node)
            {
                case ListAddNode listAddNode:
                    Visit(listAddNode);
                    break;
                case ListDeleteNode listDeleteNode:
                    Visit(listDeleteNode);
                    break;
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
            Visit(node.Expression);
            return null;
        }

        public override ASTNode? Visit(ListDeleteNode node)
        {
            Visit(node.Identifier);
            Visit(node.Expression);
            return null;
        }

        public override ASTNode? Visit(ListIndexOfNode node)
        {
            Visit(node.Identifier);
            Visit(node.Expression);
            return null;
        }

        public override ASTNode? Visit(ListValueOfNode node)
        {
            Visit(node.Identifier);
            Visit(node.Expression);
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
        public void Visit(IdentifierNode node)
        {
            var sym = Lookup(node.Name, _currentBlock);
            if (sym == null)
            {
                SymbolError(node, $"{node.Name} is not found. Declare your variable before use.");
            }
        }

        public void SymbolError(ASTNode node, string error)
        {
            symbolErrorhandler.SymbolErrorMessages.Add($"Error line {node.Line}: {error}");
        }
    }
}

