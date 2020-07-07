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
        public void CriarFicha(FichaFuncional ficha)
        {
            _appGestaoContext.Add(ficha);
            var fichaFuncionarios = _carrinhoFicha.CarrinhoFichaItens;
            foreach (var funcionario in fichaFuncionarios)
            {
                var fichaDetalhe = new FichaDetalhe()
                {
                    fichafuncId = ficha.fichafuncId,
                    FuncionarioId = funcionario.Funcionario.FuncionarioId
                };
                _appGestaoContext.FichaDetalhes.Add(fichaDetalhe);
            }
            _appGestaoContext.SaveChanges();
        }
    }
}
