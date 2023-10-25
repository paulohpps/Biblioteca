using System;

namespace Biblioteca.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int VendaId { get; set; }
        public decimal Valor { get; set; }
    }
}
