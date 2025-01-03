<script>
  import { getContext } from 'svelte';
  let user = $derived(getContext('user'));
  let { data } = $props();
</script>

<h2 class="margin-0">Front Page</h2>