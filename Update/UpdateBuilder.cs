using System.Text;
using SQLTool.v2.Select;

namespace SQLTool.v2.Update;

public class UpdateBuilder
{
    private readonly WhereBuilder _whereBuilder;
    private readonly ColumnsBuilder _columnsBuilder;
    private readonly string _table;
        
    private UpdateBuilder(string table, ColumnsBuilder columnsBuilder)
    {
        _whereBuilder = WhereBuilder.Empty();
        _table = table;
        _columnsBuilder = columnsBuilder;
    }

    public static UpdateBuilder Empty(string table, ColumnsBuilder columnsBuilder) => new(table, columnsBuilder);
    
    public UpdateBuilder Where(Action<WhereBuilder> action)
    {
        action(_whereBuilder);
        return this;
    }
        
    public string Build()
    {
        if (_columnsBuilder.ColumnsCount == 0) return String.Empty;
        StringBuilder sb = new StringBuilder($"UPDATE {_table}{Environment.NewLine}");
        sb.Append($"SET {String.Join(", ", _columnsBuilder.Columns.Select(group => $"{group.Item1} = {group.Item2}"))}");
        sb.Append(Environment.NewLine);
        sb.Append(_whereBuilder);
        return sb.ToString();
    }
}