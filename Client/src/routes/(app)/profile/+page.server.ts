import { type Actions, fail, type RequestEvent, type ServerLoadEvent } from '@sveltejs/kit';
import { authorize } from '$lib/auth';
import { client, postManageInfo, type UpdateUserInfoRequest } from '$lib/client';
import { infoSchema, updatePasswordSchema } from '$lib/schemas';
import { z } from 'zod';

export async function load(event: ServerLoadEvent) {
  authorize(event);
}

export const actions: Actions = {
  updateInfo: async (event: RequestEvent) => {
    authorize(event);
    const { request } = event;
    const formData = await request.formData();
    console.log({ formData });
    const formDataEntries: UpdateUserInfoRequest = Object.fromEntries(formData.entries());

    const validationResult = infoSchema.safeParse(formDataEntries);
    if(!validationResult.success) {
      console.log(validationResult.error.issues);
      return fail(400, { errors: validationResult.error.issues })
    }

    const response = await postManageInfo({ body: formDataEntries });
    if(response.error) {
      if (response.error) {
        const errorMessages = Object.values(response.error.errors).map(e => e.join(' '));
        return fail(400, { errors: errorMessages });
      }
    }
    return {
      success: true,
      body: response.data,
    };
  },
  updatePassword: async (event: RequestEvent) => {
    authorize(event);
    const { request } = event;
    const formData = await request.formData();
    const formDataEntries = Object.fromEntries(formData.entries());
    console.log({formDataEntries});

    const validationResult = updatePasswordSchema.safeParse(formDataEntries);
    if(!validationResult.success) {
      return fail(400, { errors: validationResult.error.issues.map(i => i.message) });
    }

    const response = await postManageInfo({ body: formDataEntries });
    if(response.error) {
      if (response.error) {
        const errorMessages = Object.values(response.error.errors).map(e => e.join(' '));
        return fail(400, { errors: errorMessages });
      }
    }
    return {
      success: true,
      body: response.data,
    };
  },
};