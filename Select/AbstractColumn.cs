namespace SQLTool.v2.Select
{
    public abstract class AbstractColumn
    {
        protected AbstractColumn(string columnName)
        {
            ColumnName = columnName;
        }

        protected string ColumnName { get; }

        public abstract string GetColumn(string sourceName);
    }

    public class Column : AbstractColumn
    {
        public Column(string columnName) : base(columnName)
        {
        }

        public override string GetColumn(string sourceName)
        {
            return $"{sourceName}.{ColumnName}";
        }
    }

    public class AgrFunc : AbstractColumn
    {
        private readonly string _agregateFunction;

        public AgrFunc(string columnName, string agregateFunction) : base(columnName)
        {
            _agregateFunction = agregateFunction;
        }

        public override string GetColumn(string sourceName)
        {
            return $"{_agregateFunction}({sourceName}.{ColumnName})";
        }
    }
}