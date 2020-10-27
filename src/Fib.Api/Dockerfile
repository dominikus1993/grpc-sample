FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ARG TRACER_VERSION=1.19.5
RUN mkdir -p /var/log/datadog
RUN mkdir -p /opt/datadog
RUN curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v${TRACER_VERSION}/datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb
RUN dpkg -i ./datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb

ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
ENV DD_INTEGRATIONS=/opt/datadog/integrations.json
ENV DD_DOTNET_TRACER_HOME=/opt/datadog
ENV DD_LOGS_INJECTION=true
ENV DD_ADONET_ENABLED=false

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