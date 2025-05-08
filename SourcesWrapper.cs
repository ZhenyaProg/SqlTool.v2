using System.Text;

namespace SQLTool.v2
{
    public class SourcesWrapper
    {
        public StringBuilder FromSB { get; init; } = new StringBuilder("FROM ");
        public StringBuilder JoinsSB { get; init; } = new StringBuilder();

        public override string ToString()
        {
            if (FromSB[FromSB.Length - 2] == ',')
                FromSB.Length = FromSB.Length - 2;
            if(JoinsSB.Length > 0)
                return $"{FromSB}{Environment.NewLine}{JoinsSB}";
            else
                return $"{FromSB}";
        }
    }
}