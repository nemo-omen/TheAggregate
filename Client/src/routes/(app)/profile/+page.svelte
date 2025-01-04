<script lang="ts">
  import { getContext } from 'svelte';
  import { enhance } from '$app/forms';
  import type { UserWithRolesResponse } from '$lib/client';
  let user: () => UserWithRolesResponse = getContext('user');
</script>

<div class="stack gap-8">
  <h2 class="margin-0">{user().name}</h2>
  <form method="post" action="?/editInfo" use:enhance>
    <h3 class="font-size-body">Profile Information</h3>
    <fieldset>
      <label for="name">Username</label>
      <input type="text" name="name" value={user().name} />
    </fieldset>
    <fieldset>
      <label for="email">Email</label>
      <input type="email" name="newEmail" value={user().email}/>
    </fieldset>
    <div class="flex justify-end">
      <button type="submit">Save</button>
    </div>
  </form>

  <form action="?/updatePassword" method="post" use:enhance>
    <h3 class="font-size-body">Change Password</h3>
    <fieldset>
      <label for="currentPassword">Current Password</label>
      <input type="password" name="oldPassword" />
    </fieldset>
    <fieldset>
      <label for="newPassword">New Password</label>
      <input type="password" name="newPassword" />
    </fieldset>
    <fieldset>
      <label for="confirmPassword">Confirm New Password</label>
      <input type="password" name="confirmPassword" />
    </fieldset>
    <div class="flex justify-end">
      <button type="submit">Change Password</button>
    </div>
  </form>
</div>

<style>
  form {
      display: flex;
      flex-direction: column;
      gap: var(--space-4);
      width: min(var(--width-3), 90vw);
  }
</style>