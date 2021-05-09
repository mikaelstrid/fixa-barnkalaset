# With inspiration from https://mherman.org/blog/dockerizing-a-react-app/ and https://stackoverflow.com/questions/58482655/what-is-the-closest-to-npm-ci-in-yarn

# build environment
FROM node:16.1.0-alpine as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY package.json ./
COPY yarn.lock ./
RUN yarn install --frozen-lockfile --silent
RUN yarn add react-scripts@3.4.1 -g --silent
COPY . ./
RUN npm run build

# production environment
FROM nginx:stable-alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]