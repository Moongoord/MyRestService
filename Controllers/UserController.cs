using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace MyRestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public UserController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            string url = "https://copy.mtapi.io/UserCopiers?userKey=a4b69dd1-5d3c-418b-87cd-7a31bab55503";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                List<UserCopier> users = JsonConvert.DeserializeObject<List<UserCopier>>(responseBody);

                return Ok(users);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Ошибка при запросе: {e.Message}");
            }
            catch (JsonException e)
            {
                return StatusCode(500, $"Ошибка при обработке JSON: {e.Message}");
            }
        }
    }

    public class UserCopier
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsSubscribed { get; set; }
        public required string UserKey { get; set; }
        public required string Link { get; set; }
        public AccountSummary? AccountSummary { get; set; }
        public List<SymbolMapping>? SymbolMappings { get; set; }
        public CopierSettings? CopierSettings { get; set; }
    }

    public class AccountSummary
    {
        public double balance { get; set; }
        public double equity { get; set; }
        public double margin { get; set; }
        public double freeMargin { get; set; }
    }

    public class SymbolMapping
    {
        [JsonProperty("slaveUser")]
        public string? SlaveUser { get; set; }

        [JsonProperty("slaveType")]
        public string? SlaveType { get; set; }

        [JsonProperty("masterServer")]
        public string? MasterServer { get; set; }

        [JsonProperty("masterUser")]
        public string? MasterUser { get; set; }

        [JsonProperty("riskMultiply")]
        public string? RiskMultiply { get; set; }

        [JsonProperty("userKey")]
        public string? UserKey { get; set; }

        [JsonProperty("masterPassword")]
        public string? MasterPassword { get; set; }

        [JsonProperty("masterType")]
        public string? MasterType { get; set; }

        [JsonProperty("slavePassword")]
        public string? SlavePassword { get; set; }

        [JsonProperty("slaveServer")]
        public string? SlaveServer { get; set; }

        public string? id { get; set; }
    }

    public class CopierSettings
    {
        public string? userKey { get; set; }
        public string? uniqueCopierId { get; set; }
        public string? masterType { get; set; }
        public int masterUser { get; set; }
        public string? masterPassword { get; set; }
        public string? masterServer { get; set; }
        public string? masterId { get; set; }
        public string? slaveType { get; set; }
        public int slaveUser { get; set; }
        public string? slavePassword { get; set; }
        public string? slaveServer { get; set; }
        public string? slaveId { get; set; }
        public DateTime timeLoadingStarted { get; set; }
        public DateTime timeLoaded { get; set; }
        public double riskMultiply { get; set; }
        public AccountSummary? masterAccountSummary { get; set; }
        public AccountSummary? slaveAccountSummary { get; set; }

        public Dictionary<string, object>? symbolParmeters { get; set; }

        public string? id { get; set; }
    }
}