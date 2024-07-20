<template>
    <div class="wrapper">
        <label v-if="label" :for="name">{{ label }}</label>
        <input :id="name" :class="{ field_error: errorMessage, valid: meta.valid }" class="input" v-model="value"
            :placeholder="placeholder" :type="type || 'text'" />

        <small :class="errorMessage ? 'error_message' : ''">{{ errorMessage }}</small>
    </div>
</template>

<script setup>
import { useField } from 'vee-validate'

const props = defineProps({
    name: {
        type: String,
        required: true
    },
    type: String,
    placeholder: String,
    label: String
})

const { value, errorMessage, meta } = useField(() => props.name)
</script>

<style scoped>

</style>