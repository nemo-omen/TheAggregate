<script lang="ts">
  import { enhance } from '$app/forms';
  import type { FeedCategoryResponse } from '$lib/client';

  type Props = {
    categories: Array<FeedCategoryResponse>;
  };

  let { data } = $props();
  const { categories } = data;
  console.log({ categories });
</script>

<div class="stack gap-8 container margin-top-8">
  <form action="?/search" method="post" style="border-color: var(--border-color-5);" use:enhance>
    <div class="input-group">
      <input
        type="search"
        name="search"
        id="search"
        placeholder="Search for a site or add an RSS URL"
        class="width-100"
        aria-label="Search for a site or add an RSS URL" />
      <button type="submit" class="button-transparent margin-0">Search</button>
    </div>
  </form>
  {#if categories}
    <div class="card-grid">
      {#each categories as category}
        <a href={`/categories/${category.category}`} class="card">
          {#if category.categoryImage}
            <img src={category.categoryImage} alt={category.category} class="card-cover"/>
          {:else}
            <img src="/images/placeholder.svg" alt={category.category} class="card-cover"/>
          {/if}
          <h3 class="margin-top-4 text-center font-size-body">{category.category.charAt(0).toUpperCase() + category.category.slice(1)}</h3>
        </a>
      {/each}
    </div>
  {/if}
</div>

<style>
  .input-group {
      display: flex;
      gap: 0;
      width: 100%;
      box-shadow: var(--shadow-1);

      & input {
          border-radius: var(--border-radius) 0 0 var(--border-radius);
      }

      & button {
          border-radius: 0 var(--border-radius) var(--border-radius) 0;
          border: 1px solid var(--border-color);
          border-left: 0;
          color: var(--text-secondary);
      }
  }
</style>