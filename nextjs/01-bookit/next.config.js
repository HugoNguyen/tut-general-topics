module.exports = {
  reactStrictMode: true,
  env: {
    DB_LOCAL_URI: 'mongodb://root:root@localhost:27017/bookit?authSource=admin',
    CLOUDINARY_CLOUD_NAME: 'hugo-dev-vn',
    CLOUDINARY_API_KEY: '728669783891486',
    CLOUDINARY_SECRET_KEY: 'PoJhZQTsYS9lADlXk-V-XQsaLZI',
    STRIPE_API_KEY: 'pk_test_51JRB2gEwfPm4lLW0gS8meswU1cwKv5znNODA4HIx9C0i3qrPoMZm2QEC7c028HXejm4mKJSrmyNgyKM5vu87DyQm003LymklTX',
    STRIPE_SECRET_KEY: 'sk_test_51JRB2gEwfPm4lLW0bXuety3Vof2yayqhZd4CoZHIwVls6w44ri9nRdX3GKYi7KHMbQKD1JgLBltpK3peNYEQzFBp00HzBqTolw',
    STRIPE_WEBHOOK_SECRET: 'whsec_GVrrzL4t5pFFmTM2hfll8dgtgPN6BQ9q',

    SMTP_HOST: 'smtp.mailtrap.io',
    SMTP_PORT: '2525',
    SMTP_USER: '0b63db7db0e1d0',
    SMTP_PASSWORD: '10531b3d0a4237',
    SMTP_FROM_NAME: 'BookIT',
    SMTP_FROM_EMAIL: 'noreply@bookit.com',
  },
  images: {
    domains: ['res.cloudinary.com'],
  },
}
