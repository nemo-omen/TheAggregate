<script lang="ts">
  import Modal from './Modal.svelte';
  import { LoginModalState } from '$lib/state/modalState.svelte';
  import { getContext } from 'svelte';
  let modal: Modal;

  let { children } = $props();
  let loginModalState: LoginModalState = getContext('loginModalState');

  $inspect(loginModalState.isOpen);

  $effect(() => {
    if(loginModalState.isOpen) {
      modal.open();
    } else {
      modal.close();
    }
  });

  export function open() {
    modal.open();
  }

  export function close() {
    modal.close();
  }
</script>

<Modal bind:this={modal} modalState={loginModalState} >
  {@render children()}
</Modal>