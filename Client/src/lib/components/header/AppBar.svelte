<script lang="ts">
  import { getContext } from 'svelte';
  import type { UserWithRolesResponse } from '$lib/client';
  import Logo from '$lib/components/Logo.svelte';
  import NavDropdown from '$lib/components/nav-dropdown/NavDropdown.svelte';
  import ThemeSwitcher from '$lib/components/header/ThemeSwitcher.svelte';
  let currentUser: () => UserWithRolesResponse = getContext('user');
  let user = $derived(currentUser);
</script>

<header class="appbar container-xlarge">
  <div class="flex justify-between align-center">
    <a class="app-logo text-body" href="/frontpage">
      <Logo />
    </a>

    {#if user()}
      <nav class="gap-2">
        <NavDropdown {user} />
        <ThemeSwitcher />
      </nav>
    {/if}
  </div>
</header>

<style>
  .appbar {
      background-color: var(--background-1);
      border-radius: var(--border-radius);
      box-shadow: var(--shadow-1);
      border: 1px solid var(--border-color-5);
      padding: var(--space-2) var(--space-4);
      margin-block-start: var(--space-4);
      z-index: 10;
  }

  .app-logo {
      display: flex;
      justify-content: center;
      align-items: center;
      font-size: var(--step-1);
  }
</style>