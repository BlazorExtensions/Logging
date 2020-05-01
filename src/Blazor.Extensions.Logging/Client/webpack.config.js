const path = require('path');

module.exports = {
  mode: 'production',
  entry: {
    'blazor.extensions.logging': './src/InitializeLogging.ts'
  },
  devtool: 'inline-source-map',
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
  resolve: {
    extensions: [
      '.ts',
      '.js'
    ],
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, '..', 'wwwroot'),
  },
};
