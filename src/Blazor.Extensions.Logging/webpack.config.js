const path = require("path");
const webpack = require("webpack");

module.exports = {
  mode: 'production',
  resolve: {
    extensions: [".ts", ".js"]
  },
  devtool: "inline-source-map",
  module: {
    rules: [
      {
        test: /\.ts?$/,
        loader: "ts-loader"
      }
    ]
  },
  entry: {
    "blazor.extensions.logging": "./typescript/InitializeLogging.ts"
  },
  output: {
    path: path.join(__dirname, "/wwwroot"),
    filename: "[name].js"
  }
};
