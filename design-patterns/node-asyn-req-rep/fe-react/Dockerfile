FROM node:14-alpine

WORKDIR /usr/app

COPY package.json ./
COPY package-lock.json ./
RUN npm ci
COPY ./ ./

#This line above will make Node.js the root process (PID1)
CMD ["npm", "start"]