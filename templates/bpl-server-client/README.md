# Clone project

# How to use

## For local
`bash
$cd <root_proj>
$docker-compose up
`

## For development
- Restore packages (optional, if want to full control)
`bash
$cd <root_proj>/server
$npm ci

$cd <root_proj>/client
$npm ci
`
- Start container
`bash
$cd <root_proj>
$docker compose -f docker-compose.yaml -f docker-compose.dev.yaml up
`

Note: for development, config mount volumn may be different depend on local machine

### To debug
Note: current support debug project backend (because it write by nodejs)

- Replace command docker compose up with this

`bash
$docker compose -f docker-compose.yaml -f docker-compose.dev.yaml -f docker-compose.debug.yaml up
`

Note: Backend container will expose port 9229. We can attach remote debug to the container

- Open root solution folder in Visual Studio Code
- Press F5 to start debug (name of profile lauching will be Backend | Docker: Attach to Node)
