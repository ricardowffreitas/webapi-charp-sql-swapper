using Globaltec_webapisql._0_Models;
using Globaltec_webapisql._1_Contexto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

//criando conexao com banco de dados
builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer(
        "Data Source=DESKTOP-J99NUG5\\SQLEXPRESS;Initial Catalog=Globaltec_WEBAPISQL;Integrated Security=False;User ID=globaltec;Password=globaltec123;Connect Timout=15;Encrypt=False;TrustServerCertificate=False"));


//criando o construtor do swagger

//para testar no brower por /swagger no navegador após final ex ://localhost:7095/swagger/index.html
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

//cadastrando fornecedor
app.MapPost("AdicionarFornecedor", async (Fornecedor fornecedor, Contexto contexto) =>
{
    contexto.Fornecedor.Add(fornecedor);
    await contexto.SaveChangesAsync();
    app.MapGet("/", () => "O fornecedor foi salvo!");
}
    );

//excluindo fornecedor
app.MapPost("ExcluirFornecedor/{id}", async (int id, Contexto contexto) =>
{
    var fornecedorExcluir = await contexto.Fornecedor.FirstOrDefaultAsync(p => p.Id == id);
    if (fornecedorExcluir != null)
    {
        contexto.Fornecedor.Remove(fornecedorExcluir);
        await contexto.SaveChangesAsync();
    }

}
    );

//listando fornecedores
app.MapPost("ListarFornecedores", async (Contexto contexto) =>
{
    await contexto.Fornecedor.ToListAsync();
}
    );


//obter 1 fornecedor pelo ID
app.MapPost("ObterFornecedor/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Fornecedor.FirstOrDefaultAsync(p => p.Id == id);
}
    );



//obter fornecedores pelo UF
app.MapPost("ObterFornecedorUF/{UF}", async (string UF, Contexto contexto) =>
{
    return await contexto.Fornecedor.FirstOrDefaultAsync(p => p.UF == UF);
}
    );


//atualizando fornecedores
app.MapPost("AtuaizarFornecedor/{id}", async (int id, Contexto contexto) =>
{
    var forneceodorAtualizar = await contexto.Fornecedor.FirstOrDefaultAsync(p => p.Id == id);
    if (forneceodorAtualizar != null)
    {
        contexto.Fornecedor.Update(forneceodorAtualizar);
        await contexto.SaveChangesAsync();
        app.MapGet("/", () => "O fornecedor foi atualizado!");

    }

}
    );


//criando produto
app.MapPost("AdicionarProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
}
    );

//app.MapGet("/", () => "Hello World!");

//excluindo produto
app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
    if (produtoExcluir != null)
    {
        contexto.Produto.Remove(produtoExcluir);
        await contexto.SaveChangesAsync();
    }

}
    );

//listando produto
app.MapPost("ListarProdutos", async (Contexto contexto) =>
{
    await contexto.Produto.ToListAsync();
}
    );


//obter 1 produto
app.MapPost("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
     return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
}
    );




app.UseSwaggerUI();
app.Run();
