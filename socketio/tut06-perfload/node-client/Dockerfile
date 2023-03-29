### STAGE 1: Build ###
FROM node:17-alpine AS build

WORKDIR /usr/app

COPY package.json ./
COPY package-lock.json ./
RUN npm ci
COPY ./ ./

### STAGE 2: Run ###
FROM node:17-alpine
WORKDIR /usr/app
COPY package.json ./
RUN npm install --only=production
COPY --from=build /usr/app .
RUN npm install pm2 -g
EXPOSE 80
CMD ["pm2-runtime","index.js"]