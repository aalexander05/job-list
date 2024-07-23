<template>
    <v-container class="mt-15">
        <v-row>
            <v-col class="d-flex justify-center">
                <div v-if="state.isAuthenticated">
                    <v-btn :text="'Log Out'" variant="outlined" @click="handleLogout" class="ma-2 pa-2"></v-btn>
                    <v-btn :to="'/'" variant="outlined" class="ma-2 pa-2">Home</v-btn>
                </div>

                <span v-else>
                    <v-btn :text="'Log In'" variant="outlined" @click="handleLogin"></v-btn>
                </span>
                
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup>
import { msalService } from '@/config/useAuth'
import { msalInstance, state } from '@/config/msalConfig'
import { useRouter } from 'vue-router'
const router = useRouter()
const { login, logout } = msalService()




async function handleLogin() {
    await login()
    router.push('/')
}

function handleLogout() {
    logout()
}
</script>