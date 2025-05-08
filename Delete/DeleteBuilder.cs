using SQLTool.v2.Select;

namespace SQLTool.v2.Delete;

public class DeleteBuilder
{
    private readonly WhereBuilder _whereBuilder;
    private readonly string _table;
        
    private DeleteBuilder(string table)
    {
        _whereBuilder = WhereBuilder.Empty();
        _table = table;
    }

    public static DeleteBuilder Empty(string table) => new(table);

    public DeleteBuilder Where(Action<WhereBuilder> where)
    {
        where(_whereBuilder);
        return this;
    }

    public string Build()
    {
        return $"DELETE FROM {_table} {_whereBuilder}";
    }
}