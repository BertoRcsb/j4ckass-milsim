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
    /// Mensagens de Autenticação e Admin
    /// </summary>
    public static class Admin
    {
        public const string LoginRequired = "Autenticação necessária para acessar esta página.";
        public const string LoginSucesso = "Login realizado com sucesso!";
        public const string LoginFalha = "Email ou senha inválidos.";
        public const string AcessoNegado = "Você não tem permissão para acessar este recurso.";
        public const string LogoutSucesso = "Logout realizado com sucesso.";
        public const string ErroProcessamento = "Erro ao processar operação. Tente novamente mais tarde.";

        public static class Aviso
        {
            public const string CriacaoSucesso = "Aviso criado com sucesso.";
            public const string EdicaoSucesso = "Aviso atualizado com sucesso.";
            public const string DelecaoSucesso = "Aviso removido com sucesso.";
            public const string ErroProcessamento = "Erro ao processar aviso. Tente novamente.";

            public static class Validacao
            {
                public const string TituloObrigatorio = "Título é obrigatório.";
                public const string ConteudoObrigatorio = "Conteúdo é obrigatório.";
            }
        }

        public static class Atualizacao
        {
            public const string CriacaoSucesso = "Atualização criada com sucesso.";
            public const string EdicaoSucesso = "Atualização atualizada com sucesso.";
            public const string DelecaoSucesso = "Atualização removida com sucesso.";
            public const string ErroProcessamento = "Erro ao processar atualização. Tente novamente.";

            public static class Validacao
            {
                public const string VersaoObrigatoria = "Versão é obrigatória.";
                public const string ConteudoObrigatorio = "Conteúdo é obrigatório.";
            }
        }

        public static class AdminUser
        {
            public const string EmailJaCadastrado = "Este email já foi cadastrado como administrador.";

            public static class Validacao
            {
                public const string EmailObrigatorio = "Email é obrigatório.";
                public const string SenhaObrigatoria = "Senha é obrigatória.";
                public const string SenhaFraca = "Senha deve ter no mínimo 8 caracteres.";
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

        // Admin/Avisos
        public const int MaxTituloLength = 200;
        public const int MaxConteudoLength = 5000;
        public const int MaxVersaoLength = 50;
        public const int MinSenhaLength = 8;
        public const int MaxSenhaLength = 128;

        public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    }
}
