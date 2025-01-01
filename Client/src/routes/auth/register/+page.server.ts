import { register } from '$lib/auth';
import type { RegisterResponse } from '$lib/auth';
import { z, ZodError } from 'zod';

const registerSchema = z.object({
  name: z.string().min(5, {message: "Name must to have at least 5 characters."}).optional(),
  email: z.string().email({message: "Invalid email."}),
  password: z.string()
    .min(8, {message: "Password must have at least eight characters"})
    .regex(/^[a-zA-Z0-9!@#$&()\\-`.+,/\"]*$/, {message: "Password must contain letters, numbers and special characters."}),
})

export async function load(event) {
  return {
    props: {}
  };
}

export const actions = {
  register: async (event) => {
    const { request } = event;
    const formData = await request.formData();
    const body = Object.fromEntries(formData);

    const validationResult = registerSchema.safeParse(body);
    if (!validationResult.success) {
      return {
        status: 400,
        body: { errors: validationResult.error.issues.map(i => i.message) },
      };
    }
    const { name, email, password } = validationResult.data;

    const response: RegisterResponse = await register(name, email, password);
    if (response.error) {
      return {
        status: 400,
        body: { errors: response.error.errors },
      };
    }
    return {
      status: 200,
      body: response,
    };
  },
};