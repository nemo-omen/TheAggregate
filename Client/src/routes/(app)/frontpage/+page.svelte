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
      subscriptions: Feed[];
    };
    form: ActionData;
  };

  type ExtendedFeedItem = FeedItem & { feedImg: string; feedTitle: string, feedId: string };

  let { data, form }: Props = $props();
  let user = $derived(getContext('user'));
  let isLoading = $state(false);
  let subscriptions = $derived(data.subscriptions);
  let subscriptionItems: ExtendedFeedItem[] = $derived.by(() => subscriptions.map(sub =>
    sub.items.map(i => ({ feedImg: sub.imageUrl, feedTitle: sub.title, feedId: sub.id, ...i })))
    .flat()
    .sort((a, b) => {
      return new Date(b.published!).getTime() - new Date(a.published!).getTime();
    }));

  $inspect(subscriptionItems);

  function getFirstSentence(text: string) {
    const s = text
      .replaceAll('. ', '.$')
      .replaceAll('! ', '!$')
      .replaceAll('? ', '?$')
      .replaceAll('&#160; ', '$')
      .split('$')[0];
    return s;
  }
</script>

{#if isLoading}
  <div class="flex justify-center font-size-large">
    <Spinner />
  </div>
{:else}
  {#if !data.subscriptions || data.subscriptions.length < 1}
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
    <div class="stack gap-8">
      <h2>The Latest</h2>
      {#if subscriptions && subscriptions.length > 0}
        {@render subscriptionItemStack(subscriptionItems)}
      {/if}
    </div>
  {/if}
{/if}

{#snippet subscriptionItemStack(items: FeedItem[] | undefined)}
  {#if items}
    <div>
        <div class="stack justify-stretch">
          {#each items as item}
              {@render subscriptionItem(item)}
          {/each}
        </div>
    </div>
  {/if}
{/snippet}

{#snippet subscriptionItem(item: ExtendedFeedItem)}
  <div class="subscription-item">
    <div class="subscription-item-header">
      <div class="flex gap-4 align-center">
        {#if item.feedImg}
          <img src={item.feedImg} alt={item.title} class="feed-item-logo" width="48px"/>
        {/if}
        <h2 class="font-size-body margin-0 font-weight-semibold">
          <a href="/articles/{item.id}" class="text-body">
            {item.title}
          </a>
        </h2>
      </div>
      <div class="flex gap-4">
          <small class="text-subtle">
            <a href="/feeds/{item.feedId}" class="text-subtle">
              {item.feedTitle}
            </a>
          </small>
          <small class="text-subtle">{new Date(item.published!).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })}</small>
        <small class="text-subtle">{item.author}</small>
      </div>
    </div>
    {#if item.summary && item.summary.length > 0}
      <small class="margin-0 text-muted">{getFirstSentence(item.summary)}</small>
    {/if}
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
                // if(result.data) {
                //   subscriptions = result.data.subscriptions;
                // }
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

<style>
  .subscription-item {
      padding-inline: 0;
      padding-block: var(--space-4);
      width: 100%;
      border-top: 1px solid var(--border-color-5);
      display: flex;
      flex-direction: column;
      gap: var(--space-3);
  }

  .subscription-item-header {
      display: flex;
      flex-direction: column;
      gap: var(--space-2);
  }
</style>