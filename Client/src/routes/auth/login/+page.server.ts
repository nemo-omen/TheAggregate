import { getUserInfo, initCredentialedClient, isAuthorized, login, setSessionCookies } from '$lib/auth';
import { type Actions, fail, redirect } from '@sveltejs/kit';
import { z } from 'zod';

const loginSchema = z.object({
  email: z.string(),
  password: z.string(),
});

export async function load(event) {
  if (isAuthorized(event)) {
    console.log('login (authorized): redirecting to /frontpage');
     redirect(303, '/frontpage');
  }
}

export const actions: Actions = {
  default: async (event) => {
    const { request, cookies } = event;
    const formData = await request.formData();
    const formDataEntries = Object.fromEntries(formData.entries());

    const validationResult = loginSchema.safeParse(formDataEntries);

    if (!validationResult.success) {
      return fail(400, { errors: validationResult.error.issues.map(i => i.message) });
    }

    const { email, password } = validationResult.data;
    const response = await login(email, password);

    if (response.detail === 'Failed') {
      return fail(400, { errors: ['Invalid email or password'] });
    }

    console.log({ response });

    const { accessToken, refreshToken, expiresIn } = response;

    setSessionCookies(accessToken, refreshToken, expiresIn, cookies);

    // redirect(303, '/frontpage');
    return {
      success: true,
    }
  }
}