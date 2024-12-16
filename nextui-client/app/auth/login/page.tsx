import { Card, CardBody } from '@nextui-org/card';
import { Form } from '@nextui-org/form';
import {Input} from "@nextui-org/input";
import {link} from "node:fs";
import Image from "next/image";
import {Button} from "@nextui-org/button";
import {Link} from "@nextui-org/link";

export default function LoginPage() {
    return <main className="grid place-content-center w-full h-full">
        <Card className="min-w-100 max-w-full shadow-large bg-gray-950 border-1 border-gray-900">
            <CardBody className="p-6">
                <Form className="flex flex-col gap-10 w-full items-center">
                    <div className="flex flex-col gap-4 w-full items-center">
                        <Image src="/AggregateLogo.svg" alt="AggregateLogo" width={96} height={96}/>
                        <h1 className="text-2xl font-bold">Log In</h1>
                    </div>
                    <div className="flex flex-col gap-6 w-full items-center">
                        <Input
                            variant="bordered"
                            label="Email"
                            name="email"
                            placeholder="Enter Your Email"
                            type="email"
                            labelPlacement="outside"
                        />
                        <Input
                            variant="bordered"
                            label="Password"
                            name="password"
                            placeholder="Enter Your Password"
                            labelPlacement="outside"
                        />
                        {/*<Input*/}
                        {/*    variant="bordered"*/}
                        {/*    label="Confirm Password"*/}
                        {/*    name="passwordConfirm"*/}
                        {/*    placeholder="Retype Your Password"*/}
                        {/*/>*/}
                    </div>
                    <div className="flex flex-col items-stretch text-center gap-4 w-full">
                        <Button className="w-full bg-primary-300 text-lg" type="submit">Log In</Button>
                        <span className="text-sm">or</span>
                        <Button className="w-full text-lg" as={Link} href="/auth/register">Sign Up</Button>
                    </div>
                </Form>
            </CardBody>
        </Card>
    </main>
}