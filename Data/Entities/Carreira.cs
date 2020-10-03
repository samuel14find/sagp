using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestao.Data.Entities
{
   [Table("Carreiras")]
    public class Carreira
    {
        [Key]
        [MaxLength(20)]
        public string Iso5 { get; set; }

        [Required]
        [MaxLength(50)]
        public string NomeCarreira { get; set; }

        public IEnumerable<ProgressaoCarreira> ProgressoesCarreira { get; set; }
  
    }
}