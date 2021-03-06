﻿using ER.Eventos.IO.Domain.Core.Models;
using ER.Eventos.IO.Domain.Organizadores;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace ER.Eventos.IO.Domain.Eventos
{
    public class Evento : Entity<Evento>
    {
        public Evento(
            string nome,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeDaEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeDaEmpresa = nomeDaEmpresa;
        }

        private Evento()
        {

        }

        public string Nome { get; private set; }

        public string DescricaoCurta { get; private set; }

        public string DescricaoLonga { get; private set; }

        public DateTime DataInicio { get; private set; }

        public DateTime DataFim { get; private set; }

        public bool Gratuito { get; private set; }

        public decimal Valor { get; private set; }

        public bool Online { get; private set; }

        public string NomeDaEmpresa { get; private set; }

        public Categoria Categoria { get; private set; }

        public ICollection<Tag> Tags { get; private set; }

        public Endereco Endereco { get; private set; }

        public Organizador Organizador { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações

        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeDaEmpresa)
                .NotEmpty().WithMessage("")
                .Length(2, 150).WithMessage("");
        }

        private void ValidarLocal()
        {
            if (Online)
                RuleFor(c => c.Endereco)
                    .Null().When(c => c.Online)
                    .WithMessage("");

            if (!Online)
                RuleFor(c => c.Endereco)
                    .NotNull().When(c => !c.Online)
                    .WithMessage("");
        }

        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                .GreaterThan(c => c.DataFim)
                .WithMessage("");

            RuleFor(c => c.DataInicio)
                .LessThan(DateTime.Now)
                .WithMessage("");
        }

        private void ValidarValor()
        {
            if (!Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000).WithMessage("O valor deve estar entre 1 e 50000");

            if (Gratuito)
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(0, 0).When(e => e.Gratuito)
                    .WithMessage("O valor não deve ser diferetente de 0 para um evento gratuito");
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do evento precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome do evento precisa ter entre 2 e 150 caracteres");
        }

        #endregion

        public static class EventoFactory
        {
            public static Evento NovoEventoCompleto(Guid id, string nome, string descCurta, string descLonga, DateTime dataInicio, DateTime dataFim, bool gratuito, decimal valor, bool online, string nomeDaEmpresa, Guid? organizadorId)
            {
                var evento = new Evento
                {
                    Id = id,
                    Nome = nome,
                    DescricaoCurta = descCurta,
                    DescricaoLonga = descLonga,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Gratuito = gratuito,
                    Valor = valor,
                    Online = online,
                    NomeDaEmpresa = nomeDaEmpresa
                };

                if (organizadorId.HasValue)
                    evento.Organizador = new Organizador(organizadorId.Value);

                return evento;
            }
        }
    }
}
