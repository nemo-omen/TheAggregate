<script lang="ts">
  import { LoaderCircle } from 'lucide-svelte';
</script>

<div class="spin">
  <LoaderCircle />
</div>

<style>
  .spin {
      position: absolute;
      inset: 0;
      display: flex;
      grid-template-areas: 'button';
      align-items: center;
      justify-content: center;
      animation: spin 1s linear infinite;
  }
</style>