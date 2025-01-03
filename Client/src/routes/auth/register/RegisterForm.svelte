<script lang="ts">
  import { enhance } from "$app/forms";
  import { fade } from 'svelte/transition';
  let { form } = $props();

  let registrationErrors = $state([]);
</script>

<form
  action="/auth/register"
  method="POST"
  class="stack gap-8 padding-4 padding-top-0"
  autocomplete="off"
  use:enhance={({ formElement, formData, action, cancel }) => {
    return async ({ result, update }) => {
      registrationErrors = [];
      console.log({ result });
      if (result.type === 'success') {
        const { data } = result;
        console.log({ data });
        await update();
      }

      if (result.type === 'failure') {
        const { errors } = result.data;
        registrationErrors = errors;
      }
    };
  }}
>
  <h2 class="margin-0 text-center">Create an Account</h2>
  {#if registrationErrors.length > 0}
    <div class="form-error">
      <ul class="unstyled-list">
        {#each registrationErrors as error}
          <li class="text-danger alert alert-danger" transition:fade={{duration: 300}}>{error}</li>
        {/each}
      </ul>
    </div>
  {/if}
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
    <input type="password" name="passwordConfirm" id="registerConfirm" placeholder="Type Password Again">
  </fieldset>
  <div class="stack align-center">
    <button type="submit" class="button button-primary">Register</button>
  </div>
</form>