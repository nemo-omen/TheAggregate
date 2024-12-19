'use client'
import {type Context, createContext, useContext, useState } from "react";
import {User} from "@/types";

const UserContext: Context<User | null> = createContext(null);

export function UserProvider({children}: {children: React.ReactNode}) {
    const [user, setUser] = useState(null);

    return (
        <UserContext.Provider value={{user, setUser}}>
            {children}
        </UserContext.Provider>
    );
}

export function useUser (): User | null {
    return useContext(UserContext);
}