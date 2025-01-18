import { fail, redirect, type ServerLoadEvent } from '@sveltejs/kit';
import { authorize, initCredentialedClient } from '$lib/auth';
import { client, getApiSubscriptions } from '$lib/client';

export async function load(event: ServerLoadEvent) {
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
    if(subscriptionsResponse.data.length > 0) {
      const subscribedFeeds = subscriptionsResponse.data
        .map(s => s.subscriptionFeed);
      return {
        subscriptions: subscribedFeeds,
        user: event.locals.user,
      }
    }
  }

  return {
    subscriptions: [],
  }
}