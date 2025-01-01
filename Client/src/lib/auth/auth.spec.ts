import { expect, test } from 'vitest';
import { client } from '$lib/client';
import { initCredentialedClient, initCredentialedApiClient } from '$lib/auth/index';

test('initCredentialedClient', async () => {
  const fakeAccessToken = 'fakeAccessToken';
  initCredentialedClient(fakeAccessToken, client);
  const clientConfig = client.getConfig();
  expect(clientConfig.baseUrl).toBe('http://localhost:5000');
});