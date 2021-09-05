# Get Start

1. Install next.js
```
$npx create-next-app .
```

2. Start project
```
$docker-compose up -d
$npm run dev
```

3. Package for Authentication
``` bash
$npm i validator bcryptjs
$npm i next-auth
```

4. Cloudinary account
name: hugo-dev-vn

# Nextjs

## [Custom Document](https://nextjs.org/docs/advanced-features/custom-document)

## Strategy define style for each page
```
project
└───styles
│   │   golbales.css
│   │   Home.module.css
│   │   Another.module.css
│   
└───pages
    │   index.js <-- include Home.module.css
    │   another.js


<index.js>
import styles from '../styles/Home.module.css'

<another.js>
import styles from '../styles/Another.module.css'

```

## [Fetch Data](https://nextjs.org/docs/basic-features/data-fetching)

1. `getStaticProps` (Static Generation): Fetch data at build time.


2. `getStaticPaths` (Static Generation): Specify dynamic routes to pre-render pages based on data.


3. `getServerSideProps` (Server-side Rendering): Fetch data on each request.

Ex:
```
project
└───pages
│   └───users
│       │   [id].js
│       │   index.js

```

> Case 1: To pre-generate data for index.js
``` js
export async function getStaticProps(context) {
  const users = await fetch('/api/users');
  return {
    props: { users }, // will be passed to the page component as props
  }
}
```


> Case 2.1: To pre-generate data for [id].js
``` js
export async function getStaticProps(context) {
  const id = context.params.id;    
  const user = await fetch(`/api/users/${id}`);
  return {
    props: { user }, // will be passed to the page component as props
  }
}

export async function getStaticPaths() {
    // Get all users
    const users = await fetch('/api/users');

    const ids = users.map(user => user.id);

    const paths = ids.map(id => ({ params: { id: id.toString() }}));

    /*
    * paths: [
        { params: { id: '1' }},
        { params: { id: '2' }},
    ]
    */
    return {
        paths
    }
}

```


> Case 2.2: [id].js. Not pre-generate data. Fetch new every request
``` js
export async function getServerSideProps(context) {
  const id = context.params.id;    
  const user = await fetch(`/api/users/${id}`);
  return {
    props: { user }, // will be passed to the page component as props
  }
}
```

## Using next/image
> [Usage](https://nextjs.org/docs/messages/next-image-unconfigured-host)

``` js
// next.config.js
module.exports = {
  images: {
    domains: ['res.cloudinary.com'],
  },
}

```

## Support Redux

### Install
``` bash
$npm i redux redux-thunk redux-devtools-extension react-redux next-redux-wrapper
```

## Next-Auth.js

### Setup

To add NextAuth.js to a project create a file called [...nextauth].js in pages/api/auth. NextAuth.js requires no modification to the next.config.js file.

All requests to /api/auth/* (signin, callback, signout, etc) will automatically be handed by NextAuth.js.

### Config [...nextauth].js
1. Setup provider
2. Set jwt
