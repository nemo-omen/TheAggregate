import { isAuthorized, register } from '$lib/auth';
import { redirect } from '@sveltejs/kit';

export async function load(event) {
  if(isAuthorized(event)) {
    redirect(303, '/frontpage');
  }
}