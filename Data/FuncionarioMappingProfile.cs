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

            CreateMap<Ficha, FichaFuncionalViewModel>()
            .ForMember(fich => fich.fichaid, ex => ex.MapFrom(fich => fich.FichaId))
            .ReverseMap();
            //CreateMap<Tarefa, TarefaViewModel>()
            //    .ForMember(dest => dest.Titulo, opts => opts.MapFrom(src => src.Titulo))
            //    .ForMember(dest => dest.Descricao, opts => opts.MapFrom(src => src.Descricao))
            //    .ForMember(dest => dest.DataLimite, opts => opts.MapFrom(src => src.DataLimite))
            //    .ForMember(dest => dest.Feita, opts => opts.MapFrom(src => src.Feita))
            //    .ForMember(dest => dest.Notas, opts => opts.MapFrom(src => src.Notas))
            //    .ForMember(dest => dest.AtribuidoParaFuncionarioId, opts => opts.MapFrom(src => src.Funcionario))
            //    .ForMember(dest => dest.AtribuidoParaMatriculaDisplay, opts => opts.MapFrom(src => src.Funcionario))
            //    .ForMember(dest => dest.CategoriaId, opts => opts.MapFrom(src => src.CategoriaId));


            //CreateMap<TarefaViewModel, Tarefa>();
        }
    }
}