<script lang="ts">
  import { Sun, Moon } from 'lucide-svelte';
  import { browser } from '$app/environment';
  import Logo from '$lib/components/Logo.svelte';
  import { RegisterModalState, LoginModalState } from '$lib/state/modalState.svelte.ts';
  import { onMount } from 'svelte';
  import { getContext } from 'svelte';

  let { user } = $props();
  let theme = $state('light');
  const registerModalState: RegisterModalState = getContext('registerModalState');
  const loginModalState: LoginModalState = getContext('loginModalState');

  function updateTheme() {
      const newTheme = theme === 'light' ? 'dark' : 'light';
      setTheme(newTheme);
  }

  function setTheme(themeValue: string) {
      theme = themeValue;
      document.documentElement.setAttribute('data-theme', theme);
      localStorage.setItem('theme', theme);
  }

  function toggleLoginModal() {
      loginModalState.toggle();
  }

  function toggleRegisterModal() {
      registerModalState.toggle();
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

<header class="container-large">
  <nav>
    <menu>
      <li>
        <a href="/" class="flex align-center">
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
      <li><a href="/design-system">Design System</a></li>
    </menu>
    <menu>
      <li>
        <button onclick={updateTheme} class="button-transparent icon-btn bg-transparent border-none">
          {#if theme === 'dark'}
            <Sun />
          {:else}
            <Moon />
          {/if}
        </button>
      </li>
      <li><button onclick={() => toggleLoginModal()} class="button-subtle">Log In</button></li>
      <li><button onclick={() => toggleRegisterModal()} class="button-primary">Create an Account</button></li>
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