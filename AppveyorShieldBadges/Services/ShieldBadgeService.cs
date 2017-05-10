using System.Net.Http;

namespace AppveyorShieldBadges.Services
{
    internal interface IShieldBadgeService
    {
        string GetBadge(string subject, string status, string color);
    }

    internal class ShieldBadgeService : IShieldBadgeService
    {
        public string GetBadge(string subject, string status, string color)
        {
            var url = $"https://img.shields.io/badge/{Encode(subject)}-{Encode(status)}-{color}.svg";

            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        private string Encode(string input)
        {
            return input.Replace("-", "--").Replace("_", "__").Replace(" ", "_");
        }
    }
}
