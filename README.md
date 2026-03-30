# 🏦 JJ Banking API
> **"Infraestrutura de Core Banking robusta, projetada para integridade financeira e alta disponibilidade."**

![.NET 10](https://img.shields.io/badge/.NET-10.0-512bd4?style=for-the-badge&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-green?style=for-the-badge)

A **JJ Banking API** é um motor de serviços financeiros de alta performance. Desenvolvida sob os princípios de **Domain-Driven Design (DDD)** e **Clean Architecture**, esta API oferece uma infraestrutura de "Core Banking" real, garantindo a consistência de dados em operações críticas de transferência e gestão de saldo.

---

## 📖 Índice
- [Diferenciais de Engenharia](#-diferenciais-de-engenharia)
- [Arquitetura e Qualidade](#-arquitetura-e-qualidade)
- [Regras de Negócio Implementadas](#-regras-de-negócio-implementadas)
- [Guia de Integração (Mobile/Front)](#-guia-de-integração-mobilefront)
- [Setup Instantâneo](#-setup-instantâneo-com-docker)

---

## 💎 Diferenciais de Engenharia

1. **Atomicidade e ACID:** Implementação de **Database Transactions** no EF Core, garantindo que transferências entre contas (Débito/Crédito) ocorram com sucesso mútuo ou falha total (*Rollback*), eliminando riscos de inconsistência de saldo.
2. **Precisão Financeira:** Utilização de `decimal(18,2)` em todas as camadas de persistência e cálculos, assegurando exatidão matemática contra erros de arredondamento inerentes a tipos de ponto flutuante.
3. **Segurança via DTOs:** Camada de abstração completa utilizando **Data Transfer Objects**, impedindo o vazamento de entidades de banco de dados e protegendo a integridade da API contra ataques de manipulação de propriedades.
4. **Identity & JWT:** Autenticação e autorização robustas via **ASP.NET Core Identity**, com emissão de Tokens JWT para consumo seguro por aplicações mobile e web.

---

## 🏗️ Arquitetura e Qualidade

O projeto segue rigorosamente a **Clean Architecture**, focada em testabilidade e baixo acoplamento:

* **`JJBanking.Domain`**: O "Coração" do sistema. Contém entidades puras, Enums de moeda, definição de DTOs e interfaces de domínio. **Zero dependências externas.**
* **`JJBanking.API`**: Orquestração da lógica de negócio e mapeamentos de dados.
* **`JJBanking.Infra`**: Camada de persistência com **PostgreSQL**, configurações de `Fluent API` para precisão decimal e gestão do `IdentityDbContext`.

---

## 💸 Regras de Negócio Implementadas

* **Transferência Segura:** Validação de saldo em tempo real e prevenção de transferências para a própria conta.
* **Geração de Conta Única:** Algoritmo de criação de número de conta (Ex: `4829-2`) com validação automática de duplicidade no cadastro.
* **Histórico Duplo (Double-Entry):** Cada transferência gera dois registros de transação vinculados (Débito para origem e Crédito para destino), permitindo auditoria e extratos precisos.

---

## 🛠️ Guia de Integração (Mobile/Front)

A API utiliza **Swagger/OpenAPI** turbinado com comentários XML para facilitar o consumo.

👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

### 🔐 Endpoints Principais
* `POST /api/Auth/register` - Cria usuário e gera o número da conta vinculado.
* `POST /api/Auth/login` - Autentica e retorna o Token JWT.
* `POST /api/Transfer` - Executa transferência atômica entre contas.
* `GET /api/Statement/{accountId}` - Retorna o extrato detalhado com filtros de data.

---

## 🚀 Setup Instantâneo com Docker

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/jamerson-mt/jjbanking-api.git](https://github.com/jamerson-mt/jjbanking-api.git)
   cd jjbanking-api
   docker compose up -d --build
   ```