<script lang="ts">
  import { z } from 'zod';
  import { getContext, onMount } from 'svelte';
  import { enhance } from '$app/forms';
  import type { UserWithRolesResponse } from '$lib/client';
  import type { ZodIssue } from 'zod';
  import Input from '$lib/components/forms/Input.svelte';
  import { passwordSchema } from '$lib/schemas';

  let user: () => UserWithRolesResponse = getContext('user');
  let infoUpdateErrors: ZodIssue[] = $state([]);
  let passwordUpdateErrors: ZodIssue[] = $state([]);

  let newPasswordValue = $state('');

  $inspect(infoUpdateErrors);

  type Inputs = {
    name: HTMLInputElement | undefined;
    newEmail: HTMLInputElement | undefined;
    oldPassword: HTMLInputElement | undefined;
    newPassword: HTMLInputElement | undefined;
    confirmPassword: HTMLInputElement | undefined;
  };

  const inputs: Inputs = {
    name: undefined,
    newEmail: undefined,
    oldPassword: undefined,
    newPassword: undefined,
    confirmPassword: undefined,
  };

  // let usernameError: string = $derived.by(() => {
  //   const error = infoUpdateErrors.find((error) => error.path[0] === 'name');
  //   return error ? error.message : '';
  // });
  //
  // let emailError: string = $derived.by(() => {
  //   const error = infoUpdateErrors.find((error) => error.path[0] === 'newEmail');
  //   return error ? error.message : '';
  // });
  //
  // let oldPasswordError: string = $derived.by(() => {
  //   const error = passwordUpdateErrors.find((error) => error.path[0] === 'oldPassword');
  //   return error ? error.message : '';
  // });
  //
  // let newPasswordError: string = $derived.by(() => {
  //   const error = passwordUpdateErrors.find((error) => error.path[0] === 'newPassword');
  //   return error ? error.message : '';
  // });
  //
  // let confirmPasswordError: string = $derived.by(() => {
  //   const error = passwordUpdateErrors.find((error) => error.path[0] === 'confirmPassword');
  //   return error ? error.message : '';
  // });
  let usernameError: string = $state('');
  let emailError: string = $state('');
  let oldPasswordError: string = $state('');
  let newPasswordError: string = $state('');
  let confirmPasswordError: string = $state('');

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
    return async ({ result, update }) => {
      infoUpdateErrors = [];
      console.log({ result });
      if (result.type === 'success') {
        const { data } = result;
        console.log({ data });
        await update();
      }

      if (result.type === 'failure') {
        console.log({result});
        const { errors } = result.data;
        infoUpdateErrors = errors;
        await update();
      }
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
        <button type="submit">Update Info</button>
      {/if}
    </div>
  </form>

  <form action="?/updatePassword" method="post" use:enhance={({ formElement, formData, action, cancel }) => {
    return async ({ result, update }) => {
      passwordUpdateErrors = [];
      console.log({ result });
      if (result.type === 'success') {
        const { data } = result;
        console.log({ data });
        await update();
      }

      if (result.type === 'failure') {
        const { errors } = result.data;
        passwordUpdateErrors = errors;
      }
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
        <button type="submit">Update Password</button>
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