using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;

namespace CobraCompiler;

public class ErrorListener : IAntlrErrorListener<CommonToken>
{
    public void SyntaxError(IRecognizer recognizer, CommonToken offendingSymbol, int line, int charPositionInLine, string message, RecognitionException e)
    {
        // Handle syntax errors
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