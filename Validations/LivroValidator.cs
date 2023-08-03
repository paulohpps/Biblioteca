using Biblioteca.Models;
using FluentValidation;
using System;

namespace Biblioteca.Validations
{
    public class LivroValidator : AbstractValidator<Livro>
    {
        public LivroValidator()
        {
            RuleFor(livro => livro.Titulo)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(livro => livro.Autor)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(livro => livro.Ano)
                .NotEmpty()
                .GreaterThan(1800)
                .LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(livro => livro.ValorVenda)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("O Valor da Venda deve ser maior que R$0,00");

            RuleFor(livro => livro.ValorAluguel)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("O Valor do Aluguel deve ser maior que R$0,00");

        }
    }
}
