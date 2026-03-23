# 🏦 JJ Banking API
> **"A espinha dorsal financeira para suas aplicações modernas."**

![.NET 10](https://img.shields.io/badge/.NET-10.0-512bd4?style=for-the-badge&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![xUnit](https://img.shields.io/badge/Tests-xUnit%20%26%20FluentAssertions-green?style=for-the-badge)

A **JJ Banking API** é um motor de serviços financeiros de alta performance desenvolvido para ser o "Core Banking" definitivo para desenvolvedores. Construída com o que há de mais moderno no ecossistema **.NET 10**, esta API oferece uma infraestrutura robusta, escalável e pronta para uso em projetos de Front-end, Mobile ou sistemas de gestão financeira.

---

## 📖 Índice
- [Visão Geral](#-visão-geral)
- [Diferenciais Estratégicos](#-diferenciais-estratégicos)
- [Arquitetura e Qualidade](#-arquitetura-e-qualidade)
- [Guia de Endpoints](#-guia-de-endpoints)
- [Configuração de Ambiente](#-configuração-de-ambiente)
- [Setup Instantâneo](#-setup-instantâneo-com-docker)

---

## 🌟 Visão Geral

A proposta da **JJ Banking API** é eliminar a necessidade de mocks estáticos e oferecer um backend financeiro persistente e documentado para a comunidade. Seja para validar um fluxo de carteira digital em React ou para estudar padrões de concorrência em transações bancárias, a JJ API entrega uma infraestrutura pronta para integrar.

### 🎯 Para quem é este projeto?
* **Devs Frontend/Mobile:** Consuma uma API real via Swagger e veja seus dados persistidos em um banco PostgreSQL.
* **Devs Backend:** Explore as novas funcionalidades do **C# 14** e a robustez do **Entity Framework Core 10**.
* **Open Source:** Use como base para criar novas funcionalidades e aprimorar seus conhecimentos em arquitetura de sistemas.

---

## 💎 Diferenciais Estratégicos

1. **Persistência Industrial:** Utiliza **PostgreSQL**, garantindo integridade transacional via EF Core 10.
2. **Invariantes de Domínio:** Regras de negócio protegidas (saldo insuficiente, transações negativas) diretamente nas Entidades.
3. **Segurança de Dados:** Uso rigoroso de **DTOs (Records)** e Variáveis de Ambiente para proteger a infraestrutura.
4. **Developer Experience (DX):** Foco total no "Clonou, Rodou" via Docker Compose e documentação interativa.

---

## 🏗️ Arquitetura e Qualidade

O projeto segue os princípios da **Clean Architecture**, garantindo que a lógica de negócio seja independente de frameworks externos:

* **`JJBanking.Domain`**: O coração do projeto (Entidades, Enums e Regras de Negócio).
* **`JJBanking.Infra`**: Camada de persistência (Contexto EF, Repositórios e Migrations).
* **`JJBanking.API`**: Porta de entrada (Controllers, DTOs e Injeção de Dependência).

### 🛡️ Qualidade de Software (Testes Automatizados):
* **Testes Unitários (xUnit):** Validação de regras isoladas no domínio.
* **Testes de Integração:** Validação do fluxo completo (API -> Service -> Banco) usando `WebApplicationFactory`.

---

## 🛠️ Guia de Endpoints (v1)

A API utiliza **Swagger/OpenAPI** para documentação. Ao rodar o projeto, acesse o link abaixo para testar os endpoints interativamente:

👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

### 👤 Gerenciamento de Contas (`/api/accounts`)
* `POST /api/accounts` - Criação de conta com depósito inicial.
* `GET /api/accounts/{id}` - Consulta de dados e saldo atual.

### 💸 Transações (`/api/transaction`)
* `POST /api/transaction/deposit` - Realiza um aporte financeiro.
* `POST /api/transaction/withdraw` - Realiza um saque (valida saldo insuficiente).
* `GET /api/transaction/statement/{accountId}` - Extrato histórico completo.

---

## 🔐 Configuração de Ambiente

Para garantir a segurança, as credenciais sensíveis não são versionadas. Antes de iniciar, configure o seu ambiente:

1. Localize o arquivo `.env.example` na raiz do projeto.
2. Crie uma cópia deste arquivo e renomeie para **`.env`**.
3. O Docker Compose carregará automaticamente estas variáveis para configurar o banco e a API.
4. Ajuste as variáveis conforme necessário para acessar o banco PostgreSQL e configurar a API.

---

## 🚀 Setup Instantâneo com Docker

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/jamerson-mt/jjbanking-api.git](https://github.com/jamerson-mt/jjbanking-api.git)
   cd jjbanking-api