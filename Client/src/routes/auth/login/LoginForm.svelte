<script lang="ts">
  let { form } = $props();
</script>

<form action="?/auth/login" class="stack gap-8 padding-4 padding-top-0 order-2">
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