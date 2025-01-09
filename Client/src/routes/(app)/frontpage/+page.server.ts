import { authorize, initCredentialedApiClient, initCredentialedClient, isAuthorized } from '$lib/auth';
import { fail, redirect } from '@sveltejs/kit';
import { client, getApiFeeds, getApiSubscriptions, postApiSubscriptionsByFeedId, type Subscription } from '$lib/client';

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
    if(subscriptionsResponse.data.length > 0) {
      const subscribedFeeds = subscriptionsResponse.data.map(s => s.feed);
      return {
        subscriptions: subscribedFeeds,
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

export const actions = {
  subscribe: async (event) => {
    authorize(event);
    const { request } = event;
    const formData = await request.formData();
    const formDataEntries = Object.fromEntries(formData.entries());
    const response = await postApiSubscriptionsByFeedId({ path: { feedId: formDataEntries.feedId.toString() } });
    if(response.error) {
      return fail(400, { errors: [response.error.detail] });
    }
    return {
      success: true,
      body: response.data,
    };
  },
};