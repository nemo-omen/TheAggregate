import { expiresSoon, getUserInfo, refreshAccessToken } from '$lib/auth';
import { redirect } from '@sveltejs/kit';

const unprotectedRoutes = [
  "/",
  "/features",
  "/auth/login",
  "/auth/register",
  "/auth/forgot-password",
];

export async function handle({event, resolve}) {
  const { cookies } = event;
  if (!cookies) {
    // console.log('hooks: no cookies found');
    event.locals.user = undefined;
    return resolve(event);
  }

  let accessToken = cookies.get('aggregate_accessToken');
  if (!accessToken) {
    // console.log('hooks: no access cookie found');
    event.locals.user = undefined;
    return resolve(event);
  }

  const refreshToken = cookies.get('aggregate_refreshToken');
  if (!refreshToken) {
    // console.log('hooks: no refresh cookie found');
    event.locals.user = undefined;
    return resolve(event);
  }

  const expiration = cookies.get('aggregate_expires');
  if (!expiration) {
    // console.log('hooks: no expiration cookie found');
    event.locals.user = undefined;
    return resolve(event);
  }

  if (expiresSoon(expiration)) {
    console.log('hooks: expiration is soon. refreshing access token');
    try {
      accessToken = await refreshAccessToken(accessToken, refreshToken, cookies);
    } catch (e) {
      console.log('hooks: error refreshing access token: ', e);
      event.locals.user = undefined;
      return resolve(event);
    }
  }

  const userInfo = await getUserInfo(accessToken);

  if (!userInfo || !userInfo.data) {
    // console.log('hooks: no user info found');
    event.locals.user = undefined;
    return resolve(event);
  }

  if (userInfo.error) {
    // console.log('hooks: error getting user info');
    event.locals.user = undefined;
    return resolve(event);
  }

  event.locals.user = userInfo.data;
  // console.log('hooks (resolving): user info found: ', event.locals.user);
  return resolve(event);
}