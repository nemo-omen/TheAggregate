<script lang="ts">
  import { onMount } from 'svelte';
  import { beforeNavigate } from '$app/navigation';
  let { user } = $props();
  let popMenu: HTMLMenuElement | null = null;
  let popMenuRect = $state({top: 0, left: 0, right: 0, bottom: 0, width: 0})
  let currentFocusIndex: number = $state(0);
  let dropdownPopover: HTMLDivElement;
  let logoutButton: HTMLButtonElement;
  let navMenuItems: NodeListOf<HTMLLIElement>;

  onMount(() => {
    popMenuRect.top = popMenu.getBoundingClientRect().top;
    popMenuRect.left = popMenu.getBoundingClientRect().left;
    popMenuRect.right = popMenu.getBoundingClientRect().right;
    popMenuRect.bottom = popMenu.getBoundingClientRect().bottom;
    popMenuRect.width = popMenu.getBoundingClientRect().width;

    navMenuItems = document.querySelectorAll('.dropdown-item');

    for(const item of navMenuItems) {
      item.addEventListener('mouseenter', () => {
        logoutButton.blur();
      });
    }

    dropdownPopover.addEventListener('toggle', (event: ToggleEvent) => {
      if(event.newState === 'open') {
        currentFocusIndex = 0;
        logoutButton.focus();
      }

      if(event.newState === 'closed') {
        currentFocusIndex = 0;
      }
    });

    function setMenuFocus(menuItem: HTMLLIElement) {
      if(menuItem.firstElementChild) {
        console.log(menuItem.firstElementChild.tagName);
        if(menuItem.firstElementChild.tagName === 'FORM') {
          const child: HTMLButtonElement | null = menuItem.firstElementChild.firstElementChild as HTMLButtonElement;
          if(child) {
            child.focus();
            return;
          }
        }
        menuItem.firstElementChild?.focus();
      }
    }

    function setMenuBlur(menuItem: HTMLLIElement) {
      if(menuItem.firstElementChild) {
        if(menuItem.firstElementChild.tagName === 'FORM') {
          const child: HTMLButtonElement | null = menuItem.firstElementChild.firstElementChild as HTMLButtonElement;
          if(child) {
            child.blur();
            return;
          }
        }
        menuItem.firstElementChild?.blur();
      }
    }

    function incrementFocusIndex() {
      setMenuBlur(navMenuItems[currentFocusIndex]);
      if(currentFocusIndex < navMenuItems.length - 1) {
        currentFocusIndex++;
      } else {
        currentFocusIndex = 0;
      }
      setMenuFocus(navMenuItems[currentFocusIndex]);
    }

    function decrementFocusIndex() {
      setMenuBlur(navMenuItems[currentFocusIndex]);
      if(currentFocusIndex > 0) {
        currentFocusIndex--;
      } else {
        currentFocusIndex = navMenuItems.length - 1;
      }
      setMenuFocus(navMenuItems[currentFocusIndex]);
    }

    dropdownPopover.addEventListener('keydown', (event: KeyboardEvent) => {
      switch (event.key) {
        case 'ArrowDown':
          incrementFocusIndex();
          break;
        case 'ArrowRight':
          incrementFocusIndex();
          break;
        case 'ArrowUp':
          decrementFocusIndex();
          break;
        case 'ArrowLeft':
          decrementFocusIndex();
          break;
        case 'Escape':
          dropdownPopover.hidePopover();
          break;
        default:
          break;
      }
    });
  });

  beforeNavigate(() => {
    dropdownPopover.hidePopover();
  });
</script>

<menu id="nav-menu-dropdown" bind:this={popMenu}>
  <li>
    <button class="button button-subtle" id="nav-menu-toggle" popovertarget="nav-dropdown-popover">
      {user().email}
    </button>
    <div
      id="nav-dropdown-popover"
      popover
      style="--top: {popMenuRect.bottom}; --left: {popMenuRect.left}; --right: {popMenuRect.right}; --width: {popMenuRect.width}px"
      bind:this={dropdownPopover}
    >
      <menu class="dropdown-menu">
        <li class="dropdown-item">
          <form action="/auth/logout" method="post" class="padding-0">
            <button class="button button-subtle" id="logout-button" bind:this={logoutButton}>Logout</button>
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
        transition: color var(--transition-default);
    }

    #logout-button:hover {
        color: var(--link-hover-color);
    }

    #nav-menu-toggle:focus-visible, #nav-menu-toggle:focus {
        outline-offset: -1px;
        outline-width: 1px;
        outline-color: var(--border-color-3);
        background-color: var(--surface-color-2);
    }

    #nav-menu-dropdown {
        position: relative;
        margin: 0;
    }

    #nav-dropdown-popover {
        background-color: var(--surface-color-1);
        border-radius: var(--border-radius);
        box-shadow: var(--shadow-2);
        border: 1px solid var(--border-color-4);
        position: absolute;
        inset: unset;
        top: var(--top);
        left: var(--left);
        padding: 0;
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
      gap: 0;
      /*gap: var(--space-2);*/
      width: 100%;
  }

  .dropdown-item {
      width: 100%;
  }

  .dropdown-item a {
      display: block;
      width: 100%;
      padding: var(--space-4) var(--space-8);
  }

  .dropdown-item a:hover {
      background-color: var(--surface-color-2);
  }

  .dropdown-item form {
      width: 100%;

      button {
          width: 100%;
          padding: var(--space-4) var(--space-8);
          align-items: start;
          text-align: left;
          border-radius: 0;
      }

      button:hover {
          background-color: var(--surface-color-2);
      }
  }

    .dropdown-item a:focus,
    .dropdown-item button:focus,
    .dropdown-item a:focus-visible,
    .dropdown-item button:focus-visible {
      outline-offset: -2px;
      outline-color: transparent;
      background-color: var(--surface-color-2);
  }

  [popover] {
      animation: fly-out 0.15s ease-out;
      transform-origin: top center;
  }

  [popover]:popover-open {
      animation: fly-in 0.15s ease-out;
  }

  @keyframes fly-in {
      from {
          /*transform: translateY(-20px);*/
          transform: rotateX(-90deg);
          opacity: 0;
      }
      to {
          /*transform: translateY(0);*/
          transform: rotateX(0);
          opacity: 1;
      }
  }

  @keyframes fly-out {
      from {
          transform: rotateX(0);
          /*transform: translateY(0);*/
          opacity: 1;
      }
      to {
          /*transform: translateY(-20px);*/
          transform: rotateX(-90deg)
          opacity: 0;
      }
  }
</style>