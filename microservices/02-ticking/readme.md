## Start
$skaffold dev

## Notes

### Creating a secret on K8s
$kubectl create secret generic <key> --from-literal=<value>
Ex: $kubectl create secret generic jwt-secret --from-literal=JWT_KEY=secret

### Making request cross namespace
Ex: call to api/users/currentuser from pod client
http://SERVICENAME.NAMESPACE.svc.cluster.local/api/users/currentuser
SERVICENAME=ingress-nginx-controller
NAMESPACE=ingress-nginx


## Test

### Tools
jest, suppertest

#### NPM package
jest ts-jest supertest mongodb-memory-server


## NPM
### Publish package
$npm login
$npm publish --access public

## NATS Streaming Server
### Use kubectl forward port, just for test
$kubectl port-forward <pod_name> 4222:4222

### Use QueueGroup to prevent message send for clone instance
### Use setManualAckMode(true) to manual set message is handled