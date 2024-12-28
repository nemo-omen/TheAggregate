<script lang="ts">
  import { Sun, Moon } from 'lucide-svelte';
  import { browser } from '$app/environment';
  import Logo from '$lib/components/Logo.svelte';
  import { onMount } from 'svelte';

  let { user } = $props();

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

<header class="container">
  <nav>
    <menu>
      <li>
        <a href="/" class="brand">
          <div class="brand-logo">
            <Logo />
          </div>
          <strong>The Aggregate</strong>
        </a>
      </li>
    </menu>
    <menu>
      <li><a href="/">Home</a></li>
      <li><a href="/features">Features</a></li>
      <li><a href="/auth/login">Log In</a></li>
      <li><a href="/auth/register" class="button">Create an Account</a></li>
      <li><button onclick={updateTheme} class="icon-btn bg-transparent border-none">
        {#if theme === 'dark'}
          <Sun />
        {:else}
          <Moon />
        {/if}
      </button></li>
    </menu>
  </nav>
</header>

<style>
  .brand {
      display: flex;
      gap: 0.5rem;
      align-items: center;
  }

  .brand-logo {
      font-size: 2rem;
      display: flex;
      align-items: center;
      justify-content: center;
  }

  .brand:hover {
      color: var(--link-hover-color);
  }

  .icon-btn:hover {
      color: var(--link-hover-color);
  }
</style>