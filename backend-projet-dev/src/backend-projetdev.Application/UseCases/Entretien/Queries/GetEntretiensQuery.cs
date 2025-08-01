﻿using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Queries
{
    public class GetEntretiensQuery : IRequest<Result<List<EntretienDto>>> { }
}
