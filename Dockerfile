FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactApi.csproj","."]
RUN dotnet restore "ContactApi.csproj"
COPY . .

RUN dotnet build "ContactApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","ContactApi.dll"]


# construire l'image
# Ouvrir un terminal a la racine du projet (ou se trouve le dockerfile)
# docker build -t asp-contact .

# test de l'image
# docker run -d --name contact-asp -p 5005:8080 asp-contact
# http://localhost:5005/contacts