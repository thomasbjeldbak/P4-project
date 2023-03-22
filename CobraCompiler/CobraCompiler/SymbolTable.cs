using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data.Metadata.Edm;
using System.Data.Common.CommandTrees;
using System.Collections;
using Antlr4.Runtime.Atn;
using static ASTNodes;

namespace CobraCompiler
{

    public class Symbol
    {
        public enum Type
        {
            number, text, boolean
        }

        public Type type { get; set; }
        public string Name { get; set; }

    }

    public class SymbolTable
    {
        List<Symbol> symTbl = new List<Symbol>();

        public void BuildSymbolTable(ASTNode astRoot)
        {
            processNode<ASTNode>(astRoot);
        }

        public void processNode<T>(T node)
        {

            switch (node) {
                case BlockNode:
                    NewScope();
                    break;
                case IdentifierNode:
                    Insert(node.GetType().Name, (TypeNode)node);
                    break;
                case ReferenceNode:

                
            }

        }

        public void NewScope()
        {
            symTbl.Add(new Dictionary<>(StringComparer.OrdinalIgnoreCase));
        }

        internal void ExitScope()
        {
            symTbl.RemoveAt(symTbl.Count - 1);
        }

        internal void Insert(string name, TypeNode value)
        {
            symTbl[symTbl.Count - 1][name] = value;
        }

        internal Symbol Lookup(string name)
        {
            for (int i = symTbl.Count - 1; i >= 0; --i) {
                if (symTbl[i].ContainsKey(name)) {
                    return symTbl[i][name];
                }
            }

            return null;
        }
    }
}

