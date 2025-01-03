import { isAuthorized } from '$lib/auth';
import { redirect } from '@sveltejs/kit';

export async function load(event) {
  console.log('/frontpage load: isAuthorized - ', isAuthorized(event));
  if(!isAuthorized(event)) {
    console.log('redirecting to /auth/login');
    redirect(303, '/auth/login');
  }
  return {
    user: event.locals.user
  }
}