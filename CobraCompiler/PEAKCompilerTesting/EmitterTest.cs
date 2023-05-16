using System.Text;
using Antlr4.Runtime;
using NUnit.Framework;

namespace PEAKCompilerTesting;

[TestFixture]
public class EmitterTest
{
    [Test]
    public void EmitterEmpty()
    {
        // Arrange
        var exprText = "";

        var inputStream = new AntlrInputStream(new StringReader(exprText));
        var lexer = new ExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new ExprParser(tokenStream);

        var errorHandler = new ErrorHandler();
        parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
        parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

        //Get root of CST (which is program)
        var cst = parser.program();
        if (errorHandler.SyntaxErrorMessages.Count > 0) {
            Console.WriteLine("Syntax errors:");
            foreach (var errorMessage in errorHandler.SyntaxErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

        var ast = new BuildASTVisitor().VisitProgram(cst);
        var st = new SymbolTable(errorHandler).BuildSymbolTable(ast);
        if (errorHandler.SymbolErrorMessages.Count > 0) {
            Console.WriteLine("Symbol errors:");
            foreach (var errorMessage in errorHandler.SymbolErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
        new TypeChecker(st, errorHandler).Visit((ASTNodes.ProgramNode)ast);
        if (errorHandler.TypeErrorMessages.Count > 0) {
            Console.WriteLine("Type errors:");
            foreach (var errorMessage in errorHandler.TypeErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

            //Order the commands so that functionDelcarations always come first
            ((ASTNodes.ProgramNode)ast).Commands =
                ((ASTNodes.ProgramNode)ast).Commands.OrderBy(x => x is not ASTNodes.FunctionDeclarationNode).ToList();

        // Act
        StringBuilder sb = new Emitter(st).Visit((ASTNodes.ProgramNode)ast);

        String testString = "#include <stdio.h>\n#include <stdlib.h>\n#include <string.h>\nchar* concat(const char *str1, const char *str2) " +
            "{\n size_t len1 = strlen(str1);\n size_t len2 = strlen(str2);\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\n strcpy(result, str1);\n " +
            "strcat(result, str2);\n return result;\n}\nvoid* input(char* format, size_t size) {\n void* input = malloc(size);\n int result = scanf(format, input);\n " +
            "if (result != 1) {\n fprintf(stderr, \"Error: Invalid input format\\n\");\n exit(EXIT_FAILURE);\n }\n return input;\n}\nstruct node\n{\n void *value;\n " +
            "struct node *next;\n};\nvoid AddToList (struct node **list, void *value, size_t value_size){\n struct node *new_node = malloc(sizeof(struct node));\n " +
            "new_node->value = malloc(value_size);\n memcpy(new_node->value, value, value_size);\n new_node->next = NULL;\n if (*list == NULL) {\n *list = new_node;\n } " +
            "else {\n struct node *last_node = *list;\n while (last_node->next != NULL) {\n last_node = last_node->next;\n }\n last_node->next = new_node;\n }\n};\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\n{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL)" +
            " {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) " +
            "{\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node->value = value;\n}\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\n{\n struct node *curr_node = list;\n int index = 0;\n while (curr_node != NULL) " +
            "{\n if (memcmp(curr_node->value, value, value_size) == 0)\n { return index; }\n curr_node = curr_node->next;\n index++;\n }\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\n exit(EXIT_FAILURE);\n}\nvoid *ValueOfList(struct node *list, int index)\n" +
            "{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n " +
            "exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n " +
            "return curr_node->value;\n}\nvoid main(){\n}\n";
        // Assert
        Assert.That(sb.ToString(), Is.EqualTo(testString));

    }

    [Test]
    public void EmitterVarDec()
    {
        // Arrange
        var exprText = "number x = 10;";

        var inputStream = new AntlrInputStream(new StringReader(exprText));
        var lexer = new ExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new ExprParser(tokenStream);

        var errorHandler = new ErrorHandler();
        parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
        parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

        //Get root of CST (which is program)
        var cst = parser.program();
        if (errorHandler.SyntaxErrorMessages.Count > 0) {
            Console.WriteLine("Syntax errors:");
            foreach (var errorMessage in errorHandler.SyntaxErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

        var ast = new BuildASTVisitor().VisitProgram(cst);
        var st = new SymbolTable(errorHandler).BuildSymbolTable(ast);
        if (errorHandler.SymbolErrorMessages.Count > 0) {
            Console.WriteLine("Symbol errors:");
            foreach (var errorMessage in errorHandler.SymbolErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
        new TypeChecker(st, errorHandler).Visit((ASTNodes.ProgramNode)ast);
        if (errorHandler.TypeErrorMessages.Count > 0) {
            Console.WriteLine("Type errors:");
            foreach (var errorMessage in errorHandler.TypeErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

            //Order the commands so that functionDelcarations always come first
            ((ASTNodes.ProgramNode)ast).Commands =
                ((ASTNodes.ProgramNode)ast).Commands.OrderBy(x => x is not ASTNodes.FunctionDeclarationNode).ToList();

        // Act
        StringBuilder sb = new Emitter(st).Visit((ASTNodes.ProgramNode)ast);

        String testString = "#include <stdio.h>\n#include <stdlib.h>\n#include <string.h>\nchar* concat(const char *str1, const char *str2) " +
            "{\n size_t len1 = strlen(str1);\n size_t len2 = strlen(str2);\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\n strcpy(result, str1);\n " +
            "strcat(result, str2);\n return result;\n}\nvoid* input(char* format, size_t size) {\n void* input = malloc(size);\n int result = scanf(format, input);\n " +
            "if (result != 1) {\n fprintf(stderr, \"Error: Invalid input format\\n\");\n exit(EXIT_FAILURE);\n }\n return input;\n}\nstruct node\n{\n void *value;\n " +
            "struct node *next;\n};\nvoid AddToList (struct node **list, void *value, size_t value_size){\n struct node *new_node = malloc(sizeof(struct node));\n " +
            "new_node->value = malloc(value_size);\n memcpy(new_node->value, value, value_size);\n new_node->next = NULL;\n if (*list == NULL) {\n *list = new_node;\n } " +
            "else {\n struct node *last_node = *list;\n while (last_node->next != NULL) {\n last_node = last_node->next;\n }\n last_node->next = new_node;\n }\n};\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\n{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL)" +
            " {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) " +
            "{\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node->value = value;\n}\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\n{\n struct node *curr_node = list;\n int index = 0;\n while (curr_node != NULL) " +
            "{\n if (memcmp(curr_node->value, value, value_size) == 0)\n { return index; }\n curr_node = curr_node->next;\n index++;\n }\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\n exit(EXIT_FAILURE);\n}\nvoid *ValueOfList(struct node *list, int index)\n" +
            "{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n " +
            "exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n " +
            "return curr_node->value;\n}\nvoid main(){\nint x = 10;\n}\n";

        // Assert
        Assert.That(sb.ToString(), Is.EqualTo(testString));
    }
    [Test]
    public void EmitterFuncDec()
    {
        // Arrange
        var exprText = "function f(number x) return number { return x + 5;}";

        var inputStream = new AntlrInputStream(new StringReader(exprText));
        var lexer = new ExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new ExprParser(tokenStream);

        var errorHandler = new ErrorHandler();
        parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
        parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

        //Get root of CST (which is program)
        var cst = parser.program();
        if (errorHandler.SyntaxErrorMessages.Count > 0) {
            Console.WriteLine("Syntax errors:");
            foreach (var errorMessage in errorHandler.SyntaxErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

        var ast = new BuildASTVisitor().VisitProgram(cst);
        var st = new SymbolTable(errorHandler).BuildSymbolTable(ast);
        if (errorHandler.SymbolErrorMessages.Count > 0) {
            Console.WriteLine("Symbol errors:");
            foreach (var errorMessage in errorHandler.SymbolErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
        new TypeChecker(st, errorHandler).Visit((ASTNodes.ProgramNode)ast);
        if (errorHandler.TypeErrorMessages.Count > 0) {
            Console.WriteLine("Type errors:");
            foreach (var errorMessage in errorHandler.TypeErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

            //Order the commands so that functionDelcarations always come first
            ((ASTNodes.ProgramNode)ast).Commands =
                ((ASTNodes.ProgramNode)ast).Commands.OrderBy(x => x is not ASTNodes.FunctionDeclarationNode).ToList();

        // Act
        StringBuilder sb = new Emitter(st).Visit((ASTNodes.ProgramNode)ast);

        String testString = "#include <stdio.h>\n#include <stdlib.h>\n#include <string.h>\nchar* concat(const char *str1, const char *str2) " +
            "{\n size_t len1 = strlen(str1);\n size_t len2 = strlen(str2);\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\n strcpy(result, str1);\n " +
            "strcat(result, str2);\n return result;\n}\nvoid* input(char* format, size_t size) {\n void* input = malloc(size);\n int result = scanf(format, input);\n " +
            "if (result != 1) {\n fprintf(stderr, \"Error: Invalid input format\\n\");\n exit(EXIT_FAILURE);\n }\n return input;\n}\nstruct node\n{\n void *value;\n " +
            "struct node *next;\n};\nvoid AddToList (struct node **list, void *value, size_t value_size){\n struct node *new_node = malloc(sizeof(struct node));\n " +
            "new_node->value = malloc(value_size);\n memcpy(new_node->value, value, value_size);\n new_node->next = NULL;\n if (*list == NULL) {\n *list = new_node;\n } " +
            "else {\n struct node *last_node = *list;\n while (last_node->next != NULL) {\n last_node = last_node->next;\n }\n last_node->next = new_node;\n }\n};\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\n{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL)" +
            " {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) " +
            "{\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node->value = value;\n}\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\n{\n struct node *curr_node = list;\n int index = 0;\n while (curr_node != NULL) " +
            "{\n if (memcmp(curr_node->value, value, value_size) == 0)\n { return index; }\n curr_node = curr_node->next;\n index++;\n }\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\n exit(EXIT_FAILURE);\n}\nvoid *ValueOfList(struct node *list, int index)\n" +
            "{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n " +
            "exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n " +
            "return curr_node->value;\n}\nint f(int x)\n{\nreturn (x + 5);}\nvoid main(){\n}\n";

        // Assert
        Assert.That(sb.ToString(), Is.EqualTo(testString));
    }

    [Test]
    public void EmitterFuncCall()
    {
        // Arrange
        var exprText = "function f(number x) return number { return x + 5;} call f(10);";

        var inputStream = new AntlrInputStream(new StringReader(exprText));
        var lexer = new ExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new ExprParser(tokenStream);

        var errorHandler = new ErrorHandler();
        parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
        parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

        //Get root of CST (which is program)
        var cst = parser.program();
        if (errorHandler.SyntaxErrorMessages.Count > 0) {
            Console.WriteLine("Syntax errors:");
            foreach (var errorMessage in errorHandler.SyntaxErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

        var ast = new BuildASTVisitor().VisitProgram(cst);
        var st = new SymbolTable(errorHandler).BuildSymbolTable(ast);
        if (errorHandler.SymbolErrorMessages.Count > 0) {
            Console.WriteLine("Symbol errors:");
            foreach (var errorMessage in errorHandler.SymbolErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
        new TypeChecker(st, errorHandler).Visit((ASTNodes.ProgramNode)ast);
        if (errorHandler.TypeErrorMessages.Count > 0) {
            Console.WriteLine("Type errors:");
            foreach (var errorMessage in errorHandler.TypeErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

            //Order the commands so that functionDelcarations always come first
            ((ASTNodes.ProgramNode)ast).Commands =
                ((ASTNodes.ProgramNode)ast).Commands.OrderBy(x => x is not ASTNodes.FunctionDeclarationNode).ToList();

        // Act
        StringBuilder sb = new Emitter(st).Visit((ASTNodes.ProgramNode)ast);

        String testString = "#include <stdio.h>\n#include <stdlib.h>\n#include <string.h>\nchar* concat(const char *str1, const char *str2) " +
            "{\n size_t len1 = strlen(str1);\n size_t len2 = strlen(str2);\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\n strcpy(result, str1);\n " +
            "strcat(result, str2);\n return result;\n}\nvoid* input(char* format, size_t size) {\n void* input = malloc(size);\n int result = scanf(format, input);\n " +
            "if (result != 1) {\n fprintf(stderr, \"Error: Invalid input format\\n\");\n exit(EXIT_FAILURE);\n }\n return input;\n}\nstruct node\n{\n void *value;\n " +
            "struct node *next;\n};\nvoid AddToList (struct node **list, void *value, size_t value_size){\n struct node *new_node = malloc(sizeof(struct node));\n " +
            "new_node->value = malloc(value_size);\n memcpy(new_node->value, value, value_size);\n new_node->next = NULL;\n if (*list == NULL) {\n *list = new_node;\n } " +
            "else {\n struct node *last_node = *list;\n while (last_node->next != NULL) {\n last_node = last_node->next;\n }\n last_node->next = new_node;\n }\n};\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\n{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL)" +
            " {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) " +
            "{\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node->value = value;\n}\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\n{\n struct node *curr_node = list;\n int index = 0;\n while (curr_node != NULL) " +
            "{\n if (memcmp(curr_node->value, value, value_size) == 0)\n { return index; }\n curr_node = curr_node->next;\n index++;\n }\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\n exit(EXIT_FAILURE);\n}\nvoid *ValueOfList(struct node *list, int index)\n" +
            "{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n " +
            "exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n " +
            "return curr_node->value;\n}\nint f(int x)\n{\nreturn (x + 5);}\nvoid main(){\nf(10);\n}\n";

        // Assert
        Assert.That(sb.ToString(), Is.EqualTo(testString));
    }
    [Test]
    public void EmitterIfStatement()
    {
        // Arrange
        var exprText = "function f(number x) return number { return x + 5;} if (5 > 4) { call f(10);}";

        var inputStream = new AntlrInputStream(new StringReader(exprText));
        var lexer = new ExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new ExprParser(tokenStream);

        var errorHandler = new ErrorHandler();
        parser.RemoveErrorListeners(); // remove the default ConsoleErrorListener
        parser.AddErrorListener(errorHandler); // set your ErrorHandler as the error listener'

        //Get root of CST (which is program)
        var cst = parser.program();
        if (errorHandler.SyntaxErrorMessages.Count > 0) {
            Console.WriteLine("Syntax errors:");
            foreach (var errorMessage in errorHandler.SyntaxErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

        var ast = new BuildASTVisitor().VisitProgram(cst);
        var st = new SymbolTable(errorHandler).BuildSymbolTable(ast);
        if (errorHandler.SymbolErrorMessages.Count > 0) {
            Console.WriteLine("Symbol errors:");
            foreach (var errorMessage in errorHandler.SymbolErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }
        new TypeChecker(st, errorHandler).Visit((ASTNodes.ProgramNode)ast);
        if (errorHandler.TypeErrorMessages.Count > 0) {
            Console.WriteLine("Type errors:");
            foreach (var errorMessage in errorHandler.TypeErrorMessages) {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(1);
        }

            //Order the commands so that functionDelcarations always come first
            ((ASTNodes.ProgramNode)ast).Commands =
                ((ASTNodes.ProgramNode)ast).Commands.OrderBy(x => x is not ASTNodes.FunctionDeclarationNode).ToList();

        // Act
        StringBuilder sb = new Emitter(st).Visit((ASTNodes.ProgramNode)ast);

        String testString = "#include <stdio.h>\n#include <stdlib.h>\n#include <string.h>\nchar* concat(const char *str1, const char *str2) " +
            "{\n size_t len1 = strlen(str1);\n size_t len2 = strlen(str2);\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\n strcpy(result, str1);\n " +
            "strcat(result, str2);\n return result;\n}\nvoid* input(char* format, size_t size) {\n void* input = malloc(size);\n int result = scanf(format, input);\n " +
            "if (result != 1) {\n fprintf(stderr, \"Error: Invalid input format\\n\");\n exit(EXIT_FAILURE);\n }\n return input;\n}\nstruct node\n{\n void *value;\n " +
            "struct node *next;\n};\nvoid AddToList (struct node **list, void *value, size_t value_size){\n struct node *new_node = malloc(sizeof(struct node));\n " +
            "new_node->value = malloc(value_size);\n memcpy(new_node->value, value, value_size);\n new_node->next = NULL;\n if (*list == NULL) {\n *list = new_node;\n } " +
            "else {\n struct node *last_node = *list;\n while (last_node->next != NULL) {\n last_node = last_node->next;\n }\n last_node->next = new_node;\n }\n};\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\n{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL)" +
            " {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) " +
            "{\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n curr_node->value = value;\n}\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\n{\n struct node *curr_node = list;\n int index = 0;\n while (curr_node != NULL) " +
            "{\n if (memcmp(curr_node->value, value, value_size) == 0)\n { return index; }\n curr_node = curr_node->next;\n index++;\n }\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\n exit(EXIT_FAILURE);\n}\nvoid *ValueOfList(struct node *list, int index)\n" +
            "{\n struct node *curr_node = list;\n int i;\n for (i = 0; i < index; i++) {\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n " +
            "exit(EXIT_FAILURE);\n }\n curr_node = curr_node->next;\n }\n if (curr_node == NULL) {\n fprintf(stderr, \"Error: Invalid index\\n\");\n exit(EXIT_FAILURE);\n }\n " +
            "return curr_node->value;\n}\nint f(int x)\n{\nreturn (x + 5);}\nvoid main(){\nif((5 > 4))\n{\nf(10);\n}\n}\n";

        // Assert
        Assert.That(sb.ToString(), Is.EqualTo(testString));
    }

}