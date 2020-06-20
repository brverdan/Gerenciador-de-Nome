using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AT_ASPNET.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataDeAniversario { get; set; }

        public int DiferencaAniversario()
        {
            DateTime dataDeHoje = DateTime.Today;
            DateTime proximaData = new DateTime(dataDeHoje.Year, DataDeAniversario.Month, DataDeAniversario.Day);

            if (proximaData < dataDeHoje)
            {
                proximaData = proximaData.AddYears(1);
            }

            int diferencaDeDias = (proximaData - dataDeHoje).Days;

            return diferencaDeDias;
        }
    }
}
