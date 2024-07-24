<template>
  <v-main class="bg-grey-lighten-3">
    <v-container>
      <v-row>
        <v-col>
          <v-sheet min-height="70vh" rounded="lg">
            <v-container>
              <v-data-table :items="jobs" :headers="headers">

                <template v-slot:item.createdDate="{ item }">
                  <span>{{ new Date(item.createdDate).toLocaleString() }}</span>
                </template>
                <template v-slot:item.updatedDate="{ item }">
                  <span>{{ new Date(item.updatedDate).toLocaleString() }}</span>
                </template>
                <template v-slot:item.actions="{ item }">
                  <router-link :to="`/job/view/${item.id}`">
                    <v-icon class="me-2" size="small" >
                      mdi-eye
                    </v-icon>
                  </router-link>
                  <router-link :to="`/job/edit/${item.id}`">
                    <v-icon class="me-2" size="small" >
                      mdi-pencil
                    </v-icon>
                  </router-link>
                  <v-icon size="small" @click="deleteItem(item)">
                    mdi-delete
                  </v-icon>
                </template>
              </v-data-table>
            </v-container>
          </v-sheet>
        </v-col>
      </v-row>
    </v-container>
  </v-main>

  <v-dialog v-model="showDeleteDialog" max-width="500px">
    <v-card>
      <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue-darken-1" variant="text" @click="closeDeleteDialog">Cancel</v-btn>
        <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const jobs = ref([])


onMounted(async () => {
  await getJobs()
})

async function getJobs() {
  const response = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/Job`)
  jobs.value = response.data
}

const headers = [
  { title: 'Name', key: 'name' },
  { title: 'Description', key: 'description' },
  { title: 'Status', key: 'status' },
  { title: 'Created Date', key: 'createdDate' },
  { title: 'UpdatedDate', key: 'updatedDate' },
  { title: 'Actions', key: 'actions' },
];

let showDeleteDialog = ref(false)
let jobToDelete;

function deleteItem(item) {
  jobToDelete = item
  showDeleteDialog.value = true

}


function closeDeleteDialog() {
  showDeleteDialog.value = false
}


async function deleteItemConfirm() {
  const result = await axios.delete(`${import.meta.env.VITE_API_BASE_URL}/Job/${jobToDelete.id}`);
  await getJobs()
  closeDeleteDialog()
}


</script>