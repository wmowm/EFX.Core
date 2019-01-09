FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /Test
COPY . .
EXPOSE 5101
ENTRYPOINT ["dotnet", "Test.Docker.dll","-b","0.0.0.0"]
