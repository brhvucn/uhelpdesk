using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            DateTime currentDateTime = DateTime.UtcNow;
            this.CreatedAt = currentDateTime;
            this.UpdatedAt = currentDateTime;
        }
        public int Id { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
