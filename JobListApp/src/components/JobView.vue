<template>
    <v-main class="bg-grey-lighten-3">
        <v-container>
            <v-sheet min-height="10vh" rounded="lg">
                <v-container>
                    <v-row>
                        <v-col cols="auto" lg="6" sm="12">
                            <v-table v-if="job">
                                <tbody>
                                    <tr>
                                        <th>Name:</th>
                                        <td>{{ job.name }}</td>
                                    </tr>
                                    <tr>
                                        <th>Description:</th>
                                        <td>{{ job.description }}</td>
                                    </tr>
                                    <tr>
                                        <th>Status:</th>
                                        <td>{{ job.status }}</td>
                                    </tr>
                                    <tr>
                                        <th>Created Date:</th>
                                        <td>{{ new Date(job.createdDate).toLocaleString() }}</td>
                                    </tr>
                                    <tr>
                                        <th>Updated Date:</th>
                                        <td>{{ new Date(job.updatedDate).toLocaleString() }}</td>
                                    </tr>

                                </tbody>
                            </v-table>

                            <strong>Notes</strong>
                            <p>{{ job.notes || "(empty)" }}</p>

                        </v-col>
                    </v-row>
                </v-container>
            </v-sheet>
        </v-container>
    </v-main>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { ref } from 'vue'
import axios from 'axios'
import { Job } from '@/models/job';

const router = useRouter()

const job: any = ref<Job>()
if (router.currentRoute.value.params.id) {
    const response = await axios.get(`https://localhost:7178/Job/View/${router.currentRoute.value.params.id}`)
    const jobDto: Job = response.data

    job.value = jobDto;
}

</script>
