# UI BUILD STAGE
FROM node:lts-alpine as ui-build-stage
WORKDIR /opt/app
COPY sources/UI/package*.json .
RUN npm install
COPY sources/UI .
RUN npm run build


# NGINX STAGE
FROM nginx:stable-alpine as production-stage
COPY .docker/dev/nginx/default.conf /etc/nginx/conf.d/

# copy ui
RUN rm -rf /usr/share/nginx/html/*
COPY --from=ui-build-stage /opt/app/dist /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]