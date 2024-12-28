<script lang="ts">
  import { page } from '$app/state';
  import { afterNavigate } from '$app/navigation';
  import { HeartCrack } from 'lucide-svelte';

  let referringLink: string;

  afterNavigate((navigation) => {
    referringLink = navigation.from.url.pathname;
  });
</script>

<main class="container">
  <h1>{page.status} {page.error.message}</h1>
  <span>
    <HeartCrack size="96"/>
  </span>
  <a href={referringLink?? '/'}>{referringLink ? 'Go Back!' : 'Go Home!'}</a>
</main>

<style>
  .container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding-block-start: 10rem;
      gap: 1rem;
  }
  span {
      color: var(--red-accent);
  }
</style>