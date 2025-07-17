# Projeto Sales API (Teste.Sale.Ambev)

## 🚀 Visão Geral

Este projeto é uma API para gestão de vendas, focada em operações de criação e cancelamento de vendas e itens de venda, com uma arquitetura limpa, testes automatizados robustos e validação rigorosa.

---

## 🛠 Tecnologias e Frameworks Utilizados

- **.NET 8 (C#)**  
  Plataforma principal para desenvolvimento backend, garantindo alta performance e modernidade.

- **Entity Framework Core (InMemory Provider para testes)**  
  ORM para persistência de dados, com banco em memória nos testes para isolamento e rapidez.

- **xUnit**  
  Framework de testes para .NET, para garantir qualidade e comportamento esperado do código.

- **Moq**  
  Biblioteca de mocking para simular dependências em testes unitários, permitindo focar no comportamento da unidade testada.

- **FluentAssertions**  
  Framework para assertions fluentes nos testes, tornando os testes mais legíveis e expressivos.

- **FluentValidation**  
  Framework para validação de comandos (DTOs), assegurando regras de negócio antes do processamento.

---

## 📦 Estrutura do Projeto

- **Domain (Entities + Repositories)**  
  - `SaleEntity`, `SaleItemEntity` — entidades principais que representam vendas e itens.  
  - Repositórios (interfaces e implementações) para abstrair acesso a dados.

- **Application (Commands + Handlers + Validators)**  
  - Comandos (ex: `CreateSaleCommand`, `CancelSaleItemCommand`) como DTOs para entrada de dados.  
  - Handlers que processam a lógica de negócios, recebem comandos e interagem com repositórios.  
  - Validadores para garantir integridade dos comandos via FluentValidation.

- **Infrastructure**  
  - Implementação do repositório usando EF Core para persistência.

- **Tests**  
  - Testes unitários cobrindo:  
    - Handlers (ex: criação e cancelamento de vendas/itens)  
    - Validação dos comandos  
    - Repositório em memória para simular acesso a dados.

---

## 💡 Principais Conceitos Aplicados

- **Clean Architecture / Arquitetura em Camadas**  
  Separação clara entre domínio, aplicação, infraestrutura e apresentação para facilitar manutenção e evolução.

- **CQRS (Command Query Responsibility Segregation)**  
  Comandos para alterar estado (CreateSaleCommand, CancelSaleItemCommand), isolados da leitura.

- **Validação Separada**  
  Uso do FluentValidation para garantir que comandos estejam sempre consistentes antes de serem processados.

- **Testes Automatizados**  
  - **Unitários**: handlers, validação e repositórios.  
  - Uso de mocks para isolar unidades e focar no comportamento esperado.  
  - Testes específicos para casos de erro e sucesso.

- **Mocking e Assertions Expressivas**  
  Moq para simular dependências e FluentAssertions para testes mais legíveis.

- **Entidades Imutáveis e Encapsulamento**  
  Uso de propriedades com setters privados para garantir integridade dos dados dentro do domínio.

---

## 📚 Exemplos de Funcionalidades Testadas

- Criação de venda com múltiplos itens, validando dados obrigatórios e regras (ex: quantidade maior que zero).  
- Cancelamento de itens da venda, incluindo verificação se o item e a venda existem.  
- Validação que não permite criar vendas sem itens ou com itens inválidos.

---

## ⚙️ Como Rodar os Testes

1. Certifique-se que .NET 8 SDK está instalado.  
2. No terminal, navegue até a pasta dos testes (`Teste.Sale.Ambev.Unit`).  
3. Execute:  
   ```bash
   dotnet test

## Json para testar no SWAGGER
 ```
{
  "customerId": "1",
  "customerName": "Cliente Teste",
  "branchId": "1",
  "branchName": "Matriz",
  "items": [
    {
      "productId": "1",
      "productName": "Coca Cola",
      "quantity": 8,
      "unitPrice": 7
    },
    {
      "productId": "2",
      "productName": "Skol Latao",
      "quantity": 13,
      "unitPrice": 4.50
    }
  ]
}
 ```