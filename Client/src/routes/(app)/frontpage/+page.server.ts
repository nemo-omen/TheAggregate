import { authorize, initCredentialedApiClient, initCredentialedClient, isAuthorized } from '$lib/auth';
import { fail, redirect } from '@sveltejs/kit';
import { client, getApiFeeds, getApiSubscriptions } from '$lib/client';

export async function load(event) {
  authorize(event);
  const { cookies } = event;
  const accessToken = cookies.get('aggregate_accessToken');
  if(!accessToken) {
    redirect(303, '/auth/login');
  }
  initCredentialedClient(accessToken, client);

  const subscriptionsResponse = await getApiSubscriptions();
  if(subscriptionsResponse.error) {
    console.log(subscriptionsResponse.error);
    fail(500, { errors: ['Failed to load subscriptions'] });
  }

  if(!subscriptionsResponse.data) {
    fail(500, { errors: ['Failed to load subscriptions'] });
  }

  if(subscriptionsResponse.data) {
    // for now, if the user has subscriptions, just return them
    // without a list of feeds they can subscribe to
    if(subscriptionsResponse.data.length > 0) {
      return {
        subscriptions: subscriptionsResponse.data,
        user: event.locals.user,
      }
    }
  }

  const feedsResponse = await getApiFeeds();
  if(feedsResponse.error) {
    console.log(feedsResponse.error);
    fail(500, { errors: ['Failed to load feeds'] });
  }

  if(!feedsResponse.data) {
    fail(500, { errors: ['Failed to load feeds'] });
  }

  return {
    feeds: feedsResponse.data,
    subscriptions: [],
    user: event.locals.user,
  }
}