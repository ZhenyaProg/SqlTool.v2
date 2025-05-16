namespace SQLTool.v2.Insert;

public class InsertBuilder
{
    private readonly string _table;
    private readonly ColumnsBuilder _columnsBuilder;
        
    private InsertBuilder(string table, ColumnsBuilder columnsBuilder)
    {
        _table = table;
        _columnsBuilder = columnsBuilder;
    }
        
    public static InsertBuilder Empty(string table, ColumnsBuilder columnsBuilder) => new(table, columnsBuilder);
    
    public string Build()
    {
        string columns = String.Join(", ", _columnsBuilder.Columns.Select(column => column.Item1));
        string values = String.Join(", ", _columnsBuilder.Columns.Select(column => column.Item2));
        return $"INSERT INTO {_table}({columns}) VALUES ({String.Join(", ", values)})";
    }
}