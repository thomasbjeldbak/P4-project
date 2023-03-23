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
        public TypeEnum Type { get; set; }
        public string Name { get; set; }

    }

    internal class SymbolTable
    {
        private Stack<Dictionary<string, Symbol>> scopes;

        public SymbolTable BuildSymbolTable(ASTNode astRoot)
        {
            scopes = new Stack<Dictionary<string, Symbol>>();
            ProcessNode(astRoot);
            return this;
        }

        private void ProcessNode(ASTNode node)
        {
            switch (node) {
                case BlockNode:
                    NewScope();
                    break;
                case DeclarationNode declarationNode:
                    Insert(declarationNode.Identifier.Name, declarationNode.Identifier.Type);
                    break;
                case IdentifierNode identifierNode:
                    var sym = Lookup(identifierNode.Name);
                    if (sym == null) {
                        throw new Exception("Symbol not found");
                    }
                    break;
            }

            // Get all children that are ASTNodes
            foreach (var child in node.GetChildren().Where(x => x is ASTNode))
            {
                ProcessNode(child);
            }
            
            if (node is BlockNode) {
                ExitScope();
            }

        }

        // create a function that creates a new scope
        private void NewScope()
        {
            scopes.Push(new Dictionary<string, Symbol>());
        }

        private void ExitScope()
        {
            scopes.Pop();
        }

        private void Insert(string name, TypeNode value)
        {
            scopes.Peek().Add(name, new Symbol { Name = name, Type = value.Type });
        }

        private Symbol Lookup(string name)
        {
            foreach (var scope in scopes)
            {
                if (scope.TryGetValue(name, out Symbol symbol))
                {
                    return symbol;
                }
            }

            return null;
        }
    }
}

