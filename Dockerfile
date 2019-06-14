FROM microsoft/dotnet:2.2-sdk-alpine AS builder
WORKDIR /app

COPY *.fsproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "fsharp-webapi.dll"]
