<script>
  import { getContext } from 'svelte';
  let user = $derived(getContext('user'));
  let { data } = $props();
</script>
<main class="container-large margin-top-0">
  <h2 class="margin-0">Front Page</h2>
</main>