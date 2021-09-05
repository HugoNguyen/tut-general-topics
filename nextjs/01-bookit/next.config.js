module.exports = {
  reactStrictMode: true,
  env: {
    DB_LOCAL_URI: 'mongodb://root:root@localhost:27017/bookit?authSource=admin',
    CLOUDINARY_CLOUD_NAME: 'hugo-dev-vn',
    CLOUDINARY_API_KEY: '728669783891486',
    CLOUDINARY_SECRET_KEY: 'PoJhZQTsYS9lADlXk-V-XQsaLZI',
  },
  images: {
    domains: ['res.cloudinary.com'],
  },
}
