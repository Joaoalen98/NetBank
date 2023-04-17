namespace NetBank.DTOs
{
    public class ErroDTO
    {
        public int Status { get; set; }

        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public ErroDTO(int status)
        {
            Errors = new Dictionary<string, IEnumerable<string>>();
            Status = status;
        }
    }
}
