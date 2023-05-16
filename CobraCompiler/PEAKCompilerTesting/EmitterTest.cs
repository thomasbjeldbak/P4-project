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

        String testString = "#include <stdio.h>\r\n#include <stdlib.h>\r\n#include <string.h>\r\nchar* concat(const char *str1, const char *str2) " +
            "{\r\n size_t len1 = strlen(str1);\r\n size_t len2 = strlen(str2);\r\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\r\n strcpy(result, str1);\r\n " +
            "strcat(result, str2);\r\n return result;\r\n}\r\nvoid* input(char* format, size_t size) {\r\n void* input = malloc(size);\r\n int result = scanf(format, input);\r\n " +
            "if (result != 1) {\r\n fprintf(stderr, \"Error: Invalid input format\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n return input;\r\n}\r\nstruct node\n{\r\n void *value;\r\n " +
            "struct node *next;\r\n};\r\nvoid AddToList (struct node **list, void *value, size_t value_size){\r\n struct node *new_node = malloc(sizeof(struct node));\r\n " +
            "new_node->value = malloc(value_size);\r\n memcpy(new_node->value, value, value_size);\r\n new_node->next = NULL;\r\n if (*list == NULL) {\r\n *list = new_node;\r\n } " +
            "else {\r\n struct node *last_node = *list;\r\n while (last_node->next != NULL) {\r\n last_node = last_node->next;\r\n }\r\n last_node->next = new_node;\r\n }\r\n};\r\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\r\n{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL)" +
            " {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) " +
            "{\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node->value = value;\r\n}\r\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\r\n{\r\n struct node *curr_node = list;\r\n int index = 0;\r\n while (curr_node != NULL) " +
            "{\r\n if (memcmp(curr_node->value, value, value_size) == 0)\r\n { return index; }\r\n curr_node = curr_node->next;\r\n index++;\r\n }\r\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\r\n exit(EXIT_FAILURE);\r\n}\r\nvoid *ValueOfList(struct node *list, int index)\r\n" +
            "{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n " +
            "exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n " +
            "return curr_node->value;\r\n}\r\nvoid main(){\r\n}\r\n";
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

        String testString = "#include <stdio.h>\r\n#include <stdlib.h>\r\n#include <string.h>\r\nchar* concat(const char *str1, const char *str2) " +
            "{\r\n size_t len1 = strlen(str1);\r\n size_t len2 = strlen(str2);\r\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\r\n strcpy(result, str1);\r\n " +
            "strcat(result, str2);\r\n return result;\r\n}\r\nvoid* input(char* format, size_t size) {\r\n void* input = malloc(size);\r\n int result = scanf(format, input);\r\n " +
            "if (result != 1) {\r\n fprintf(stderr, \"Error: Invalid input format\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n return input;\r\n}\r\nstruct node\n{\r\n void *value;\r\n " +
            "struct node *next;\r\n};\r\nvoid AddToList (struct node **list, void *value, size_t value_size){\r\n struct node *new_node = malloc(sizeof(struct node));\r\n " +
            "new_node->value = malloc(value_size);\r\n memcpy(new_node->value, value, value_size);\r\n new_node->next = NULL;\r\n if (*list == NULL) {\r\n *list = new_node;\r\n } " +
            "else {\r\n struct node *last_node = *list;\r\n while (last_node->next != NULL) {\r\n last_node = last_node->next;\r\n }\r\n last_node->next = new_node;\r\n }\r\n};\r\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\r\n{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL)" +
            " {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) " +
            "{\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node->value = value;\r\n}\r\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\r\n{\r\n struct node *curr_node = list;\r\n int index = 0;\r\n while (curr_node != NULL) " +
            "{\r\n if (memcmp(curr_node->value, value, value_size) == 0)\r\n { return index; }\r\n curr_node = curr_node->next;\r\n index++;\r\n }\r\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\r\n exit(EXIT_FAILURE);\r\n}\r\nvoid *ValueOfList(struct node *list, int index)\r\n" +
            "{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n " +
            "exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n " +
            "return curr_node->value;\r\n}\r\nvoid main(){\r\nint x = 10;\r\n}\r\n";

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

        String testString = "#include <stdio.h>\r\n#include <stdlib.h>\r\n#include <string.h>\r\nchar* concat(const char *str1, const char *str2) " +
            "{\r\n size_t len1 = strlen(str1);\r\n size_t len2 = strlen(str2);\r\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\r\n strcpy(result, str1);\r\n " +
            "strcat(result, str2);\r\n return result;\r\n}\r\nvoid* input(char* format, size_t size) {\r\n void* input = malloc(size);\r\n int result = scanf(format, input);\r\n " +
            "if (result != 1) {\r\n fprintf(stderr, \"Error: Invalid input format\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n return input;\r\n}\r\nstruct node\n{\r\n void *value;\r\n " +
            "struct node *next;\r\n};\r\nvoid AddToList (struct node **list, void *value, size_t value_size){\r\n struct node *new_node = malloc(sizeof(struct node));\r\n " +
            "new_node->value = malloc(value_size);\r\n memcpy(new_node->value, value, value_size);\r\n new_node->next = NULL;\r\n if (*list == NULL) {\r\n *list = new_node;\r\n } " +
            "else {\r\n struct node *last_node = *list;\r\n while (last_node->next != NULL) {\r\n last_node = last_node->next;\r\n }\r\n last_node->next = new_node;\r\n }\r\n};\r\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\r\n{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL)" +
            " {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) " +
            "{\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node->value = value;\r\n}\r\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\r\n{\r\n struct node *curr_node = list;\r\n int index = 0;\r\n while (curr_node != NULL) " +
            "{\r\n if (memcmp(curr_node->value, value, value_size) == 0)\r\n { return index; }\r\n curr_node = curr_node->next;\r\n index++;\r\n }\r\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\r\n exit(EXIT_FAILURE);\r\n}\r\nvoid *ValueOfList(struct node *list, int index)\r\n" +
            "{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n " +
            "exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n " +
            "return curr_node->value;\r\n}\r\nint f(int x)\r\n{\r\nreturn (x + 5);}\r\nvoid main(){\r\n}\r\n";

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

        String testString = "#include <stdio.h>\r\n#include <stdlib.h>\r\n#include <string.h>\r\nchar* concat(const char *str1, const char *str2) " +
            "{\r\n size_t len1 = strlen(str1);\r\n size_t len2 = strlen(str2);\r\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\r\n strcpy(result, str1);\r\n " +
            "strcat(result, str2);\r\n return result;\r\n}\r\nvoid* input(char* format, size_t size) {\r\n void* input = malloc(size);\r\n int result = scanf(format, input);\r\n " +
            "if (result != 1) {\r\n fprintf(stderr, \"Error: Invalid input format\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n return input;\r\n}\r\nstruct node\n{\r\n void *value;\r\n " +
            "struct node *next;\r\n};\r\nvoid AddToList (struct node **list, void *value, size_t value_size){\r\n struct node *new_node = malloc(sizeof(struct node));\r\n " +
            "new_node->value = malloc(value_size);\r\n memcpy(new_node->value, value, value_size);\r\n new_node->next = NULL;\r\n if (*list == NULL) {\r\n *list = new_node;\r\n } " +
            "else {\r\n struct node *last_node = *list;\r\n while (last_node->next != NULL) {\r\n last_node = last_node->next;\r\n }\r\n last_node->next = new_node;\r\n }\r\n};\r\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\r\n{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL)" +
            " {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) " +
            "{\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node->value = value;\r\n}\r\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\r\n{\r\n struct node *curr_node = list;\r\n int index = 0;\r\n while (curr_node != NULL) " +
            "{\r\n if (memcmp(curr_node->value, value, value_size) == 0)\r\n { return index; }\r\n curr_node = curr_node->next;\r\n index++;\r\n }\r\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\r\n exit(EXIT_FAILURE);\r\n}\r\nvoid *ValueOfList(struct node *list, int index)\r\n" +
            "{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n " +
            "exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n " +
            "return curr_node->value;\r\n}\r\nint f(int x)\r\n{\r\nreturn (x + 5);}\r\nvoid main(){\r\nf(10);\r\n}\r\n";

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

        String testString = "#include <stdio.h>\r\n#include <stdlib.h>\r\n#include <string.h>\r\nchar* concat(const char *str1, const char *str2) " +
            "{\r\n size_t len1 = strlen(str1);\r\n size_t len2 = strlen(str2);\r\n char *result = malloc(strlen(str1) + strlen(str2) + 1);\r\n strcpy(result, str1);\r\n " +
            "strcat(result, str2);\r\n return result;\r\n}\r\nvoid* input(char* format, size_t size) {\r\n void* input = malloc(size);\r\n int result = scanf(format, input);\r\n " +
            "if (result != 1) {\r\n fprintf(stderr, \"Error: Invalid input format\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n return input;\r\n}\r\nstruct node\n{\r\n void *value;\r\n " +
            "struct node *next;\r\n};\r\nvoid AddToList (struct node **list, void *value, size_t value_size){\r\n struct node *new_node = malloc(sizeof(struct node));\r\n " +
            "new_node->value = malloc(value_size);\r\n memcpy(new_node->value, value, value_size);\r\n new_node->next = NULL;\r\n if (*list == NULL) {\r\n *list = new_node;\r\n } " +
            "else {\r\n struct node *last_node = *list;\r\n while (last_node->next != NULL) {\r\n last_node = last_node->next;\r\n }\r\n last_node->next = new_node;\r\n }\r\n};\r\nvoid " +
            "ReplaceInList(struct node *list, void *value, int index)\r\n{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL)" +
            " {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) " +
            "{\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n curr_node->value = value;\r\n}\r\nint " +
            "IndexOfList(struct node *list, void *value, size_t value_size)\r\n{\r\n struct node *curr_node = list;\r\n int index = 0;\r\n while (curr_node != NULL) " +
            "{\r\n if (memcmp(curr_node->value, value, value_size) == 0)\r\n { return index; }\r\n curr_node = curr_node->next;\r\n index++;\r\n }\r\n " +
            "fprintf(stderr, \"Error: Value not found in list\\n\");\r\n exit(EXIT_FAILURE);\r\n}\r\nvoid *ValueOfList(struct node *list, int index)\r\n" +
            "{\r\n struct node *curr_node = list;\r\n int i;\r\n for (i = 0; i < index; i++) {\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n " +
            "exit(EXIT_FAILURE);\r\n }\r\n curr_node = curr_node->next;\r\n }\r\n if (curr_node == NULL) {\r\n fprintf(stderr, \"Error: Invalid index\\n\");\r\n exit(EXIT_FAILURE);\r\n }\r\n " +
            "return curr_node->value;\r\n}\r\nint f(int x)\r\n{\r\nreturn (x + 5);}\r\nvoid main(){\r\nif((5 > 4))\r\n{\r\nf(10);\r\n}\r\n}\r\n";

        // Assert
        Assert.That(sb.ToString(), Is.EqualTo(testString));
    }

}