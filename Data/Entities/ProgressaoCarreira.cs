using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestao.Data.Entities
{
    [Table("Progressoes")]
    public class ProgressaoCarreira
    {
        [Key]
        [MaxLength(10)]
        public string ProgressaoCode { get; set; }
        public string NomeProgressao { get; set; }    
        public string Iso5 { get; set; }
         [ForeignKey("Iso5")]
        public Carreira Carreira { get; set; }
    }
}   