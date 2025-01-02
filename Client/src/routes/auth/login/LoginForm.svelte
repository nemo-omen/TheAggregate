<script lang="ts">
  import { enhance } from "$app/forms";
  let { form } = $props();
  let loginErrors = $state([]);
</script>

<form
  method="POST"
  class="stack gap-8 padding-4 padding-top-0 order-2"
  use:enhance={({ formElement, formData, action, cancel }) => {
  return async ({ result, update }) => {
    console.log({ result });
    if (result.type === 'success') {
      const { data } = result;
      console.log({ data });
      await update();
    }

    if (result.type === 'failure') {
        const { errors } = result.data;
        loginErrors = errors;
      }
  };
}}>
  <h2 class="margin-0 text-center">Log In</h2>
  {#if loginErrors.length > 0}
    <div class="form-error">
      <ul class="unstyled-list">
        {#each loginErrors as error}
          <li class="text-danger alert alert-danger" transition:fade={{duration: 300}}>{error}</li>
        {/each}
      </ul>
    </div>
  {/if}
  <fieldset>
    <label for="loginEmail">Email</label>
    <input name="email" id="loginEmail" type="email" placeholder="Email" required>
  </fieldset>
  <fieldset>
    <label for="loginPassword">Password</label>
    <input name="password" id="loginPassword" type="password" placeholder="Password" required>
  </fieldset>
  <div class="stack align-center">
    <button type="submit" class="button button-primary">Log In</button>
  </div>
</form>