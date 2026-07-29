# Guia de Assets - J4CKASS MILSIM

## Estrutura de Imagens

```
wwwroot/assets/
├── logos/
│   ├── jckss.png              # Logo principal (navbar)
│   ├── j4novo.png             # Logo grande (hero home)
│   └── logo-j4.png            # Logo alternativo
└── wallpapers/
    ├── operations-hero.jpg     # Background hero home
    └── tactical-hero.jpg       # Background alternativo
```

## Integração com Clean Architecture

### 1. **Serviço de Assets** (`IAssetService`)

Em vez de hardcodear caminhos de imagens nas views, usamos um serviço:

```csharp
// Core/Interfaces/IAssetService.cs
public interface IAssetService
{
    string GetLogoUrl(string logoName);
    string GetWallpaperUrl(string wallpaperName);
    string GetHeroBackgroundUrl(string heroName);
}
```

**Benefícios:**
- ✅ Centralizado - Alterar caminhos em um só lugar
- ✅ Testável - Mock para testes
- ✅ Type-safe - Evita strings mágicas
- ✅ Escalável - Pronto para CDN, otimizações, etc

### 2. **Uso no PageModel**

```csharp
// Pages/Index.cshtml.cs
public class IndexModel : PageModel
{
    private readonly IAssetService _assetService;
    
    public string LogoUrl { get; private set; }
    public string HeroBackgroundUrl { get; private set; }
    
    public IndexModel(IAssetService assetService)
    {
        _assetService = assetService;
    }
    
    public void OnGet()
    {
        LogoUrl = _assetService.GetLogoUrl("j4novo.png");
        HeroBackgroundUrl = _assetService.GetWallpaperUrl("operations-hero.jpg");
    }
}
```

**Clean Code aplicado:**
- ✅ Injeção de dependência
- ✅ Sem magic strings na view
- ✅ Lógica fora da apresentação

### 3. **Uso na View**

```html
<!-- Pages/Index.cshtml -->
<img class="hero-logo" src="@Model.LogoUrl" alt="Logo J4CKASS MILSIM" loading="lazy">
<section style="background-image: url('@Model.HeroBackgroundUrl')">
```

**Clean Code aplicado:**
- ✅ Atributo `loading="lazy"` para performance
- ✅ Imagens vêm do model (não hardcoded)
- ✅ Alt text descritivo para acessibilidade

## Otimizações Implementadas

### 1. **Lazy Loading**

```html
<!-- Carrega apenas quando necessário -->
<img src="@Model.LogoUrl" loading="lazy">
```

### 2. **Registro no DI Container**

```csharp
// Program.cs
builder.Services.AddSingleton<IAssetService, AssetService>();
```

**Singleton** porque:
- Assets são estáticos
- Não mudam durante a requisição
- Reutilizável em todas as páginas

### 3. **Separação de Responsabilidades**

| Responsabilidade | Localização |
|------------------|------------|
| Paths de assets | `IAssetService` |
| Exibição | Views |
| Business logic | Services |

## Extensões Futuras

### Otimização de Imagens

```csharp
public interface IAssetService
{
    string GetLogoUrl(string logoName, int? width = null, int? height = null);
    
    // Com suporte a WebP, compressão, etc
    string GetOptimizedImageUrl(string path, ImageOptions options);
}
```

### Versionamento de Assets

```csharp
public string GetAssetVersion(string filename)
{
    // Usar hash do arquivo para cache busting
    // Garante que browsers pegam a versão mais nova
    return $"{filename}?v={GetFileHash(filename)}";
}
```

### CDN Integration

```csharp
private string _cdnUrl = "https://cdn.j4ckass.com";

public string GetLogoUrl(string logoName) =>
    string.IsNullOrEmpty(_cdnUrl)
        ? $"/assets/logos/{logoName}"
        : $"{_cdnUrl}/logos/{logoName}";
```

### Admin CMS para Assets

```csharp
public interface IAssetAdminService
{
    Task<AssetUploadResultDTO> UploadAsync(IFormFile file, string category);
    Task<bool> DeleteAsync(string filename);
    Task<IEnumerable<AssetDTO>> ListAsync(string category);
}
```

## Clean Code Principles Aplicados

### 1. **Single Responsibility**
- `AssetService` cuida apenas de gerar URLs
- View cuida apenas de renderizar
- PageModel orquestra

### 2. **DRY (Don't Repeat Yourself)**
- Uma fonte única de verdade para caminhos de assets
- Evita duplicação em múltiplas views

### 3. **Dependency Injection**
- Fácil mockar para testes
- Fácil trocar implementação

### 4. **Meaningful Names**
```csharp
// ✅ Bom
_assetService.GetLogoUrl("jckss.png")

// ❌ Ruim
GetImg("jckss.png")
LoadAsset("logo")
```

## Estrutura de Nomes de Arquivos

### Convenções

- **logos/**: PNG, máx 2MB, nomes: `jckss.png`, `j4novo.png`
- **wallpapers/**: JPG, máx 500KB, nomes descritivos: `operations-hero.jpg`, `tactical-hero.jpg`
- **heroes/**: JPG, máx 300KB, para seções específicas

### Nomeação

```
✅ operations-hero.jpg       # Descritivo, lowercase, hífen
✅ jckss.png                 # Identificador da logo

❌ image1.jpg                # Genérico
❌ LOGO_GRANDE_NOVO.png      # Inconsistente
❌ op-hero (2).jpg           # Versionamento manual
```

## Performance

### Imagem Grande (Hero)

**Antes:**
```html
<img src="/assets/logos/j4novo.png">  <!-- 1.4 MB -->
```

**Depois:**
```csharp
// Considerar redimensionamento em build-time ou on-demand
public string GetOptimizedLogoUrl(string logoName) =>
    $"{LogosPath}/{logoName}?w=500&q=80";  // 500px, 80% quality
```

### Checklist de Otimização

- [ ] Usar JPG para fotos/wallpapers
- [ ] Usar PNG para logos com transparência
- [ ] WebP como fallback para browsers modernos
- [ ] Lazy loading em imagens below-the-fold
- [ ] Responsive images com srcset
- [ ] Alt text descritivo
- [ ] Compressão em build-time

## Exemplo Completo de Extensão

```csharp
// Application/Services/OptimizedAssetService.cs
public class OptimizedAssetService : IAssetService
{
    private readonly IImageOptimizationService _optimizer;
    
    public string GetLogoUrl(string logoName)
    {
        var url = $"/assets/logos/{logoName}";
        
        // Se não é SVG, otimizar
        if (!logoName.EndsWith(".svg"))
            url = _optimizer.Optimize(url, new ImageOptions 
            { 
                MaxWidth = 500,
                Quality = 85
            });
        
        return url;
    }
}
```

## Referência Rápida

```csharp
// Injetar em qualquer PageModel
public class MinhaPageModel : PageModel
{
    public MinhaPageModel(IAssetService assetService)
    {
        LogoUrl = assetService.GetLogoUrl("jckss.png");
        BgUrl = assetService.GetWallpaperUrl("operations-hero.jpg");
    }
}
```

---

**Próximo Passo:** Implementar CMS para upload de novos assets sem redeploy
