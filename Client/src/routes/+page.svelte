<script lang="ts">
  import RegisterModal from '$lib/components/modal/RegisterModal.svelte';
  import LoginModal from '$lib/components/modal/LoginModal.svelte';
  import { getContext } from 'svelte';
  import type { LoginModalState, RegisterModalState } from '$lib/state/modalState.svelte';

  let registerModalState: RegisterModalState = getContext('registerModalState');
  let loginModalState: LoginModalState = getContext('loginModalState');
  let registerModal: RegisterModal;
  let loginModal: LoginModal;
  let { data, form } = $props();

</script>

<main class="container">
  <div class="hero stack">
    <h1>Make Your News <br><mark>Personal</mark></h1>
    <p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nunc sem, dictum vel elit sed, efficitur tristique turpis. Nunc a gravida metus.
    </p>
    <div class="container-small flex justify-center align-center gap-4">
      <button class="button button-primary" onclick={() => registerModalState.toggle()}>Get Started</button>
      <a href="/features" class="button button-subtle">Learn More</a>
    </div>
  </div>

  <RegisterModal bind:this={registerModal}>
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
      <div class="stack align-center">
        <button type="submit" class="button button-primary">Register</button>
      </div>
    </form>
    <div class="stack gap-4 align-center margin-bottom-4">
      <small class="text-center">Already have an account?</small>
      <button class="button button-subtle" onclick={() => {
        registerModalState.toggle();
        loginModalState.toggle();
      }}>Log In Instead</button>
    </div>
  </RegisterModal>
  <LoginModal bind:this={loginModal}>
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
      <div class="stack align-center">
        <button type="submit" class="button button-primary">Log In</button>
      </div>
    </form>
    <div class="stack gap-4 align-center margin-bottom-4">
      <small class="text-center">Don't have an account?</small>
      <button class="button button-subtle" onclick={() => {
        loginModalState.toggle();
        registerModalState.toggle();
      }}>Create an Account</button>
    </div>
  </LoginModal>
</main>

<style>
  .hero {
    align-items: center;
      gap: calc(var(--gap-8) * 2);
  }
  h1 {
      font-size: var(--step-6);
      font-weight: var(--font-weight-extrabold);
      text-align: center;
      line-height: 1.2;
      margin-bottom: 0;

      & mark {
          background-color: transparent;
          color: currentColor;
          text-decoration: underline;
          text-decoration-color: var(--primary-color);
          text-decoration-thickness: var(--step--2);
          text-underline-offset: var(--step--2);
      }
  }
  p {
      font-size: var(--step-2);
      max-width: 55ch;
      text-align: center;
      margin: 0;
  }
</style>