using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class FileDto
    {
        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}