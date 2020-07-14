using gestao.Data.Entities;

namespace gestao.Data
{
    public class FichaFuncionalRepository : IFichaFuncionalRepository
    {
        private readonly AppGestaoContext _appGestaoContext;
        private readonly CarrinhoFicha _carrinhoFicha;

        public FichaFuncionalRepository(AppGestaoContext appGestaoContext,
                                        CarrinhoFicha carrinhoFicha)
        {
            this._appGestaoContext = appGestaoContext;
            this._carrinhoFicha = carrinhoFicha;
        }
        public void CriarFicha(Ficha ficha)
        {
            _appGestaoContext.Fichas.Add(ficha);
            _appGestaoContext.SaveChanges();
            var fichaFuncionarios = _carrinhoFicha.CarrinhoFichaItens; 
            foreach (var funcionario in fichaFuncionarios)
            {
                var fichaDetalhe = new FichaDetalhe()
                {
                    FichaId = ficha.FichaId,
                    FuncionarioId = funcionario.Funcionario.FuncionarioId
                };
                _appGestaoContext.FichaDetalhes.Add(fichaDetalhe);
            }
            _appGestaoContext.SaveChanges();
        }
    }
}
