<script lang="ts">
	import '$lib/style/app.css';
  import Header from '$lib/components/header/Header.svelte';
  import { RegisterModalState, LoginModalState } from '$lib/state/modalState.svelte.ts';
  import { setContext } from 'svelte';
  import type { UserWithRolesResponse } from '$lib/client';

  const registerModalState = new RegisterModalState();
  const loginModalState = new LoginModalState();
	let { children, data } = $props();

  setContext('user', () => data.user);
  setContext('registerModalState', registerModalState);
  setContext('loginModalState', loginModalState);
</script>

<Header />
{@render children()}
<footer></footer>