# 🏦 BankManager Console App

Um sistema bancário robusto desenvolvido em **C#** para consolidar conceitos de **Programação Orientada a Objetos (POO)** e persistência de dados. O projeto foi projetado para ser executado de forma ágil através de **Docker**, garantindo que o ambiente de desenvolvimento seja idêntico em qualquer máquina, especialmente otimizado para **WSL2**.

## 🚀 Tecnologias e Ferramentas
* **Linguagem:** C# (.NET 10)
* **Banco de Dados:** SQLite (Persistência leve e eficiente)
* **Containerização:** Docker & Docker Compose
* **Ambiente de Desenvolvimento:** WSL2 (Ubuntu)
* **IDE Sugerida:** VS Code com extensão WSL

## 🏗️ Princípios de Engenharia Aplicados
Este projeto não é apenas um script, mas uma aplicação estruturada seguindo boas práticas:
* **Encapsulamento:** Gerenciamento rigoroso do estado financeiro (Saldo) através de métodos específicos.
* **Padronização de Dados:** Uso de Repository Pattern para isolar a lógica do SQLite da lógica de negócio.
* **Volume Docker:** Persistência garantida do arquivo `.db`, permitindo que os dados sobrevivam ao reinício dos containers.



## 🛠️ Como Executar

Você não precisa ter o SDK do .NET instalado localmente, apenas o **Docker**.

### 1. Clonar o Repositório
```bash
git clone [https://github.com/seu-usuario/BankManager-Console-App.git](https://github.com/seu-usuario/BankManager-Console-App.git)
cd BankManager-Console-App
