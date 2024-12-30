<script lang="ts">
  import { getContext } from 'svelte';
  import { RegisterModalState } from '$lib/state/modalState.svelte';
  import Modal from './Modal.svelte';
  let modal: Modal;
  let { children } = $props();

  const registerModalState: RegisterModalState = getContext('registerModalState');

  $inspect(registerModalState.isOpen);

  $effect(() => {
    if(registerModalState.isOpen) {
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

<Modal bind:this={modal} modalState={registerModalState}>
  {@render children()}
</Modal>