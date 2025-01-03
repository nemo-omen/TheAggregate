<script lang="ts">
  import { getContext } from 'svelte';
  import type { UserWithRolesResponse } from '$lib/client';
  import Logo from '$lib/components/Logo.svelte';
  let currentUser: () => UserWithRolesResponse = getContext('user');
  let user = $derived(currentUser);
</script>

<header class="appbar container-xlarge">
  <div class="flex justify-between align-center">
    <div class="app-logo">
      <Logo />
    </div>

    {#if user()}
      <div class="flex align-center">
        <form action="/auth/logout" method="post">
          <button class="btn btn-subtle">Logout</button>
        </form>
      </div>
    {/if}
  </div>
</header>

<style>
  .appbar {
      background-color: var(--background-2);
      border-radius: var(--border-radius);
      box-shadow: var(--box-shadow-1);
      border: 1px solid var(--border-color-5);
      padding: var(--space-2) var(--space-4);
      margin-block-start: var(--space-4);
  }

  .app-logo {
      display: flex;
      justify-content: center;
      align-items: center;
      font-size: var(--step-1);
  }
</style>