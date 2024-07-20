<template>
    <v-app-bar flat>
        <v-container class="mx-auto d-flex align-center justify-center">
            <v-avatar class="me-4 " color="grey-darken-1" size="32"></v-avatar>

            <v-btn :text="'Jobs'" :to="'/'"></v-btn>
            <v-btn :text="'About'" :to="'/about'"></v-btn>
            <v-btn :text="'Create New'" :to="'/job/edit'"></v-btn>

            <v-spacer></v-spacer>

            <v-responsive max-width="160">
                <v-text-field density="compact" label="Search" rounded="lg" variant="solo-filled" flat hide-details
                    single-line></v-text-field>
            </v-responsive>
        </v-container>
    </v-app-bar>
</template>

<script setup>

</script>
