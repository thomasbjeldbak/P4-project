using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;

namespace CobraCompiler;

public class ErrorHandler : BaseErrorListener, IAntlrErrorListener<IToken>
{
    private readonly List<string> _syntaxSyntaxErrorMessages = new();
    public IReadOnlyList<string> SyntaxErrorMessages => _syntaxSyntaxErrorMessages;

    private readonly List<string> _symbolErrorMessages = new();
    public List<string> SymbolErrorMessages => _symbolErrorMessages;

    private readonly List<string> _typeErrorMessages = new();
    public List<string> TypeErrorMessages => _typeErrorMessages;

    public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string message, RecognitionException e)
    {
        var error =
            $"Error line {line}: {message} Caused by {offendingSymbol.Text}.";
        _syntaxSyntaxErrorMessages.Add(error);
    }
}