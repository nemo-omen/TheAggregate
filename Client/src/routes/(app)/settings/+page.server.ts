import { authorize } from '$lib/auth';
import type { ServerLoadEvent } from '@sveltejs/kit';

export async function load(event: ServerLoadEvent) {
  authorize(event);
}