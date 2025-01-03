import { unsetSessionCookies, logout } from '$lib/auth';
import { redirect } from '@sveltejs/kit';

export async function POST(event) {
  const {cookies, fetch} = event;
  const token = cookies.get('aggregate_accessToken');
  if(!token) {
    unsetSessionCookies(cookies);
    return new Response('Redirect', {status: 303, headers: {location: '/auth/login'}});
  }

  const response = await logout();
  unsetSessionCookies(cookies);
  redirect(303, '/auth/login');
}