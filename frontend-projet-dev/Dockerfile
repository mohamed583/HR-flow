# Étape 1 - Build
FROM node20 AS build
WORKDIR app
COPY package.json .
RUN npm install
COPY . .
RUN npm run build

# Étape 2 - Nginx pour servir le build
FROM nginxalpine
COPY --from=build appdist usrsharenginxhtml
EXPOSE 80
CMD [nginx, -g, daemon off;]
