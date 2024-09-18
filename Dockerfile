# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# restore
COPY ["src/Domain/ExpenseTrackerGroup1.csproj", "Domain/"]
RUN dotnet restore 'Domain/ExpenseTrackerGroup1.csproj'

# build
COPY ["src/Domain", "Domain/"]
WORKDIR /src/Domain
RUN dotnet build 'ExpenseTrackerGroup1.csproj' -c Release -o /app/build

# Stage 2: Publish Stage
FROM build as publish
RUN dotnet publish 'ExpenseTrackerGroup1.csproj' -c Release -o /app/publish

# Stage 3: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5002
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseTrackerGroup1.dll"]