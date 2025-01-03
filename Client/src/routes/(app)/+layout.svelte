<script lang="ts">
  let { children } = $props();
</script>

<div id="app-content" class="container-xlarge margin-top-0">
  <aside class="sidebar">

  </aside>
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
    }
    .sidebar {
        height: 100%;
        width: min(18rem, 100vw);
        background-color: var(--background-2);
        padding: 1rem;
        border-radius: var(--border-radius);
    }
  main {
    margin: 0;
  }
</style>