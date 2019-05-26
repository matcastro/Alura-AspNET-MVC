using System.IO;

namespace Alura.ListaLeitura.App.HTML
{
    public class HtmlUtils
    {
        public static string CarregaHtml(string caminho)
        {
            var caminhoCompleto = $"HTML/{caminho}.html";
            using (var arquivo = File.OpenText(caminhoCompleto))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
