<main class="container-large stack gap-6">
  <h1>Design System</h1>
  <h2>Typography</h2>
  <div class="stack gap-4">
    <h1>Heading 1</h1>
    <h2>Heading 2</h2>
    <h3>Heading 3</h3>
    <h4>Heading 4</h4>
    <p>Body text</p>
    <a href="#">Link</a>
  </div>
  <h2>Spacing</h2>
  <div class="stack gap-4">
    <div class="flex align-center gap-4">
      <code>.space-1</code>
      <div class="space-demo width-1"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-2</code>
      <div class="space-demo width-2"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-3</code>
      <div class="space-demo width-3"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-4</code>
      <div class="space-demo width-4"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-5</code>
      <div class="space-demo width-5"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-6</code>
      <div class="space-demo width-6"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-7</code>
      <div class="space-demo width-7"></div>
    </div>
    <div class="flex align-center gap-4">
      <code>.space-8</code>
      <div class="space-demo width-8"></div>
    </div>
  </div>

  <h2>Containers</h2>
  <div class="stack gap-4">
    <div class="container-demo container-small">
      <code>.container-small</code>
    </div>
    <div class="container-demo container">
      <code>.container-medium | .container</code>
    </div>
    <div class="container-demo container-large">
      <code>.container-large</code>
    </div>
  </div>

  <h2>Cards</h2>
  <div class="flex-grid" style="--min: var(--card-width);">
    <article>
      <div class="card-body">
        <code>.card</code>
      </div>
    </article>

    <article>
      <header>
        <h3>Header</h3>
      </header>
      <div class="card-body">
        <code>.card</code>
      </div>
    </article>

    <article>
      <header>
        <h3>Header and Footer</h3>
      </header>
      <div class="card-body">
        <code>.card</code>
      </div>
      <footer>
        <span>Footer Content</span>
      </footer>
    </article>
    <article>
      <img src="/desert.jpg" alt="Desert" />
      <div class="card-body">
        <p>This is some card content. It's meant to fill some space to see what things look like.</p>
      </div>
      <footer>
        <span>Footer Content</span>
      </footer>
    </article>
  </div>

  <h2>Buttons</h2>
  <div class="flex gap-4">
    <button class="button">Default Button</button>
    <button class="button button-subtle">Subtle Button</button>
    <button class="button button-primary">Primary Button</button>
    <button class="button button-warning">Warning Button</button>
    <button class="button button-danger">Error Button</button>
  </div>

  <h2>Inputs</h2>
  <form class="stack gap-4 surface-width-4 border border-1 padding-4 border-radius-2" autocomplete="off">
    <fieldset>
      <label for="text-input">Text Input</label>
      <input type="text" id="text-input" class="input" placeholder="Text Input">
    </fieldset>
    <fieldset>
      <label for="email-input">Email Input</label>
      <input type="email" name="email-input" id="email-input" class="input" placeholder="Email Input">
    </fieldset>
    <fieldset>
      <label for="password-input">Password Input</label>
      <input type="password" name="password-input" id="password-input" class="input" placeholder="Password Input">
    </fieldset>
    <fieldset>
      <label for="number-input">Number Input</label>
      <input type="number" id="number-input" class="input" placeholder="Number Input">
    </fieldset>
    <fieldset>
      <label for="date-input">Date Input</label>
      <input type="date" id="date-input" class="input" placeholder="Date Input">
    </fieldset>
    <fieldset>
      <label for="time-input">Time Input</label>
      <input type="time" id="time-input" class="input" placeholder="Time Input">
    </fieldset>
    <fieldset>
      <label for="textarea">Textarea</label>
      <textarea id="textarea" class="input" placeholder="Textarea"></textarea>
    </fieldset>

    <fieldset>
      <label for="select">Select</label>
      <select id="select" class="input">
        <option value="1">Option 1</option>
        <option value="2">Option 2</option>
        <option value="3">Option 3</option>
      </select>
    </fieldset>

    <fieldset>
      <input type="checkbox" id="checkbox" class="input">
      <label for="checkbox">Checkbox</label>
    </fieldset>

    <fieldset>
      <input type="radio" id="radio" class="input">
      <label for="radio">Radio</label>
      <input type="radio" id="radio2" class="input">
      <label for="radio2">Radio 2</label>
    </fieldset>
    <fieldset>

    <fieldset>
      <label for="file">File</label>
      <input type="file" id="file" class="input">
    </fieldset>

    <fieldset>
      <label for="range">Range</label>
      <input type="range" id="range" class="input">
    </fieldset>
  </form>
</main>

<style>
  .container-demo {
      background-color: var(--surface-color-1);
      border-radius: var(--border-radius);
      display: flex;
      align-items: center;
      padding: var(--space-4);
  }

  .space-demo {
      background-color: var(--surface-color-1);
      border-radius: var(--border-radius);
      height: 2rem;
  }
</style>