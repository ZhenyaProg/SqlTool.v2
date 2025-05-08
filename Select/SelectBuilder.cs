using System.Text;

namespace SQLTool.v2.Select
{
    public class SelectBuilder
    {
        private readonly List<ColumnGroup> _columns;
        private readonly WhereBuilder _whereBuilder;
        private readonly StringBuilder _forGroup, _forOrder;
        private bool _distinct;

        private SelectBuilder()
        {
            _columns = new List<ColumnGroup>();
            _whereBuilder = WhereBuilder.Empty();
            _forGroup = new StringBuilder();
            _forOrder = new StringBuilder();
        }
        
        public static SelectBuilder Empty() => new();

        public void Distinct() => _distinct = true;

        public SelectBuilder WithColumns(Func<SourceBuilder, AbstractSource> action, params string[] names)
        {
            _columns.Add(new ColumnGroup
            {
                Names = names,
                Source = action(SourceBuilder.Empty())
            });
            return this;
        }

        public SelectBuilder Where(Action<WhereBuilder> action)
        {
            action(_whereBuilder);
            return this;
        }

        public SelectBuilder GroupBy(params string[] grouped)
        {
            _forGroup.Append("GROUP BY ");
            _forGroup.Append(String.Join(", ", grouped));
            return this;
        }

        public SelectBuilder OrderBy(params string[] ordered)
        {
            _forOrder.Append("ORDER BY ");
            _forOrder.Append(String.Join(", ", ordered));
            return this;
        }

        public SelectBuilder OrderBy(int offset, int fetch, params string[] ordered)
        {
            _forOrder.Append("ORDER BY ");
            _forOrder.Append(String.Join(", ", ordered));
            _forOrder.Append($" OFFSET {offset} ROWS FETCH NEXT {fetch} ROWS ONLY");
            return this;
        }

        private string BuildSelect()
        {
            var sb = new StringBuilder();
            for(var i = 0; i < _columns.Count; i++)
            {
                sb.Append(String.Join(", ", _columns[i].Names.Select(name => $"{ _columns[i].Source.ForSelect}.{name}")));
                if (i != _columns.Count - 1)
                    sb.Append(", ");
            }
            return $"SELECT {(_distinct ? "DISTINCT " : "")}{sb}";
        }

        private string BuildSource()
        {
            SourcesWrapper sourcesWrapper = new SourcesWrapper();
            _columns.ForEach(column => column.Source.AppendSource(sourcesWrapper));
            return sourcesWrapper.ToString();
        }
        
        public string Build()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BuildSelect());
            sb.Append(Environment.NewLine);
            sb.Append(BuildSource());
            sb.Append(_whereBuilder);
            sb.Append(Environment.NewLine);
            sb.Append(_forGroup.ToString());
            sb.Append(Environment.NewLine);
            sb.Append(_forOrder.ToString());
            return sb.ToString();
        }
    }
    
    public class WhereBuilder
    {
        private readonly StringBuilder _sb = new();
        private readonly ConditionWithJoinBuilder _conditionWithJoinBuilder;

        private WhereBuilder()
        {
            _conditionWithJoinBuilder = new ConditionWithJoinBuilder();
        }

        public static WhereBuilder Empty() => new();

        public ConditionWithJoinBuilder WithCondition(string arg1, string operation, string arg2)
        {
            _sb.Append($"WHERE {arg1} {operation} {arg2}");
            return _conditionWithJoinBuilder;
        }

        public ConditionWithJoinBuilder WithCondition(string condition)
        {
            _sb.Append($"WHERE {condition}");
            return _conditionWithJoinBuilder;
        }

        public override string ToString()
        {
            _sb.Append(_conditionWithJoinBuilder);
            return _sb.ToString();
        }

        public class ConditionWithJoinBuilder
        {
            private readonly StringBuilder _sb = new();

            public ConditionWithJoinBuilder And(string arg1, string operation, string arg2)
            {
                _sb.Append($" AND {arg1} {operation} {arg2}");
                return this;
            }

            public ConditionWithJoinBuilder Or(string arg1, string operation, string arg2)
            {
                _sb.Append($" OR {arg1} {operation} {arg2}");
                return this;
            }

            public ConditionWithJoinBuilder And(string condition)
            {
                _sb.Append($" AND {condition}");
                return this;
            }

            public ConditionWithJoinBuilder Or(string condition)
            {
                _sb.Append($" OR {condition}");
                return this;
            }

            public override string ToString()
            {
                return _sb.ToString();
            }
        }
    }
}