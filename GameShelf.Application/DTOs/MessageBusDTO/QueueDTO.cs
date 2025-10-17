namespace GameShelf.Application.DTOs.MessageBusDTO
{
    public class QueueDTO
    {

        public string Exchange { get; set; }
        public string Queue { get; set; }

        public QueueDTO(string exchange, string queue)
        {
            Exchange = exchange;
            Queue = queue;
        }

    }
}
