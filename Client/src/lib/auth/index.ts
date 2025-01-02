import { type Client, createClient } from '@hey-api/client-fetch';
import { serverOptions } from '$lib/config';
import {
  client,
  getManageInfo, type LoginRequest,
  postLogin,
  postLogout,
  postRefresh,
  postRegister,
  type PostRegisterError
} from '$lib/client';
import { type Cookies, redirect, type RequestEvent, type ServerLoadEvent } from '@sveltejs/kit';
// @ts-ignore
import type { CookieSerializeOptions, SerializeOptions } from 'cookie';

export function isAuthorized(event: ServerLoadEvent | RequestEvent): boolean {
  return !!event.locals.user;
}

export function initBaseClient(client: Client) {
  client.setConfig({
    baseUrl: serverOptions.baseUrl,
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

export function setSessionCookies(accessToken: string, refreshToken: string, expiresIn: number, cookies: Cookies) {
  const accessCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: '/aggregate_at'
  };

  const refreshCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: '/aggregate_rt'
  };

  const expirationCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: 'aggregate_expires'
  };

  let now = Date.now();
  const expires = now + (expiresIn * 1000);

  cookies.set('accessToken', accessToken, accessCookieOptions as CookieSerializeOptions);
  cookies.set('refreshToken', refreshToken, refreshCookieOptions as CookieSerializeOptions);
  cookies.set('expires', expires.toString(), expirationCookieOptions as CookieSerializeOptions);
}

function setAccessCookie(accessToken: string, expiresIn: number, cookies: Cookies) {
  const accessCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: '/aggregate_at'
  };

  cookies.set('accessToken', accessToken, accessCookieOptions as CookieSerializeOptions);
}

function unsetSessionCookies(cookies: Cookies) {
  cookies.delete('accessToken', { path: '/aggregate_at' });
  cookies.delete('refreshToken', { path: '/aggregate_rt' });
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
  initBaseClient(client);
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

export async function refreshAccessToken(accessToken: string, refreshToken: string, cookies: Cookies): Promise<string> {
  initCredentialedClient(accessToken, client);
  const response = await postRefresh({body: {refreshToken}});
  if (response.error) {
    throw new Error(response.error as string);
  }

  const newAccessToken = response.data?.accessToken;
  if (!newAccessToken) {
    throw new Error('No access token in response')
  }

  setAccessCookie(newAccessToken, response.data?.expiresIn!, cookies);
  return newAccessToken;
}

export function expiresSoon(expires: string) {
  const expiresDate = new Date(parseInt(expires));
  const now = new Date();
  const diff = expiresDate.getTime() - now.getTime();
  return diff < 60000;
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
  return response;
}