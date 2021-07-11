#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        tzdata \
     && rm -rf /var/lib/apt/lists/*
     
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=Asia/Ashgabat
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TTHandiCrafts/TTHandiCrafts.csproj", "TTHandiCrafts/"]
COPY ["TTHandiCrafts.Infrastructure/TTHandiCrafts.Infrastructure.csproj", "TTHandiCrafts.Infrastructure/"]
COPY ["TTHandiCrafts.UseCases/TTHandiCrafts.UseCases.csproj", "TTHandiCrafts.UseCases/"]
COPY ["TTHandiCrafts.Utils/TTHandiCrafts.Utils.csproj", "TTHandiCrafts.Utils/"]
COPY ["TTHandiCrafts.Infrastructure.Interfaces/TTHandiCrafts.Infrastructure.Interfaces.csproj", "TTHandiCrafts.Infrastructure.Interfaces/"]
COPY ["TTHandiCrafts.Models/TTHandiCrafts.Models.csproj", "TTHandiCrafts.Models/"]
RUN dotnet restore "TTHandiCrafts/TTHandiCrafts.csproj"
COPY . .
WORKDIR "/src/TTHandiCrafts"
RUN dotnet build "TTHandiCrafts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TTHandiCrafts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TTHandiCrafts.dll"]
