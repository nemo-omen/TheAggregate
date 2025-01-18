<script lang="ts">
  import { getContext } from 'svelte';
  import type { Feed } from '$lib/client';
  import { Plus } from 'lucide-svelte';

  let subscriptions: Feed[] = getContext('subscriptions');
</script>

<aside class="sidebar flex flex-column gap-4">
  <menu class="unstyled-list">
    <li class="menu-item">
      <a href="/frontpage" class="width-100 height-100">
        Frontpage
      </a>
    </li>
  </menu>
  {#if subscriptions}
    <menu class="unstyled-list">
      <h2>Subscriptions</h2>
      {#each subscriptions as subscription}
        <li class="menu-item flex">
          <a href="/feeds/{subscription.id}" class="width-100 height-100">
            {subscription.title}
          </a>
        </li>
      {/each}
      <li class="menu-item">
        <a href="/feeds/add" class="width-100 height-100 flex gap-2 align-center">
          Add Feed
          <Plus size="18" />
        </a>
      </li>
    </menu>
  {:else}
    <p>No subscriptions found</p>
  {/if}
  <menu class="unstyled-list">
    <h2>Collections</h2>
  </menu>
</aside>

<style>
    .sidebar {
        /*height: 100%;*/
        width: min(18rem, 100vw);
        background-color: var(--background-1);
        padding: 1rem;
        margin-block: var(--space-4);
        border-radius: var(--border-radius);
        border: 1px solid var(--border-color-5);
        display: flex;
        flex-direction: column;
        box-shadow: var(--shadow-1);
        z-index: 10;

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

        & menu {
            padding: 0;
        }

        & menu h2 {
            border-bottom: 2px solid var(--border-color-5);
            font-size: var(--font-size-body);
            margin-top: 0;
            padding: 0 0 var(--space-2) 0;
            line-height: 1;
        }

        & .menu-item {
            padding: var(--space-2) 0;
            border-bottom: 1px solid var(--border-color-5);
            transition: background-color var(--transition-default);
        }
    }
</style>