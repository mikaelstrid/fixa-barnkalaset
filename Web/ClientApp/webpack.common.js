const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = {
    entry: {
        main: './src/index.ts'
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, '../wwwroot/dist'),
        publicPath: '/dist/'
    },
    optimization: {
        runtimeChunk: 'single',
        splitChunks: {
            chunks: 'all',
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/]/,
                    name: 'vendors',
                    chunks: 'all'
                }
            }
        }
    },
    module: {
        rules: [{
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.ts$/,
                loader: 'ts-loader',
                exclude: file => (
                    /node_modules/.test(file) &&
                    !/\.vue\.js/.test(file)
                ),
                options: {
                    onlyCompileBundledFiles: true,
                    appendTsSuffixTo: [/\.vue$/]
                }
            },
            {
                test: /\.scss$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    {
                        loader: 'css-loader',
                        options: {
                            sourceMap: true
                        }
                    },
                    'postcss-loader',
                    {
                        loader: 'sass-loader',
                        options: {
                            sourceMap: true
                        }
                    }
                ]
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]?[hash]'
                }
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: [
                    'file-loader'
                ]
            }
        ]
    },
    resolve: {
        extensions: ['.ts', '.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
    plugins: [
        new VueLoaderPlugin(),
        new webpack.HashedModuleIdsPlugin(),
        new HtmlWebpackPlugin({
            filename: path.resolve(__dirname, '../Views/Shared/_Layout.cshtml'),
            template: path.resolve(__dirname, '../Views/Shared/_Layout_Template.cshtml')
        }),
        new HtmlWebpackPlugin({
            filename: path.resolve(__dirname, '../Areas/Admin/Views/Shared/_Layout.cshtml'),
            template: path.resolve(__dirname, '../Areas/Admin/Views/Shared/_Layout_Template.cshtml')
        })
    ]
};
