# Oppgave 2 - Todo-API

<p align="center">
<img src=https://github.com/johnkors/MinimalHttpLogger/assets/206726/d73273aa-a077-45ce-9539-d582c022c651 width=200 />
</p>

Denne oppgaven er for å bli kjent med web-utvikling i .NET.

### 2.1 Lag et TODO-API

- [ ] Lag en `web` app ved hjelp av CLI-et (`dotnet new`)
- [ ] Få appen til å eksponere et API som kan lagre og hente TODOs:

TODO:

```json
{
  "id": 1,
  "name": "Lære web-dev i .NET",
  "isComplete": false
}
```

API krav spec:

```http filename=test.http
@baseUrl = http://localhost:5000
@todoId = 1

### Burde svare med en tom array hvis det ikke er noen todos
GET {{baseUrl}}/todos

### Skal filtrere på ferdigstilte:
GET {{baseUrl}}/todos?complete

### Burde svare med samme som input, og en location response header til ny-opprettet ressurs
POST {{baseUrl}}/todos
Content-Type: application/json

{
  "name" : "soft-chore",
  "isComplete" : false
}

### Henter en spesifikk todo
GET {{baseUrl}}/todos/{{todoId}}

### Oppdatere en todo
PUT {{baseUrl}}/todos/{{todoId}}
Content-Type: application/json

{
  "name" : "hard-chore",
  "isComplete" : true
}

### Svarer med slettet todo
DELETE {{baseUrl}}/todos/{{todoId}}
```
