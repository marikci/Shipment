<template>
  <b-button variant="primary" @click.native="AddParcel">+ Add New Parcels</b-button>
</template>

<script>
export default {
  data() {
    return {};
  },
  methods: {
    AddParcel() {
      this.$store.dispatch("addNewRow");
    },
  },
};
</script>
