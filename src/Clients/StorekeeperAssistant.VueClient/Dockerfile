# build stage
FROM node:16.13.2 as build-stage
WORKDIR /app
COPY Clients/StorekeeperAssistant.VueClient/package*.json ./
RUN npm install
COPY Clients/StorekeeperAssistant.VueClient .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 1315
CMD ["nginx", "-g", "daemon off;"]