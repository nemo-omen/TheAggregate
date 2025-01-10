<script lang="ts">

</script>

<aside class="sidebar">

</aside>

<style>
    .sidebar {
        height: 100%;
        width: min(18rem, 100vw);
        background-color: var(--background-1);
        padding: 1rem;
        border-radius: var(--border-radius);
        border: 1px solid var(--border-color-5);

        @media (max-width: 1024px) {
            position: absolute;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            z-index: 100;
            background-color: var(--background-1);
            border-radius: 0;
            transform: translateX(-100%);
        }
    }
</style>