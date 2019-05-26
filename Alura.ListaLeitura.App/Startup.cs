using Alura.ListaLeitura.App.Logica;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
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
            rotas.MapRoute("/Livros/ParaLer", LivrosLogica.LivrosParaLer);
            rotas.MapRoute("/Livros/Lidos", LivrosLogica.LivrosLidos);
            rotas.MapRoute("/Livros/Lendo", LivrosLogica.LivrosLendo);
            rotas.MapRoute("/Livro/Detalhe/{id:int}", LivrosLogica.LivroDetalhes);
            rotas.MapRoute("/Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.CadastroNovoLivro);
            rotas.MapRoute("/Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            rotas.MapRoute("/Cadastro/Incluir", CadastroLogica.ProcessaFormulario);
            app.UseRouter(rotas.Build());
        }
    }
}