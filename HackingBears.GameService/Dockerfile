FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic

# Configure locale and timezone
ENV LANG=de_CH.UTF8
RUN ln -sf /usr/share/zoneinfo/Europe/Zurich /etc/localtime
RUN echo "Europe/Zurich" > /etc/timezone

WORKDIR /app
COPY bin/Debug/netcoreapp3.1 .

LABEL io.k8s.display-name="app name" \
      io.k8s.description="container description..." \
      io.openshift.expose-services="8080:http"

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "HackingBears.GameService.dll"]