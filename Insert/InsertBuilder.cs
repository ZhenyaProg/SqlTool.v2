namespace SQLTool.v2.Insert;

public class InsertBuilder
{
    private readonly string _table;
    private readonly List<string> _columns = [];
    private readonly List<object> _values = [];
        
    private InsertBuilder(string table)
    {
        _table = table;
    }
        
    public static InsertBuilder Empty(string table) => new(table);

    public InsertBuilder InColumn(string columnName, object values)
    {
        _columns.Add(columnName);
        _values.Add(values);
        return this;
    }
        
    public string Build()
    {
        return $"INSERT INTO {_table}({String.Join(", ", _columns)}) VALUES ({String.Join(", ", _values)})";
    }
}