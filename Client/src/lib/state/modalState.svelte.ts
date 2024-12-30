export type ModalState = {
  isOpen: boolean;
  open: () => void;
  close: () => void;
  toggle: () => void;
};

export class RegisterModalState {
  isOpen = $state(false);

  open() {
    this.isOpen = true;
  }

  close() {
    this.isOpen = false;
  }

  toggle() {
    this.isOpen = !this.isOpen;
  }
}

export class LoginModalState {
  isOpen = $state(false);

  open() {
    this.isOpen = true;
  }

  close() {
    this.isOpen = false;
  }

  toggle() {
    this.isOpen = !this.isOpen;
  }
}