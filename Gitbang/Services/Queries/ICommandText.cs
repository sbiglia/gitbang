namespace Gitbang.Services.Queries
{
    public interface ICommandText
    {
        string GetRepositories { get; }
        string AddRepository { get; }
        string UpdateRepository { get; }
        string RemoveRepository { get; }
        
        string GetGroups { get; }
        string AddGroup { get; }
        string RemoveGroup { get; }
        string UpdateGroup { get; }
    }
}