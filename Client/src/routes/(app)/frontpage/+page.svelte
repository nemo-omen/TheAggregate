<script lang="ts">
  import { getContext } from 'svelte';
  import { Newspaper } from 'lucide-svelte';
  import { enhance } from '$app/forms';
  import ImgWithFallback from '$lib/components/images/ImgWithFallback.svelte';
  import Spinner from '$lib/components/ui/Spinner.svelte';
  import type { Feed, FeedItem, Subscription } from '$lib/client';
  import type { ActionData } from './$types';

  type Props = {
    data: {
      feeds: Feed[];
      subscriptions: Subscription[];
    };
    form: ActionData;
  };

  let { data, form }: Props = $props();
  let user = $derived(getContext('user'));
  let isLoading = $state(false);
  let { subscriptions } = data;
  let subscriptionItems: FeedItem[] = $derived.by(() => subscriptions.map(sub => sub.items).flat().sort((a, b) => {
    return new Date(b.published!).getTime() - new Date(a.published!).getTime();
  }));
  $inspect(subscriptionItems);
  // for(const subscription of subscriptions) {
  //   if(subscription.feed && subscription.feed.items) {
  //     subscriptionItems.push(...subscription.feed.items);
  //     subscriptionItems.sort((a, b) => {
  //       return new Date(b.published!).getTime() - new Date(a.published!).getTime();
  //     });
  //   }
  // }
  // console.log({subscriptionItems});

  function hasSubscriptions() {
    return data.subscriptions && data.subscriptions.length > 0;
  }

  function hasSubscriptionItems() {
    return subscriptionItems && subscriptionItems.length > 0;
  }
</script>

{#if isLoading}
  <div class="flex justify-center font-size-large">
    <Spinner />
  </div>
{:else}
  {#if !hasSubscriptions()}
    <div class="hero stack gap-8 align-center justify-center margin-top-8 padding-8 border-radius-2">
      <Newspaper size="144" opacity="15%" />
      <h2 class="margin-0">You're not subscribed to any feeds.</h2>
      {#if data.feeds && data.feeds.length}
        <div class="container flex justify-center margin-bottom-8">
          <p class="font-size-large">Check out these feeds to get started.</p>
        </div>
        <div class="container margin-top-8" style="flex-wrap: wrap;">
          {#each data.feeds as feed}
            {#if feed.items && feed.items.length > 0}
              {@render feedSnippet(feed)}
              <hr style="border-color: var(--border-color-5);"/>
            {/if}
          {/each}
        </div>
      {/if}
    </div>
  {:else}
    <h2>The Latest</h2>
    {#if hasSubscriptionItems()}
      {@render subscriptionItemStack(subscriptionItems)}
    {/if}
  {/if}
{/if}

{#snippet subscriptionItemStack(items: FeedItem[] | undefined)}
  {#if items}
    <div>
        <div class="stack">
          {#each items as item}
            {@render subscriptionItem(item)}
          {/each}
        </div>
    </div>
  {/if}
{/snippet}

{#snippet subscriptionItem(item: FeedItem)}
  <div class="subscription-item">
<!--    <h2 class="font-size-body margin-0">{item.title}</h2>-->
    <h2 class="font-size-body margin-0">DOOKIE</h2>
  </div>
{/snippet}

{#snippet feedSnippet(feed: Feed)}
  <div class="stack gap-4 margin-top-8 margin-bottom-8">
    <div class="flex justify-between">
      <h3 class="margin-0" style="line-height: 1;">
        <a href="/feeds/{feed.id}" class="flex gap-2 text-body outline-hidden">
          {#if feed.imageUrl}
            <img class="feed-logo" src={feed.imageUrl} alt={feed.title}>
          {/if}
          {feed.title}
        </a>
      </h3>
      <form action="?/subscribe" method="post" use:enhance={
        ({ formElement, formData, action, cancel, submitter }) => {
          isLoading = true;
          return async ({ result, update }) => {
            if(result) {
              if(result.type === 'failure') {
                console.error(result.data);
                update();
              }

              if(result.type === 'success') {
                if(result.data) {
                  subscriptions = result.data.subscriptions;
                }
                update();
              }
              isLoading = false;
          };
      }}}>
        <input type="hidden" name="feedId" value={feed.id}/>
        <button type="submit">Subscribe</button>
      </form>
    </div>
    <div class="card-grid">
      {#each feed.items.slice(0, 3) as item, index}
        {@render feedItem(item)}
      {/each}
    </div>
  </div>
{/snippet}

{#snippet feedItem(item: FeedItem)}
  <article>
    <a href="/articles/{item.id}" class="width-100">
      {#if item.imageUrl}
        <ImgWithFallback src={item.imageUrl} fallback="/images/placeholder.svg" alt={item.title?? ''} height={150} />
      {:else}
        <img src="/images/placeholder.svg" alt={item.title} height="150" style="opacity: 0.2"/>
      {/if}
    </a>
    <h4 class="margin-top-0 margin-bottom-0">
      <a href="/articles/{item.id}">
        {item.title}
      </a>
    </h4>
    <footer>
      <a href="/articles/{item.id}">Read More</a>
    </footer>
  </article>
{/snippet}