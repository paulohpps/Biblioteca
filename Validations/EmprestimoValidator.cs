using Biblioteca.Models;
using FluentValidation;
using System;

namespace Biblioteca.Validations
{
    public class EmprestimoValidator : AbstractValidator<Emprestimo>
    {
        public EmprestimoValidator()
        {
            RuleFor(e => e.NomeUsuario)
                .NotNull()
                .WithMessage("O 'Usuário' deve ser informado");

            RuleFor(e => e.Telefone)
                .NotNull()
                .WithMessage("O 'Telefone' deve ser informado")
                .MinimumLength(10)
                .MaximumLength(11);

            RuleFor(e => e.LivroId)
                .NotNull()
                .WithMessage("O 'Livro' deve ser informado");

            RuleFor(e => e.DataEmprestimo)
                .NotNull()
                .WithMessage("A 'Data de Empréstimo' deve ser informada")
                .GreaterThan(DateTime.Now.AddMonths(-12))
                .WithMessage("A 'Data de Empréstimo' deve ser maior que 1 ano atrás");

            RuleFor(e => e.DataDevolucao)
                .NotNull()
                .WithMessage("A 'Data de Devolução' deve ser informada")
                .GreaterThan(e => e.DataEmprestimo)
                .WithMessage("A 'Data de Devolução' deve ser Maior que a 'Data de Emprestimo'");
        }
    }
}
