const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyWebpackPlugin = require("copy-webpack-plugin");
//const VueLoaderPlugin = require('vue-loader/lib/plugin')

const devMode = process.env.NODE_ENV !== 'production';

module.exports = {
    entry: {
        app: "./wwwroot/app.js"
    },
    context: path.join(__dirname),
    devtool: 'inline-source-map',
    output: {
        filename: devMode ? '[name].js' : '[name].[hash].js',
        chunkFilename: devMode ? '[id].js' : '[id].[hash].js',
        path: path.resolve(__dirname, 'wwwroot/dist'),
        publicPath: '/'
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                styles: {
                    name: 'styles',
                    test: /\.css$/,
                    chunks: 'all',
                    enforce: true
                }
            }
        }
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: devMode ? '[name].css' : '[name].[hash].css',
            chunkFilename: devMode ? '[id].css' : '[id].[hash].css',
        }),
        new CleanWebpackPlugin({ cleanStaleWebpackAssets: false }),
        //new VueLoaderPlugin()
    ],
    module: {
        rules: [
            //{
            //    test: /\.vue$/,
            //    loader: 'vue-loader'
            //},
            {
                test: /\.(sa|sc|c|pc)ss$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'postcss-loader',
                ],
            },
            {
                test: /\.(png|jpg|gif)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[name].[hash].[ext]',
                            limit: 10000,
                            publicPath: 'img/',
                            outputPath: 'img/',
                            esModule: false
                        }
                    }
                ]
            },
            {
                test: /\.svg$/,
                use: [
                    { loader: 'svg-sprite-loader' },
                ]
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: ['file-loader']
            }
        ],
    }
};

// "build": "webpack --mode=development --watch",