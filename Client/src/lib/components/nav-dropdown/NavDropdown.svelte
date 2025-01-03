<script lang="ts">
  import { onMount } from 'svelte';
  import { fly } from 'svelte/transition';
  let { user } = $props();
  let popMenu = null;
  let popMenuRect = $state({top: 0, left: 0, right: 0, bottom: 0, width: 0})

  $inspect({ popMenuRect });

  onMount(() => {
    popMenuRect.top = popMenu.getBoundingClientRect().top;
    popMenuRect.left = popMenu.getBoundingClientRect().left;
    popMenuRect.right = popMenu.getBoundingClientRect().right;
    popMenuRect.bottom = popMenu.getBoundingClientRect().bottom;
    popMenuRect.width = popMenu.getBoundingClientRect().width;
  });
</script>

<menu id="nav-menu-dropdown" bind:this={popMenu}>
  <li>
    <button class="button button-subtle" id="nav-menu-toggle" popovertarget="nav-dropdown-popover">
      {user().email}
    </button>
    <div id="nav-dropdown-popover" popover style="--top: {popMenuRect.bottom}; --left: {popMenuRect.left}; --right: {popMenuRect.right}; --width: {popMenuRect.width}px">
      <menu class="dropdown-menu">
        <li class="dropdown-item">
          <form action="/auth/logout" method="post" class="padding-0">
            <button class="button button-subtle" id="logout-button">Logout</button>
          </form>
        </li>
        <li class="dropdown-item">
          <a href="/profile">Profile</a>
        </li>
        <li class="dropdown-item">
          <a href="/settings">Settings</a>
        </li>
      </menu>
    </div>
  </li>
</menu>


<style>
    #logout-button {
        padding: 0;
        transition: color var(--transition-default);
    }
    #logout-button:hover {
        color: var(--link-hover-color);
    }
    #nav-menu-dropdown {
        position: relative;
    }
    #nav-dropdown-popover {
        background-color: var(--surface-color-1);
        border-radius: var(--border-radius);
        box-shadow: var(--shadow-2);
        padding: var(--space-4);
        border: 1px solid var(--border-color-4);
        position: absolute;
        inset: unset;
        top: var(--top);
        left: var(--left);
        /*right: var(--right);*/
        /*width: var(--width);*/
        transform: translateY(var(--space-1)) translateX(calc((var(--right) - var(--left)) / 2)px);
    }
    #nav-dropdown-popover::backdrop {
        display: none;
    }
  .dropdown-menu {
      display: flex;
      flex-direction: column;
      gap: var(--space-2);
      width: 100%;
  }
  .dropdown-item {
      width: 100%;
  }
  .dropdown-item a {
      display: block;
      width: 100%;
      padding-block: var(--space-2);
  }
  .dropdown-item form {
      width: 100%;

      & button {
          width: 100%;
          padding-block: var(--space-2);
          align-items: start;
          text-align: left;
      }
  }
</style>