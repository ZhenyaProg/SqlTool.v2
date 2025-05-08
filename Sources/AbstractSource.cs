namespace SQLTool.v2
{
    public abstract class AbstractSource
    {
       public abstract string ForSelect { get; }

        public abstract void AppendSource(SourcesWrapper wrapper);
    }
}