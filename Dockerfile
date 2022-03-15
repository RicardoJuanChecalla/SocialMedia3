# https://hub.docker.com/
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
# EXPOSE 5001

# copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["SocialMedia3.Api/SocialMedia3.Api.csproj", "SocialMedia3.Api/"]
COPY ["SocialMedia3.Core/SocialMedia3.Core.csproj", "SocialMedia3.Core/"]
COPY ["SocialMedia3.Infrastructure/SocialMedia3.Infrastructure.csproj", "SocialMedia3.Infrastructure/"]
COPY ["SocialMedia3.IntegrationTests/SocialMedia3.IntegrationTests.csproj", "SocialMedia3.IntegrationTests/"]
COPY ["SocialMedia3.UnitTests/SocialMedia3.UnitTests.csproj", "SocialMedia3.UnitTests/"]

RUN dotnet restore "SocialMedia3.Api/SocialMedia3.Api.csproj"

# copy everything else and build app
COPY . .
WORKDIR "/src/SocialMedia3.Api"
RUN dotnet build "SocialMedia3.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialMedia3.Api.csproj" -c Release -o /app/publish

# final stage/image
FROM base AS final
RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so
RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/libdl.so
RUN apt-get clean && apt-get update
RUN apt-get install -y apt-transport-https
RUN apt-get install -y --allow-unauthenticated --no-install-recommends libgdiplus libc6-dev libx11-dev
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed -i "s/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g" "/etc/ssl/openssl.cnf"
RUN sed -i "s/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g" "/etc/ssl/openssl.cnf"
ENV TZ America/Lima
ENTRYPOINT ["dotnet", "SocialMedia3.Api.dll"]