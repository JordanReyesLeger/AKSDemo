# Build and run the project
docker build -f "C:\Dev\Tsp-AKS\Portal\Dockerfile" --force-rm -t pasteleriaportal:last --build-arg "BUILD_CONFIGURATION=Debug" "C:\Dev\Tsp-AKS"

# Run the container
docker run -d --name PasteleriaPortal -p 8082:8082 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_HTTP_PORTS=8082 pasteleriaportal:last

