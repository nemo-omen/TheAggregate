import { fail, redirect, type ServerLoadEvent } from '@sveltejs/kit';
import { authorize, initCredentialedClient } from '$lib/auth';
import { client, getFeedItemsById } from '$lib/client';

export async function load(event: ServerLoadEvent) {
  authorize(event);
  const { cookies, params, request } = event;
  const { id } = params;
  if(!id) {
    return fail(400, { error: 'Missing id parameter' });
  }

  const accessToken = cookies.get('aggregate_accessToken');
  if(!accessToken) {
    redirect(303, '/login');
  }
  initCredentialedClient(accessToken, client);

  const response = await getFeedItemsById({path: {id}});
  if(response.error) {
    if(response.error.errors.any(e => e.status === '404')) {
      return fail(404, { error: response.error });
    }
    return fail(500, { error: response.error });
  }

  const referer = request.headers.get('referer');

  return {
    item: response.data,
    referer
  };
}