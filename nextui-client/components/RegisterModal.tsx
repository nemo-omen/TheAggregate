'use client'

import {
    Modal,
    ModalContent,
    ModalBody,
    ModalFooter,
    useDisclosure,
} from '@nextui-org/modal';

import { Button } from "@nextui-org/button";
import Image from "next/image";
import {Input} from "@nextui-org/input";
import {Link} from "@nextui-org/link";
import {Form} from "@nextui-org/form";

export default function RegisterModal({fontWeight = "semibold", buttonSize = "sm"}: {fontWeight: string, buttonSize: "sm" | "md" | "lg"}) {
    const { isOpen, onOpen, onOpenChange } = useDisclosure();
    return (
        <>
            <Button className={`text-md button font-${fontWeight}`} size={buttonSize} variant="solid" color="primary" onPress={onOpen}>Create an Account</Button>
            <Modal isOpen={isOpen} onClose={onOpenChange} size="2xl" radius="md" className="bg-slate-950 shadow shadow-xl border border-slate-800 hover:border-slate-700 transition transition-colors duration-200">
                <ModalContent>
                    {(onClose) => (
                        <>
                            <ModalBody className="p-8">
                                <Form className="flex flex-col gap-10 w-full items-center">
                                    <div className="flex flex-col gap-4 w-full items-center">
                                        <Image src="/AggregateLogo.svg" alt="AggregateLogo" width={96} height={96}/>
                                        <h1 className="text-2xl font-bold">Create an Account</h1>
                                    </div>
                                    <div className="flex flex-col gap-6 w-full items-center">
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
                                        <Input
                                            variant="bordered"
                                            label="Confirm Password"
                                            name="passwordConfirm"
                                            placeholder="Retype Your Password"
                                            labelPlacement="outside"
                                            radius="md"
                                        />
                                    </div>
                                    <div className="flex flex-col items-stretch text-center gap-4 w-full">
                                        <Button className={`w-full text-lg button font-${fontWeight}`} color="primary" type="submit"
                                                variant="solid">Sign Up</Button>
                                        <div className="flex flex-col gap-2 items-center">
                                            <span className="text-sm">Already have an account?</span>
                                            <Link href="/auth/login">Log in instead.</Link>
                                        </div>
                                        </div>
                                </Form>
                            </ModalBody>
                        </>
                    )}
                </ModalContent>
            </Modal>
        </>
    )
}