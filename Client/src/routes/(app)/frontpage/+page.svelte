<script lang="ts">
  import { getContext } from 'svelte';
  import { Newspaper } from 'lucide-svelte';
  import { enhance } from '$app/forms';

  let user = $derived(getContext('user'));
  let { data, form } = $props();

  function replaceImage(event: Event) {
    const img = event.target as HTMLImageElement;
    const newImage = new Image();
    newImage.src = '/images/placeholder.svg';
    newImage.height = 150;
    newImage.style.opacity = '0.2';
    newImage.alt = img.alt;
    img.parentElement?.replaceChild(newImage, img);
  }
</script>

{#if !data.subscriptions || !data.subscriptions.length}
  <div class="hero stack gap-8 align-center justify-center margin-top-8 bg-2 padding-8 border-radius-2">
    <Newspaper size="144" opacity="50%" />
    <h2 class="margin-0">You're not subscribed to any feeds.</h2>
  </div>
{/if}

{#if data.feeds && data.feeds.length}
  <div class="container flex justify-center margin-bottom-8">
    <p class="font-size-large">Check out these feeds to get started.</p>
  </div>
  <div class="container margin-top-8" style="flex-wrap: wrap;">
    {#each data.feeds as feed}
        {#if feed.items && feed.items.length > 0}
          <div class="stack gap-4 margin-top-8">
            <div class="flex justify-between">
              <h3 class="margin-0" style="line-height: 1;">
                <a href="/feeds/{feed.id}" class="flex gap-2 text-body outline-hidden">
                  {#if feed.imageUrl}
                    <img class="feed-logo" src={feed.imageUrl} alt={feed.title}>
                  {/if}
                  {feed.title}
                </a>
              </h3>
              <form action="?/subscribe" method="post">
                <input type="hidden" name="feedId" value={feed.id}/>
                <button type="submit">Subscribe</button>
              </form>
            </div>
            <div class="card-grid">
              {#each feed.items.slice(0, 3) as item, index}
                <article>
                  <a href="/articles/{item.id}" class="width-100">
                    {#if item.imageUrl}
                      <img src={item.imageUrl} alt={item.title} height="150" onerror={(event) => replaceImage(event)}/>
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
              {/each}
            </div>
          </div>
        {/if}
    {/each}
  </div>
{/if}

<style>
  .card-grid {
      display: grid;
      grid-template-columns: repeat(
              auto-fill,
              minmax(min(var(--card-width), 90vw), 1fr)
      );
      gap: var(--space-4);
  }
</style>