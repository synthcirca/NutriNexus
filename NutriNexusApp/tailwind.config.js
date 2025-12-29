// tailwind.config.js
/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      fontFamily: {
        mono: ['Overpass Mono', 'monospace'],
        overpass: ['Overpass Mono', 'monospace'],
      },
    },
  },
  plugins: [],
};
