using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IPersonneService
    {
        Task<Personne?> GetByIdAsync(string id);
    }
}
