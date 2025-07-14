# deploy.ps1 - Build Docker image, delete old pods, apply Kubernetes manifests, and restart deployment

# Step 1: Build Docker image
Write-Host "Building Docker image..."
docker build -t obiletjourneyapp-web .

if ($LASTEXITCODE -ne 0) {
    Write-Error "Docker build failed! Exiting."
    exit 1
}

# Step 2: Delete existing web pods to force reload the new image
Write-Host "Deleting existing obilet-web pods..."
kubectl delete pod -l app=obilet-web

if ($LASTEXITCODE -ne 0) {
    Write-Warning "Could not delete pods. Pods might not exist yet."
}

# Step 3: Apply Kubernetes manifests to ensure all resources are up to date
Write-Host "Applying Kubernetes manifests..."
kubectl apply -f kubernetes/

if ($LASTEXITCODE -ne 0) {
    Write-Error "kubectl apply failed! Exiting."
    exit 1
}

# Step 4: Restart deployment rollout (optional but good practice)
Write-Host "Restarting obilet-web deployment rollout..."
kubectl rollout restart deployment obilet-web

if ($LASTEXITCODE -ne 0) {
    Write-Warning "Failed to restart rollout. Please check your deployment."
}

Write-Host "Deployment script finished successfully! Check http://localhost:30080"
