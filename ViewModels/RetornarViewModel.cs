using gestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.ViewModels
{
    public class RetornarViewModel
    {
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public int MensagemThreadId { get; set; }
        //public virtual List<Mensagem> Mensagens { get; set; }
    }
}
