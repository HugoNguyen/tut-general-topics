### Stucture
/client
/comments
/event-bus
/moderation
/posts
/query
/infra: contain all the configuration off all code realted to the deployment or management

### Build images
$docker build -t hugo2908/01-simple-app-posts ./posts/
$docker build -t hugo2908/01-simple-app-event-bus ./event-bus/
$docker build -t hugo2908/01-simple-app-comments ./comments/
$docker build -t hugo2908/01-simple-app-moderation ./moderation/
$docker build -t hugo2908/01-simple-app-query ./query/
$docker build -t hugo2908/01-simple-app-client ./client/

$kubectl apply -f ./infra/k8s/posts-depl.yaml
$kubectl apply -f ./infra/k8s/posts-srv.yaml
$kubectl apply -f ./infra/k8s/query-depl.yaml
$kubectl apply -f ./infra/k8s/moderation-depl.yaml
$kubectl apply -f ./infra/k8s/comments-depl.yaml
$kubectl apply -f ./infra/k8s/event-bus-depl.yaml
$kubectl apply -f ./infra/k8s/ingress-srv.yaml
$kubectl apply -f ./infra/k8s/client-depl.yaml

or
$kubectl apply -f ./infra/k8s/



### Useful cmd

#### Troubleshooting: Unable to pull a private image 
$kubectl describe pod <pod_name>

#### Print out infor aboult all of the running pods
$kubectl get pods

#### Execute the given command in a running pod
$kubectl exec -it <pod_name> -- <cmd>
Ex: kubectl exec -it posts -- sh

#### Print out logs from the given pod
$kubectl logs <pod_name>

#### Delete pod
$kubectl delete pod <pod_name>


#### List all the running deployments
$kubectl get deployments

#### Print out detail about a specifice deployment
$kubectl describe deployment <depl_name>

#### Delete a deployment
$kubectl delete deployment <depl_name>

#### K8s restart deployment
$kubectl rollout restart deployment <depl_name>

#### Apply config
$kubectl apply -f <path_to_yaml_file>


#### Execute the given command in a running container
$docker exec -it <container_id> <cmd>

#### Find minikube ip
$minikube ip
Ex: 192.168.49.2
$curl http://192.168.49.2:31510/posts
$curl -d '{"title":"POST", "key2":"value2"}' -H "Content-Type: application/json" -X POST http://192.168.49.2:31510/posts


#### Skaffold
$Skaffold dev