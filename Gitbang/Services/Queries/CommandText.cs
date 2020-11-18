namespace Gitbang.Services.Queries
{
    public class CommandText : ICommandText
    {
        public string GetRepositories => "SELECT * FROM repositories";
        public string AddRepository => "";
        public string UpdateRepository => "";
        public string RemoveRepository => "";
        public string GetGroups => "SELECT * FROM groups";
        public string AddGroup => "";
        public string RemoveGroup => "";
        public string UpdateGroup => "";
    }
}