<script lang="ts">
  import { fade } from "svelte/transition";
  type Props = {
    errors: string[];
  };

  let { errors }: Props = $props();
</script>

<div class="form-error">
  <ul class="unstyled-list">
    {#each errors as error}
      <li class="text-danger alert alert-danger" transition:fade={{duration: 300}}>{error}</li>
    {/each}
  </ul>
</div>