using GitHub.Core;
using Microsoft.Extensions.Options;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace GitHub.Services.services
{
    public class GitHubService : IGitHubService
    {

        private readonly GitHubClient _client;
        private readonly GitHubIntegrationOptions _option;

        public GitHubService(IOptions<GitHubIntegrationOptions> options)
        {
            _client = new GitHubClient(new ProductHeaderValue("GitHubService"));
            _option = options.Value;
            //_client.Credentials = new Credentials(); // טוקן אישי
        }
        public async Task<IReadOnlyList<Repository>> GetPortfolioAsync()
        {
            var repos = await _client.Repository.GetAllForUser(_option.UserName);
            return repos;
        }
        public async Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? repoName, string? language, string? username)
        {
            var queryParts = new List<string>();

            if (!string.IsNullOrWhiteSpace(repoName))
                queryParts.Add($"{repoName} in:name");

            if (!string.IsNullOrWhiteSpace(language))
                queryParts.Add($"language:{language}");

            if (!string.IsNullOrWhiteSpace(username))
                queryParts.Add($"user:{username}");

            var query = string.Join(" ", queryParts);

            var request = new SearchRepositoriesRequest(query);
            var result = await _client.Search.SearchRepo(request);
            return result.Items;
        }

    }
}
