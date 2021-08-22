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

### Problem with @types/node
If fn global.signin show show error, update @types/node inside package-lock.json to this
`
    "node_modules/@types/node": {
      "version": "13.9.5",
      "resolved": "https://registry.npmjs.org/@types/node/-/node-13.9.5.tgz",
      "integrity": "sha512-hkzMMD3xu6BrJpGVLeQ3htQQNAcOrJjX7WFmtK8zWQpz2UJf13LCFF2ALA7c9OVdvc2vQJeDdjfR35M0sBCxvw=="
    },
`

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
$kubectl port-forward <pod_name> 8222:8222

### Use QueueGroup to prevent message send for clone instance
### Use setManualAckMode(true) to manual set message is handled

### Healcheck
http://localhost:8222/streaming/channelsz?subs=1


## Stripe

### 1. Install
$npm install stripe

### 2. Create a Stripe account
Go to https://stripe.com/

### 3. Retrieve api test key
Go to Develop page, get secrect key

### 4. Mange secret key with kubectl
$kubectl create secret generic stripe-secret --from-literal STRIPE_KEY=sk_test_51JRB2gEwfPm4lLW0bXuety3Vof2yayqhZd4CoZHIwVls6w44ri9nRdX3GKYi7KHMbQKD1JgLBltpK3peNYEQzFBp00HzBqTolw