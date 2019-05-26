using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController : Controller
    {
        IEnumerable<Livro> Livros { get; set; }
        public Livro Livro { get; set; }
        public IActionResult Detalhe(int id)
        {
            var repo = new LivroRepositorioCSV();
            ViewBag.Livro = repo.Todos.First(l => l.Id == id);
            return View("detalhe");
        }
        public IActionResult ParaLer()
        {
            var repositorio = new LivroRepositorioCSV();
            ViewBag.Livros = repositorio.ParaLer.Livros;
            return View("lista");
        }


        public IActionResult Lidos()
        {
            var repositorio = new LivroRepositorioCSV();
            ViewBag.Livros = repositorio.Lidos.Livros;
            return View("lista");
        }
        public IActionResult Lendo()
        {
            var repositorio = new LivroRepositorioCSV();
            ViewBag.Livros = repositorio.Lendo.Livros;
            return View("lista");
        }
    }
}
