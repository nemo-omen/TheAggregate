export type LoginUserRequest = {
    email: string;
    password: string;
};

export type LoginUserResponse = {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
}

export type Session = {
    token: string | undefined;
    refreshToken: string | undefined;
}