using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Application.DTOs;
using GrupoArmaReforger.Core.Interfaces;

namespace GrupoArmaReforger.Pages.Admin;

/// <summary>
/// PageModel para autenticação de administradores
/// </summary>
[IgnoreAntiforgeryToken]
public class LoginModel : PageModel
{
    private readonly IAdminService _adminService;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Senha { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public LoginModel(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public void OnGet()
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            RedirectToPage("/Admin/Dashboard");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resultado = await _adminService.AutenticarAsync(Email, Senha);

        if (!resultado.Sucesso)
        {
            ErrorMessage = resultado.Mensagem;
            return Page();
        }

        await AutenticarCookieAsync(resultado.AdminId.Value, Email);
        return RedirectToPage("/Admin/Dashboard");
    }

    private async Task AutenticarCookieAsync(int adminId, string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, adminId.ToString() ?? "0"),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
        };

        await HttpContext.SignInAsync(
            "AdminScheme",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
}
