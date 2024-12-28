<script lang="ts">
	import '$lib/style/app.css';
  import Header from '$lib/components/header/Header.svelte';
	let { children } = $props();
</script>

<Header user={{}}/>
{@render children()}
