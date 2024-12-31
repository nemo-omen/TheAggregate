import { redirect } from '@sveltejs/kit';

const unprotectedRoutes = [
  '/',
  '/login',
  '/register',
  '/forgot-password',
  '/reset-password',
  '/features',
  'design-system'
];

export async function handle({event, resolve}) {
  const { cookies } = event;
  // console.log('server hook: ', event);
  const path = event.route.id;
  console.log({ path });
  // if(!cookies) {
  //   redirect(303, '/');
  // }
  return resolve(event);
}