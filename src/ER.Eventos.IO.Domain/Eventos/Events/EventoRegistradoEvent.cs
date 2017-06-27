using ER.Eventos.IO.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ER.Eventos.IO.Domain.Eventos.Events
{
    public class EventoRegistradoEvent : BaseEventoEvent
    {
        public EventoRegistradoEvent(
            Guid id,
            string nome,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeDaEmpresa)
        {
            Id = id;
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeDaEmpresa = nomeDaEmpresa;

            AggregateId = id;
        }
    }
}
