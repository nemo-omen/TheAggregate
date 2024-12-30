<script lang="ts">
  import { getContext } from 'svelte';
  import { RegisterModalState } from '$lib/state/modalState.svelte';
  import Modal from './Modal.svelte';
  let modal: Modal;
  let { form } = $props();

  const registerModalState: RegisterModalState = getContext('registerModalState');

  $inspect(registerModalState.isOpen);

  $effect(() => {
    if(registerModalState.isOpen) {
      modal.open();
    } else {
      modal.close();
    }
  });

</script>

<Modal bind:this={modal} modalState={registerModalState}>
  <form action="?/auth/register" class="stack gap-8 padding-4 padding-top-0">
    <h2 class="margin-0 text-center">Create an Account</h2>
    <fieldset>
      <label for="registerEmail">Email</label>
      <input name="email" id="registerEmail" type="email" placeholder="Email" required>
    </fieldset>
    <fieldset>
      <label for="registerPassword">Password</label>
      <input name="password" id="registerPassword" type="password" placeholder="Password" required>
    </fieldset>
    <fieldset>
      <label for="registerConfirm">Confirm Password</label>
      <input type="password" name="passwordConfirm" id="registerConfirm">
    </fieldset>
    <button type="submit" class="button button-primary">Register</button>
  </form>
</Modal>