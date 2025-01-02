import { $fetch, setup } from 'vite-test-utils';
import { describe, expect, test } from 'vitest';

import './+page.server.ts';

await setup({
  server: true,
  configFile: 'vite.api.test.config.ts',
  mode: 'dev'
});

describe('POST /auth/register', () => {
  test('responds with 200', async () => {
    const formData = new FormData();
    formData.append('email', 'test@test.com');
    formData.append('password', 'Password123!');
    formData.append('name', 'Test User');

    const response = await $fetch('/auth/register', {
      method: 'POST',
      body: formData,
    });

    expect(response.status).toBe(200);
  });
});