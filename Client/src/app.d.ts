// See https://svelte.dev/docs/kit/types#app.d.ts
// for information about these interfaces
import type { UserWithRolesResponse } from '$lib/client';

declare global {
	namespace App {
		// interface Error {}
		interface Locals {
			user: UserWithRolesResponse | undefined
		}
		// interface PageData {}
		// interface PageState {}
		// interface Platform {}
	}
}

export {};
