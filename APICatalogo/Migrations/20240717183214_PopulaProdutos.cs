using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaID)" + 
                "Values('Coca-Cola','Refrigerante de Cola 350ml',5.45,'cocacola.jpg',50,now(),1)");

            mb.Sql("insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaID)" +
                "Values('Misto Quente','Pão Francês, queijo,presunto,tomate.',19.00,'mistoquente.jpg',100,now(),2)");

            mb.Sql("insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaID)" +
                "Values('Sorvete de Creme','um delicioso sorvete de creme',5.00,'sorvetecreme.jpg',40,now(),3)");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        { 
            mb.Sql("Delete from Produtos");
        }
    }
}
