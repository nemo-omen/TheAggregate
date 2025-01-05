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

export function authorize(event: ServerLoadEvent | RequestEvent) {
  if(!isAuthorized(event)) {
    redirect (303, '/auth/login');
  }
}

export function isAuthorized(event: ServerLoadEvent | RequestEvent): boolean {
  // console.log(event.locals);
  if(!event.locals || !event.locals.user) {
    return false;
  }
  return true;
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
    path: '/'
  };

  const refreshCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: '/'
  };

  const expirationCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: '/'
  };

  let now = Date.now();
  const expires = now + (expiresIn * 1000);

  cookies.set('aggregate_accessToken', accessToken, accessCookieOptions as CookieSerializeOptions);
  cookies.set('aggregate_refreshToken', refreshToken, refreshCookieOptions as CookieSerializeOptions);
  cookies.set('aggregate_expires', expires.toString(), expirationCookieOptions as CookieSerializeOptions);
}

function setAccessCookie(accessToken: string, expiresIn: number, cookies: Cookies) {
  const accessCookieOptions = {
    maxAge: expiresIn,
    httpOnly: false,
    sameSite: 'strict',
    path: '/'
  };

  cookies.set('aggregate_accessToken', accessToken, accessCookieOptions as CookieSerializeOptions);
}

export function unsetSessionCookies(cookies: Cookies) {
  cookies.delete('aggregate_accessToken', { path: '/' });
  cookies.delete('aggregate_refreshToken', { path: '/' });
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

  const newRefreshToken = response.data?.refreshToken;
  if (!newRefreshToken) {
    throw new Error('No refresh token in response')
  }

  setSessionCookies(newAccessToken, newRefreshToken, response.data?.expiresIn!, cookies);
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
  return response;
}

export async function getUserInfo(accessToken: string) {
  initCredentialedClient(accessToken, client);
  const response = await getManageInfo();
  return response;
}