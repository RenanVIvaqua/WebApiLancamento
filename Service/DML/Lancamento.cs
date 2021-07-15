using System;
using System.ComponentModel;

namespace Service.DML
{
    public class Lancamento
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public string Descricao { get; set; }

        public string Conta { get; set; }

        public char Tipo { get; set; }
    }

    public enum TipoConta
    {
        [Description("N�o Definido")]
        N = 'N',
        [Description("Cr�dito")]
        C = 'C',
        [Description("D�bito")]
        D = 'D'
    }
}