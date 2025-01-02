<script lang="ts">
  import { enhance } from "$app/forms";
  import LoginForm from './LoginForm.svelte';
  let { form } = $props();
</script>

<main class="container">
  <article class="container-xsmall border-1 border-radius-2 padding-8">
    <LoginForm {form} />
    <div class="stack align-center gap-4 order-2">
      <small>Don't Have an Account?</small>
      <a href="/auth/register" class="button button-subtle">Sign Up</a>
    </div>
  </article>
</main>