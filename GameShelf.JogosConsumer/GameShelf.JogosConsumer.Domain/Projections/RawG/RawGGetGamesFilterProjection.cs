using Refit;

namespace GameShelf.JogosConsumer.Domain.Projections.RawG
{
    public class RawGGetGamesFilterProjection(string key, int page, int pageSize)
    {

        [AliasAs("key")]
        public string Key { get; set; } = key;

        [AliasAs("page")]
        public int Page { get; set; } = page;

        [AliasAs("page_size")]
        public int PageSize { get; set; } = pageSize;
    }
}
