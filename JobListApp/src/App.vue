<template>
  <v-app>
    <v-main>
      <AppMain />
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
  //
</script>

<style>

.btn {
    padding: 0px 20px;
    background-color: gray;
    color: white;
}

.button-wrapper {
    display: flex;
    gap: 10px;
}

.btn-success {
    background-color: rgb(64, 181, 64);
}

.btn-danger {
    background-color: rgb(186, 17, 17);
}


.wrapper {
    display: flex;
    flex-direction: column;
    margin-bottom: 10px;
    gap: 3px;
}

.error_message {
    color: red;
}

.field_error {
    border: 1px solid red !important;
}

.input {
    width: 100%;
    height: 30px;
    border-radius: 5px;
    padding: 10px;
    border: 0.5px solid gray;
}

.input:focus {
    outline: none;
}

.valid {
    border: 1px solid green !important;
}

label {
    font-size: 14px;
    font-weight: 600;
}

</style>