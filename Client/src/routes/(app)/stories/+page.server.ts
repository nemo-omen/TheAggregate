import { isAuthorized } from '$lib/auth';
import { redirect } from '@sveltejs/kit';

export async function load(event) {
  if(!isAuthorized(event)) {
    redirect(303, '/auth/login');
  }
}