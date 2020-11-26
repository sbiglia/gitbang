namespace Gitbang.Services.Queries
{
    public class CommandText : ICommandText
    {
        public string GetRepositories => "SELECT id as Id, name as Name, group_id as GroupId FROM repositories";
        public string AddRepository => "";
        public string UpdateRepository => "";
        public string RemoveRepository => "";
        public string GetGroups => "SELECT id as Id, name as Name, parent_group_id as ParentGroupId FROM groups";
        public string AddGroup => "";
        public string RemoveGroup => "";
        public string UpdateGroup => "";
    }
}