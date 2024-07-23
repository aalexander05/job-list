import {
    PublicClientApplication,
    type AccountInfo,
    type RedirectRequest
  } from '@azure/msal-browser'
  import { reactive } from 'vue'
  
  export const msalConfig = {
    auth: {
      clientId: 'a8175144-8e9c-4e03-af9e-3000cee05814',
      authority: 'https://login.microsoftonline.com/f5b9491d-79c7-44ef-8f04-d4062c7a780a',
      redirectUri: 'http://localhost:3000', // Replace with your actual redirect URI
      postLogoutUri: 'http://localhost:3000'
    },
    cache: {
      cacheLocation: 'localStorage', // This configures where your cache will be stored
      storeAuthStateInCookie: false
    }
  }
  export const graphScopes: RedirectRequest = {
    scopes: ['user.read', 'openid', 'profile', 'myapi.read']
  }
  export const state = reactive({
    isAuthenticated: false,
    user: null as AccountInfo | null
  })
  
  export const msalInstance = new PublicClientApplication(msalConfig)
  