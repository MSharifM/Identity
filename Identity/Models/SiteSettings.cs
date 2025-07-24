using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class SiteSettings
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime? LastTimeChanged { get; set; }
    }
}
