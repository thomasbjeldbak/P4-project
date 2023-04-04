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

    internal class SymbolTable
    {
        private Dictionary<BlockNode, Scope> _scopes; //Key = BlockNode belonging to the Scope, Value = Scope
        private Stack<Scope> _stackScopes; //Stack of scopes for building _scopes
        private BlockNode _currentBlock; //The current Block (used for look-up)

        public SymbolTable BuildSymbolTable(ASTNode astRoot)
        {
            _scopes = new Dictionary<BlockNode, Scope>();
            _stackScopes = new Stack<Scope>();
            ProcessNode(astRoot);
            return this;
        }

        //Recursively go all children of each ASTNode
        private void ProcessNode(ASTNode node)
        {
            switch (node) {
                //If node is a BlockNode, open new scope
                case BlockNode blockNode:
                    NewScope(blockNode);
                    break;
                //If node is a DeclarationNode, Insert the name and type in the scope at the top of the stack
                case DeclarationNode declarationNode: 
                    Insert(declarationNode.Identifier.Name, declarationNode.Identifier.TypeNode.Type);
                    break;
                //If node is a IdentifierNode, check if it exists in the symbolTable
                case IdentifierNode identifierNode:
                    var sym = Lookup(identifierNode.Name, _currentBlock);
                    if (sym == null) {
                        throw new Exception("Symbol not found");
                    }
                    break;
            }
            
            //Go through the children of the node and process them
            foreach (var child in node.GetChildren().Where(x => x is ASTNode))
            {
                ProcessNode(child);
            }

            //The BlockNode is now processed, we can exit the scope
            if (node is BlockNode)
            {
                ExitScope();
            }
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
        private void Insert(string name, TypeEnum type)
        {
            _stackScopes.Peek().Symbols.Add(name, new Symbol 
            { 
                Name = name, Type = type,
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
    }
}

