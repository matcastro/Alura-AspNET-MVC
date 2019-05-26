using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosLogica
    {
        public static Task LivroDetalhes(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var livro = repo.Todos.First(l => l.Id == id);
            var html = HtmlUtils.CarregaHtml("detalhe");
            html = html.Replace("#TITULO#", livro.Titulo);
            html = html.Replace("#AUTOR#", livro.Autor);
            html = html.Replace("#LISTA#", livro.Lista.Titulo);
            return context.Response.WriteAsync(html);
        }
        public static Task LivrosParaLer(HttpContext contexto)
        {
            var repositorio = new LivroRepositorioCSV();
            string html = PreencheLista("para-ler", repositorio.ParaLer.Livros);
            return contexto.Response.WriteAsync(html);
        }


        public static Task LivrosLidos(HttpContext contexto)
        {
            var repositorio = new LivroRepositorioCSV();
            string html = PreencheLista("lidos", repositorio.Lidos.Livros);
            return contexto.Response.WriteAsync(html);
        }
        public static Task LivrosLendo(HttpContext contexto)
        {
            var repositorio = new LivroRepositorioCSV();
            string html = PreencheLista("lendo", repositorio.Lendo.Livros);
            return contexto.Response.WriteAsync(html);
        }
        private static string PreencheLista(string caminho, IEnumerable<Livro> livros)
        {
            var html = HtmlUtils.CarregaHtml(caminho);
            foreach (var livro in livros)
            {
                html = html.Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }
            html = html.Replace("#NOVO-ITEM#", "");
            return html;
        }
    }
}
