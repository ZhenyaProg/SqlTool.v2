namespace SQLTool.v2
{
    public class FromSource : AbstractSource
    {
        private readonly string _source;

        public FromSource(string source)
        {
            _source = source;
        }

        public override string ForSelect => _source;

        public override void AppendSource(SourcesWrapper wrapper)
        {
            wrapper.FromSB.Append($"{_source}, ");
        }
    }
}