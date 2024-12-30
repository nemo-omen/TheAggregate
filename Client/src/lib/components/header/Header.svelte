<script lang="ts">
  import { Sun, Moon, Menu, X } from 'lucide-svelte';
  import { browser } from '$app/environment';
  import Logo from '$lib/components/Logo.svelte';
  import { RegisterModalState, LoginModalState } from '$lib/state/modalState.svelte.ts';
  import { onMount } from 'svelte';
  import { getContext } from 'svelte';

  let { user } = $props();
  let theme = $state('light');
  let navMenu: HTMLMenuElement;
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

  function toggleNavMenu() {
    // navMenu.getAttribute('hidden') ? navMenu.removeAttribute('hidden') : navMenu.setAttribute('hidden', '');
      navMenu.style.transform = navMenu.style.transform === 'translateX(0)' ? 'translateX(100%)' : 'translateX(0)';
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
    <menu id="brand-menu">
      <li>
        <a href="/" class="flex align-center brand">
          <div class="brand-logo">
            <Logo />
          </div>
          <strong>The Aggregate</strong>
        </a>
      </li>
    </menu>
    <menu id="nav-menu" bind:this={navMenu}>
      <li class="flex justify-end" id="mobile-nav-menu-toggle"><button class="icon-btn" onclick={toggleNavMenu}><X /></button></li>
      <li><a href="/">Home</a></li>
      <li><a href="/features">Features</a></li>
<!--      <li><a href="/design-system">Design System</a></li>-->
    </menu>
    <menu id="auth-menu">
      <li><button onclick={() => toggleLoginModal()} class="button-subtle">Log In</button></li>
      <li id="header-register-btn"><button onclick={() => toggleRegisterModal()} class="button-primary">Create an Account</button></li>
      <li id="header-theme-toggle-btn">
        <button onclick={updateTheme} class="button-transparent icon-btn bg-transparent border-none">
          {#if theme === 'dark'}
            <Sun />
          {:else}
            <Moon />
          {/if}
        </button>
      </li>
      <li id="header-nav-toggle-btn"><button class="icon-btn" onclick={toggleNavMenu()}><Menu /></button></li>
    </menu>
  </nav>
</header>

<style>
  .brand {
      display: flex;
      gap: var(--space-3);
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

  #nav-menu {
      position: absolute;
      inset: 0;
      height: 100vh;
      transform: translateX(100%);
      flex-direction: column;
      align-items: start;
      padding: var(--space-8);
      background-color: var(--surface-color-0);
      @media(min-width: 850px) {
          position: inherit;
          flex-direction: row;
          align-items: center;
          gap: 1rem;
          height: unset;
          background-color: transparent;
          transform: translateX(0);
          padding: unset;
      }
  }

  #header-register-btn {
      display: none;

      @media(min-width: 768px) {
          display: block;
      }
  }

  #header-theme-toggle-btn {
      display: none;

      @media(min-width: 850px) {
          display: block;
      }
  }

  #header-nav-toggle-btn, #mobile-nav-menu-toggle {
      display: block;

      @media(min-width: 850px) {
          display: none;
      }
  }
</style>