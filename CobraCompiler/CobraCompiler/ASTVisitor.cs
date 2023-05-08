using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASTNodes;

namespace CobraCompiler
{
    //ASTVisitor is used for TypeChecking and CodeGeneration
    internal abstract class ASTVisitor<T> 
    {

        public abstract T Visit(ProgramNode node);
        public abstract T Visit(BlockNode node);
        public abstract T Visit(FunctionBlockNode node);
        public abstract T Visit(DeclarationNode node);
        public abstract T Visit(StatementNode node);
        public abstract T Visit(AssignNode node);
        public abstract T Visit(ExpressionNode node);
        public abstract T Visit(InfixExpressionNode node);
        public abstract T Visit(IfNode node);
        public abstract T Visit(ElseIfNode node);
        public abstract T Visit(ElseNode node);
        public abstract T Visit(RepeatNode node);
        public abstract T Visit(WhileNode node);
        public abstract T Visit(ForeachNode node);
        public abstract T Visit(ListOprStatementNode node);
        public abstract T Visit(ListOprExpressionNode node);
        public abstract T Visit(ListAddNode node);
        public abstract T Visit(ListReplaceNode node);
        public abstract T Visit(ListIndexOfNode node);
        public abstract T Visit(ListValueOfNode node);
        public abstract T Visit(AdditionNode node);
        public abstract T Visit(SubtractionNode node);
        public abstract T Visit(MultiplicationNode node);
        public abstract T Visit(DivisionNode node);
        public abstract T Visit(AndNode node);
        public abstract T Visit(OrNode node);
        public abstract T Visit(GreaterNode node);
        public abstract T Visit(LessNode node);
        public abstract T Visit(GreaterEqualNode node);
        public abstract T Visit(LessEqualNode node);
        public abstract T Visit(EqualNode node);
        public abstract T Visit(NotEqualNode node);
        public abstract T Visit(CommentNode node);
        public abstract T Visit(FunctionCallExprNode node);
        public abstract T Visit(FunctionCallStmtNode node);
        public abstract T Visit(FunctionDeclarationNode node);
        public abstract T Visit(InputExprNode node);
        public abstract T Visit(OutputExprNode node);
        public abstract T Visit(InputStmtNode node);
        public abstract T Visit(OutputStmtNode node);
        public abstract T Visit(ReturnNode node);


    }
}
