# Rota de Viagem #

Um turista deseja viajar pelo mundo pagando o menor preço possível independentemente do número de conexões necessárias.
Este programa visa facilitar ao nosso turista, escolher a melhor rota para sua viagem.

Para isso precisamos inserir as rotas através de um arquivo de entrada.

### Execução do Console ###
* Realizar o download do repositório
* Ter [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.302-windows-x64-installer) ou superior instalado
* Navegar para a pasta raíz do repositório.
* Buildar e rodar aplicação através do seguintes comandos: 
```shell
$ dotnet build
$ dotnet run --project ".\Desafio.Bexs.Console\Desafio.Bexs.Console.csproj" -- "<path para input-file.txt>"
```

### Execução da API ###

* Realizar o download do repositório
* Ter [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.302-windows-x64-installer) ou superior instalado
* Navegar para a pasta raíz do repositório.
* Buildar e rodar aplicação através do seguintes comandos: 
```shell
$ dotnet build
$ dotnet run --project ".\Desafio.Bexs.Api\Desafio.Bexs.Api.csproj" -- "<path para input-file.txt>"
```
O Swagger que contém detalhes da API poderá ser visualizado através da url: `http://localhost:5000/swagger/`

### Estrutura de pasta ###

* `\buscador-rotas`: raiz do projeto, contém a solução e README
* `\buscador-rotas\Desafio.Bexs.Api`: contém o projeto de API e faz referência aos projeto de IoC e Domain.
* `\buscador-rotas\Desafio.Bexs.Console`: contém o projeto de Console e faz referência aos projeto de IoC e Domain.
* `\buscador-rotas\Desafio.Bexs.Domain`: contém o core da aplicação, como definição de interfaces, entidades de modelo, e serviços. Não faz referência a nenhum outro projeto.
* `\buscador-rotas\Desafio.Bexs.Data`: contém implementações de repositórios da aplicação. Faz referência ao projeto Domain para acesso as interfaces de repositório.
* `\buscador-rotas\Desafio.Bexs.Infra.IoC`: contém extensões para facilitar a inversão de controle. Faz referência ao projeto Domain para acesso as interfaces e entidades, e também faz referência ao projeto de Data para ter acesso as implementações de repositório.
* `\buscador-rotas\Desafio.Bexs.Tests`: contém extensões para facilitar a inversão de controle. Faz referência ao projeto Domain para acesso as interfaces e entidades, e também faz referência ao projeto de Data para ter acesso as implementações de repositório.

### Descrição API ###
O projeto de API contempla 2 endpoints: 
* `GET /api/MelhorRota?aeroportoOrigemId=GRU&aeroportoDestinoId=CDG`: endpoint que realizará consulta de melhor rota. É obrigatório informar os parâmetros `aeroportoOrigemId` e `aeroportoDestinoId`. Para rotas válidas, a API deve retornar statusCode 200 e o body deve conter os aeroportos da rota, o preço total da rota, e também uma descrição. Por exemplo:
```json
{
  "aeroportos": [
    "GRU",
    "BRC",
    "SCL",
    "ORL",
    "CDG"
  ],
  "precoTotalRota": 40,
  "descricao": "best route: GRU-BRC-SCL-ORL-CDG > $40"
}
```
Em caso de parâmetros inválidos, a API retornará o statusCode 400 e o body com descrição do erro:
```json
{
  "erro": 400,
  "messagem": "route not found"
}
```

* `POST /api/Rotas`: endpoint que realizará cadastro de nova rota. É obrigatório informar os parâmetros `aeroportoOrigemId`, `aeroportoDestinoId` e `preco` no body json. Por exemplo:
```json
{
  "preco": 200,
  "aeroportoOrigemId": "GRU",
  "aeroportoDestinoId": "LAX"
}
```
Para rotas cadastradas com sucesso, a API deve retornar statusCode 201 e o body deve conter os mesmos dados de entrada com adição do Id. Por exemplo:
```json
{
  "id": "GRU-LAX",
  "preco": 200,
  "aeroportoOrigemId": "GRU",
  "aeroportoDestinoId": "LAX"
}
```
Em caso de parâmetros inválidos, a API retornará o statusCode 400 e o body com descrição do erro:
```json
{
  "erro": 400,
  "messagem": "route not found"
}
```

### Melhorias Futuras ###
* Avaliar a possibilidade de migrar persistência em .CSV para um banco de dados de maior escalabilidade.
* Implementar novos endpoints para que as APIs sejam RESTFull
* Implementar testes de integração
