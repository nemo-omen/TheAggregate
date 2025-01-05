<script>
  import { getContext } from 'svelte';
  import { Newspaper } from 'lucide-svelte';

  let user = $derived(getContext('user'));
  let { data } = $props();
</script>

{#if !data.subscriptions || !data.subscriptions.length}
  <div class="stack gap-8 align-center justify-center margin-top-8">
    <Newspaper size="64" />
    <h2 class="margin-0">No subscriptions found</h2>
    <p class="font-size-large">Check out these feeds to get started.</p>
  </div>
{/if}

{#if data.feeds && data.feeds.length}
  <div class="flex gap-6 container margin-top-8" style="flex-wrap: wrap;">
    {#each data.feeds as feed}
      <div class="card">
        <header>
          <h3 class="margin-0">{feed.title}</h3>
        </header>
        <div class="card-body">
          {#if feed.imageUrl}
            <img src={feed.imageUrl} alt={feed.title} />
          {/if}
          <p>{feed.description}</p>
          {#if feed.items}
            <ul class="unstyled-list stack gap-4">
              {#each feed.items.slice(0, 5) as item, index}
                <li>{item.title.substring(0, 55)}...</li>
                {#if index < feed.items.length - 1}
                  <hr />
                {/if}
              {/each}
            </ul>
            {/if}
        </div>
        <footer>
          <a href="/feeds/{feed.id}">Learn More</a>
        </footer>
      </div>
    {/each}
  </div>
{/if}