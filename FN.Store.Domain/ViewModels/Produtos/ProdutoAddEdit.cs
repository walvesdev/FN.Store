using FN.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Domain.ViewModels.Produtos
{
    public class ProdutoAddEdit
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(maximumLength:100,  ErrorMessage = "Limite de cracteres excedido!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O preço é inválido!")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "A categoria é obrigatório!")]
        public int CategoriaId { get; set; }
    }
}
