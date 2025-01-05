<script lang="ts">
  import { z } from 'zod';
  import { getContext } from 'svelte';
  import { enhance } from '$app/forms';
  import type { UserWithRolesResponse } from '$lib/client';
  import { passwordSchema } from '$lib/schemas';
  import Input from '$lib/components/forms/Input.svelte';
  import LoadingButton from '$lib/components/buttons/LoadingButton.svelte';

  let user: () => UserWithRolesResponse = getContext('user');
  let newPasswordValue = $state('');
  let usernameError: string = $state('');
  let emailError: string = $state('');
  let oldPasswordError: string = $state('');
  let newPasswordError: string = $state('');
  let confirmPasswordError: string = $state('');
  let userInfoLoading = $state(false);
  let passwordLoading = $state(false);

  function hasUserInfoUpdateErrors() {
    return usernameError.length > 0 || emailError.length > 0;
  }
  function hasPasswordValidationErrors() {
    return oldPasswordError.length > 0 || newPasswordError.length > 0 || confirmPasswordError.length > 0;
  }
</script>

<div class="stack gap-4">
  <h2 class="margin-0">{user().name}</h2>
  <form method="post" action="?/updateInfo" use:enhance={({ formElement, formData, action, cancel }) => {
    userInfoLoading = true;
    return async ({ result, update }) => {
      if (result.type === 'success') {
        await update();
      }

      if (result.type === 'failure') {
        const { errors } = result.data;
        for(const error of errors) {
          if(error.path === 'name') {
            usernameError = error.message;
          }

          if(error.path === 'newEmail') {
            emailError = error.message;
          }
        }
        await update();
      }
      userInfoLoading = false;
    };
  }}>
    <h3 class="font-size-body">Profile Information</h3>
    <Input
      type="text"
      name="name"
      id="name"
      label="Username"
      value={user().name}
      bind:error={usernameError}
      schema={z.string().min(5, 'Username must be at least 5 characters.')} />

    <Input
      type="email"
      name="newEmail"
      id="newEmail"
      label="Email"
      value={user().email}
      bind:error={emailError}
      schema={z.string().email('Invalid email address.')} />

    <div class="flex justify-end">
      {#if hasUserInfoUpdateErrors()}
        <button type="submit" disabled>Update Info</button>
      {:else}
        <LoadingButton type="submit" loading={userInfoLoading} text="Update Info" />
      {/if}
    </div>
  </form>

  <form action="?/updatePassword" method="post" use:enhance={({ formElement, formData, action, cancel }) => {
    return async ({ result, update }) => {
      passwordLoading = true;
      if (result.type === 'success') {
        const { data } = result;
        await update();
      }

      if (result.type === 'failure') {
        const { errors } = result.data;
        for(const error of errors) {
          if(error.path === 'oldPassword') {
            oldPasswordError = error.message;
          }

          if(error.path === 'newPassword') {
            newPasswordError = error.message;
          }

          if(error.path === 'confirmPassword') {
            confirmPasswordError = error.message;
          }
        }
        await update();
      }

      passwordLoading = false;
    }
  }}>
    <h3 class="font-size-body">Change Password</h3>
    <Input
      type="password"
      name="oldPassword"
      id="oldPassword"
      label="Current Password"
      bind:error={oldPasswordError} />
    <Input
      type="password"
      name="newPassword"
      id="newPassword"
      label="New Password"
      bind:error={newPasswordError}
      schema={passwordSchema} bind:value={newPasswordValue}/>
    <Input
      type="password"
      name="confirmPassword"
      id="confirmPassword"
      label="Confirm New Password"
      bind:error={confirmPasswordError}
      schema={z.literal(newPasswordValue, { errorMap: (err, ctx) => {
        switch (err.code) {
          case 'invalid_literal':
            return {message: 'Passwords do not match.'};
          default:
            return {message: 'An error occurred.'};
        }
      }})}/>

    <div class="flex justify-end">
      {#if hasPasswordValidationErrors()}
        <button type="submit" disabled>Update Password</button>
      {:else}
        <LoadingButton type="submit" loading={passwordLoading} text="Update Password" />
      {/if}
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