<script lang="ts">
  import { getContext } from 'svelte';
  import type { Feed } from '$lib/client';

  let subscriptions: Feed[] = getContext('subscriptions');
</script>

<aside class="sidebar">
  {#if subscriptions}
    <h2>Subscriptions</h2>
    <ul>
      {#each subscriptions as subscription}
        <li>{subscription.title}</li>
      {/each}
    </ul>
  {:else}
    <p>No subscriptions found</p>
  {/if}
</aside>

<style>
    .sidebar {
        height: 100%;
        width: min(18rem, 100vw);
        background-color: var(--background-1);
        padding: 1rem;
        border-radius: var(--border-radius);
        border: 1px solid var(--border-color-5);

        @media (max-width: 1024px) {
            position: absolute;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            z-index: 100;
            background-color: var(--background-1);
            border-radius: 0;
            transform: translateX(-100%);
        }
    }
</style>