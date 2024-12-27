# 1.	Inicia sesión en Azure:
az login
# 2.	Inicia sesión en tu ACR:
az acr login --name akscontenedoresdev02.azurecr.io
   
# 3.	Compila la imagen de Docker del portal:
docker build -f "C:\Dev\Tsp-AKS\Portal\Dockerfile" --force-rm -t akscontenedoresdev02.azurecr.io/pasteleriaportal:1.0.2 --build-arg "BUILD_CONFIGURATION=Debug" "C:\Dev\Tsp-AKS"

# 3.1	Compila la imagen de Docker del api:
docker build -f "C:\Dev\Tsp-AKS\PasteleriaApi\Dockerfile" --force-rm -t akscontenedoresdev02.azurecr.io/pasteleriaapi:1.0.2 --build-arg "BUILD_CONFIGURATION=Debug" "C:\Dev\Tsp-AKS"

# 4.	Sube la imagen a tu ACR portal:
docker push akscontenedoresdev02.azurecr.io/pasteleriaportal:1.0.2

# 4.1	Sube la imagen a tu ACR api:
docker push akscontenedoresdev02.azurecr.io/pasteleriaapi:1.0.2

# 5. Configura kubectl para usar tu clúster de AKS:
az aks get-credentials --resource-group rg-containers-dev-001 --name akscontenedoresdev02
   
# 6. Publica la imagen en tu AKS:
kubectl apply -f "C:\Dev\Tsp-AKS\aks-deployment.yaml"

# 7. Verifica que los despliegues y servicios estén corriendo:
kubectl get deployments
kubectl get services

kubectl get pods

kubectl logs pasteleriaapi-deployment 

kubectl delete pod <nombre-del-pod>


# 8.Borrar el despliegue y el servicio:
kubectl delete -f "C:\Dev\Tsp-AKS\aks-deployment.yaml"

   

   

   
   
   