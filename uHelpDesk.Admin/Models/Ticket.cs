using System.ComponentModel.DataAnnotations;

namespace uHelpDesk.Admin.Models
{
    public class Ticket
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; } = "Open"; // Set default status as open

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
