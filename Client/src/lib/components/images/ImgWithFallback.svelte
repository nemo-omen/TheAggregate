<script lang="ts">
  import { fade } from 'svelte/transition';
  import { onMount } from 'svelte';

  type Props = {
    src: string;
    fallback: string;
    alt: string;
    height: number;
    width?: number;
    square?: boolean;
  };

  let { src, fallback, alt, height, width, square }: Props = $props();

  let isLoading = $state(true);
  let isError = $state(false);

  function setError() {
    isLoading = false;
    isError = true;
  }

  function setLoading() {
    isLoading = true;
    isError = false;
  }

  function setLoaded() {
    setTimeout(() => {
      if(!isError) {
        isLoading = false;
        isError = false;
      }
    }, 1500);
  }
</script>


<div class="img-wrapper {square ? 'square' : ''}" style="height: {height}px;">
    <img
      src={src}
      alt={alt}
      height={height}
      width={width}
      onerror={setError}
      onload={setLoaded}
      class:loading={isLoading}
      class:error={isError}
      in:fade={{duration: 250, delay: 2000}}
    />

  <img
    src={fallback}
    style="opacity: 0.2"
    alt={alt}
    height={height}
    width={width}
  />
</div>

<style>
    .img-wrapper {
        position: relative;
        /*width: 100%;*/
    }

    .square {
        aspect-ratio: 1;
    }

    img {
        position: absolute;
        top: 0;
        left: 0;
        /*height: 100%;*/
        /*width: 100%;*/
        object-fit: cover;
        transition: opacity var(--transition-default);
    }

    img.loading, img.error {
        opacity: 0;
        visibility: hidden;
        width: 0;
        height: 0;
    }
</style>