namespace GrupoArmaReforger.Core.Constants;

/// <summary>
/// Constantes globais da aplicação
/// </summary>
public static class AppConstants
{
    /// <summary>
    /// URLs e Links
    /// </summary>
    public static class Links
    {
        public const string DiscordUrl = "https://discord.gg/j4ckass";
    }

    /// <summary>
    /// Caminhos de Assets
    /// </summary>
    public static class Assets
    {
        public const string LogoNavbar = "jckss.png";
        public const string LogoHome = "j4novo.png";
        public const string LogoAlt = "logo-j4.png";
        public const string HeroOperations = "operations-hero.jpg";
        public const string HeroTactical = "tactical-hero.jpg";
    }

    /// <summary>
    /// Mensagens de Erro e Sucesso
    /// </summary>
    public static class Messages
    {
        public static class Recrutamento
        {
            public const string SucessoCadastro = "Recrutamento realizado com sucesso! Aguarde contato da staff.";
            public const string EmailDuplicado = "Este email já foi cadastrado no sistema.";
            public const string ErroProcessamento = "Erro ao processar recrutamento. Tente novamente mais tarde.";

            public static class Validacao
            {
                public const string NomeObrigatorio = "Nome é obrigatório.";
                public const string EmailObrigatorio = "Email é obrigatório.";
                public const string EmailInvalido = "Email inválido. Verifique o formato.";
                public const string PlataformaObrigatoria = "SteamID ou PSN é obrigatório.";
            }
        }
    }

    /// <summary>
    /// Validações
    /// </summary>
    public static class Validation
    {
        public const int MaxNomeLength = 100;
        public const int MaxEmailLength = 100;
        public const int MaxSteamIdLength = 50;
        public const int MaxPsnLength = 50;

        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    }
}
