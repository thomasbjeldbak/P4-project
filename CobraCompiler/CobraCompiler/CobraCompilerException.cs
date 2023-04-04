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

    public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string message, RecognitionException e)
    {
        var error = $"Error: {message} at line {line}, position {charPositionInLine}. Caused by {offendingSymbol.Text}.";
        _syntaxSyntaxErrorMessages.Add(error);

        //throw new Exception(error);
    }
    
    public void ReportAmbiguity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, bool exact, BitSet ambigAlts, ATNConfigSet configs)
    {
        // Handle ambiguity errors
    }

    public void ReportAttemptingFullContext(Parser recognizer, DFA dfa, int startIndex, int stopIndex, BitSet conflictingAlts, SimulatorState conflictState)
    {
        // Handle full context errors
    }

    public void ReportContextSensitivity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, int prediction, SimulatorState acceptState)
    {
        // Handle context sensitivity errors
    }
}

public class SymbolNotFoundException : Exception
{
    public SymbolNotFoundException(string name)
        : base($"SymbolNotFoundException: Symbol '{name}' not found.")
    {
    } 
}