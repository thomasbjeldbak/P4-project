﻿using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using static System.Net.Mime.MediaTypeNames;
using static ASTNodes;

namespace CobraCompiler
{
    public class Emitter : ASTVisitor<StringBuilder>
    {

        private readonly SymbolTable _symbolTable;
        private BlockNode _currentBlock;

        public Emitter(SymbolTable symbolTable)
        {
            _symbolTable = symbolTable;
        }

        public override StringBuilder Visit(ProgramNode node)
        {
            _currentBlock = node;
            var stringBuilder = new StringBuilder();
            //Libraries
            stringBuilder.AppendLine("#include <stdio.h>");
            stringBuilder.AppendLine("#include <stdlib.h>");
            stringBuilder.AppendLine("#include <string.h>");
            //Cocatenate two strings function:
            stringBuilder.AppendLine("char* concat(const char *str1, const char *str2) {");
            stringBuilder.AppendLine(" size_t len1 = strlen(str1);");
            stringBuilder.AppendLine(" size_t len2 = strlen(str2);");
            stringBuilder.AppendLine(" char *result = malloc(strlen(str1) + strlen(str2) + 1);");
            stringBuilder.AppendLine(" strcpy(result, str1);");
            stringBuilder.AppendLine(" strcat(result, str2);");
            stringBuilder.AppendLine(" return result;");
            stringBuilder.AppendLine("}");
            //divide function:
            stringBuilder.AppendLine("float divide(float left, float right) {");
            stringBuilder.AppendLine("if (right != 0) return left / right; ");
            stringBuilder.AppendLine("else { printf(\"Error: Division by zero!\\n\"); exit(EXIT_FAILURE); }");
            stringBuilder.AppendLine("}");
            //Scan in a value:
            stringBuilder.AppendLine("void* input(char* format, size_t size) {");
            stringBuilder.AppendLine(" void* input = malloc(size);");
            stringBuilder.AppendLine(" int result = scanf(format, input);");
            stringBuilder.AppendLine(" if (result != 1) {");
            stringBuilder.AppendLine(" fprintf(stderr, \"Error: Invalid input format\\n\");");
            stringBuilder.AppendLine(" exit(EXIT_FAILURE);");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" return input;");
            stringBuilder.AppendLine("}");
            //Struct List:
            stringBuilder.AppendLine("struct node\n{");
            stringBuilder.AppendLine(" void *value;");
            stringBuilder.AppendLine(" struct node *next;");
            stringBuilder.AppendLine("};");
            //get length of list function:
            //stringBuilder.AppendLine("int GetListLength(struct node *list) {");
            //stringBuilder.AppendLine("int length = 0;");
            //stringBuilder.AppendLine("struct node *current = list;");
            //stringBuilder.AppendLine("while (current != NULL) {");
            //stringBuilder.AppendLine("length++;");
            //stringBuilder.AppendLine("current = current->next;");
            //stringBuilder.AppendLine("}");
            //stringBuilder.AppendLine("return length;");
            //stringBuilder.AppendLine("}");
            //list:Add() function:
            stringBuilder.AppendLine("void AddToList (struct node **list, void *value, size_t value_size){");
            stringBuilder.AppendLine(" struct node *new_node = malloc(sizeof(struct node));");
            stringBuilder.AppendLine(" new_node->value = malloc(value_size);");
            stringBuilder.AppendLine(" memcpy(new_node->value, value, value_size);");
            stringBuilder.AppendLine(" new_node->next = NULL;");
            stringBuilder.AppendLine(" if (*list == NULL) {");
            stringBuilder.AppendLine(" *list = new_node;");
            stringBuilder.AppendLine(" } else {");
            stringBuilder.AppendLine(" struct node *last_node = *list;");
            stringBuilder.AppendLine(" while (last_node->next != NULL) {");
            stringBuilder.AppendLine(" last_node = last_node->next;");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" last_node->next = new_node;");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine("};");
            //list:Replace() function:
            stringBuilder.AppendLine("void ReplaceInList(struct node *list, void *value, int index)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(" struct node *curr_node = list;");
            stringBuilder.AppendLine(" int i;");
            stringBuilder.AppendLine(" for (i = 0; i < index; i++) {");
            stringBuilder.AppendLine(" if (curr_node == NULL) {");
            stringBuilder.AppendLine(" fprintf(stderr, \"Error: Invalid index\\n\");");
            stringBuilder.AppendLine(" exit(EXIT_FAILURE);");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" curr_node = curr_node->next;");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" if (curr_node == NULL) {");
            stringBuilder.AppendLine(" fprintf(stderr, \"Error: Invalid index\\n\");");
            stringBuilder.AppendLine(" exit(EXIT_FAILURE);");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" curr_node->value = value;");
            stringBuilder.AppendLine("}");
            //list:IndexOfList() function:
            stringBuilder.AppendLine("int IndexOfList(struct node *list, void *value, size_t value_size)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(" struct node *curr_node = list;");
            stringBuilder.AppendLine(" int index = 0;");
            stringBuilder.AppendLine(" while (curr_node != NULL) {");
            stringBuilder.AppendLine(" if (memcmp(curr_node->value, value, value_size) == 0)");
            stringBuilder.AppendLine(" { return index; }");
            stringBuilder.AppendLine(" curr_node = curr_node->next;");
            stringBuilder.AppendLine(" index++;");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" fprintf(stderr, \"Error: Value not found in list\\n\");");
            stringBuilder.AppendLine(" exit(EXIT_FAILURE);");
            stringBuilder.AppendLine("}");
            //List:ValueOf() function:
            stringBuilder.AppendLine("void *ValueOfList(struct node *list, int index)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(" struct node *curr_node = list;");
            stringBuilder.AppendLine(" int i;");
            stringBuilder.AppendLine(" for (i = 0; i < index; i++) {");
            stringBuilder.AppendLine(" if (curr_node == NULL) {");
            stringBuilder.AppendLine(" fprintf(stderr, \"Error: Invalid index\\n\");");
            stringBuilder.AppendLine(" exit(EXIT_FAILURE);");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" curr_node = curr_node->next;");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" if (curr_node == NULL) {");
            stringBuilder.AppendLine(" fprintf(stderr, \"Error: Invalid index\\n\");");
            stringBuilder.AppendLine(" exit(EXIT_FAILURE);");
            stringBuilder.AppendLine(" }");
            stringBuilder.AppendLine(" return curr_node->value;");
            stringBuilder.AppendLine("}");

            foreach (var funcDecCommand in node.Commands.OfType<FunctionDeclarationNode>())
            {
                stringBuilder.Append(Visit(funcDecCommand));
                _currentBlock = node;
            }

            stringBuilder.Append("void main()");
            stringBuilder.AppendLine("{");

            foreach (var command in node.Commands.Where(x => x is not FunctionDeclarationNode))
            {
                switch (command)
                {
                    case DeclarationNode declarationNode:
                        stringBuilder.Append(Visit(declarationNode));
                        stringBuilder.AppendLine(";");
                        break;
                    case AssignNode assignNode:
                        stringBuilder.Append(Visit(assignNode));
                        break;
                    case StatementNode statementNode:
                        stringBuilder.Append(Visit(statementNode));
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
                _currentBlock = node;
            }

            stringBuilder.AppendLine("}");
            _currentBlock = node;

            return stringBuilder;
        }
        public override StringBuilder Visit(BlockNode node)
        {
            _currentBlock = node;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("{");

            if (node.Commands == null)
            {
                stringBuilder.AppendLine("}");
                return stringBuilder;
            }

            foreach (var command in node.Commands)
            {
                switch (command)
                {
                    case DeclarationNode declarationNode:
                        stringBuilder.Append(Visit(declarationNode));
                        stringBuilder.AppendLine(";");
                        break;
                    case AssignNode assignNode:
                        stringBuilder.Append(Visit(assignNode));
                        break;
                    case StatementNode statementNode:
                        stringBuilder.Append(Visit(statementNode));
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
                _currentBlock = node;
            }

            stringBuilder.AppendLine("}");
            _currentBlock = node;

            return stringBuilder;
        }

        public override StringBuilder Visit(DeclarationNode node)
        {
            var symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{getTypeInC(symbol.Type)} ");
           
            if (isFunctionReference(symbol.Name))
                stringBuilder.Append($"*");

            stringBuilder.Append($"{symbol.Name}");

            if (!isList(symbol.Type) && node.Expression != null)
            {
                stringBuilder.Append($" = {Visit(node.Expression)}");
            }
            else if (isList(symbol.Type))
            {
                stringBuilder.Append(" = NULL");
            }
            else
            {
                switch (symbol.Type)
                {
                    case TypeEnum.number:
                    case TypeEnum.boolean:
                    case TypeEnum._decimal:
                        stringBuilder.Append($" = 0");
                        break;
                    case TypeEnum.text:
                        stringBuilder.Append($" = \"\"");
                        break;
                }
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(StatementNode node)
        {
            var stringBuilder = new StringBuilder();

            switch (node)
            {
                case IfNode ifNode:
                    stringBuilder.Append(Visit(ifNode));
                    break;
                case RepeatNode repeatNode:
                    stringBuilder.Append(Visit(repeatNode));
                    break;
                case WhileNode whileNode:
                    stringBuilder.Append(Visit(whileNode));
                    break;
                case ForeachNode foreachNode:
                    stringBuilder.Append(Visit(foreachNode));
                    break;
                case ListOprStatementNode listOperationNode:
                    stringBuilder.Append(Visit(listOperationNode));
                    break;
                case CommentNode commentNode:
                    stringBuilder.Append(Visit(commentNode));
                    break;
                //case FunctionDeclarationNode functionDeclarationNode:
                //    stringBuilder.Append(Visit(functionDeclarationNode));
                //    break;
                case InputStmtNode inputStmtNode:
                    stringBuilder.Append(Visit(inputStmtNode));
                    break;
                case OutputStmtNode outputStmtNode:
                    stringBuilder.Append(Visit(outputStmtNode));
                    break;
                case FunctionCallStmtNode functionCallStmtNode:
                    stringBuilder.Append(Visit(functionCallStmtNode));
                    break;
                case ReturnNode returnNode:
                    stringBuilder.Append(Visit(returnNode));
                    break;
                default:
                    throw new Exception();
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(AssignNode node)
        {
            var stringBuilder = new StringBuilder();
            var symbol = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            if (isFunctionReference(symbol.Name))
                stringBuilder.Append("*");

            stringBuilder.Append(symbol.Name);
            stringBuilder.Append(" = ");
            stringBuilder.Append(Visit(node.Expression));
            stringBuilder.AppendLine(";");

            return stringBuilder;
        }

        public override StringBuilder Visit(ExpressionNode node)
        {
            var stringBuilder = new StringBuilder();

            switch (node)
            {
                case NumberNode numberNode:
                    stringBuilder.Append(numberNode.Value.ToString());
                    break;
                case DecimalNode decimalNode:
                    stringBuilder.Append(decimalNode.Value.ToString().Replace(',', '.'));
                    break;
                case TextNode textNode:
                    stringBuilder.Append($"\"{textNode.Value}\"");
                    break;
                case BooleanNode booleanNode:
                    stringBuilder.Append(booleanNode.Value ? "1" : "0");
                    break;
                case InfixExpressionNode infixExpressionNode:
                    stringBuilder.Append(Visit(infixExpressionNode));
                    break;
                case IdentifierNode identifierNode:
                    Symbol symbol = _symbolTable.Lookup(identifierNode.Name, _currentBlock);

                    if (isFunctionReference(symbol.Name))
                        stringBuilder.Append("*");

                    stringBuilder.Append(symbol.Name); //stringBuilder.Append(identifierNode.Value.ToString());
                    break;
                case ListNode listNode:
                    throw new NotImplementedException();
                case ListOprExpressionNode listOprExpressionNode:
                    stringBuilder.Append(Visit(listOprExpressionNode));
                    break;
                case InputExprNode inputExprNode:
                    stringBuilder.Append(Visit(inputExprNode));
                    break;
                case OutputExprNode outputExprNode:
                    stringBuilder.Append(Visit(outputExprNode));
                    break;
                case FunctionCallExprNode functionCallExprNode:
                    stringBuilder.Append(Visit(functionCallExprNode));
                    break;
                default:
                    throw new Exception($"ExpressionNode type not valid");
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(InfixExpressionNode node)
        {
            var stringBuilder = new StringBuilder();

            switch (node)
            {
                case AdditionNode additionNode:
                    stringBuilder.Append(Visit(additionNode));
                    break;
                case SubtractionNode subtractionNode:
                    stringBuilder.Append(Visit(subtractionNode));
                    break;
                case MultiplicationNode multiplicationNode:
                    stringBuilder.Append(Visit(multiplicationNode));
                    break;
                case DivisionNode divideNode:
                    stringBuilder.Append(Visit(divideNode));
                    break;
                case AndNode andNode:
                    stringBuilder.Append(Visit(andNode));
                    break;
                case OrNode orNode:
                    stringBuilder.Append(Visit(orNode));
                    break;
                case EqualNode equalNode:
                    stringBuilder.Append(Visit(equalNode));
                    break;
                case NotEqualNode notEqualNode:
                    stringBuilder.Append(Visit(notEqualNode));
                    break;
                case GreaterNode greaterNode:
                    stringBuilder.Append(Visit(greaterNode));
                    break;
                case LessNode lessNode:
                    stringBuilder.Append(Visit(lessNode));
                    break;
                case GreaterEqualNode greaterEqualNode:
                    stringBuilder.Append(Visit(greaterEqualNode));
                    break;
                case LessEqualNode lessEqualNode:
                    stringBuilder.Append(Visit(lessEqualNode));
                    break;
                default:
                    throw new Exception();
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(IfNode node)
        {
            var stringBuilder = new StringBuilder();

            //Generate code for the if block
            stringBuilder.Append("if(");

            //Generate code for the condition
            stringBuilder.Append(Visit(node.Condition));

            stringBuilder.AppendLine(")");

            stringBuilder.Append(Visit(node.Block));

            //Generate code for the else if blocks - may also be an else block
            foreach (var @else in node.ElseIfs)
            {
                switch (@else)
                {
                    case ElseIfNode elseIf:
                        stringBuilder.Append(Visit(elseIf));
                        break;
                    case ElseNode:
                        stringBuilder.Append(Visit(@else));
                        break;
                    default:
                        throw new Exception();
                }
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(ElseIfNode node)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("else if(");
            stringBuilder.Append(Visit(node.Condition));
            stringBuilder.AppendLine(")");
            stringBuilder.Append(Visit(node.Block));

            return stringBuilder;
        }

        public override StringBuilder Visit(ElseNode node)
        {
            var stringBuilder = new StringBuilder();

            //Generate code for the else block
            stringBuilder.AppendLine("else");
            stringBuilder.Append(Visit(node.Block));

            return stringBuilder;
        }

        public override StringBuilder Visit(RepeatNode node)
        {
            var stringBuilder = new StringBuilder();

            //Generate code for the Repeat node
            stringBuilder.Append("for (int number = 0; number < ");
            //Generate code for the expression in the repeat
            stringBuilder.Append(Visit(node.Expression));
            stringBuilder.Append("; number++");
            stringBuilder.Append(")");
            stringBuilder.Append(Visit(node.Block));

            return stringBuilder;
        }

        public override StringBuilder Visit(WhileNode node)
        {
            var stringBuilder = new StringBuilder();

            //Generate code for the repeat while loop
            stringBuilder.Append("while(");

            //Generates the condition for the while part
            stringBuilder.Append(Visit(node.Condition));
            stringBuilder.AppendLine(")");
            stringBuilder.Append(Visit(node.Block));

            return stringBuilder;
        }

        public override StringBuilder Visit(ForeachNode node)
        {
            var stringBuilder = new StringBuilder();

            //Open new scope
            stringBuilder.AppendLine("{");
            stringBuilder.Append(Visit(node.Block));
            //Close the scope
            stringBuilder.AppendLine("}");

            return stringBuilder;
        }

        public override StringBuilder Visit(ListOprStatementNode node)
        {
            var stringBuilder = new StringBuilder();

            switch (node)
            {
                case ListAddNode listAddNode:
                    stringBuilder.Append(Visit(listAddNode));
                    break;
                case ListReplaceNode listReplaceNode:
                    stringBuilder.Append(Visit(listReplaceNode));
                    break;
                default:
                    throw new Exception();
            }
            return stringBuilder;
        }
        public override StringBuilder Visit(ListOprExpressionNode node)
        {
            var stringBuilder = new StringBuilder();

            switch (node)
            {
                case ListIndexOfNode listIndexOfNode:
                    stringBuilder.Append(Visit(listIndexOfNode));
                    break;
                case ListValueOfNode listValueOfNode:
                    stringBuilder.Append(Visit(listValueOfNode));
                    break;
                default:
                    throw new Exception();
            }
            return stringBuilder;
        }

        public override StringBuilder Visit(ListAddNode node)
        {
            var stringBuilder = new StringBuilder();
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            List<string> arguments = new List<string>();

            foreach (var expr in node.Arguments.Expressions)
            {
                arguments = new List<string>();
                stringBuilder.Append($"AddToList(&{list.Name}, ");
                TypeEnum type = TypeEnum.nothing;

                if (expr is TypeNode)
                {
                    type = ((TypeNode)expr).Type;
                    switch (type)
                    {
                        case TypeEnum.number:
                        case TypeEnum.boolean:
                            arguments.Add($"&(int){{{Visit(expr)}}}");
                            break;
                        case TypeEnum.text:
                            arguments.Add($"&(char *){{{Visit(expr)}}}");
                            break;
                        case TypeEnum._decimal:
                            arguments.Add($"&(float){{{Visit(expr)}}}");
                            break;
                    }
                }
                else if (expr is IdentifierNode)
                {
                    type = _symbolTable.Lookup(((IdentifierNode)expr).Name, _currentBlock).Type;

                    switch (type)
                    {

                        case TypeEnum.number:
                        case TypeEnum.boolean:
                        case TypeEnum._decimal:
                            arguments.Add($"&{Visit(expr)}");
                            break;
                        case TypeEnum.text:
                        case TypeEnum.list_number:
                        case TypeEnum.list_text:
                        case TypeEnum.list_boolean:
                        case TypeEnum.list_decimal:
                            arguments.Add($"{Visit(expr)}");
                            break;
                    }
                }

                arguments.Add($"sizeof({getTypeInC(type)})");
                stringBuilder.Append(string.Join(", ", arguments));
                stringBuilder.AppendLine($");");
            }

            return stringBuilder;
        }

        public override StringBuilder Visit(ListReplaceNode node)
        {
            var stringBuilder = new StringBuilder();
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            stringBuilder.Append($"ReplaceInList({list.Name}, ");

            List<string> arguments = new List<string>();
            var expr0 = node.Arguments.Expressions[0];
            TypeEnum type = TypeEnum.nothing;

            if (expr0 is TypeNode)
            {
                type = ((TypeNode)expr0).Type;
                switch (type)
                {
                    case TypeEnum.number:
                    case TypeEnum.boolean:
                        arguments.Add($"&(int){{{Visit(expr0)}}}");
                        break;
                    case TypeEnum.text:
                        arguments.Add($"&(char *){{{Visit(expr0)}}}");
                        break;
                    case TypeEnum._decimal:
                        arguments.Add($"&(float){{{Visit(expr0)}}}");
                        break;
                }
            }
            else if (expr0 is IdentifierNode)
            {
                type = _symbolTable.Lookup(((IdentifierNode)expr0).Name, _currentBlock).Type;

                switch (type)
                {

                    case TypeEnum.number:
                    case TypeEnum.boolean:
                    case TypeEnum._decimal:
                        arguments.Add($"&{Visit(expr0)}");
                        break;
                    case TypeEnum.text:
                    case TypeEnum.list_number:
                    case TypeEnum.list_text:
                    case TypeEnum.list_boolean:
                    case TypeEnum.list_decimal:
                        arguments.Add($"{Visit(expr0)}");
                        break;
                }
            }

            var expr1 = node.Arguments.Expressions[1];
            arguments.Add($"{Visit(expr1)}");

            stringBuilder.AppendLine($"{string.Join(", ", arguments)});");

            return stringBuilder;
        }

        public override StringBuilder Visit(ListIndexOfNode node)
        {
            var stringBuilder = new StringBuilder();
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            stringBuilder.Append($"IndexOfList({list.Name}, ");

            List<string> arguments = new List<string>();
            var expr0 = node.Arguments.Expressions[0];
            TypeEnum type = TypeEnum.nothing;

            if (expr0 is TypeNode)
            {
                type = ((TypeNode)expr0).Type;
                switch (type)
                {
                    case TypeEnum.number:
                    case TypeEnum.boolean:
                        arguments.Add($"&(int){{{Visit(expr0)}}}");
                        break;
                    case TypeEnum.text:
                        arguments.Add($"&(char *){{{Visit(expr0)}}}");
                        break;
                    case TypeEnum._decimal:
                        arguments.Add($"&(float){{{Visit(expr0)}}}");
                        break;
                }
            }
            else if (expr0 is IdentifierNode)
            {
                type = _symbolTable.Lookup(((IdentifierNode)expr0).Name, _currentBlock).Type;

                switch (type)
                {

                    case TypeEnum.number:
                    case TypeEnum.boolean:
                    case TypeEnum._decimal:
                        arguments.Add($"&{Visit(expr0)}");
                        break;
                    case TypeEnum.text:
                    case TypeEnum.list_number:
                    case TypeEnum.list_text:
                    case TypeEnum.list_boolean:
                    case TypeEnum.list_decimal:
                        arguments.Add($"{Visit(expr0)}");
                        break;
                }
            }
            arguments.Add($"sizeof({getTypeInC(type)})");

            stringBuilder.AppendLine($"{string.Join(", ", arguments)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(ListValueOfNode node)
        {
            var stringBuilder = new StringBuilder();
            Symbol list = _symbolTable.Lookup(node.Identifier.Name, _currentBlock);

            //Return type:
            switch (getListType(list.Type))
            {
                case TypeEnum.number:
                case TypeEnum.boolean:
                    stringBuilder.Append($"*(int *)");
                    break;
                case TypeEnum._decimal:
                    stringBuilder.Append($"*(float *)");
                    break;
                case TypeEnum.text:
                    stringBuilder.Append($"*(char **)");
                    break;
            }

            stringBuilder.Append($"ValueOfList({list.Name}, ");
            List<string> arguments = new List<string>();

            var expr1 = node.Arguments.Expressions[0];
            if (expr1 is IdentifierNode)
                arguments.Add($"&{Visit(expr1)}");
            else
                arguments.Add($"{Visit(expr1)}");

            stringBuilder.AppendLine($"{string.Join(", ", arguments)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(AdditionNode node)
        {
            var stringBuilder = new StringBuilder();

            if (node.Type is not TypeEnum.text)
            {
                stringBuilder.Append($"({Visit(node.Left)} + {Visit(node.Right)})");
            }
            else
                stringBuilder.Append($"concat({Visit(node.Left)}, {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(SubtractionNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} - {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(MultiplicationNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} * {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(DivisionNode node)
        {
            var stringBuilder = new StringBuilder();
            // Check if the right side of the division evaluates to zero
            stringBuilder.Append($"(divide({Visit(node.Left)}, {Visit(node.Right)}))");

            return stringBuilder;
        }

        public override StringBuilder Visit(AndNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} && {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(OrNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} || {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(GreaterNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} > {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(LessNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} < {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(GreaterEqualNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} >= {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(LessEqualNode node)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"({Visit(node.Left)} <= {Visit(node.Right)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(EqualNode node)
        {
            var stringBuilder = new StringBuilder();
            TypeEnum type = TypeEnum.nothing;

            if (node.Left is IdentifierNode)
                type = _symbolTable.Lookup(((IdentifierNode)node.Left).Name, _currentBlock).Type;
            else
                type = node.Left.Type;

            if (type is not TypeEnum.text)
                stringBuilder.Append($"({Visit(node.Left)} == {Visit(node.Right)})");
            else
                stringBuilder.Append($"(0 == strcmp({Visit(node.Left)}, {Visit(node.Right)}))");

            return stringBuilder;
        }

        public override StringBuilder Visit(NotEqualNode node)
        {
            var stringBuilder = new StringBuilder();
            TypeEnum type = TypeEnum.nothing;

            if (node.Left is IdentifierNode)
                type = _symbolTable.Lookup(((IdentifierNode)node.Left).Name, _currentBlock).Type;
            else
                type = node.Left.Type;

            if (type is not TypeEnum.text)
                stringBuilder.Append($"({Visit(node.Left)} != {Visit(node.Right)})");
            else
                stringBuilder.Append($"(0 != strcmp({Visit(node.Left)}, {Visit(node.Right)}))");

            return stringBuilder;
        }

        public override StringBuilder Visit(FunctionBlockNode node)
        {
            var stringBuilder = new StringBuilder();
            _currentBlock = node;

            stringBuilder.Append("(");

            List<string> arguments = new List<string>();
            for (int i = 0; i < node.Parameters.Declarations.Count; i++)
            {
                var expr = node.Parameters.Declarations[i];
                var arg = Visit(expr).ToString();
                _currentBlock = node;

                if (arg.Contains(" = "))
                    arguments.Add($"{arg.Substring(0, arg.IndexOf(" = "))}");
                else
                    arguments.Add(arg);
            }

            foreach(var name in node.UsedVariables.Keys)
            {
                if (node.UsedVariables[name] == TypeEnum.text ||
                    isList(node.UsedVariables[name]))
                    arguments.Add($"{getTypeInC(node.UsedVariables[name])} {name}");
                else
                    arguments.Add($"{getTypeInC(node.UsedVariables[name])} *{name}");
            }
            stringBuilder.AppendLine($"{string.Join(", ", arguments)})");

            stringBuilder.AppendLine("{");

            if (node.Commands != null)
            {
                foreach (var command in node.Commands)
                {
                    switch (command)
                    {
                        case DeclarationNode declarationNode:
                            stringBuilder.Append(Visit(declarationNode));
                            stringBuilder.AppendLine(";");
                            break;
                        case AssignNode assignNode:
                            stringBuilder.Append(Visit(assignNode));
                            break;
                        case StatementNode statementNode:
                            stringBuilder.Append(Visit(statementNode));
                            break;
                        default:
                            throw new Exception($"Command was not valid");
                    }
                    _currentBlock = node;
                }
            }
            //if (node.ReturnExpression != null)
            //{
            //    stringBuilder.AppendLine($"return {Visit(node.ReturnExpression)};");
            //}
            stringBuilder.AppendLine("}");

            _currentBlock = node;
            return stringBuilder;
        }

        public override StringBuilder Visit(CommentNode node)
        {
            return new StringBuilder();
        }

        public override StringBuilder Visit(FunctionCallExprNode node)
        {
            var stringBuilder = new StringBuilder();
            var symbol = _symbolTable.Lookup(node.Name, _currentBlock);
            var declaration = (FunctionDeclarationNode)symbol.Reference;

            stringBuilder.Append($"{node.Name}(");
            List<string> arguments = new List<string>();
            for (int i = 0; i < node.Arguments.Expressions.Count; i++)
            {
                var expr = node.Arguments.Expressions[i];
                arguments.Add($"{Visit(expr)}");
            }

            foreach(var name in declaration.Block.UsedVariables.Keys)
            {
                var usedVariableType = declaration.Block.UsedVariables[name];

                if (_currentBlock is ProgramNode && (
                    usedVariableType == TypeEnum.number ||
                    usedVariableType == TypeEnum._decimal ||
                    usedVariableType == TypeEnum.boolean ))
                {
                    arguments.Add($"&{name}");
                }
                else
                    arguments.Add($"{name}");
            }

            stringBuilder.Append($"{string.Join(", ", arguments)})");
            return stringBuilder;
        }

        public override StringBuilder Visit(FunctionCallStmtNode node)
        {
            var stringBuilder = new StringBuilder();
            var symbol = _symbolTable.Lookup(node.Name, _currentBlock);
            var declaration = (FunctionDeclarationNode)symbol.Reference;

            stringBuilder.Append($"{node.Name}(");
            List<string> arguments = new List<string>();
            for (int i = 0; i < node.Arguments.Expressions.Count; i++)
            {
                var expr = node.Arguments.Expressions[i];
                arguments.Add($"{Visit(expr)}");
            }

            foreach(var name in declaration.Block.UsedVariables.Keys)
            {
                var usedVariableType = declaration.Block.UsedVariables[name];

                if (_currentBlock is ProgramNode && (
                    usedVariableType == TypeEnum.number ||
                    usedVariableType == TypeEnum._decimal ||
                    usedVariableType == TypeEnum.boolean))
                {
                    arguments.Add($"&{name}");
                }
                else
                    arguments.Add($"{name}");
            }

            stringBuilder.AppendLine($"{string.Join(", ", arguments)});");
            return stringBuilder;
        }

        public override StringBuilder Visit(FunctionDeclarationNode node)
        {
            var symbol = _symbolTable.Lookup(node.Name, _currentBlock);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{getTypeInC(node.ReturnType)} {node.Name}");

            node.Block.UsedVariables.Remove(node.Name);
            stringBuilder.Append(Visit(node.Block));
            return stringBuilder;
        }

        public override StringBuilder Visit(InputExprNode node)
        {
            var stringBuilder = new StringBuilder();

            //Return type:
            switch (node.Type)
            {
                case TypeEnum.number:
                case TypeEnum.boolean:
                    stringBuilder.Append($"*(int *)");
                    break;
                case TypeEnum._decimal:
                    stringBuilder.Append($"*(float *)");
                    break;
                case TypeEnum.text:
                    stringBuilder.Append($"(char *)");
                    break;
            }

            stringBuilder.Append($"input(");

            List<string> arguments = new List<string>();
            switch (node.Type)
            {
                case TypeEnum.number:
                case TypeEnum.boolean:
                    arguments.Add($"\"%d\"");
                    arguments.Add($"sizeof(int)");
                    break;
                case TypeEnum.text:
                    arguments.Add($"\"%s\"");
                    arguments.Add($"sizeof(char *)");
                    break;
                case TypeEnum._decimal:
                    arguments.Add($"\"%g\"");
                    arguments.Add($"sizeof(float)");
                    break;
            }

            stringBuilder.Append($"{string.Join(", ", arguments)})");

            return stringBuilder;
        }

        public override StringBuilder Visit(OutputExprNode node)
        {
            return new StringBuilder();
        }

        public override StringBuilder Visit(InputStmtNode node)
        {
            return new StringBuilder();
        }

        public override StringBuilder Visit(OutputStmtNode node)
        {
            var stringBuilder = new StringBuilder();

            var argument = node.Arguments.Expressions[0];
            var expr = Visit(argument);
            TypeEnum type = TypeEnum.nothing;

            switch (argument)
            {
                case TypeNode typeNode:
                    type = typeNode.Type;
                    switch (typeNode.Type)
                    {
                        case TypeEnum.boolean:
                            expr = new StringBuilder($"({expr} ? \"true\" : \"false\")");
                            break;
                        default:
                            expr = new StringBuilder($"{expr}");
                            break;
                    }
                    break;
                case FunctionCallExprNode functionCallExprNode:
                    type = _symbolTable.Lookup(functionCallExprNode.Name, _currentBlock).Type;
                    break;
                case ListIndexOfNode listIndexOfNode:
                    type = TypeEnum.number;
                    break;
                case ListValueOfNode listValueOfNode:
                    type = getListType(_symbolTable.Lookup(listValueOfNode.Identifier.Name, _currentBlock).Type);
                    break;
                case IdentifierNode identifierNode:
                    type = _symbolTable.Lookup(identifierNode.Name, _currentBlock).Type;
                    break;
            }

            switch (type)
            {
                case TypeEnum.number:
                    stringBuilder.AppendLine($"printf(\"%d\\n\", {expr});");
                    break;
                case TypeEnum._decimal:
                    stringBuilder.AppendLine($"printf(\"%g\\n\", {expr});");
                    break;
                case TypeEnum.boolean:
                    stringBuilder.AppendLine($"printf(\"%s\\n\", ({expr} ? \"true\" : \"false\"));");
                    break;
                default:
                    stringBuilder.AppendLine($"printf(\"%s\\n\", {expr});");
                    break;
            }
            return stringBuilder;
        }

        public override StringBuilder Visit(ReturnNode node)
        {
            return new StringBuilder($"return {Visit(node.Expression)};");
        }

        public override StringBuilder Visit(ForeachBlockNode node)
        {
            var stringBuilder = new StringBuilder();
            _currentBlock = node;
            var list = _symbolTable.Lookup(node.ListName, _currentBlock);
            var localVariable = _symbolTable.Lookup(node.LocalVariable.Identifier.Name, _currentBlock);

            //Initialize local variables for use later. The number variable is used for keeping the while loop running until the end of the list is reached
            //The LocalVariable is used as the variable on which operations will be carried out on in the foreach loop

            _currentBlock = node;
            stringBuilder.AppendLine($"struct node *list = {list.Name};");
            stringBuilder.AppendLine("while (list != NULL) {");

            stringBuilder.Append($"{getTypeInC(localVariable.Type)} {localVariable.Name}");
            //Assign the current value in the list to the LocalVariable
            switch (localVariable.Type)
            {
                case TypeEnum.number:
                case TypeEnum.boolean:
                    stringBuilder.AppendLine($" = *(int *)list->value;");
                    break;
                case TypeEnum._decimal:
                    stringBuilder.AppendLine($" = *(float *)list->value;");
                    break;
                case TypeEnum.text:
                    stringBuilder.AppendLine($" = (char *)list->value;");
                    break;
                default:
                    stringBuilder.AppendLine($" = list->value;");
                    break;
            }

            //Go through the commands in the block
            foreach (var command in node.Commands)
            {
                switch (command)
                {
                    case DeclarationNode declarationNode:
                        stringBuilder.Append(Visit(declarationNode));
                        stringBuilder.AppendLine(";");
                        break;
                    case AssignNode assignNode:
                        stringBuilder.Append(Visit(assignNode));
                        break;
                    case StatementNode statementNode:
                        stringBuilder.Append(Visit(statementNode));
                        break;
                    default:
                        throw new Exception($"Command was not valid");
                }
                _currentBlock = node;
            }
            //Check if next element is NULL. If this is true, set number to 0 in order to stop iterating
            stringBuilder.AppendLine($"list = list->next;");

            stringBuilder.AppendLine("}"); //Close While Loop
            _currentBlock = node;
            return stringBuilder;
        }


        #region List helper functions

        //Takes TypeNode (list_[TYPE]) and returns the Type
        //e.g. list_number -> return number
        private TypeEnum getListType(TypeEnum listType)
        {
            switch (listType)
            {
                case TypeEnum.list_number:
                    return TypeEnum.number;
                case TypeEnum.list_text:
                    return TypeEnum.text;
                case TypeEnum.list_boolean:
                    return TypeEnum.boolean;
                case TypeEnum.list_decimal:
                    return TypeEnum._decimal;
                default:
                    throw new Exception();
            }
        }

        //Checks if TypeNode is a list
        //e.g. list_number -> return true,
        //  number -> return false;
        private bool isList(TypeEnum? listType)
        {
            switch (listType)
            {
                case TypeEnum.list_number:
                case TypeEnum.list_text:
                case TypeEnum.list_boolean:
                case TypeEnum.list_decimal:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        public string getTypeInC(TypeEnum type)
        {
            switch (type)
            {
                case TypeEnum.number:
                case TypeEnum.boolean:
                    return "int";
                case TypeEnum._decimal:
                    return "float";
                case TypeEnum.text:
                    return "char *";
                    break;
                case TypeEnum.list_number:
                case TypeEnum.list_decimal:
                case TypeEnum.list_text:
                case TypeEnum.list_boolean:
                    return "struct node *";
                case TypeEnum.nothing:
                    return "void";
                default:
                    throw new Exception();

            }
        }

        public bool isFunctionReference(string name)
        {
            var scope = _symbolTable._scopes[_currentBlock];

            while (scope.Parent != null)
            {
                if (scope.Block is FunctionBlockNode)
                {
                    var functionBlock = (FunctionBlockNode)scope.Block;

                    if (functionBlock.UsedVariables.ContainsKey(name))
                        return true;
                }
                scope = scope.Parent;
            }
            return false;
        }
    }
}
