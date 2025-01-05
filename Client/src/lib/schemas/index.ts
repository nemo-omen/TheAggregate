import { z } from 'zod';

export const userNameSchema = z.string().min(5, {message: "Username must to have at least 5 characters."}).optional();

export const emailSchema = z.string().email({message: "Invalid email."})

export const passwordSchema = z.string()
  .min(8, {message: "Password must have at least eight characters"})
  .regex(
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])/,
    {
      message: 'Password must include at least one uppercase letter, one lowercase letter, one number, and one special character',
    }
  );

export const registerSchema = z.object({
  name: userNameSchema,
  email: emailSchema,
  password: passwordSchema,
});

export const infoSchema = z.object({
  name: userNameSchema,
  newEmail: emailSchema,
});

export const updatePasswordSchema = z.object({
  oldPassword: z.string(),
  newPassword: passwordSchema,
  confirmPassword: z.string(),
}).superRefine(({ newPassword, confirmPassword }, ctx) => {
  if(newPassword !== confirmPassword) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      message: "Passwords do not match.",
      path: ['confirmPassword'],
    });
  }
});