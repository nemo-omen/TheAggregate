<script lang="ts">
  import { enhance } from "$app/forms";
  import RegisterForm from './RegisterForm.svelte';
  let { form } = $props();
</script>

<main class="container">
  <article class="container-xsmall border-1 border-radius-2 padding-8">
    <RegisterForm {form} />
    <div class="stack align-center gap-4">
      <small>Already Have an Account?</small>
      <a href="/auth/login" class="button button-subtle">Log In</a>
    </div>
  </article>
</main>