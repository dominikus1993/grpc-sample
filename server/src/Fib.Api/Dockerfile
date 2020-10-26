FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.403 AS build
WORKDIR /src
COPY . .
RUN dotnet tool restore
ENV PATH="/root/.dotnet/tools:${PATH}"

WORKDIR /src/src/Fib.Api

RUN dotnet paket restore
RUN dotnet restore -nowarn:msb3202,nu1503
RUN find /root/.nuget/packages/grpc.tools/ -type f -name "protoc" -o -name "grpc_csharp_plugin" | xargs chmod u+x
RUN dotnet build -c Release -o /app


FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .


ENTRYPOINT ["dotnet", "Fib.Api.dll"]