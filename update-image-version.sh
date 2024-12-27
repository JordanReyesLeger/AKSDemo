#!/bin/bash

# Variables
IMAGE_VERSION=$1
DEPLOYMENT_FILE="aks-deployment.yaml"

# Reemplazar la versión de la imagen en el archivo YAML
sed -i "s|image: akscontenedoresdev02.azurecr.io/pasteleriaapi:.*|image: akscontenedoresdev02.azurecr.io/pasteleriaapi:${IMAGE_VERSION}|g" $DEPLOYMENT_FILE
sed -i "s|image: akscontenedoresdev02.azurecr.io/pasteleriaportal:.*|image: akscontenedoresdev02.azurecr.io/pasteleriaportal:${IMAGE_VERSION}|g" $DEPLOYMENT_FILE

echo "Updated image versions to ${IMAGE_VERSION} in ${DEPLOYMENT_FILE}"
