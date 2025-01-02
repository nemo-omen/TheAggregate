<script lang="ts">
  import { Sun, Moon, Menu, X } from 'lucide-svelte';
  import { browser } from '$app/environment';
  import Logo from '$lib/components/Logo.svelte';
  import { RegisterModalState, LoginModalState } from '$lib/state/modalState.svelte.ts';
  import { onMount } from 'svelte';
  import { getContext } from 'svelte';
  import { afterNavigate, beforeNavigate } from '$app/navigation';

  let { user } = $props();
  let theme = $state('light');
  let navMenuOpen = $state(false);
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
    navMenuOpen = !navMenuOpen;
  }

  $effect(() => {
    if(navMenuOpen) {
      navMenu.setAttribute('open', '');
    } else {
      navMenu.removeAttribute('open');
    }
  });

  afterNavigate(() => {
    setTimeout(() => {
      navMenuOpen = false;
    }, 200);
  });

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
    <menu id="nav-menu" class="overlay" bind:this={navMenu}>
      <li class="flex" id="mobile-nav-menu-toggle">
        <button class="icon-btn button-subtle" onclick={toggleNavMenu}><X /></button>
      </li>
      <li><a href="/">Home</a></li>
      <li><a href="/features">Features</a></li>
<!--      <li><a href="/design-system">Design System</a></li>-->
    </menu>
    <menu id="auth-menu">
      <li><a href="/auth/login" class="button button-subtle">Log In</a></li>
      <li id="header-register-btn"><a href="/auth/register" class="button button-primary">Create an Account</a></li>
      <li id="header-theme-toggle-btn">
        <button onclick={updateTheme} class="button-transparent icon-btn bg-transparent border-none">
          {#if theme === 'dark'}
            <Sun />
          {:else}
            <Moon />
          {/if}
        </button>
      </li>
      <li id="header-nav-toggle-btn"><button class="icon-btn button-transparent" onclick={toggleNavMenu}><Menu /></button></li>
    </menu>
  </nav>
</header>

<div class="backdrop"></div>

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
      position: fixed;
      inset: 0;
      margin: 0;
      height: 100vh;
      transform: translateX(100%);
      flex-direction: column;
      align-items: start;
      padding-inline: var(--space-8);
      padding-block: var(--space-4);
      background-color: var(--surface-color-0);
      transition: transform var(--transition-default);
      z-index: 101;
      box-shadow: -5px 0 10px rgba(0, 0, 0, 0.1);

      & li {
          width: 100%;
          margin-block-start: var(--space-4);
          display: flex;
      }

      & li a {
          color: var(--text-color);
          font-size: var(--step-1);
          font-weight: var(--font-weight-semibold);
          width: 100%;
          height: 100%;
      }

      & li:has(button) {
          justify-content: flex-end;
      }

      @media(min-width: 850px) {
          position: inherit;
          flex-direction: row;
          align-items: center;
          gap: 1rem;
          height: unset;
          background-color: transparent;
          transform: translateX(0);
          padding: unset;
          box-shadow: none;

          & li:has(button) {
              justify-content: unset;
          }

          & li {
              width: unset;
              padding-block: unset;
              border-bottom: none;
              display: unset;
          }

          & li button {
              display: none;
          }

          & li a {
              font-size: var(--step-0);
              font-weight: var(--font-weight-semibold);
          }
      }
  }

  :global(#nav-menu[open]) {
      transform: translateX(0);

      & li {
          width: 100%;
          padding-block: var(--space-4);
          display: flex;
      }

      & li:has(button) {
          justify-content: flex-end;
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