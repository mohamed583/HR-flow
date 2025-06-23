using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend_projetdev.Application.DTOs;

namespace backend_projetdev.Application.Interfaces
{
    public interface ICvMatchingService
    {
        Task<List<CvMatchResultDto>> GetMatchingsAsync(int posteId);
    }

    
}
