using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
namespace MyApp.Namespace
{
    public class RecrutamentoModel : PageModel
    {
        // Lista que salva os os dados dos recrutas (fica na memória sempre)
        public static List<Operador> Recrutas = new List<Operador>();
        [BindProperty]
        public string? Nome { get; set; }
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? SteamID { get; set; }
        [BindProperty]
        public string? PSN { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            // Criar novo recruta com os dados do formulário
            Operador  novo =  new Operador
            {
                Nome = Nome,
                Email = Email,
                SteamID = SteamID,
                PSN = PSN
            };
            //salvar na lista de recrutas
            Recrutas.Add(novo);
            // Aqui você pode processar os dados do formulário
            // Nome, Email, SteamID e PSN já estarão preenchidos
        }
    }
}
// Classe do operador (modelo)
public class Operador
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? SteamID { get; set; }
    public string? PSN { get; set; }
}
