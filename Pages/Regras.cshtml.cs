using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrupoArmaReforger.Pages
{
    public class RegrasModel : PageModel
    {
        public string DiscordUrl { get; } = "https://discord.gg/j4ckass";

        public void OnGet()
        {
        }
    }
}
