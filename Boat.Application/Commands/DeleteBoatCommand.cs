using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Application.Commands;

public record DeleteBoatCommand(Guid Id) : IRequest<bool>;

