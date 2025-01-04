import type { ServerLoadEvent } from '@sveltejs/kit';
import { authorize } from '$lib/auth';

export async function load(event: ServerLoadEvent) {
  authorize(event);
}