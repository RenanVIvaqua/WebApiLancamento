using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Service.DML;

namespace WebApiCadastroLancamento.Model
{
    /// <summary>
    /// Classe modelo de entrada Lan�amento
    /// </summary>
    public class LancamentoModel
    {            
        [Required]
        public DateTime Data { get; set; }

        [Required, Range(0.0, Double.MaxValue, ErrorMessage = "No campo {0} n�o � permitido informar valor negativo.")]        
        public decimal Valor { get; set; }

        [Required, StringLength(60, ErrorMessage = "O campo {0} deve ser uma string com comprimento m�ximo de 60 caracteres.")]
        public string Descricao { get; set; }

        [Required, StringLength(30, ErrorMessage = "O campo {0} deve ser uma string com comprimento m�ximo de 30 caracteres.")]
        public string Conta { get; set; }

        [Required, MinLength(1), MaxLength(1)]
        public string Tipo { get; set; }
    }
}