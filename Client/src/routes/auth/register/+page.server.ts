import type { RegisterResponse } from '$lib/auth';
import { registerSchema } from '$lib/schemas';
import { fail, redirect } from '@sveltejs/kit';
import { isAuthorized, register } from '$lib/auth';

export async function load(event) {
  if(isAuthorized(event)) {
    redirect(303, '/frontpage');
  }
}

export const actions = {
  default: async (event) => {
    const { request } = event;
    const formData = await request.formData();
    const body = Object.fromEntries(formData);

    const validationResult = registerSchema.safeParse(body);
    if (!validationResult.success) {
      return fail(400, { errors: validationResult.error.issues.map(i => i.message) });
    }
    const { name, email, password } = validationResult.data;

    const response: RegisterResponse = await register(name, email, password);
    if (response.error) {
      const errorMessages = Object.values(response.error.errors).map(e => e.join(' '));
      return fail(400, { errors: errorMessages });
    }
    return {
      success: true,
      body: response.data,
    };
  },
};