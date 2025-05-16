namespace SQLTool.v2
{
    public class ColumnsBuilder
    {
        private readonly List<(string, object)> _columns = [];
        
        private ColumnsBuilder() { }

        public static ColumnsBuilder Empty() => new ColumnsBuilder();

        public ColumnsBuilder Column(string columnName, object value)
        {
            _columns.Add((columnName, value));
            return this;
        }

        public int ColumnsCount => _columns.Count;

        public IReadOnlyList<(string, object)> Columns => _columns;
    }
}