FROM ubuntu:20.04
EXPOSE 7282
#VOLUME ${HOME}/.aspnet/https:/https/
ENV ASPNETCORE_URLS=https://*:7282
ENV ASPNETCORE_HTTPS_PORT=7282
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.ssl/cert.crt
#ENV ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/root/.ssl/cert.key
ENV ASPNETCORE_ENVIRONMENT=Development
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
#RUN mkdir /root/.ssl/
#RUN openssl req -newkey rsa:4096 \
#            -x509 \
#            -sha256 \
#            -days 3650 \
#            -nodes \
#            -out /root/.ssl/cert.crt \
#            -keyout /root/.ssl/cert.key \
#            -subj "/C=RU/ST=Moscow/L=Moscow/O=ITCompany/OU=ITDepartment/CN=localhost"
RUN dotnet dev-certs https
RUN dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM
RUN update-ca-certificates
WORKDIR /app/
COPY . .
RUN dotnet restore
RUN dotnet build
WORKDIR /app/bin/Debug/net6.0/
ENTRYPOINT ["dotnet", "TestWebApi.dll"]