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

        public InsertBuilder Insert(string table, Action<InsertBuilder> into)
        {
            var insertBuilder = InsertBuilder.Empty(table);
            into(insertBuilder);
            return insertBuilder;
        }

        public UpdateBuilder Update(string table, Action<UpdateBuilder> updated)
        {
            var updateBuilder = UpdateBuilder.Empty(table);
            updated(updateBuilder);
            return updateBuilder;
        }

        public DeleteBuilder Delete(string from)
        {
            var updateBuilder = DeleteBuilder.Empty(from);
            return updateBuilder;
        }
    }
}