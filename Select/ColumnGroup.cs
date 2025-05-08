namespace SQLTool.v2
{
    public class ColumnGroup
    {
        public string[] Names { get; init; } = [];
        public AbstractSource Source { get; init; }
    }
}