<template>
    <v-main class="bg-grey-lighten-3">
        <v-container>
            <v-sheet min-height="10vh" rounded="lg">
                <v-container>
                    
                    <v-form class="px-4">
                        <v-row>
                            <v-col lg='6' xl="4" sm="12">
                                <v-text-field v-model="v$.name.$model" label="Name" 
                                :error-messages="v$.name.$errors.map(e => e.$message)"/>

                                <v-text-field v-model="v$.description.$model" label="Description" 
                                :error-messages="v$.description.$errors.map(e => e.$message)"/>
                                
                                <v-select
                                v-model="v$.status.$model"
                                :items="statuses"
                                item-title="status"
                                item-value="value"
                                label="Status"
                                :error-messages="v$.status.$errors.map(e => e.$message)">
                                </v-select>

                            </v-col>
                            <v-col cols="12">
                                <v-textarea label="Notes" v-model="v$.notes.$model"></v-textarea>

                            </v-col>
                        </v-row>
                        

                        <div class="button-wrapper">
                            <v-btn class="btn btn-success" @click="onSubmit">submit</v-btn>
                            <v-btn @click="reset" class="btn btn-danger" type="button">Reset</v-btn>
                        </div>
                    </v-form>
                    <pre>{{ submitError }}</pre>
                </v-container>
            </v-sheet>
        </v-container>
    </v-main>
</template>

<script setup>
import axios from 'axios'
import { reactive, ref } from 'vue' // "from '@vue/composition-api'" if you are using Vue <2.7
import { useVuelidate } from '@vuelidate/core'
import { required, email, helpers } from '@vuelidate/validators'
import { useRouter } from 'vue-router'
import { statuses } from '@/models/job-statuses'

const router = useRouter()

let currentPathObject = router.currentRoute.value

let state
if (router.currentRoute.value.params.id) {
    const job = await axios.get(`https://localhost:7178/Job/${router.currentRoute.value.params.id}`)
    state = reactive(job.data);
} else {
    state = reactive({
        id: 0,
        name: '',
        description: '',
        status: null,
        notes: '',
    })
}

const rules = {
    id: { required }, 
    name: { required: helpers.withMessage('This field cannot be empty', required) }, 
    description: { required }, 
    status: { required }, 
    notes: {}
}

const v$ = useVuelidate(rules, state)

const submitError = ref(null)

async function onSubmit() {
    await v$.value.$validate()
    if (v$.value.$invalid) {
        return
    }
    if (!state.id) {
        try {
            const response = await axios.post("https://localhost:7178/Job", state);
        } catch (error) {
            console.error(error)
            if (error.response.status === 500) {
                submitError.value = "There was a server error"
            }
            return
        }
    } else {
        const response = await axios.put("https://localhost:7178/Job", state);
    }
    router.push('/')
}

function reset() {
    state = reactive({
        id: 5,
        name: '5',
        description: '5',
        status: 2,
    })
    console.log(state)
}

</script>