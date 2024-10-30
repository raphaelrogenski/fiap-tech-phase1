# TechChallenge-PhaseOne
PÓS TECH FIAP - Projeto Fase 1 - GRUPO 60

# Running SQL Server on Docker
Run the following command:
```
docker run -d --name sqlserver -p 1433:1433 --restart unless-stopped -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=T3chCh4ll4ng3Phase1" mcr.microsoft.com/mssql/server:2022-latest
```
Note that the command above don't create any volumes, this way, if you delete the container, the data will also be deleted.

Update the ConnectionString to:
```
"Server=localhost;Database=G60_TechChallenge_01;User Id=sa;Password=T3chCh4ll4ng3Phase1;MultipleActiveResultSets=true;Encrypt=False;"
```
