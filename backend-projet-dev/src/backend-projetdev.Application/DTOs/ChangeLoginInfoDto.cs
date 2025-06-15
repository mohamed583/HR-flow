using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class ChangeLoginInfoDto
    {
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }
    }
}
