import { register } from '$lib/auth';
import type { RegisterResponse } from '$lib/auth';
import { z, ZodError } from 'zod';
import { fail } from '@sveltejs/kit';

const registerSchema = z.object({
  name: z.string().min(5, {message: "Name must to have at least 5 characters."}).optional(),
  email: z.string().email({message: "Invalid email."}),
  password: z.string()
    .min(8, {message: "Password must have at least eight characters"})
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])/,
      {
        message: 'Password must include at least one uppercase letter, one lowercase letter, one number, and one special character',
      }
    ),
})

export async function load(event) {
  return {
    props: {}
  };
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