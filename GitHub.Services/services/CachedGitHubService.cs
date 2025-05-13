using GitHub.Core;
using Microsoft.Extensions.Caching.Memory;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub.Services.services
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;

        private const string UserPortFolioKey = "UserPortFolioKey";
        public CachedGitHubService(IGitHubService gitHubService, IMemoryCache memoryCache)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
        }

        public async Task<IReadOnlyList<Repository>> GetPortfolioAsync()
        {
            if (_memoryCache.TryGetValue(UserPortFolioKey, out IReadOnlyList<Repository> portfolio))
                return portfolio;
            var cachedOptions=new MemoryCacheEntryOptions().
                SetAbsoluteExpiration(TimeSpan.FromSeconds(30)).
                SetSlidingExpiration(TimeSpan.FromSeconds(10));

            portfolio = await _gitHubService.GetPortfolioAsync();
            _memoryCache.Set(UserPortFolioKey, portfolio,cachedOptions);
            return portfolio;
        }

        public async Task<IReadOnlyList<Repository>> SearchRepositoriesAsync(string? repoName, string? language, string? username)
        {
            return await _gitHubService.SearchRepositoriesAsync(repoName, language, username);
        }
    }
}
