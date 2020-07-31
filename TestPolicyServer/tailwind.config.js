const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
    purge: false,
    theme: {
        extend: {
            fontFamily: {
                sans: ['Inter var', ...defaultTheme.fontFamily.sans],
            },
        },
    },
    variants: {
        backgroundColor: ['responsive', 'hover', 'focus', 'disabled'],
        opacity: ['responsive', 'hover', 'focus', 'disabled'],
        textColor: ['responsive', 'hover', 'focus', 'disabled'],
    },
    plugins: [
        require('@tailwindcss/ui')({
            layout: 'sidebar',
        }),
    ],
}
