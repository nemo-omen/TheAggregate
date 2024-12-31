<script lang="ts">
  let { form } = $props();
</script>

<form action="?/auth/register" class="stack gap-8 padding-4 padding-top-0" autocomplete="off">
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