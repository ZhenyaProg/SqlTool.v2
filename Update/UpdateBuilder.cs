using System.Text;
using SQLTool.v2.Select;

namespace SQLTool.v2.Update;

public class UpdateBuilder
{
    private readonly WhereBuilder _whereBuilder;
    private readonly List<(string, object)> _updated = [];
    private readonly string _table;
        
    private UpdateBuilder(string table)
    {
        _whereBuilder = WhereBuilder.Empty();
        _table = table;
    }

    public static UpdateBuilder Empty(string table) => new(table);
        
    public UpdateBuilder Column(string columnName, object value)
    {
        _updated.Add((columnName, value));
        return this;
    }
        
    public UpdateBuilder Where(Action<WhereBuilder> action)
    {
        action(_whereBuilder);
        return this;
    }
        
    public string Build()
    {
        if(_updated.Count == 0) return String.Empty;
        StringBuilder sb = new StringBuilder($"UPDATE {_table}{Environment.NewLine}");
        sb.Append($"SET {String.Join(", ", _updated.Select(group => $"{group.Item1} = {group.Item2}"))}");
        sb.Append(Environment.NewLine);
        sb.Append(_whereBuilder);
        return sb.ToString();
    }
}