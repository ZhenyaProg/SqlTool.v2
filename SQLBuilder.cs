using SQLTool.v2.Delete;
using SQLTool.v2.Insert;
using SQLTool.v2.Select;
using SQLTool.v2.Update;

namespace SQLTool.v2
{
    public class SqlBuilder
    {
        private SqlBuilder()
        {
        }

        public static SqlBuilder Empty() => new();

        public SelectBuilder Select(Action<SelectBuilder> action)
        {
            var selectBuilder = SelectBuilder.Empty();
            action(selectBuilder);
            return selectBuilder;
        }

        public InsertBuilder Insert(string table, Action<ColumnsBuilder> into)
        {
            ColumnsBuilder columnsBuilder = ColumnsBuilder.Empty();
            into(columnsBuilder);
            return InsertBuilder.Empty(table, columnsBuilder);
        }

        public UpdateBuilder Update(string table, Action<ColumnsBuilder> updated)
        {
            ColumnsBuilder columnsBuilder = ColumnsBuilder.Empty();
            updated(columnsBuilder);
            return UpdateBuilder.Empty(table, columnsBuilder);
        }

        public DeleteBuilder Delete(string from)
        {
            var updateBuilder = DeleteBuilder.Empty(from);
            return updateBuilder;
        }
    }
}