namespace GameShelf.Domain.Models.Projections.RawG
{

    public class RawGListGamesResultProjection
    {

        public bool Success { get; set; }
        public int QuantidadeTotalPaginas { get; set; }
        public int count { get; set; }
        public List<RawGGameProjection> results { get; set; } = [];

        public void SetStatusResult(bool status, int page_size)
        {
            Success = status;
            QuantidadeTotalPaginas = count / page_size;
        }

    }

    public class RawGGameProjection
    {
        public string name { get; set; }
        public string background_image { get; set; }
        public List<RawGPlataformaProjection> platforms { get; set; } = [];
        public List<RawGGeneroProjection> genres { get; set; } = [];
    }

    public class RawGPlataformaProjection
    {
        public string name { get; set; }
    }

    public class RawGGeneroProjection
    {
        public string name { get; set; }
    }

}
