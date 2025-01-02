export async function load(event) {
  // console.log('load', event.getClientAddress());
  return {
    user: event.locals.user
  };
}