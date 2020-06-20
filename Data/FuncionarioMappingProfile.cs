using AutoMapper;
using gestao.Data.Entities;
using gestao.ViewModels;

namespace gestao.Data
{
    //  Comentarios:
    // Aqui temos que herdar de Profile. Ele é apenas um Container. Observar o métodp
    // ForMember. Estava acontecendo o seguinte. Antes deu declarar ele, quando eu 
    // cadastrava o funcionario lá no PostMan, e depois dava um Get para buscá-la, o id 
    // do funcionario vinha zerado, mesmo embora o banco de dados tenha gerado ele. 
    // Esse ForMember tá dizendo o seguinte: quando eu olhar o funcionarioid, mapeia ele 
    // para a fonte FuncionarioId. 
    public class FuncionarioMappingProfile: Profile
    {
        public FuncionarioMappingProfile()
        {
            CreateMap<Funcionario, FuncionarioViewModel>()
            .ForMember(fuc => fuc.funcionarioid, ex => ex.MapFrom(fuc => fuc.FuncionarioId))
            .ReverseMap();
        }
    }
}