using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
        public void Configure(IApplicationBuilder app)
        {
            var rotas = new RouteBuilder(app);
            rotas.MapRoute("/Livros/ParaLer", LivrosParaLer);
            rotas.MapRoute("/Livros/Lidos", LivrosLidos);
            rotas.MapRoute("/Livros/Lendo", LivrosLendo);
            rotas.MapRoute("/Cadastro/NovoLivro/{nome}/{autor}", CadastroNovoLivro);
            rotas.MapRoute("/Livro/Detalhe/{id:int}", LivroDetalhes);
            app.UseRouter(rotas.Build());
        }

        private Task LivroDetalhes(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        private Task CadastroNovoLivro(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = Convert.ToString(context.GetRouteValue("nome")),
                Autor = Convert.ToString(context.GetRouteValue("autor"))
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("Livro cadastrado com sucesso");
        }

        public Task Roteamento(HttpContext contexto)
        {
            var rotas = new Dictionary<string, RequestDelegate>()
            {
                { "/Livros/ParaLer", LivrosParaLer },
                { "/Livros/Lidos", LivrosLidos },
                { "/Livros/Lendo", LivrosLendo }
            };
            if (rotas.ContainsKey(contexto.Request.Path))
            {
                return rotas[contexto.Request.Path].Invoke(contexto);
            }
            contexto.Response.StatusCode = 404;
            return contexto.Response.WriteAsync("Caminho inexistente");
        }

        public Task LivrosParaLer(HttpContext contexto)
        {
            var repositorio = new LivroRepositorioCSV();
            return contexto.Response.WriteAsync(repositorio.ParaLer.ToString());
        }
        public Task LivrosLidos(HttpContext contexto)
        {
            var repositorio = new LivroRepositorioCSV();
            return contexto.Response.WriteAsync(repositorio.Lidos.ToString());
        }
        public Task LivrosLendo(HttpContext contexto)
        {
            var repositorio = new LivroRepositorioCSV();
            return contexto.Response.WriteAsync(repositorio.Lendo.ToString());
        }
    }
}