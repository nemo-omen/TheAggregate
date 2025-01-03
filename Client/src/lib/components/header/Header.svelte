<script lang="ts">
  import PageHeader from './PageHeader.svelte';
  import AppBar from './AppBar.svelte';
  import type { UserWithRolesResponse } from '$lib/client';
  import { getContext } from 'svelte';

  let currentUser: () => UserWithRolesResponse = getContext('user');

  let user = $derived(currentUser);
</script>

{#if user()}
  <AppBar />
{:else}
  <PageHeader />
{/if}

