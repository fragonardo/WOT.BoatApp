/// <reference types="@angular/localize" />

// src/main.ts

fetch('/assets/config.json')
  .then(response => response.json())
  .then(config => {
    (window as any)['env'] = config;
    bootstrapApp();
  })
  .catch((error) => {
    console.error('Erreur lors du chargement de config.json', error);
    bootstrapApp(); // fallback sans config si nécessaire
  });

function bootstrapApp() {
  import('./bootstrap').then(module => module.bootstrapApp());
}
