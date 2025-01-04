<script lang="ts">
  import { Sun, Moon } from 'lucide-svelte';
  import { onMount } from 'svelte';
  import { browser } from '$app/environment';
  let theme = $state('light');

  function updateTheme() {
    const newTheme = theme === 'light' ? 'dark' : 'light';
    setTheme(newTheme);
  }

  function setTheme(themeValue: string) {
    theme = themeValue;
    document.documentElement.setAttribute('data-theme', theme);
    localStorage.setItem('theme', theme);
  }


  onMount(() => {
    if(browser) {
      (function() {
        const storedTheme = localStorage.getItem('theme');
        if(storedTheme) {
          setTheme(storedTheme);
          return;
        }

        const preferredScheme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
        setTheme(preferredScheme);
      }());
    }
  });
</script>

<li id="header-theme-toggle-btn">
  <button onclick={updateTheme} class="button-transparent icon-btn bg-transparent border-none">
    {#if theme === 'dark'}
      <Sun />
    {:else}
      <Moon />
    {/if}
  </button>
</li>