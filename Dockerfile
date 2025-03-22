FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ShoppingList.API/ShoppingList.API.csproj", "ShoppingList.API/"]
RUN dotnet restore "ShoppingList.API/ShoppingList.API.csproj"
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ShoppingList.API.dll"]