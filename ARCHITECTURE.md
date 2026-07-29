# Clean Architecture - J4CKASS MILSIM

Este projeto segue os princípios de **Clean Architecture** e **Clean Code**, mantendo separação clara entre camadas e responsabilidades.

## Estrutura de Camadas

```
GrupoArmaReforger/
├── Core/
│   ├── Domain/              # Entidades de domínio
│   │   └── Operador.cs      # Modelo com validações
│   └── Interfaces/          # Contratos (repositórios, serviços)
│       ├── IOperadorRepository.cs
│       └── IRecrutamentoService.cs
│
├── Application/
│   ├── DTOs/                # Data Transfer Objects
│   │   └── RecrutamentoDTO.cs
│   └── Services/            # Lógica de aplicação
│       └── RecrutamentoService.cs
│
├── Infrastructure/
│   ├── Data/                # Contexto do banco de dados
│   │   └── AppDbContext.cs
│   └── Repositories/        # Implementação de repositórios
│       └── OperadorRepository.cs
│
├── Pages/                   # Presentation Layer (Razor Pages)
│   ├── Index.cshtml.cs
│   ├── Recrutamento.cshtml.cs
│   ├── Regras.cshtml.cs
│   ├── Avisos.cshtml.cs
│   ├── Atualizacoes.cshtml.cs
│   └── ...
│
└── Program.cs              # Configuração de DI e startup
```

## Princípios Aplicados

### 1. **Separation of Concerns**
- Cada camada tem uma responsabilidade bem definida
- Domínio não conhece infraestrutura
- Apresentação não contém lógica de negócio

### 2. **Dependency Injection**
```csharp
// Program.cs
builder.Services.AddScoped<IOperadorRepository, OperadorRepository>();
builder.Services.AddScoped<IRecrutamentoService, RecrutamentoService>();
```

### 3. **SOLID Principles**

#### Single Responsibility
- `Operador`: representa um candidato a recrutamento
- `RecrutamentoService`: orquestra fluxo de recrutamento
- `OperadorRepository`: gerencia persistência

#### Open/Closed
- `IOperadorRepository` permite implementações diferentes sem alterar código existente

#### Liskov Substitution
- Implementações podem ser trocadas (mock em testes)

#### Interface Segregation
- `IRecrutamentoService` expõe apenas métodos necessários

#### Dependency Inversion
- PageModels dependem de abstrações (interfaces), não implementações

### 4. **Clean Code**

#### Naming
```csharp
// Bom: nomes expressivos
public async Task<RecrutamentoResultadoDTO> CadastrarRecrutaAsync(...)

// Evita: nomes genéricos ou abreviados
public async Task ProcessAsync(...)
```

#### Small Functions
- Cada método faz uma coisa bem
- Fácil de testar e entender

#### Error Handling
```csharp
// Validação na entidade
operador.Validar();  // Lança exceção se inválido

// Service captura e transforma em DTO
try { ... } catch (ArgumentException ex) { ... }
```

### 5. **DTOs (Data Transfer Objects)**
- `RecrutamentoDTO`: transporta dados da apresentação → aplicação
- `RecrutamentoResultadoDTO`: retorna resultado com mensagens
- Desacoplam camadas

### 6. **Repository Pattern**
```csharp
public interface IOperadorRepository
{
    Task<Operador> AdicionarAsync(Operador operador);
    Task<bool> ExisteEmailAsync(string email);
    // ... abstrair operações de BD
}
```

## Fluxo de Recrutamento

```
User Form (Recrutamento.cshtml)
        ↓
RecrutamentoModel.OnPostAsync()
        ↓
IRecrutamentoService.CadastrarRecrutaAsync()
        ↓
Operador.Validar() → Validações de negócio
        ↓
IOperadorRepository.ExisteEmailAsync() → Verificar duplicidade
        ↓
IOperadorRepository.AdicionarAsync() → Persistir
        ↓
RecrutamentoResultadoDTO → Retornar resultado
        ↓
View renderiza mensagem (sucesso/erro)
```

## Benefícios

✅ **Testabilidade**: Serviços podem ser testados com repositórios mock
✅ **Manutenibilidade**: Código organizado e claro
✅ **Escalabilidade**: Fácil adicionar novos serviços/features
✅ **Reusabilidade**: Serviços podem ser usados por múltiplos PageModels
✅ **Independência de Framework**: Lógica não depende de ASP.NET

## Extensões Futuras

### CMS para Regras/Avisos
```csharp
// Padrão similar para Avisos
public interface IAvisoRepository { ... }
public class AvisoService : IAvisoService { ... }
```

### Autenticação e Admin
```csharp
public interface IAdminService
{
    Task<bool> AutenticarAsync(string email, string senha);
    Task<AvisoResultadoDTO> CriarAvisoAsync(AvisoDTO aviso);
}
```

### Testes Unitários
```csharp
[TestClass]
public class RecrutamentoServiceTests
{
    [TestMethod]
    public async Task CadastrarRecrutaAsync_ComDadosValidos_RetornaSucesso()
    {
        // Arrange
        var mockRepo = new Mock<IOperadorRepository>();
        var service = new RecrutamentoService(mockRepo.Object);
        
        // Act
        var resultado = await service.CadastrarRecrutaAsync(dto);
        
        // Assert
        Assert.IsTrue(resultado.Sucesso);
    }
}
```
