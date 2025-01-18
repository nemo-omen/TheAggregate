import { error, redirect, type ServerLoadEvent } from '@sveltejs/kit';
import { authorize, initCredentialedClient } from '$lib/auth';
import { client, getApiFeedsCategories } from '$lib/client';

export async function load(event: ServerLoadEvent) {
  authorize(event);
  const { cookies } = event;
  const accessToken = cookies.get('aggregate_accessToken');
  if(!accessToken) {
    redirect(303, '/auth/login');
  }

  initCredentialedClient(accessToken, client);

  const categoriesResponse = await getApiFeedsCategories();
  if(categoriesResponse.error) {
    console.log(categoriesResponse.error);
    error(500, {message: `Failed to load categories: ${categoriesResponse.error}`});
  }
  const { data } = categoriesResponse;
  if(!data) {
    error(500, {message: `Failed to load categories`});
  }

  console.log({data});
  return {
    categories: data
  }
}