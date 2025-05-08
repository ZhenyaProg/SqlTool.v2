namespace SQLTool.v2
{
    public class InnerJoinSource : AbstractSource
    {
        private readonly string _source;
        private readonly string? _condition;

        public InnerJoinSource(string source, string? condition = null)
        {
            _source = source;
            _condition = condition;
        }

        public override string ForSelect => _source;

        public override void AppendSource(SourcesWrapper wrapper)
        {
            if(_condition is not null)
                wrapper.JoinsSB.AppendLine($"INNER JOIN {_source} ON {_condition}");
            else
                wrapper.JoinsSB.AppendLine($"INNER JOIN {_source}");
        }
    }
}