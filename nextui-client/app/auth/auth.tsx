'use server'

import { z } from 'zod';
import { cookies } from 'next/headers';
let apiBaseAddress: string | undefined;

type LoginUserRequest = {
    email: string;
    password: string;
};

export async function loginAction(formData: FormData) {
    const cookieStore = await cookies();
    const env = process.env.NODE_ENV || 'development';
    if(env === 'development') {
        apiBaseAddress = process.env.NEXT_PUBLIC_DEV_API_BASE_ADDRESS;
    } else {
        apiBaseAddress = process.env.NEXT_PUBLIC_PROD_API_BASE_ADDRESS;
    }
    // create the request data object from the form data
    const loginData = Object.fromEntries(formData.entries()) as LoginUserRequest;

    // define the schema for the request data
    const schema = z.object({
        email: z.string().email(),
        password: z.string().min(8),
    });

    // validate the request data
    const parse = schema.safeParse(loginData)
    // if the request data is invalid, return the error
    if (!parse.success) {
        return {message: parse.error};
    }

    // send the request to the server
    let response: Response;
    try {
        console.log({apiBaseAddress});
        response = await fetch(`${apiBaseAddress}/account/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(loginData),
        });
    } catch(e) {
        console.log({e});
        if(e instanceof Error) {
            return {message: e.cause};
        }
        return {message: 'An unknown error occurred'};
    }

    if(!response || !response.ok) {
        return {message: response.statusText};
    }

    const data = await response.json();
    const maxAge = Date.parse(data.expiration).valueOf() - Date.now().valueOf();
    console.log(maxAge);
    cookieStore.set({
        name: 'token',
        value: data.token,
        httpOnly: true,
        path: '/',
        sameSite: 'strict',
        secure: true,
        maxAge: maxAge,
    });
    return data;
}