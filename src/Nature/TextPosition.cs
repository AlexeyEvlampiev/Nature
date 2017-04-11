namespace Nature
{
    struct TextPosition
    {
        public TextPosition(int line, int column) : this() { this.Line = line; this.Column = column; }
        public readonly int Line;
        public readonly int Column;

        public static TextPosition[] Get(string text)
        {
            var positions = new TextPosition[text.Length];
            int line = 1;
            int column = 0;
            for (int i = 0; i < text.Length; ++i)
            {
                char c = text[i];
                column++;
                if (c == '\n') { line++; column = 0; }
                positions[i] = new TextPosition(line, column);
            }
            return positions;
        }

        public override string ToString() => $"({Line},{Column})";
    }
}
