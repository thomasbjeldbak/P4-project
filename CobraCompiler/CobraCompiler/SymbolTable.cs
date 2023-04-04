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

    public class Symbol
    {
        public string Name { get; set; }
        public TypeEnum Type { get; set; }
    }

    public class Scope
    {
        public Scope() 
        {
            Symbols = new Dictionary<string, Symbol>();
        }
        public Dictionary<string, Symbol> Symbols { get; set; }
        public Scope Parent { get; set; }
    }

    public class SymbolTable
    {
        private Dictionary<BlockNode, Scope> _scopes;
        private Stack<Scope> _stackScopes;
        private BlockNode _currentBlock;

        public SymbolTable BuildSymbolTable(ASTNode astRoot)
        {
            _scopes = new Dictionary<BlockNode, Scope>();
            _stackScopes = new Stack<Scope>();
            ProcessNode(astRoot);
            return this;
        }

        private void ProcessNode(ASTNode node)
        {
            switch (node) {
                case BlockNode blockNode:
                    NewScope(blockNode);
                    break;
                case DeclarationNode declarationNode:
                    Insert(declarationNode.Identifier.Name, declarationNode.Identifier.TypeNode.Type);
                    break;
                case IdentifierNode identifierNode:
                    var sym = Lookup(identifierNode.Name, _currentBlock);
                    if (sym == null) {
                        throw new Exception("Symbol not found");
                    }
                    break;
            }
            
            foreach (var child in node.GetChildren().Where(x => x is ASTNode))
            {
                ProcessNode(child);
            }
            
            if (node is BlockNode) 
            {
                ExitScope();
            }
        }

        // create a function that creates a new scope
        private void NewScope(BlockNode blockNode)
        {
            var scope = new Scope();

            if (_stackScopes.Count > 0)
                scope.Parent= _stackScopes.Peek();

            _scopes.Add(blockNode, scope);
            _stackScopes.Push(scope);
            _currentBlock = blockNode;
        }

        private void ExitScope()
        {
            _stackScopes.Pop();
        }

        private void Insert(string name, TypeEnum type)
        {
            _stackScopes.Peek().Symbols.Add(name, new Symbol 
            { 
                Name = name, Type = type,
            });
        }

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

