namespace NetBank.Api.Models
{
    public class ErrorModel
    {
        public int Status { get; set; }

        public Dictionary<string, IEnumerable<string>> Errors { get; set; }

        public ErrorModel(int status)
        {
            Errors = new Dictionary<string, IEnumerable<string>>();
            Status = status;
        }
    }
}
