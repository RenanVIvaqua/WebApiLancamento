using System;
using System.Collections.Generic;

namespace Service.DML
{
    public class Totalizador
    {
        public Dictionary<string, decimal> Totalizadores { get; set; }

        public List<Lancamento> Lancamentos { get; set; }
    }
}
