<script lang="ts">
  import type { Feed } from '$lib/client';
  import { ArrowLeftCircle } from 'lucide-svelte';
  import ImgWithFallback from '$lib/components/images/ImgWithFallback.svelte';

  type Props = {
    data: {
      feed: Feed,
      referer: string,
    }
  }
  let { data }: Props = $props();
  let { feed, referer } = data;

  if(referer.includes('articles')) {
    let refUrl = new URL(referer);
    refUrl.pathname = '/frontpage';
    referer = refUrl.href;
  }

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

<div class="container">
  <div class="stack gap-8">
    {#if referer && referer.length > 0}
      <a href={referer} class="flex align-center outline-hidden">
        <ArrowLeftCircle size="24" />
        <span>Back</span>
      </a>
     {/if}
    {#if feed}
      <div class="stack gap-2">
        <h1>{feed.title}</h1>
        <hr class="width-100">
          {#if feed.items && feed.items.length > 0}
            <div class="card-grid">
              {#each feed.items.slice(0, feed.items.length >= 10 ? 10 : feed.items.length - 1) as item, index}
                <article>
                  <a href="/articles/{item.id}" class="width-100">
                    {#if item.imageUrl}
                      <ImgWithFallback src={item.imageUrl} fallback="/images/placeholder.svg" alt={item.title?? ''} height={150} />
<!--                      <img src={item.imageUrl} alt={item.title} height="150" onerror={(event) => replaceImage(event)}/>-->
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
          {/if}
      </div>
    {/if}
  </div>
</div>