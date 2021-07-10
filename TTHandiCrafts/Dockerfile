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
