<script lang="ts">
  import Modal from './Modal.svelte';
  import { LoginModalState } from '$lib/state/modalState.svelte';
  import { getContext } from 'svelte';
  let modal: Modal;
  let { form } = $props();

  let loginModalState: LoginModalState = getContext('loginModalState');

  $inspect(loginModalState.isOpen);

  $effect(() => {
    if(loginModalState.isOpen) {
      modal.open();
    } else {
      modal.close();
    }
  });
</script>

<Modal bind:this={modal} modalState={loginModalState} >
  <form action="?/auth/login" class="stack gap-8 padding-4 padding-top-0">
    <h2 class="margin-0 text-center">Log In</h2>
    <fieldset>
      <label for="loginEmail">Email</label>
      <input name="email" id="loginEmail" type="email" placeholder="Email" required>
    </fieldset>
    <fieldset>
      <label for="loginPassword">Password</label>
      <input name="password" id="loginPassword" type="password" placeholder="Password" required>
    </fieldset>
    <button type="submit" class="button button-primary">Log In</button>
  </form>
</Modal>