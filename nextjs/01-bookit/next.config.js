module.exports = {
  reactStrictMode: true,
  env: {
    DB_LOCAL_URI: 'mongodb://root:root@localhost:27017/bookit?authSource=admin',
    CLOUDINARY_CLOUD_NAME: 'hugo-dev-vn',
    CLOUDINARY_API_KEY: '728669783891486',
    CLOUDINARY_SECRET_KEY: 'PoJhZQTsYS9lADlXk-V-XQsaLZI',

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
