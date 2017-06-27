using ER.Eventos.IO.Domain.CommandHandlers;
using ER.Eventos.IO.Domain.Core.Bus;
using ER.Eventos.IO.Domain.Core.Events;
using ER.Eventos.IO.Domain.Core.Notifications;
using ER.Eventos.IO.Domain.Eventos.Commands;
using ER.Eventos.IO.Domain.Eventos.Events;
using ER.Eventos.IO.Domain.Eventos.Repository;
using ER.Eventos.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ER.Eventos.IO.Domain.Eventos.Commands
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>,
        IHandler<AtualizarEventoCommand>,
        IHandler<ExcluirEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IBus _bus;

        public EventoCommandHandler(IEventoRepository eventoRepository, 
                                    IUnitOfWork uow, 
                                    IBus bus, 
                                    IDomainNotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _eventoRepository = eventoRepository;
            _bus = bus;
        }

        public void Handle(RegistrarEventoCommand message)
        {
            var evento = new Evento(message.Nome, message.DataInicio, message.DataFim, message.Gratuito, message.Valor, message.Online, message.NomeDaEmpresa);

            if(!EventoValido(evento)) return;

            _eventoRepository.Add(evento);

            if(Commit())
            {
                _bus.RaiseEvent(new EventoRegistradoEvent(evento.Id, evento.Nome, evento.DataInicio, evento.DataFim, evento.Gratuito, evento.Valor, evento.Online, evento.NomeDaEmpresa));
            }
        }

        public void Handle(AtualizarEventoCommand message)
        {
            if (!EventoExistente(message.Id, message.MessageType)) return;

            var evento = Evento.EventoFactory.NovoEventoCompleto(message.Id, message.Nome, message.DescricaoCurta, 
                message.DescricaoLonga, message.DataInicio, message.DataFim, message.Gratuito, message.Valor, 
                message.Online, message.NomeDaEmpresa, null);

            if (!EventoValido(evento)) return;

            _eventoRepository.Update(evento);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoAtualizadoEvent(evento.Id, evento.Nome, evento.DescricaoCurta, evento.DescricaoLonga, evento.DataInicio, evento.DataFim, evento.Gratuito, evento.Valor, evento.Online, evento.NomeDaEmpresa));
            }
        }

        public void Handle(ExcluirEventoCommand message)
        {
            if (!EventoExistente(message.Id, message.MessageType)) return;

            _eventoRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoExcluidoEvent(message.Id));
            }
        }

        private bool EventoValido(Evento evento)
        {
            if (evento.EhValido()) return true;

            NotificarValidacoesErro(evento.ValidationResult);
            return false;
        }

        private bool EventoExistente(Guid id, string messageType)
        {
            var evento = _eventoRepository.GetById(id);

            if (evento != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado."));
            return false;
        }
    }
}
