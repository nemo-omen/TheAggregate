import { defineConfig } from '@hey-api/openapi-ts';

export default defineConfig({
  client: '@hey-api/client-fetch',
  input: `http://localhost:5050/openapi/v1.json`,
  output: {
    format: 'prettier',
    lint: 'eslint',
    path: 'src/lib/client',
  },
  // experimentalParser: true,
});