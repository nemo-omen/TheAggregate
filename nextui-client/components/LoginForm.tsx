'use client'
import {Form} from "@nextui-org/form";
import {getUserInfoAction, loginAction} from "@/lib/actions/auth/auth";
import Image from "next/image";
import {Input} from "@nextui-org/input";
import {Link} from "@nextui-org/link";
import {Button} from "@nextui-org/button";
import {useState} from "react";
import {LoginUserResponse} from "@/lib/actions/auth/types";
import {createSession} from "@/lib/sessions/sessions";
import {useUser} from "@/lib/context/user";

export default function LoginForm({fontWeight = 'semibold'}: {fontWeight: string}) {
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const { setUser } = useUser();

    async function handleSubmit(formData: FormData) {
        setLoading(true);
        try {
            const loginResponse: LoginUserResponse = await loginAction(formData);
            const session = await createSession(loginResponse);
            const user = await getUserInfoAction(session);
            setUser(user);
        } catch (e: any) {
            console.error(`Error logging in: ${e.message}`);
            setError(e.message);
        } finally {
            setLoading(false);
        }
    }

    return (
        <Form action={handleSubmit} className="flex flex-col gap-10 w-full items-center">
            <div className="flex flex-col gap-4 w-full items-center">
                <Image src="/AggregateLogo.svg" alt="AggregateLogo" width={96} height={96}/>
                <h1 className="text-2xl font-bold">Log In</h1>
            </div>
            <div className="flex flex-col gap-6 w-full items-center">
                <div className="w-full flex content-center h-4 ">
                    {error && <span
                        className="text-red-500 font-bold tracking-wide leading-none w-full text-center">{error}</span>}
                </div>
                <Input
                    variant="bordered"
                    label="Email"
                    name="email"
                    placeholder="Enter Your Email"
                    type="email"
                    labelPlacement="outside"
                    radius="md"
                />
                <Input
                    variant="bordered"
                    label="Password"
                    name="password"
                    placeholder="Enter Your Password"
                    labelPlacement="outside"
                    radius="md"
                />
            </div>
            <Button className={`w-full text-lg btn font-${fontWeight}`} color="primary" type="submit"
                    variant="solid">Log In</Button>
            <div className="flex flex-col items-stretch text-center gap-4 w-full">
                <div className="flex flex-col gap-2 items-center">
                    <span className="text-sm">Don't have an account?</span>
                    <Link href="/auth/register">Sign up for one.</Link>
                </div>
            </div>
        </Form>
    );
}