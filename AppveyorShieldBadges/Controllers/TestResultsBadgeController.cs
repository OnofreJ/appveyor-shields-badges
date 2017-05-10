using System.Net.Http;
using AppveyorShieldBadges.Models;
using AppveyorShieldBadges.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AppveyorShieldBadges.Controllers
{
    [Route("api/testResults")]
    public class TestResultsBadgeController : Controller
    {
        private readonly IShieldBadgeService _badgeService = new ShieldBadgeService();

        [Produces("image/svg+xml")]
        [HttpGet("{username}/{projectSlug}/badge.svg")]
        public string Get(string username, string projectSlug)
        {
            var results = GetAppVeyorTestsResults(username, projectSlug);

            if (results.BuildRunning) return _badgeService.GetBadge("tests", $"pending", "lightgrey"); 

            return results.FailingCount == 0 ? 
                _badgeService.GetBadge("tests", $"{results.PassingCount} passing", "brightgreen") : 
                _badgeService.GetBadge("tests", $"{results.FailingCount} failed", "red");
        }

        private TestResults GetAppVeyorTestsResults(string username, string projectSlug, string branchName = "master")
        {
            long passedTestsCount = 0;
            long failedTestsCount = 0;
            bool running = false;

            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(AppVeyorLatestBuildApiAddress(username, projectSlug, branchName)).Result)
                {
                    response.EnsureSuccessStatusCode();

                    var json = JToken.Parse(response.Content.ReadAsStringAsync().Result);

                    foreach (var job in json["build"]["jobs"])
                    {
                        passedTestsCount += job.Value<int>("passedTestsCount");
                        failedTestsCount += job.Value<int>("failedTestsCount");
                        running = running | (job.Value<string>("status") == "running" || job.Value<string>("status") == "queued");
                    }
                }
            }

            return new TestResults(running, passedTestsCount, failedTestsCount);
        }

        private string AppVeyorLatestBuildApiAddress(string username, string projectSlug, string branchName)
        {
            return $"https://ci.appveyor.com/api/projects/{username}/{projectSlug}/branch/{branchName}";
        }
    }
}
