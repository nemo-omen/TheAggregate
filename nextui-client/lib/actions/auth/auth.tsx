'use server'

import { z } from 'zod';
import { cookies } from 'next/headers';
import type {LoginUserRequest, LoginUserResponse, Session} from "@/lib/actions/auth/types";
import type { User } from '@/types';

function apiBaseAddress() {
    let apiBaseAddress: string | undefined;
    const env = process.env.NODE_ENV || 'development';
    if(env === 'development') {
        apiBaseAddress = process.env.NEXT_PUBLIC_DEV_API_BASE_ADDRESS;
    } else {
        apiBaseAddress = process.env.NEXT_PUBLIC_PROD_API_BASE_ADDRESS;
    }
    return apiBaseAddress;
}

export async function getUserInfoAction(session: Session): Promise<User> {
    if(!session.token) {
        throw new Error('No token in session');
    }
    return fetch(`${apiBaseAddress()}/api/account/user`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Bearer': session.token,
        },
    })
    .then(response => {
        if(!response.ok) {
            console.log(response.json());
            throw new Error('Failed to get user info');
        }
        return response.json();
    });
}

export async function loginAction(formData: FormData): Promise<LoginUserResponse> {

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
        throw new Error (parse.error.errors[0].message);
    }

    // send the request to the server
    let response: Response;
    try {
        response = await fetch(`${apiBaseAddress()}/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(loginData),
        });
    } catch(e) {
        throw e;
    }

    if(!response || !response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Login failed');
    }

    const data: LoginUserResponse = await response.json();
    return data;
}