import {nextui} from '@nextui-org/theme'

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
    './node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}'
  ],
  theme: {
    extend: {
      fontFamily: {
        sans: ["var(--font-sans)"],
        mono: ["var(--font-mono)"],
      },
      spacing: {
        '100': '30rem',
        '128': '32rem'
      }
    },
  },
  darkMode: "class",
  plugins: [nextui({
    themes: {
      light: {
        colors: {
          primary: {
            DEFAULT: "#0891b2",
            foreground: "#f1f5f9"
          },
          background: "#f1f5f9"
        },
        layout: {
          radius: {
            small: "2px",
            medium: "4px",
            large: "6px"
          }
        }
      },
      dark: {
        colors: {
          primary: {
            DEFAULT: "#22d3ee",
            foreground: "#020617"
          },
          background: "#020617"
        },
        layout: {
          radius: {
            small: "0.125rem",
            medium: "0.25rem",
            large: "0.375rem"
          }
        }
      },
    },
  })],
}
