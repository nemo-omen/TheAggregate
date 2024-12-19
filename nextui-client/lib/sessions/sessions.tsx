'use server'
import { cookies } from "next/headers";
import type {LoginUserResponse, Session} from "@/lib/actions/auth/types";
export async function createSession(data: LoginUserResponse): Promise<Session> {
    const cookieStore = await cookies();
    cookieStore.set({
        name: 'token',
        value: data.accessToken,
        httpOnly: true,
        path: '/',
        sameSite: 'strict',
        secure: true,
        maxAge: data.expiresIn,
    });
    cookieStore.set({
        name: 'refreshToken',
        value: data.refreshToken,
        httpOnly: true,
        path: '/',
        sameSite: 'strict',
        secure: true,
    });
    return { token: data.accessToken, refreshToken: data.refreshToken };
}

export async function getSession(): Promise<Session> {
    const cookieStore = await cookies();
    const token = cookieStore.get('token')?.value;
    const refreshToken = cookieStore.get('refreshToken')?.value;
    return { token, refreshToken };
}