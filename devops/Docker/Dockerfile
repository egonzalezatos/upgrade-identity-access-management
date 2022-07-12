FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80
WORKDIR /app
COPY ./release .

#Config ENVS
#DATABASE
ENV DB_SERVER=identity-server-db
ENV DB_USERNAME=admin
ENV DB_PASSWORD=admin 
ENV DB_PORT=1433
ENV DB_DATABASE=IdentityServerDb

ENTRYPOINT ["dotnet", "IdentityServer.dll"]