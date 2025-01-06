<script lang="ts">
  import { navigating } from '$app/state';
  import { getContext } from 'svelte';
  import type { FeedItem } from '$lib/client';
  import { fade } from 'svelte/transition';
  import { ArrowLeftCircle } from 'lucide-svelte';

  type Props = {
    data: {
      item: FeedItem,
      referer: string,
    }
  }


  let { data }: Props = $props();
  let { item } = data;
  let user = $derived(getContext('user'));

  function formatDate(date: string) {
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  }

  function handleImageError(event: Event) {
    const img = event.target as HTMLImageElement;
    img.remove();
  }
</script>

<div class="container-small stack">
  <div class="article-header stack gap-8">
    {#if data.referer && data.referer.length > 0}
      <a href={data.referer} class="flex align-center outline-hidden">
        <ArrowLeftCircle size="24" />
        <span>Back</span>
      </a>
    {/if}
    {#if item.imageUrl}
      <img src={item.imageUrl} alt={item.title} onerror={(event) => handleImageError(event)} in:fade={{duration: 250, delay: 2000}} />
    {/if}
  </div>
  <h1>{item.title}</h1>
  <hr class="width-100">
  <div class="flex justify-between">
    {#if item.feed && item.feed.title}
      <small>
        <a href="/feeds/{item.feed.id}">{item.feed.title}</a>
      </small>
    {/if}
    <div class="flex gap-4">
      {#if item.published}
        <small>{formatDate(item.published)}</small>
      {/if}
      <small>{item.author}</small>
    </div>
  </div>
  <div class="stack gap-8">
    {#if item.htmlContent}
      {@html item.htmlContent}
    {:else if item.plainTextContent}
      <p>{item.plainTextContent}</p>
    {:else}
      <p>{item.summary}</p>
    {/if}
    {#if item.url}
      <div class="flex justify-start">
        <a href={item.url} class="button" target="_blank">Read More at {item.feed.title}</a>
      </div>
    {/if}
  </div>
</div>