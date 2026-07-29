# Clean Code Practices - J4CKASS MILSIM

Documento detalhando as práticas de Clean Code aplicadas ao projeto.

## 1. Nomes Significativos

### ✅ Nomes Expressivos e Sem Ambigüidade

```csharp
// ❌ Ruim: nomes genéricos
public class Op { }
public Task Add(object x) { }
public string getVal() { }

// ✅ Bom: nomes que revelam intenção
public class Operador { }
public async Task<Operador> AdicionarAsync(Operador operador) { }
public string ObterDiscordUrl() { }
```

**Aplicado em:**
- `RecrutamentoService` (não `Service`)
- `CadastrarRecrutaAsync` (não `ProcessAsync`)
- `ExisteEmailAsync` (não `Check`)

### ✅ Evitar Desinformação

```csharp
// ❌ Ruim: nomes que enganam
public interface IRepository { }  // genérico demais

// ✅ Bom: específico e claro
public interface IOperadorRepository { }
public async Task<bool> ExisteEmailAsync(string email) { }
```

### ✅ Nomes Pronunciáveis e Pesquisáveis

```csharp
// ❌ Ruim: abreviações
private string strEmail;
private IRepository repo;
private bool isValid;

// ✅ Bom: nomes claros
private string emailPrincipal;
private IOperadorRepository operadorRepository;
private bool emailValido;
```

## 2. Funções Pequenas e Focadas

### Single Responsibility Principle

```csharp
// ❌ Ruim: múltiplas responsabilidades
public async Task OnPostAsync()
{
    if (string.IsNullOrEmpty(Nome)) { ... }
    var op = new Operador { Nome = Nome, ... };
    db.Operadores.Add(op);
    await db.SaveChangesAsync();
    if (db.Operadores.Count() > 100) { NotifyAdmin(); }
    return View("Success");
}

// ✅ Bom: responsabilidade delegada
public async Task OnPostAsync()
{
    var recrutamento = new RecrutamentoDTO { ... };
    Resultado = await _recrutamentoService.CadastrarRecrutaAsync(recrutamento);
}
```

### ✅ Funções com Poucos Parâmetros

```csharp
// ❌ Ruim: muitos parâmetros
public async Task CadastrarAsync(string nome, string email, string steam, 
    string psn, int? idade, bool verificado, string telefone) { }

// ✅ Bom: usar objeto de valor
public async Task<Operador> CadastrarAsync(Operador operador) { }
```

### ✅ Sem Efeitos Colaterais

```csharp
// ❌ Ruim: modifica estado implícito
public void CadastrarRecrutamento(RecrutamentoDTO dto)
{
    var operador = MapToOperador(dto);
    _repository.Adicionar(operador);
    Email.EnviarEmailAdmin();  // efeito colateral!
}

// ✅ Bom: claro sobre o que faz
public async Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(
    RecrutamentoDTO recrutamento)
{
    // ... lógica
    return new RecrutamentoResultadoDTO { ... };
}
```

## 3. Tratamento de Erros

### ✅ Use Exceções em vez de Codes de Erro

```csharp
// ❌ Ruim: retornar código de erro
public int Validar(Operador op)
{
    if (string.IsNullOrEmpty(op.Nome)) return 1;
    if (string.IsNullOrEmpty(op.Email)) return 2;
    return 0;  // sucesso
}

// ✅ Bom: usar exceções
public void Validar(Operador op)
{
    if (string.IsNullOrEmpty(op.Nome))
        throw new ArgumentException("Nome é obrigatório", nameof(op.Nome));
    if (string.IsNullOrEmpty(op.Email))
        throw new ArgumentException("Email é obrigatório", nameof(op.Email));
}
```

### ✅ Fornecer Contexto

```csharp
// ❌ Ruim: mensagem genérica
throw new Exception("Erro!");

// ✅ Bom: contexto claro
throw new ArgumentException(
    "Email já cadastrado no sistema", 
    nameof(operador.Email));
```

### ✅ Clean try-catch-finally

