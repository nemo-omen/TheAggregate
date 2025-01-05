<script lang="ts">
  import { LoaderCircle } from 'lucide-svelte';
  import { onMount } from 'svelte';

  type Props = {
    loading?: boolean;
    text: string;
    className?: string;
    type?: 'button' | 'submit' | 'reset' | null | undefined;
  }

  let { text, className = '', loading = false, type = 'button' }: Props = $props();

  let isLoading = $derived(loading?? false);

  onMount(() => {
    $effect(() => {
      if(isLoading) {
        document.body.style.cursor = 'wait';
      } else {
        document.body.style.cursor = 'default';
      }
    });
  });
</script>

<button {type} class={[className, 'button']} disabled={isLoading}>
  {#if isLoading}
    <span class="hidden">
      {text}
    </span>
    <div class="spin">
      <LoaderCircle />
    </div>
  {:else}
    {text}
  {/if}
</button>

<style>
    button {
      position: relative;
    }
  .spin {
      position: absolute;
      inset: 0;
      display: flex;
      grid-template-areas: 'button';
      align-items: center;
      justify-content: center;
      animation: spin 1s linear infinite;
  }

  .hidden {
      opacity: 0;
  }

  @keyframes spin {
    0% {
      transform: rotate(0deg);
    }
    100% {
      transform: rotate(360deg);
    }
  }
</style>