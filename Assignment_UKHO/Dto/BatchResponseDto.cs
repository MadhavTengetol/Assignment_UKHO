using Assignment_UKHO.Data;
using System.Text.Json.Serialization;

namespace Assignment_UKHO.Dto
{
    public class BatchResponseDto
    {
       
        public Guid BatchId { get; set; }
        public string Status { get; set; } 

        public List<Attributes> Attributes { get; set; }
        public string BusinessUnit { get; set; }
        public DateTime BatchPublicationDate { get; set; } 

        public DateTime ExpiryDate { get; set; }

        public List<Files> Files { get; set; }
    }
}
