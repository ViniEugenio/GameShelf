using GameShelf.Application.DTOs.RawGDTO;
using GameShelf.Domain.Interfaces.ExternalServicesInterfaces;
using GameShelf.Domain.Models.Filters.RawG;
using GameShelf.Domain.Models.Projections.RawG;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GameShelf.Infrastructure.ExternalServices
{
    public class RawGService : IRawGService
    {

        private readonly HttpClient _httpClient;

        public RawGService(HttpClient httpClient, IOptions<RawGConfigurationDTO> rawGConfiguration)
        {

            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(rawGConfiguration.Value.EndPoint);
            _httpClient.DefaultRequestHeaders.Add("key", rawGConfiguration.Value.Key);

        }

        public async Task<RawGListGamesResultProjection> GetGames(GetGamesFilter filter)
        {

            RawGListGamesResultProjection result = new();

            _httpClient.DefaultRequestHeaders.Add("page", filter.page.ToString());
            _httpClient.DefaultRequestHeaders.Add("page_size", filter.page_size.ToString());

            HttpResponseMessage response = await _httpClient
                .GetAsync("/games");

            if (!response.IsSuccessStatusCode)
            {

                result.SetStatusResult(false);
                return result;

            }

            string json = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<RawGListGamesResultProjection>(json);
            result.SetStatusResult(true);

            return result;

        }

    }
}
