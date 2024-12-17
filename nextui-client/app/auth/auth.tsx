'use server'

const apiBaseAddress = 'http://localhost:5050';

type UserLoginRequest = {
    email: string;
    password: string;
};

export async function login(userLoginRequest: UserLoginRequest) {
    const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(userLoginRequest),
    });
    return await response.json();
}