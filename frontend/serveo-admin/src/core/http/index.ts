import { setupRequestInterceptor } from './interceptors';

export function initializeHttp() {
  setupRequestInterceptor();
}
