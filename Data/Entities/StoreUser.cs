using Microsoft.AspNetCore.Identity;

namespace gestao.Data.Entities
{
    //  Comentários:
    // Aqui começarei a trabalhar com o Loggin da aplicação. Por padrão para isso funcionar
    // podemos o Identity que faz parte do E.F Core, usando a classe Base IdentityUser. 
    // Olha essa classe Base, podemos ver que ela já com várias propriedades prontas, como Email,
    // UserName etc. Sendo assim, esse StoreUser já vai ter um  monte de propriedades, como 
    // Email, Username, PhoneNumber
    // Mas podemos ter nossas proprias propriedades.
    // Ao criar essa classe ir lá no AppGestaoContext é mudar a classe base para IdentityDbContext.
    // Instalar o pacote Microsoft.AspNetCore.Identity.EntityFrameworkCore.    
    public class StoreUser: IdentityUser
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }

    }
}