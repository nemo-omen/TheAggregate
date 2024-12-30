<script lang="ts">
  import { X } from 'lucide-svelte';
  import { onMount } from 'svelte';
  import { fly } from 'svelte/transition';
  import { type ModalState } from '$lib/state/modalState.svelte.ts';

  let dialog: HTMLDialogElement;
  let dialogForm: HTMLFormElement;

  interface Props {
    modalState: ModalState;
  }

  let { modalState }: Props = $props();

  export function open() {
    dialog.showModal();
  }

  export function close() {
    dialog.close();
  }

  export function toggle() {
    modalState.toggle();
  }

  onMount(() => {
    document.addEventListener('click', (event) => {
      if(event.target === dialog) {
        modalState.close();
      }
    })
  });
</script>

<div class="modal-wrapper" transition:fly={{ y: -20, opacity: 0, duration: 300 }}>
  <dialog bind:this={dialog} class="modal">
    <div class="stack margin-start-0">
      <div class="flex justify-end" style="width: 100%">
        <form method="dialog" bind:this={dialogForm}>
          <button class="button-transparent icon-btn font-size-large" aria-label="Close" onclick={() => modalState.close()}>
            <X />
          </button>
        </form>
      </div>
      <div class="modal-body">
        <slot/>
      </div>
    </div>
  </dialog>
</div>

<style>
  .modal-body {
    padding: var(--padding-4);
  }

  dialog[open] {
      translate: 0 0;
      opacity: 1;
  }

  dialog[open]::backdrop {
      opacity: 0.75;
  }

  @starting-style {
      dialog[open] {
          translate: 0 -20px;
          opacity: 0;
      }
      dialog[open]::backdrop {
          opacity: 0;
      }
  }

  dialog {
      transition: translate 0.3s ease-out, overlay 0.3s ease-out allow-discrete, display 0.3s ease-out allow-discrete;
      translate: 0 -20px;
      opacity: 0;
  }

  dialog::backdrop {
      opacity: 0;
  }
</style>
