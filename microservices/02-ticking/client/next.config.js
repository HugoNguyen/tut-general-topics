module.exports = {
  webpackDevMiddleware: config => {
    config.watchOptions.poll = 300; // to fix sometime nextjs not detech change
    return config;
  }
};
