using ER.Eventos.IO.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ER.Eventos.IO.Domain.Eventos.Events
{
    public class EventoAtualizadoEvent : BaseEventoEvent
    {
        public EventoAtualizadoEvent(
            Guid id,
            string nome,
            string descCurta,
            string descLonga,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeDaEmpresa)
        {
            Id = id;
            Nome = nome;
            DescricaoCurta = descCurta;
            DescricaoLonga = descLonga;
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
