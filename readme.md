
Jag använde Net 6.0
Om du vill testa Api via post man så skriv dotnet run och länken till api är
https://localhost:5000/api
Films
https://localhost:5000/api/films/
Filmstudio
https://localhost:5000/api/filmstudio

Om du vill kolla på Klientgränssnittet så tryck på IIS Express eller gå till länken https://localhost:5000

-----------------------------------------------
Om du vill testa andra tillgängliga examplar så skriv så här
[
    {
    "op": "replace",
    "path": "/FilmCopies/-",
    "value": {
        "FilmCopyId" : 1,
        "RentedOut": true,
        "StudioId": 1
    }

}
]
Method är Put 
-----------------------------------------