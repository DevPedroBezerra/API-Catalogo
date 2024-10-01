namespace APICatalogo.Repositories
{
    public interface IUnitOfWork
    {
        IProdutosRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        void Commit();
    }
}
