using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrupoArmaReforger.Pages.Admin;

/// <summary>
/// PageModel para logout de administradores
/// </summary>
public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        await HttpContext.SignOutAsync("AdminScheme");
        return RedirectToPage("/Index");
    }
}
