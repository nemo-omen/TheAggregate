<script lang="ts">
  import type { Feed } from '$lib/client';
  import { ArrowLeftCircle } from 'lucide-svelte';

  type Props = {
    data: {
      feed: Feed,
      referer: string,
    }
  }
  let { data }: Props = $props();
  let { feed, referer } = data;

</script>

<div class="container">
  <div class="stack gap-8">
    {#if referer && referer.length > 0}
      <a href={referer} class="flex align-center">
        <ArrowLeftCircle size="24" />
        <span>Back</span>
      </a>
     {/if}
    {#if data.feed}
      <div class="stack gap-2">
        <h1>{feed.title}</h1>
        <hr class="width-100">
      </div>
    {/if}
  </div>
</div>