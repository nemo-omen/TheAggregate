<script lang="ts">
	import '$lib/style/app.css';
  import Header from '$lib/components/header/Header.svelte';
	let { children, data } = $props();
  import { RegisterModalState, LoginModalState } from '$lib/state/modalState.svelte.ts';
  import { setContext } from 'svelte';
  const registerModalState = new RegisterModalState();
  const loginModalState = new LoginModalState();

  setContext('registerModalState', registerModalState);
  setContext('loginModalState', loginModalState);
</script>

<Header user={{}} />
{@render children()}
