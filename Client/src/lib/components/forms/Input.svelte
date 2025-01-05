<script lang="ts">
  import { onMount } from 'svelte';
  import { ZodSchema } from 'zod';

  type Props = {
    id: string;
    label: string;
    name: string;
    error?: string;
    value?: string;
    schema?: ZodSchema,
  }

  let { id, label, name, type, schema = undefined, error = $bindable(), value = $bindable() } = $props();
  let input: HTMLInputElement | undefined;

  $effect(() => {
    if(error && error.length) {
      input!.classList.add('error');
      input!.focus();
    } else {
      input!.classList.remove('error');
    }
  });

  function validate() {
    if(schema) {
      const result = schema.safeParse(input!.value);
      if(result.success) {
        error = "";
      } else {
        error = result.error.errors[0].message;
      }
    }
  }

  onMount(() => {
    if(input) {
      input.addEventListener('input', () => {
        input!.classList.remove('error');
        if(error.length) {
          error = "";
        }
      });

      input.addEventListener('blur', () => {
        if(input!.value.length) {
          validate();
        }
      });
    }
  });
</script>

<fieldset>
  <label for={id}>{label}</label>
  <input
    type={type}
    name={name}
    id={id}
    bind:value={value}
    bind:this={input}
    class:error={error}
  >
  {#if error && error.length}
    <small class="text-danger padding-0">{error}</small>
  {/if}
</fieldset>