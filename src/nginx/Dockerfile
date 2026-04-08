# Stage 1 — React build
FROM node:20-alpine AS client-build
WORKDIR /app
COPY client/package.json client/package-lock.json* ./
RUN npm install
ARG VITE_API_URL=""
ENV VITE_API_URL=$VITE_API_URL
COPY client/ .
RUN npm run build

# Stage 2 — nginx
FROM nginx:alpine
COPY --from=client-build /app/dist        /usr/share/nginx/html
COPY nginx/nginx.prod.conf                /etc/nginx/conf.d/default.conf
EXPOSE 80 443
CMD ["nginx", "-g", "daemon off;"]
