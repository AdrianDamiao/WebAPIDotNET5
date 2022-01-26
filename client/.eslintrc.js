module.exports = {
  root: true,
  env: {
    node: true,
  },
  extends: [
    'plugin:vue/essential',
    '@vue/airbnb',
  ],
  parserOptions: {
    parser: 'babel-eslint',
  },
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-underscore-dangle': [0, 'allowAfterThis'],
    'import/no-cycle': [0],
    'class-methods-use-this': [0],
    'import/prefer-default-export': [0],
    'no-unused-vars': [0],
    'no-multiple-empty-lines': [0],
    "linebreak-style": 0,
  },
};
