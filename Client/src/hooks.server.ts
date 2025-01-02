import { expiresSoon, getUserInfo, refreshAccessToken } from '$lib/auth';

export async function handle({event, resolve}) {
  const { cookies } = event;
  if(!cookies) {
    event.locals.user = undefined;
    return resolve(event);
  }

  let accessToken = cookies.get('aggregate_accessToken', );
  if(!accessToken) {
    console.log('no access cookie found');
    event.locals.user = undefined;
    return resolve(event);
  }

  const refreshToken = cookies.get('aggregate_refreshToken');
  if(!refreshToken) {
    console.log('no refresh cookie found');
    event.locals.user = undefined;
    return resolve(event);
  }

  const expiration = cookies.get('aggregate_expires');
  if(!expiration) {
    console.log('no expiration cookie found');
    event.locals.user = undefined;
    return resolve(event);
  }

  if(expiresSoon(expiration)) {
    console.log('expiration is soon. refreshing access token');
    try {
      accessToken = await refreshAccessToken(accessToken, refreshToken, cookies);
    } catch (e) {
      console.log('error refreshing access token: ', e);
      event.locals.user = undefined;
      return resolve(event);
    }
  }

  const userInfo = await getUserInfo(accessToken);

  if(!userInfo || !userInfo.data) {
    console.log('no user info found');
    event.locals.user = undefined;
    return resolve(event);
  }

  if(userInfo.error) {
    console.log('error getting user info');
    event.locals.user = undefined;
    return resolve(event);
  }

  event.locals.user = userInfo.data;
  console.log('user info found: ', event.locals.user);
  return resolve(event);
}