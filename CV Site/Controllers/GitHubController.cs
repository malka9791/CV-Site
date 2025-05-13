using GitHub.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace CV_Site.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        public GitHubController(IGitHubService gitHubService)
        {
           _gitHubService = gitHubService;
        }
        [HttpGet("username")]
        public async Task<IReadOnlyList<Repository>> GetRepositories()
        {
            return await _gitHubService.GetPortfolioAsync();
        }
        [HttpGet("search")]
        public async Task<IReadOnlyList<Repository>> SearchRepositories(string? repoName, string? language, string? username)
        {
            return await _gitHubService.SearchRepositoriesAsync(repoName, language, username);
        }



    }
}
