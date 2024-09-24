# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# restore
COPY ["./ExpenseTracker/ExpenseTracker.csproj", "ExpenseTracker/"]
RUN dotnet restore 'ExpenseTracker/ExpenseTracker.csproj'

# build
COPY ["./ExpenseTracker", "ExpenseTracker/"]
WORKDIR /src/ExpenseTracker
RUN dotnet build 'ExpenseTracker.csproj' -c Release -o /app/build

# Stage 2: Publish Stage
FROM build as publish
RUN dotnet publish 'ExpenseTracker.csproj' -c Release -o /app/publish

# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5002
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseTracker.dll"]
