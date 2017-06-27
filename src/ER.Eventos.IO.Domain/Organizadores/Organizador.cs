using System;
using ER.Eventos.IO.Domain.Core.Models;

namespace ER.Eventos.IO.Domain.Organizadores
{
    public class Organizador : Entity<Organizador>
    {
        public Organizador(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}