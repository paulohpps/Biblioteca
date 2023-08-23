using Biblioteca.Models;
using FluentValidation;

namespace Biblioteca.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario.Nome)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(usuario => usuario.Login)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(usuario => usuario.Senha)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}
