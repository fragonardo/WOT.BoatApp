export const environment = {
  production: false,
  get apiBaseUrl() {
    return (window as any)['env']?.API_BASE_URL || 'http://localhost:5000';
  }
};
