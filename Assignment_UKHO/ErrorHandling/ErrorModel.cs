namespace Assignment_UKHO.ErrorHandling
{
    public class ErrorModel
    {
        public string CorrelationId { get; set; }
        public List<Errors> Errors { get; set; }
    }
}
