import { fail, redirect } from '@sveltejs/kit';
import { authorize, initCredentialedClient } from '$lib/auth';
import { client, type FeedItem, getApiFeedsById, getFeedItemsById } from '$lib/client';

type ApiArror = {
  type: string;
  title: string;
  status: string;
  traceId: string;
}

export async function load(event) {
  authorize(event);
  const { cookies, params, request } = event;
  const { id } = params;
  console.log({params});

  if (!id) {
    return fail(400, { error: 'Missing id parameter' });
  }

  const accessToken = cookies.get('aggregate_accessToken');
  if (!accessToken) {
    redirect(303, '/login');
  }
  initCredentialedClient(accessToken, client);

  const response = await getApiFeedsById({path: {id}});
  if (response.error) {
    return fail(response.error.status, { error: response.error.detail });
  }

  console.log(response.data);
  const referer = request.headers.get('referer');

  return {
    feed: response.data,
    referer
  };
}