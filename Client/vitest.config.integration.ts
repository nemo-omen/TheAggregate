import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    include: ['**/*.test.api.ts'],
    deps: {
      inline: [/vite-test-utils/],
    },
    alias: {'$lib': new URL('./src/lib', import.meta.url).pathname},
  }
});