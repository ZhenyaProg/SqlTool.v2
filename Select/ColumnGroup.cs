namespace SQLTool.v2.Select
{
    public class ColumnGroup
    {
        public AbstractColumn[] Columns { get; init; } = [];
        public AbstractSource Source { get; init; }
    }
}