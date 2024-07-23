<template>
    <v-app-bar flat>
        <v-container class="mx-auto d-flex align-center justify-center">
            <v-avatar class="me-4 " color="grey-darken-1" size="32"></v-avatar>

            <v-btn :text="'Jobs'" :to="'/'"></v-btn>
            <v-btn :text="'About'" :to="'/about'"></v-btn>
            <v-btn :text="'Create New'" :to="'/job/edit'"></v-btn>

            <v-spacer></v-spacer>
            <span v-if="state.isAuthenticated" class="mx-5">
                <span>Hello, {{ state.user?.name }}! </span>
            </span>

            <span v-if="state.isAuthenticated">
                <v-btn :text="'Log Out'" variant="outlined" @click="handleLogout"></v-btn>
            </span>

            <span v-else>
                <v-btn :text="'Log In'"  variant="outlined" @click="handleLogin"></v-btn>
            </span>

            <!-- <v-responsive max-width="160">

                <v-text-field density="compact" label="Search" rounded="lg" variant="solo-filled" flat hide-details
                    single-line></v-text-field>
            </v-responsive> -->
        </v-container>
    </v-app-bar>
</template>

<script setup>
import axios from 'axios'
import { onMounted, ref } from 'vue'
import { msalService } from '@/config/useAuth'
import { msalInstance, state } from '@/config/msalConfig'
const { login, logout, handleRedirect, registerAuthorizationHeaderInterceptor } = msalService()


async function handleLogin() {
    await login()
}

function handleLogout() {
    logout()
}

async function initialize() {
    try {
        await msalInstance.initialize()
        registerAuthorizationHeaderInterceptor() // Call the initialize function
    } catch (error) {
        console.log('Initialization error', error)
    }
}

onMounted(async () => {
    await initialize()
    await handleRedirect()
})

</script>
