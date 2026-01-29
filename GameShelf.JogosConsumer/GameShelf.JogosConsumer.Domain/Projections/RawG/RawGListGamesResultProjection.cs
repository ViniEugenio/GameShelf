using System.Text.Json.Serialization;

namespace GameShelf.JogosConsumer.Domain.Projections.RawG
{

    public class RawGListGamesResultProjection
    {

        [JsonPropertyName("results")]
        public List<RawGGameProjection> Games { get; set; }

        [JsonPropertyName("count")]
        public int QuantidadeTotalDeJogos { get; set; }

    }

    public class RawGGameProjection
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("background_image")]
        public string Image { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("genres")]
        public List<RawGGenreProjection> Generos { get; set; }

        [JsonPropertyName("platforms")]
        public List<RawGPlatformDetailsProjection> Plataformas { get; set; }

    }

    public class RawGGenreProjection
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

    public class RawGPlatformDetailsProjection
    {

        [JsonPropertyName("platform")]
        public RawGPlataformProjection Platform { get; set; }
    }

    public class RawGPlataformProjection
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

}
