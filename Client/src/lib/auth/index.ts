import type { Client } from '@hey-api/client-fetch';
import { serverOptions } from '$lib/config';
import {
  client,
  getManageInfo,
  postLogin,
  postLogout,
  postRefresh,
  postRegister,
  type PostRegisterError
} from '$lib/client';

export function initBaseClient(client: Client) {
  client.setConfig({
    baseUrl: serverOptions.baseUrl
  });
}

export function initCredentialedClient(accessToken: string, client: Client) {
  client.setConfig({
    baseUrl: serverOptions.baseUrl
  });

  client.interceptors.request.use((request, options) => {
    request.headers.set('Authorization', `Bearer ${accessToken}`);
    return request;
  });
}

export function initCredentialedApiClient(accessToken: string, client: Client) {
  client.setConfig({
    baseUrl: serverOptions.apiUrl
  });

  client.interceptors.request.use((request, options) => {
    request.headers.set('Authorization', `Bearer ${accessToken}`);
    return request;
  });
}

export type RegisterResponse = {
  data?: unknown,
  error?: PostRegisterError
};

export async function register(name: string | undefined, email: string, password: string): Promise<RegisterResponse> {
  const registerData = { name, email, password };
  initBaseClient(client);
  const response = await postRegister({ body: registerData });
  if (response.error) {
    return {
      error: response.error
    };
  }

  return {
    data: response.data,
  };
}

export async function login(email: string, password: string) {
  const response = await postLogin({
    body: {
      email,
      password,
    },
  });

  if (response.error) {
    return response.error;
  }

  return response.data;
}

export async function refresh(refreshToken: string) {
  const response = await postRefresh({body: {refreshToken}});
  if (response.error) {
    return response.error;
  }

  return response.data;
}

export async function logout() {
  const response = await postLogout({body: {}});
  if (response.error) {
    return response.error;
  }
}

export async function getUserInfo(accessToken: string) {
  initCredentialedClient(accessToken, client);
  const response = await getManageInfo();
  if (response.error) {
    return response.error;
  }

  return response.data;
}