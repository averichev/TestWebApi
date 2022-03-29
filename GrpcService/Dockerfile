FROM ubuntu:20.04
EXPOSE 7283
ENV ASPNETCORE_URLS=https://*:7283
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTPS_PORT=7283
ENV DEBIAN_FRONTEND noninteractive
ENV DEBCONF_NOWARNINGS="yes"
RUN apt-get update
RUN apt-get install -y wget
RUN apt-get update && apt-get install -y apt-transport-https curl
RUN apt-get install -y libicu-dev
RUN apt-get update
RUN wget https://dot.net/v1/dotnet-install.sh
RUN chmod u+x ./dotnet-install.sh
RUN ./dotnet-install.sh -c 6.0
RUN ln -s /root/.dotnet/dotnet  /usr/bin/dotnet
RUN dotnet --info
RUN rm dotnet-install.sh
RUN dotnet dev-certs https
RUN dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM
RUN update-ca-certificates
WORKDIR /app/
COPY . .
RUN dotnet restore
RUN dotnet build
WORKDIR /app/bin/Debug/net6.0/
ENTRYPOINT ["dotnet", "GrpcService.dll"]