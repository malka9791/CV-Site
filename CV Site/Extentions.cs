using GitHub.Core;
using GitHub.Services.services;

namespace CV_Site
{
    public static class Extentions
    {
        public static void AddGitHubIntegartions(this IServiceCollection services, Action<GitHubIntegrationOptions> action)
        {
            services.Configure(action);
            services.AddScoped<IGitHubService, GitHubService>();
        }
    }
}
