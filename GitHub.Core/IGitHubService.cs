using Octokit;

namespace GitHub.Core
{
    public interface IGitHubService
    {
        Task<IReadOnlyList<Repository>> GetPortfolioAsync();
        Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? repoName, string? language, string? username);

    }
}