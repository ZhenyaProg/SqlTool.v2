namespace SQLTool.v2
{
    public class SourceBuilder
    {
        private SourceBuilder()
        {
        }

        public static SourceBuilder Empty() => new();

        public AbstractSource From(string source)
        {
            return new FromSource(source);
        }

        public AbstractSource InnerJoin(string source, string? on = null)
        {
            return new InnerJoinSource(source, on);
        }
    }
}