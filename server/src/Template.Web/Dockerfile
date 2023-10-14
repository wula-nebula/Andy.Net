FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY . /publish  
WORKDIR /publish 
EXPOSE 80
CMD ["dotnet", "Template.Web.dll", "--server.urls", "http://*:80"]