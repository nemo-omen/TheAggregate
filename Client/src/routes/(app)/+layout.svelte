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

        @media (max-width: 1024px) {
            grid-template-columns: 1fr;
        }
    }
    .sidebar {
        height: 100%;
        width: min(18rem, 100vw);
        background-color: var(--background-2);
        padding: 1rem;
        border-radius: var(--border-radius);

        @media (max-width: 1024px) {
            position: absolute;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            z-index: 100;
            background-color: var(--background-1);
            border-radius: 0;
            transform: translateX(-100%);
        }
    }
  main {
    margin: 0;
  }
</style>