using Microsoft.EntityFrameworkCore;
using TestePratico.Data;
using TestePratico.Models;

namespace TestePratico.Routes
{
    public static class CadeiraRoutes
    {
        public static void AddRoutes(this WebApplication app)
        {
            var routesCadeira = app.MapGroup("cadeiras");

            //GET All
            routesCadeira.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                var cadeiraListModel = await context.Cadeiras.Select(x => new CadeiraModel(x.Numero, x.Descricao, x.Valor)).ToListAsync(ct);

                return Results.Ok(cadeiraListModel);
            });

            //GET by id
            routesCadeira.MapGet("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var cadeira = await context.Cadeiras.SingleOrDefaultAsync(x => x.Id == id);

                if(cadeira == null)
                    return Results.NotFound("Cadeira não encontrada.");

                return Results.Ok(new CadeiraModel(cadeira.Numero, cadeira.Descricao, cadeira.Valor));
            });

            //POST
            routesCadeira.MapPost("", async (CadeiraRequestModel? request, AppDbContext context, CancellationToken ct) => 
            {
                if(request == null)
                    return Results.BadRequest("Necessário passar os parametros para cadastrar nova cadeira.");

                if(request.Numero == null)
                    return Results.Conflict("Campo número é obrigatório.");

                if(request.Valor == null)
                    return Results.Conflict("Campo valor é obrigatório.");

                var isExiste = await context.Cadeiras.AnyAsync(x => x.Numero == request.Numero, ct);

                if(isExiste)
                    return Results.Conflict("Já existe uma cadeira com essa numeração cadastrada no banco.");

                var novaCadeira = new Cadeira()
                {
                    Numero = request.Numero.Value,
                    Descricao = request.Descricao,
                    Valor = request.Valor.Value,
                    DataCriacao = DateTime.Today
                };

                await context.Cadeiras.AddAsync(novaCadeira, ct);

                await context.SaveChangesAsync(ct);

                return Results.Ok("Cadeira cadastrada com sucesso.");
            });

            //PUT
            routesCadeira.MapPut("{id}", async (int id, CadeiraRequestModel? request, AppDbContext context, CancellationToken ct) => 
            {
                var cadeira = await context.Cadeiras.SingleOrDefaultAsync(x => x.Id == id, ct);

                if(cadeira == null)
                    return Results.NotFound("Cadeira não encontrada.");

                if(request == null || (request.Numero == null && string.IsNullOrEmpty(request.Descricao) && request.Valor == null))
                    return Results.BadRequest("Necessário passar algum parametro para atualizar a cadeira.");

                if(request.Numero != null)
                {
                    var isExiste = await context.Cadeiras.AnyAsync(x => x.Numero == request.Numero && x.Id != id, ct);

                    if(isExiste)
                        return Results.Conflict("Já existe uma cadeira com essa numeração cadastrada no banco.");

                    cadeira.Numero = request.Numero.Value;
                }
                
                if(request.Valor != null)
                    cadeira.Valor = request.Valor.Value;

                if(!string.IsNullOrEmpty(request.Descricao))
                    cadeira.Descricao = request.Descricao;

                await context.SaveChangesAsync(ct);

                return Results.Ok("Cadeira atualizada com sucesso.");
            });

             //DELETE 
            routesCadeira.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var cadeira = await context.Cadeiras.SingleOrDefaultAsync(x => x.Id == id, ct);

                if(cadeira == null)
                    return Results.NotFound("Cadeira não encontrada.");

                var cadeiraNumero = cadeira.Numero;

                await context.Cadeiras.Where(x => x.Id == id).ExecuteDeleteAsync(ct);

                return Results.Ok($"Cadeira de numeração {cadeiraNumero} foi excluída com sucesso.");
            });
        }
    }
}