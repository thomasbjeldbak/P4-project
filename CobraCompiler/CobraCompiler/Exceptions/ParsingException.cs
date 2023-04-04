public class SyntaxException : Exception
{
    public int Line { get; }
    public int Column { get; }

    public SyntaxException(int line, int column, string message)
        : base(message)
    {
        Line = line;
        Column = column;
    }
}

public class SymbolNotFoundException : Exception
    {
        public SymbolNotFoundException(string name)
            : base($"SymbolNotFoundException: Symbol '{name}' not found.")
        {
        } 
    }
