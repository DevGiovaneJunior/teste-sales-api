# API de Vendas - Teste Técnico DeveloperStore

## 🚀 Visão Geral

Esta API é uma implementação completa de um sistema de vendas seguindo a arquitetura **DDD (Domain-Driven Design)** e o padrão **External Identities** para referência de entidades externas com denormalização dos seus dados descritivos.

O projeto oferece um **CRUD completo para vendas**, respeitando regras de negócio específicas de descontos por quantidade e limitações, além de simular publicação de eventos via logging.

---

## 📚 Conceitos Aplicados

- **DDD (Domain-Driven Design):** Separação clara entre domínio, aplicação, infraestrutura e API.
- O projeto utiliza DI para desacoplar componentes, facilitando manutenção, testes e escalabilidade.
- Serviços como `ISaleService` e repositórios (`ISaleRepository`) são registrados no container de DI e consumidos via construtor.
- Exemplo no `Program.cs`:
  ```csharp
  builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
  builder.Services.AddScoped<ISaleService, SaleService>();
- **External Identities:** Uso de IDs externos (`customerId`, `branchId`, `productId`) com nomes e descrições denormalizadas para evitar dependências diretas entre domínios.
- **Repositório em memória:** Implementação mock para persistência, facilitando testes e prototipação.
- **Regra de negócio:** Aplicação automática de descontos com base na quantidade vendida.
- **Eventos simulados:** Eventos de venda e item são logados, simulando publicação para message brokers.
- **Swagger:** Documentação e testes via interface visual.


---

## 🛠 Tecnologias e Bibliotecas

- **.NET 8** (.NET SDK 8.0)
- **C#**
- **Swashbuckle.AspNetCore:** para documentação Swagger UI
- **xUnit:** para testes unitários
- **Microsoft.Extensions.Logging:** para logs e eventos simulados

---

## 📂 Estrutura do Projeto

# Entidades e interfaces do domínio
/Teste.Sales.Domain 
# Serviços, DTOs, regras de negócio
/Teste.Sales.Application 
# Repositórios (in-memory e EF se desejar)
/Teste.Sales.Infrastructure 
# API web, controllers, configuração Swagger
/Teste.Sales.Api 
# Testes unitários e de integração
/Teste.Sales.Tests 



---

## 🚦 Regras de Negócio

- **Descontos por quantidade:**
  - 4 a 9 unidades do mesmo produto → 10% de desconto no item
  - 10 a 20 unidades do mesmo produto → 20% de desconto no item
- **Limite máximo:** máximo 20 unidades por produto em uma venda
- **Sem desconto:** para compras abaixo de 4 unidades
- **Cancelamento:** vendas e itens podem ser cancelados, com atualização no total

---

## 📖 Endpoints principais (exemplo com controller `SalesController`)

| Método  | Endpoint                         | Descrição                      |
|---------|---------------------------------|-------------------------------|
| POST    | `/api/sales`                    | Criar nova venda              |
| GET     | `/api/sales`                    | Listar todas as vendas        |
| GET     | `/api/sales/{id}`               | Buscar venda por ID           |
| PUT     | `/api/sales/{id}`               | Atualizar venda               |
| PATCH   | `/api/sales/{id}/cancel`        | Cancelar venda                |
| PATCH   | `/api/sales/{id}/items/{productId}/cancel` | Cancelar item específico |

---

## ⚙️ Como rodar a aplicação

1. Clone o repositório:
   ```bash
   git clone https://github.com/DevGiovaneJunior/teste-sales-api.git
   cd teste-sales-api/src/Teste.Sales.Api
Restaure os pacotes e rode o projeto:

bash
- dotnet restore
- dotnet run

- Teste os endpoints diretamente pelo Swagger UI.

## 📦   DTO de exemplo para criar uma venda
json
```
{
  "customerId": "1",
  "customerName": "Giovane",
  "branchId": "1",
  "branchName": "Matriz São Paulo",
  "items": [
    {
      "productId": "1",
      "productName": "Cerveja Brahma",
      "quantity": 26,
      "unitPrice": 6.50
    },
    {
      "productId": "2",
      "productName": "Cerveja Skol",
      "quantity": 8,
      "unitPrice": 7.00
    }
  ]
}
```
