### STAGE 1: Build ###
FROM node:14-alpine AS build

WORKDIR /usr/app

COPY package.json ./
COPY package-lock.json ./
RUN npm ci
COPY ./ ./
RUN npm run build

### STAGE 2: Run ###
FROM node:14-alpine
WORKDIR /usr/app
COPY ./public ./public
COPY package.json ./
RUN npm install --only=production
COPY --from=build /usr/app/dist ./src
RUN npm install pm2 -g
EXPOSE 80
CMD ["pm2-runtime","src/index.js"]