# 🚀 SmartCEP API - Guia de Configuração do Banco de Dados MySQL

## ✅ O que foi implementado

✔️ **Domain Layer**: Interface `ICodeRepository` para desacoplar a lógica de negócio
✔️ **Application Layer**: `SearchCodeFromDatabaseUseCase` para buscar CEPs do banco
✔️ **Infrastructure Layer**: 
   - `CodeEntity` - Entidade de persistência
   - `AppDbContext` - Contexto do Entity Framework Core
   - `CodeRepository` - Implementação do repositório
✔️ **API Layer**: Controller atualizado para usar o banco de dados

---

## 📋 Pré-requisitos

- MySQL 8.0+ instalado e rodando
- .NET 8.0 SDK
- Entity Framework Core Tools (já instalado)

---

## 🗄️ PASSO 1 - Configurar o MySQL

### Opção A: MySQL Local

```bash
# Criar o banco de dados
mysql -u root -p
CREATE DATABASE smartcep CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
EXIT;
```

### Opção B: Docker (Recomendado)

```bash
# Criar container MySQL
docker run --name smartcep-mysql \
  -e MYSQL_ROOT_PASSWORD=root \
  -e MYSQL_DATABASE=smartcep \
  -p 3306:3306 \
  -d mysql:8.0

# Verificar se está rodando
docker ps
```

---

## 🔧 PASSO 2 - Configurar Connection String

O arquivo `appsettings.json` já está configurado com:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=smartcep;User=root;Password=root;"
  }
}
```

**⚠️ IMPORTANTE**: Para produção, use variáveis de ambiente ou User Secrets:

```bash
# Configurar com User Secrets (Desenvolvimento)
cd SmartCep.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=SEU_HOST;Port=3306;Database=smartcep;User=SEU_USER;Password=SUA_SENHA;"
```

---

## 🧬 PASSO 3 - Aplicar as Migrations

```bash
cd /caminho/para/SmartCep

