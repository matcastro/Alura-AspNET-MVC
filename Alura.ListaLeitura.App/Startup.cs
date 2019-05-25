using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
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