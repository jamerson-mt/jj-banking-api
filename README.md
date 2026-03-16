# 🏦 JJ Banking API
> **"A espinha dorsal financeira para suas aplicações modernas."**

![.NET 10](https://img.shields.io/badge/.NET-10.0-512bd4?style=for-the-badge&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-green?style=for-the-badge)

A **JJ Banking API** é um motor de serviços financeiros de alta performance desenvolvido para ser o "Core Banking" definitivo para desenvolvedores. Construída com o que há de mais moderno no ecossistema **.NET 10**, esta API oferece uma infraestrutura robusta, escalável e pronta para uso em projetos de Front-end, Mobile ou sistemas de gestão financeira.

---

## 📖 Índice
- [Visão Geral](#-visão-geral)
- [Diferenciais Estratégicos](#-diferenciais-estratégicos)
- [Stack Tecnológica](#-stack-tecnológica)
- [Arquitetura (Clean Architecture)](#-arquitetura-clean-architecture)
- [Setup Instantâneo com Docker](#-setup-instantâneo-com-docker)
- [Guia de Endpoints](#-guia-de-endpoints)
- [Roadmap e Contribuição](#-roadmap-e-contribuição)

---

## 🌟 Visão Geral

A proposta da **JJ Banking API** é eliminar a necessidade de mocks estáticos e oferecer um backend financeiro persistente e documentado para a comunidade. Seja para validar um fluxo de carteira digital em React ou para estudar padrões de concorrência em transações bancárias, a JJ API entrega uma infraestrutura pronta para integrar.

### 🎯 Para quem é este projeto?
* **Devs Frontend/Mobile:** Consuma uma API real via Swagger e veja seus dados persistidos em um banco PostgreSQL.
* **Devs Backend:** Explore as novas funcionalidades do **C# 14** e a robustez do **Entity Framework Core 10**.
* **Open Source Contributors:** Use como base para criar novas funcionalidades e aprimorar seus conhecimentos em arquitetura de sistemas.

---

## 💎 Diferenciais Estratégicos

1. **Persistência Industrial:** Utiliza **PostgreSQL**, garantindo integridade transacional e suporte a grandes volumes de dados.
2. **Encapsulamento de Domínio:** Regras de negócio protegidas dentro do Core (Domain), impedindo estados inválidos de saldo ou transações.
3. **Developer Experience (DX):** Foco total no "Clonou, Rodou" via Docker Compose.
4. **Arquitetura Escalável:** Estrutura preparada para suportar o crescimento da aplicação e futura migração para microserviços.

---

## 🛠️ Stack Tecnológica

* **Runtime:** .NET 10 (ASP.NET Core)
* **Linguagem:** C# 14
* **Database:** PostgreSQL 17+
* **ORM:** Entity Framework Core 10
* **Container:** Docker & Docker Compose
* **Documentação:** Swagger (OpenAPI 3.1)

---

## 🏗️ Arquitetura (Clean Architecture)

O projeto é dividido em camadas para garantir manutenibilidade e testabilidade:

* **`JJBanking.Domain`**: Entidades, interfaces e lógica de negócio pura.
* **`JJBanking.Infra`**: Implementação de repositórios, contexto do PostgreSQL e Migrations.
* **`JJBanking.API`**: Gerenciamento de rotas, DTOs e Injeção de Dependência.

---

## 🚀 Setup Instantâneo com Docker

Esqueça configurações manuais. Com o Docker, você sobe toda a stack **JJ Banking** com apenas um comando:

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/seu-usuario/jj-banking-api.git](https://github.com/seu-usuario/jj-banking-api.git)
   cd jj-banking-api