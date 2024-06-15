## CRIADO EM DOTNET 8.0 (NECESSÁRIO PARA RODAR)

## ALTERAÇÃO DE BANCO DE DADOS
 Para rodar na máquina, é necessário que se altere no arquivo Data/AppDbContext.cs a porta da sua máquina. (A que foi utilizado na criação é a 3306)

 ## SWAGGER
    localhost:5142/swagger/index.html

## AS ROUTES
POST
    localhost:(sua porta)/cadeiras

    body{
        "Numero": 32145,
        "Descricao": "Cadeira do presidente",
        "Valor" : 890.00
    }

GET (All)
    localhost:(sua porta)/cadeiras

GET (ById)
    localhost:(sua porta)/cadeiras/1

PUT
    localhost:(sua porta)/cadeiras/1
    body{
        "Numero": 2457,
        "Descricao": "Cadeira do vice-presidente",
        "Valor" : 790.00
    }

DELETE
    localhost:(sua porta)/cadeiras/1

 ## CONDIÇÕES

    Não pode salvar uma cadeira que possuir a mesma numeração da outra, inclusive isso também ocorrerá se tentar atualizar uma cadeira com a mesma numeração de uma já salva.




