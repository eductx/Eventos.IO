using ER.Eventos.IO.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ER.Eventos.IO.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
