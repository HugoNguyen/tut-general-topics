## Start
$skaffold dev

## Notes

### Creating a secret on K8s
$kubectl create secret generic <key> --from-literal=<value>
Ex: $kubectl create secret generic jwt-secret --from-literal=JWT_KEY=secret


## Test

### Tools
jest, suppertest

#### NPM package
jest ts-jest supertest mongodb-memory-server
