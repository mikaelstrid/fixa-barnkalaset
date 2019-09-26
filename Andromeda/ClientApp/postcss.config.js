module.exports = {
    plugins: [
        require('cssnano')({
            preset: ['advanced', {
                zindex: false,
                reduceIdents: false,
                autoprefixer: {
                    add: true,
                },
            }]
        }),
    ],
};
