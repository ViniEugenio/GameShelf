namespace GameShelf.Domain.Models.Filters.RawG
{
    public class GetGamesFilter
    {

        public int page { get; set; }
        public int page_size { get; set; }

        public GetGamesFilter(int page, int page_size)
        {
            this.page = page;
            this.page_size = page_size;
        }
    }
}
