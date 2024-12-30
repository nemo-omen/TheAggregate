'use client'

import {
    Modal,
    ModalContent,
    ModalBody,
    useDisclosure,
} from '@nextui-org/modal';

import { Button } from "@nextui-org/button";
import React from "react";

export default function LoginModal({children}: {children: React.ReactNode}) {
    const { isOpen, onOpen, onOpenChange } = useDisclosure();
    return (
        <>
            <Button className="font-semibold button text-md" size="md" variant="light" onPress={onOpen}>Log In</Button>
            <Modal isOpen={isOpen} onClose={onOpenChange} size="2xl" radius="md" className="bg-slate-950 shadow-xl border border-slate-800 hover:border-slate-700 transition-colors duration-200">
                <ModalContent>
                    {(onClose) => (
                        <>
                            <ModalBody className="p-8">
                                {children}
                            </ModalBody>
                        </>
                    )}
                </ModalContent>
            </Modal>
        </>
    )
}