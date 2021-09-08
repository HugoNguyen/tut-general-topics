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

5. MailTrap
```bash
$npm i nodemailer
```

6. Stripe
```bash
$npm i stripe
$npm i @stripe/stripe-js
```
acc: nguyenquoctuan61089@gmail.com

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

## Protect routes, using getServerSideProps()

* Example 1: Protect page update user profile. If not login, redirect to login page
```js
// Protect => pages/me/updatejs
// If not logined, redirect to login page

export async function getServerSideProps(context) {
    const session = await getSession({ req: context.req });

    if (!session) {
        return {
            redirect: {
                destination: '/login',
                permanent: false,
            }
        }
    }

    return {
        props: { session }
    }
}

```

* Example 2: If user logined, prevent user access page Login or Register again. In this case, redirect user to home page

```js
// pages/login.js
// pages/register.js

export async function getServerSideProps(context) {
  const session = await getSession({ req: context.req });

  if (session) {
      return {
          redirect: {
              destination: '/',
              permanent: false,
          }
      }
  }

  return {
      props: { session }
  }
}

```

## Strategy of Forgot Password

1. Go to ForgotPassword page
``` js
// pages/password/forgot.js
```

2. Submitting user email need to recover password
``` js
// submit to /api/password/forgot
await axios.post(`/api/password/forgot`, email, config);
```
3. API forgot password handler
- Generate resetPasswordToken, restPasswordExpired
- Genereate resetPasswordUrl from resetPasswordToken
``` js
const resetUrl = `${origin}/password/reset/${resetToken}`;
```

- Send resetPasswordUrl to user email

4. User open resetPasswordUrl, attached in email send from forgot password handler
```js
// pages/password/reset/[token].js
```

5. Input new password, then submit
``` js
// submit to /api/password/reset/:token
```


# Stripe

## [About Webhook](https://stripe.com/docs/webhooks)

### Setup for local test
1. Install StripeCLI
- Download the latest linux tar.gz file from https://github.com/stripe/stripe-cli/releases/latest
- Copy file stripe_X.X.X_linux_x86_64.tar.gz/stripe to /usr/local/bin
- Set exce
$chmod 777 stripe
$stripe -v

* Opt 2, docker
```yml
  stripe:
    image : stripe/stripe-cli:latest
    volumes:
      - ./.stripe:/root/.config/stripe
```

2. Login
$stripe login

* With docker
```bash
$docker-compose run stripe login

Your pairing code is: vivid-neatly-helped-smile
This pairing code verifies your authentication with Stripe.
To authenticate with Stripe, please go to: https://dashboard.stripe.com/stripecli/confirm_auth?t=rsMB3NzDgj4p8iazp5wJBmZLyMAVRxec
> Done! The Stripe CLI is configured for Hugo-Dev-Vn with account id acct_1JRB2gEwfPm4lLW0

Please note: this key will expire after 90 days, at which point you'll need to re-authenticate.
```


3. Listen
$stripe listen --events checkout.session.completed --forward-to localhost:3000/api/webhook

After this on, on Stripe/Dashboard/Webhook, an device will be added

And a webhook signing secrect

* Op with docker
```bash
$docker-compose run stripe listen --events checkout.session.completed --forward-to host.docker.internal:3000/api/webhook
```

4. Copy webhook signing secrect to next.config.js
```js
module.exports = {
  reactStrictMode: true,
  env: {
    // Others configs
  
    STRIPE_WEBHOOK_SECRET: 'whsec_GVrrzL4t5pFFmTM2hfll8dgtgPN6BQ9q',
}

```


## Flow payment, checkout
1. User login
2. Pick a room
3. Pick checkin && checkout date
4. System check 'checkin' & 'checkout' is avaible or not
```js
//bookingActions.js -> checkBooking()
let link = `/api/bookings/check?roomId=${roomId}&checkInDate=${checkInDate}&checkOutDate=${checkOutDate}`;
const { data } = await axios.get(link);
```

5. Room avaible, user pay. Click pay redirect to page https://stripe/checkout?....
- Get stripe session
```js
const sessionId = `/api/checkout_session/${id}?checkInDate=${checkInDate.toISOString()}&checkOutDate=${checkOutDate.toISOString()}&daysOfStay=${daysOfStay}`;

// Redirect to checkout
stripe.redirectToCheckout({ sessionId: data.id });
```

6. Fill card info, then submit
- Stripe process payment, then post a completed webhook to
'/api/webhook'

7. On webhook receive
- Create new booking with status is paid