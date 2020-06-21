using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AT_ASPNET.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
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
