using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uHelpDesk.BLL.DTOS
{
    public class CustomFieldDTO
    {
        public int CustomFieldId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
