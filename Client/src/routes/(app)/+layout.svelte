<script lang="ts">
  import Sidebar from '$lib/components/sidebar/Sidebar.svelte';

  let { children } = $props();
</script>

<div id="app-content" class="container-xlarge margin-top-0">
  <Sidebar />
  <main>
    {@render children()}
  </main>
</div>

<style>
    #app-content {
        display: grid;
        grid-template-columns: auto 10fr;
        gap: var(--space-8);
        position: relative;
        overflow: hidden;

        @media (max-width: 1024px) {
            grid-template-columns: 1fr;
        }
    }
  main {
    margin: 0;
    overflow: auto;
  }
</style>