```csharp
public async Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(
    RecrutamentoDTO recrutamento)
{
    try
    {
        var operador = new Operador { ... };
        operador.Validar();  // Exceção se inválido
        
        if (await _repository.ExisteEmailAsync(operador.Email))
            return Erro("Email já cadastrado");
        
        await _repository.AdicionarAsync(operador);
        return Sucesso("Cadastrado com sucesso");
    }
    catch (ArgumentException ex)
    {
        _logger.LogWarning($"Validação falhou: {ex.Message}");
        return Erro(ex.Message);
    }
    catch (Exception ex)
    {
        _logger.LogError($"Erro inesperado: {ex.Message}");
        return Erro("Erro ao processar");
    }
}
```

## 4. Comentários

### ✅ Código auto-explicativo > Comentários

```csharp
// ❌ Ruim: comentários desnecessários
// Adiciona o operador à lista
var op = new Operador { Nome = n };
repo.Add(op);

// ✅ Bom: código que fala por si
var operador = new Operador { Nome = nome };
await _operadorRepository.AdicionarAsync(operador);
```

### ✅ Comentários úteis (quando necessários)

```csharp
// Usa ToLowerInvariant() para consistência em pesquisas cross-database
var emailNormalizado = operador.Email.Trim().ToLowerInvariant();

// Índice único garante que duplicate emails retorna erro do BD
// em vez de falha silenciosa
entity.HasIndex(e => e.Email).IsUnique();
```

### ❌ Evite Comentários Ruins

```csharp
// ❌ Comentários óbvios
i++;  // incrementa i

// ❌ Comentários desatualizados
// Checa se o email está vazio - OUTDATED
if (!email.Contains("@")) { ... }

// ❌ Código comentado (use git)
// var antigoMetodo = ...
// repo.ProcessarTudo(op);
```

## 5. Estrutura e Formatação

### ✅ DRY (Don't Repeat Yourself)

```csharp
// ❌ Ruim: repetição
public async Task<RecrutamentoResultadoDTO> CadastrarAsync(RecrutamentoDTO dto)
{
    if (string.IsNullOrEmpty(dto.Nome))
        return new RecrutamentoResultadoDTO { Sucesso = false, Mensagem = "..." };
    
    if (string.IsNullOrEmpty(dto.Email))
        return new RecrutamentoResultadoDTO { Sucesso = false, Mensagem = "..." };
}

// ✅ Bom: método helper
private RecrutamentoResultadoDTO ResultadoErro(string mensagem) =>
    new() { Sucesso = false, Mensagem = mensagem };

if (string.IsNullOrEmpty(dto.Nome))
    return ResultadoErro("Nome obrigatório");
```

### ✅ Organização e Coesão

```csharp
// Classes organizadas por responsabilidade
Core/
  Domain/           // Modelos de negócio (Operador)
  Interfaces/       // Contratos (IOperadorRepository, IRecrutamentoService)
Application/
  DTOs/             // Objetos de transferência (RecrutamentoDTO)
  Services/         # Orquestração de regras de negócio
Infrastructure/
  Data/             // Acesso a dados (AppDbContext)
  Repositories/     // Implementação de persistência
Pages/              // Apresentação (PageModels, views)
```

### ✅ Importações Organizadas

```csharp
// ✅ Agrupa por namespace, ordenado alfabeticamente
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GrupoArmaReforger.Core.Interfaces;
using GrupoArmaReforger.Application.DTOs;
```

## 6. SOLID Principles

### Single Responsibility

```csharp
// Cada classe tem uma razão para mudar
public class Operador { }                    // Domínio
public class RecrutamentoService { }         // Lógica de negócio
public class OperadorRepository { }          // Persistência
public class RecrutamentoModel { }           // Apresentação
```

### Open/Closed

```csharp
// Aberto para extensão, fechado para modificação
public interface IRecrutamentoService
{
    Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(...);
}

// Pode criar nova implementação sem alterar código existente
public class RecrutamentoServiceV2 : IRecrutamentoService { }
```

### Liskov Substitution

