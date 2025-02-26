import axios from 'axios'
import { msalInstance, state } from './msalConfig'

export function msalService() {
  const initialize = async () => {
    try {
      await msalInstance.initialize() // Call the initialize function
    } catch (error) {
      console.log('Initialization error', error)
    }
  }

  const login = async () => {
    try {
      // Check if MSAL is initialized before using it
      if (!msalInstance) {
        throw new Error('MSAL not initialized. Call initializeMsal() before using MSAL API.')
      }
      await msalInstance.loginRedirect()
      state.isAuthenticated = true
    } catch (error) {
      console.error('Login error:', error)
    }
  }

  const logout = () => {
    if (!msalInstance) {
      throw new Error('MSAL not initialized. Call initializeMsal() before using MSAL API.')
    }
    msalInstance.logoutRedirect()
    state.isAuthenticated = false
    state.user = null
  }

  const handleRedirect = async () => {
    try {
      await msalInstance.handleRedirectPromise()
      state.isAuthenticated = msalInstance.getAllAccounts().length > 0
      state.user = msalInstance.getAllAccounts()[0]
    } catch (error) {
      console.error('Redirect error:', error)
    }
  }
  const getToken = async () => {
    if (!msalInstance) {
      throw new Error('MSAL not initialized. Call initializeMsal() before using MSAL API.')
    }
    try {
      const accounts = msalInstance.getAllAccounts()
      if (accounts.length === 0) {
        throw new Error('No accounts found. Please login first.')
      }
      const silentRequest = {
        // removed "api://" to get both swagger and vue to work
        scopes: ['a8175144-8e9c-4e03-af9e-3000cee05814/myapi.read'],
        account: accounts[0]
      }
      const silentResponse = await msalInstance.acquireTokenSilent(silentRequest)
      return silentResponse.accessToken
    } catch (error) {
      console.error('Silent token acquisition error:', error)
    }
  }
  const registerAuthorizationHeaderInterceptor = () => {
    axios.interceptors.request.use(async (config) => {
      const accessToken = await getToken()
      if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken}`
      }
      return config
    })
  }
  return {
    initialize,
    login,
    logout,
    handleRedirect,
    getToken,
    registerAuthorizationHeaderInterceptor
  }
}
