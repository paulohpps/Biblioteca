using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class CadEmprestimoViewModel
    {
        public ICollection<Livro> Livros { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public Emprestimo Emprestimo { get; set; }
    }
}