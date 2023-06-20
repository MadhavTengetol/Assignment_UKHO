using Assignment_UKHO.Data;
using System.Text.Json.Serialization;

namespace Assignment_UKHO.Dto
{
    public class BatchDto
    {
        [JsonIgnore]
        public Guid BatchId { get; set; }

        public string BusinessUnit { get; set; }
        public Acl Acl { get; set; }
        public List<Attributes> Attributes { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.Today;
    }
}