# Aplicar migrations e criar as tabelas
dotnet ef database update --project SmartCep.Infrastructure --startup-project SmartCep.Api
```

Isso irá criar a tabela `postal_codes` com a estrutura:

```sql
CREATE TABLE postal_codes (
    postal_code VARCHAR(8) PRIMARY KEY,
    street VARCHAR(200) NOT NULL,
    neighborhood VARCHAR(100) NOT NULL,
    city VARCHAR(100) NOT NULL,
    state VARCHAR(2) NOT NULL
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

---

## 📊 PASSO 4 - Popular o Banco com Dados de Teste

```bash
# Conectar ao MySQL
mysql -u root -p smartcep

# Ou via Docker
docker exec -it smartcep-mysql mysql -u root -proot smartcep
```

Então execute o conteúdo do arquivo `seed-database.sql`:

```bash
# Ou direto do arquivo
mysql -u root -p smartcep < seed-database.sql

# Via Docker
docker exec -i smartcep-mysql mysql -u root -proot smartcep < seed-database.sql
```

---

## ▶️ PASSO 5 - Executar a API

```bash
cd SmartCep.Api
dotnet run
```

A API estará disponível em: `https://localhost:7XXX` ou `http://localhost:5XXX`

---

## 🧪 PASSO 6 - Testar

### Via Swagger
Acesse: `http://localhost:PORTA/swagger`

### Via cURL

```bash
# Buscar CEP existente
curl -X GET "http://localhost:5000/api/cep/01001000"

# Resposta esperada:
{
  "code": "01001000",
  "street": "Praça da Sé",
  "neighborhood": "Sé",
  "city": "São Paulo",
  "state": "SP"
}

# Buscar CEP inexistente
curl -X GET "http://localhost:5000/api/cep/99999999"

# Resposta esperada:
{
  "message": "CEP não Encontrado"
}
```

### Via HTTP File (JetBrains)

Crie um arquivo `test-database.http`:

```http
### Buscar CEP de São Paulo
GET http://localhost:5000/api/cep/01001000

### Buscar CEP da Paulista
GET http://localhost:5000/api/cep/01310100

### Buscar CEP do Rio de Janeiro
GET http://localhost:5000/api/cep/22041001

### Buscar CEP inexistente
GET http://localhost:5000/api/cep/99999999
```

---

## 🎯 Arquitetura Implementada

```
┌─────────────────────────────────────────────┐
│           SmartCep.Api (Controller)         │
│  ┌───────────────────────────────────────┐  │
│  │  GET /api/cep/{cep}                   │  │
│  └───────────────────────────────────────┘  │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│     SmartCep.Application (UseCase)          │
│  ┌───────────────────────────────────────┐  │
│  │  SearchCodeFromDatabaseUseCase        │  │
│  │  - Valida CEP (8 dígitos)             │  │
│  │  - Chama repository                   │  │
│  │  - Converte para DTO                  │  │
│  └───────────────────────────────────────┘  │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│      SmartCep.Domain (Interface)            │
│  ┌───────────────────────────────────────┐  │
│  │  ICodeRepository                      │  │
│  │  - GetByPostalCodeAsync()             │  │
│  │  - SaveAsync()                        │  │
│  └───────────────────────────────────────┘  │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────┐
│   SmartCep.Infrastructure (Repository)      │
│  ┌───────────────────────────────────────┐  │
│  │  CodeRepository                       │  │
│  │  - Acessa AppDbContext                │  │
│  │  - Faz query no MySQL                 │  │
│  │  - Converte Entity → Domain           │  │
│  └───────────────────────────────────────┘  │
│  ┌───────────────────────────────────────┐  │
│  │  AppDbContext (EF Core)               │  │
│  └───────────────────────────────────────┘  │
└──────────────────┬──────────────────────────┘
                   │
                   ▼
          ┌───────────────────┐
          │     MySQL DB      │
          │  postal_codes     │
          └───────────────────┘
```

---

## 🔥 O que você ganhou

✅ **Independência total** - Não depende mais de APIs externas
✅ **Performance** - Busca local, sem latência de rede
✅ **Controle total** - Você gerencia os dados
✅ **Escalabilidade** - Pronto para milhões de consultas
✅ **Clean Architecture** - Código desacoplado e testável

---

## 📦 Próximos Passos (Opcional)

### 1️⃣ Importar Base Completa de CEPs

Existem bases públicas de CEPs dos Correios que você pode importar:
- https://github.com/chandez/CEP-Banco-de-Dados
- https://viacep.com.br/

### 2️⃣ Adicionar Cache Redis

Para consultas ainda mais rápidas, adicione cache em memória:

```csharp
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
```

### 3️⃣ Implementar Fallback

Mantenha o ViaCEP como fallback quando o CEP não existir no banco:

```csharp
public class SearchCodeWithFallbackUseCase
{
    private readonly ICodeRepository _repository;
    private readonly ICodeProvider _viaCepProvider;

    public async Task<CodeDto?> ExecuteAsync(string postalCode)
    {
        // 1. Busca no banco
        var result = await _repository.GetByPostalCodeAsync(postalCode);
        
        if (result != null)
            return CodeDto.FromDomain(result);
        
        // 2. Fallback para ViaCEP
        var viaCepResult = await _viaCepProvider.SearchAsync(postalCode);
        
        if (viaCepResult != null)
        {
            // 3. Salva no banco para próxima vez
            await _repository.SaveAsync(viaCepResult);
            return CodeDto.FromDomain(viaCepResult);
        }
        
        return null;
    }
}
```

### 4️⃣ Adicionar Testes Unitários

```bash
cd ..
dotnet new xunit -n SmartCep.Tests
cd SmartCep.Tests
dotnet add reference ../SmartCep.Application/SmartCep.Application.csproj
dotnet add package Moq
```

---

## 🐛 Troubleshooting

### Erro: "Connection refused"
✅ Verifique se o MySQL está rodando: `sudo systemctl status mysql`

### Erro: "database does not exist"
✅ Crie o banco: `mysql -u root -p -e "CREATE DATABASE smartcep;"`

### Erro: "Access denied for user"
✅ Verifique usuário e senha na connection string

### Migration não aplica
✅ Delete as migrations e recrie:
```bash
rm -rf SmartCep.Infrastructure/Migrations
dotnet ef migrations add InitialCreateMySQL --project SmartCep.Infrastructure --startup-project SmartCep.Api
dotnet ef database update --project SmartCep.Infrastructure --startup-project SmartCep.Api
```

---

## 📝 Comandos Úteis

```bash
# Ver migrations pendentes
dotnet ef migrations list --project SmartCep.Infrastructure --startup-project SmartCep.Api

# Reverter última migration
dotnet ef migrations remove --project SmartCep.Infrastructure --startup-project SmartCep.Api

# Gerar script SQL da migration
dotnet ef migrations script --project SmartCep.Infrastructure --startup-project SmartCep.Api -o migration.sql

# Dropar banco e recriar
dotnet ef database drop --project SmartCep.Infrastructure --startup-project SmartCep.Api --force
dotnet ef database update --project SmartCep.Infrastructure --startup-project SmartCep.Api
```

---

**🎉 Parabéns! Sua API agora usa banco de dados próprio com Clean Architecture!**