```csharp
// Implementações são substituíveis
IOperadorRepository repo = new OperadorRepository(context);
IOperadorRepository mockRepo = new OperadorRepositoryMock();

// Ambas funcionam igual
var operador = await repo.ObterPorIdAsync(1);
var operador2 = await mockRepo.ObterPorIdAsync(1);
```

### Interface Segregation

```csharp
// ❌ Ruim: interface gorda
public interface ICommunity
{
    Task CadastrarRecrutaAsync(...);
    Task CriarAvisoAsync(...);
    Task AtualizarVersaoAsync(...);
    Task AutenticarAsync(...);
}

// ✅ Bom: interfaces segregadas
public interface IRecrutamentoService { Task CadastrarRecrutaAsync(...); }
public interface IAvisoService { Task CriarAvisoAsync(...); }
public interface IAdminService { Task AutenticarAsync(...); }
```

### Dependency Inversion

```csharp
// ❌ Ruim: depende de implementação concreta
public class RecrutamentoModel : PageModel
{
    private OperadorRepository _repo = new();
    public async Task OnPostAsync()
    {
        await _repo.AdicionarAsync(...);
    }
}

// ✅ Bom: depende de abstração (injetada)
public class RecrutamentoModel : PageModel
{
    private readonly IRecrutamentoService _service;
    
    public RecrutamentoModel(IRecrutamentoService service)
    {
        _service = service;
    }
    
    public async Task OnPostAsync()
    {
        await _service.CadastrarRecrutaAsync(...);
    }
}
```

## 7. Validação e Defensive Programming

### ✅ Validar na Entidade

```csharp
public class Operador
{
    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("Nome obrigatório", nameof(Nome));
        
        if (!Email.Contains("@"))
            throw new ArgumentException("Email inválido", nameof(Email));
    }
}
```

### ✅ Falhar Rápido

```csharp
public async Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(
    RecrutamentoDTO recrutamento)
{
    var operador = new Operador { ... };
    operador.Validar();  // Falha imediatamente se inválido
    
    if (await _repository.ExisteEmailAsync(operador.Email))
        return Erro("Email duplicado");
    
    // Só chega aqui se válido
    await _repository.AdicionarAsync(operador);
    return Sucesso();
}
```

## 8. Async/Await

### ✅ Sempre use Async para I/O

```csharp
// ❌ Ruim: bloqueia thread
public Operador ObterPorEmail(string email)
{
    return _context.Operadores.FirstOrDefault(o => o.Email == email);
}

// ✅ Bom: não-bloqueante
public async Task<Operador?> ObterPorEmailAsync(string email)
{
    return await _context.Operadores
        .FirstOrDefaultAsync(o => o.Email == email);
}
```

### ✅ Nomes com Suffix Async

```csharp
// ✅ Claro que é assíncrono
public async Task<Operador> AdicionarAsync(Operador operador)
public async Task<IEnumerable<Operador>> ObterTodosAsync()
public async Task<bool> ExisteEmailAsync(string email)
```

## Checklist de Clean Code

- [x] Nomes significativos e sem ambiguidade
- [x] Funções pequenas e focadas (SRP)
- [x] Sem duplicação de código (DRY)
- [x] Tratamento robusto de erros
- [x] Comentários apenas quando necessário
- [x] Código auto-explicativo
- [x] SOLID principles aplicados
- [x] Organização clara de namespaces
- [x] Validação defensiva
- [x] Async/await para I/O
- [x] Dependency Injection
- [x] Logging apropriado
- [x] Sem código comentado (usar git)
- [x] Formatação consistente

## Benefícios Observados

✅ Código mais legível e manutenível
✅ Fácil de testar (componentes desacoplados)
✅ Menos bugs (validação clara)
✅ Escalável (adicionar features sem quebrar código)
✅ Equipe colaborativa (código que fala por si)

## Referências

- Robert C. Martin - "Clean Code"
- Robert C. Martin - "Clean Architecture"
- Martin Fowler - "Refactoring"
- Microsoft Docs - SOLID Principles
