# BlueBank
Aplicação bancaria onte o usuário pode cadastrar novos usuários, contas, fazer transações (saques, depósitos e transferências), e checar extrato bancário.

# Instalação

# Documentação da API:

Caso prefira, você pode acessar a documentação do swagger acessando a url `https://localhost:5001/swagger/index.html`

## Descrição dos Endpoints

### Rotas relativas a Conta

 **/account - POST** - Rota para criação de conta. 
Recebe  através do **body** da requisição o nome e documento do usuário e cria uma conta.
```json
// Corpo da requisição:
   {
   	"name":"João da Silva",
   	"doc": "1235678910"
   }
```
 **/account/{id} - GET** - Rota para exibir uma conta especifica. 
Recebe  através da **URL** da requisição o id da conta e retorna a conta especifica.

 **/account/{id} - DELETE** - Rota para excluir uma conta especifica. 
Recebe  através da **URL** da requisição o id da conta e exclui a conta especifica.

### Rotas relativas a Contatos

 **/contact/{accountNumber} - POST** - Rota para adicionar contato ao usuário de uma conta especifica. Recebe  através da **URL** da requisição o id da conta e através do **body** da requisição o novo número a ser adicionado.
```json
// Corpo da requisição:
   {
   	"phoneNumber":"21912345678"
   }
```

 **/contact/{/Contact/{document} - DELETE** - Rota para excluir contato especifico do usuário de uma conta especifica. Recebe  através da **URL** da requisição o id da conta e através do **body** da requisição o novo número a ser adicionado.
```json
// Corpo da requisição:
   {
   	"phoneNumber":"21912345678"
   }
```
### Rotas relativas a Transações

 **/deposit/{accountNumber}  - POST** - Rota para depósitos. Recebe  através da **URL** da requisição o id da conta e através do **body** da requisição o valor a ser depositados.
```json
// Corpo da requisição:
   {
   	"value":"100"
   }
```

 **/withdraw/{accountNumber}  - POST** - Rota para saques. Recebe  através da **URL** da requisição o id da conta e através do **body** da requisição o valor a ser sacado.
```json
// Corpo da requisição:
   {
   	"value":"100"
   }
```

 **/transfer/{accountNumber}/  - POST** - Rota para transferências. Recebe  através da **URL** da requisição o id da conta e através do **body** da requisição o valor a ser transferido e a conta destino.
```json
// Corpo da requisição:
	{
	  "value": 100,
	  "accountNumberTo": 23456
	}
```

# Equipe
**Dan**  - github - linkedin
**Daniel** - github - linkedin
**Karine** - github - linkedin
**Alexandre** - github - linkedin
**Luciano** - github - linkedin
