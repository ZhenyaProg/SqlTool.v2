using System.Text;

namespace SQLTool.v2
{
    public class SourcesWrapper
    {
        private List<string> _fromSources = [];
        private List<string> _joinsSources = [];
        
        public void AddFromSource(string source) => _fromSources.Add(source);
        public void AddInnerJoinSource(string source) => _joinsSources.Add(source);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"FROM {String.Join(", ", _fromSources)}{Environment.NewLine}");
            sb.Append($"{String.Join(Environment.NewLine, _joinsSources)}{Environment.NewLine}");
            return sb.ToString();
        }
    }
}