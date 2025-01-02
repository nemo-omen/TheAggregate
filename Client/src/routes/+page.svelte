<script lang="ts">
  let { data } = $props();
</script>

<main class="container">
  <div class="hero stack">
    <h1>Make Your News <br><mark>Personal</mark></h1>
    <p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nunc sem, dictum vel elit sed, efficitur tristique turpis. Nunc a gravida metus.
    </p>
    <div class="container-small flex justify-center align-center gap-4">
      <a href="/auth/register" class="button button-primary">Get Started</a>
      <a href="/features" class="button button-subtle">Learn More</a>
    </div>
  </div>

<!--  <RegisterModal bind:this={registerModal}>-->
<!--    <RegisterForm {form} />-->
<!--    <div class="stack gap-4 align-center margin-bottom-4">-->
<!--      <small class="text-center">Already have an account?</small>-->
<!--      <button class="button button-subtle" onclick={() => {-->
<!--        registerModalState.toggle();-->
<!--        loginModalState.toggle();-->
<!--      }}>Log In</button>-->
<!--    </div>-->
<!--  </RegisterModal>-->
<!--  <LoginModal bind:this={loginModal}>-->
<!--    <LoginForm {form} />-->
<!--    <div class="stack gap-4 align-center margin-bottom-4">-->
<!--      <small class="text-center">Don't have an account?</small>-->
<!--      <button class="button button-subtle" onclick={() => {-->
<!--        loginModalState.toggle();-->
<!--        registerModalState.toggle();-->
<!--      }}>Create an Account</button>-->
<!--    </div>-->
<!--  </LoginModal>-->
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