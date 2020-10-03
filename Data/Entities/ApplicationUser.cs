using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime DataIngressoSetor { get; set; }
        public string NomeCompleto { get; set; }
       
    }
}
