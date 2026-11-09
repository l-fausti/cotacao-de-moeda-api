# 📈 DashFinance API (.NET 9 / ASP.NET Core)

API RESTful desenvolvida em C# e ASP.NET Core com foco no consumo assíncrono de serviços externos de mercado financeiro, processamento concorrente e orquestração de dados de cotação de moedas em tempo real.

O objetivo técnico deste projeto é aplicar padrões de desenvolvimento backend para consumo de serviços I/O de alta concorrência sem bloquear threads do servidor, utilizando `async`/`await` e chamadas paralelas com `Task.WhenAll`.

---

## Tecnologias e Conceitos Aplicados

* **ASP.NET Core Web API** (Controllers Architecture)
* **Programação Assíncrona (`async` / `await` / `Task<T>`)**
* **Paralelismo de Requisições (`Task.WhenAll`)** para otimização de latência I/O
* **Injeção de Dependência (`IHttpClientFactory` & Scoped Services)**
* **Parsing de JSON Assíncrono (`JsonDocument` / `System.Text.Json`)**
* **Tratamento de Cultura (`CultureInfo.InvariantCulture`)**
* **Documentação OpenAPI (Swagger)**

---

## Arquitetura e Fluxo de Execução

A API atua como uma camada integradora entre o cliente e provedores externos de dados financeiros (AwesomeAPI). 

Em vez de realizar chamadas HTTP de forma sequencial (onde a latência total seria a soma de todas as requisições), a aplicação dispara as `Tasks` simultaneamente e aguarda a resolução conjunta antes de serializar o contrato final.

```text
Client (Postman/Frontend) 
        │
        ▼
 FinanceController 
        │
        ▼
  CotacaoService ──(Task.WhenAll)──► [GET /last/USD-BRL] (Paralelo)
        │                          ► [GET /last/EUR-BRL] (Paralelo)
        │                          ► [GET /last/BRL-USD] (Paralelo)
        ▼
 ResumoDashboard (JSON DTO + Métricas de Resposta)


---

## 🛠️ Como Executar o Projeto

### Pré-requisitos
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) ou superior

### Passos

1. Clone o repositório:
   ```bash
   git clone [https://github.com/seu-usuario/cotacao-moeda-api.git](https://github.com/seu-usuario/cotacao-moeda-api.git)
   cd cotacao-moeda-api/DashFinanceApi

2. Restaure as dependências e rode a aplicação:
    dotnet restore
    dotnet run

3. Acesse a documentação Swagger/OpenAPI pela URL local gerada no terminal:
    http://localhost:{PORTA}/swagger
