using AutoMapper;
using gestao.Data.Entities;
using gestao.ViewModels;

namespace gestao.Data
{
    //  Comentarios:
    // Aqui temos que herdar de Profile. Ele é apenas um Container. Observar o métodp
    // ForMember. Estava acontecendo o seguinte. Antes deu declarar ele, quando eu 
    // cadastrava o ficha lá no PostMan, e depois dava um Get para buscá-la, o id 
    // da ficha vinha zerado, mesmo embora o banco de dados tenha gerado ele. 
    // Esse ForMember tá dizendo o seguinte: quando eu olhar o fichaid, mapeia ele 
    // para a fonte fichafuncid. 
    public class FichaFuncionalMappingProfile: Profile
    {
        public FichaFuncionalMappingProfile()
        {
            CreateMap<FichaFuncional, FichaFuncionalViewModel>()
            .ForMember(f => f.fichaid, ex => ex.MapFrom(f => f.fichafuncId))
            .ReverseMap();
        }
    }
}