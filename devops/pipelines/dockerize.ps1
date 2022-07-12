#Vars
$project = "upgrade.identity-server"
$docker_domain = "egonzalezatos"

#build
rm -r ./release
dotnet clean
dotnet build
dotnet publish -c Release -o release

#dockerize
docker rmi $docker_domain/$project
docker build -t $docker_domain/$project -f ./devops/docker/Dockerfile .
docker push $docker_domain/$project

#clean files
rm -r ./release
