<template>
  <div>
    <b-container class="bv-example-row">
      <b-row>
        <b-col cols="3"><b>Parcels</b> &nbsp; <AddNewParcel></AddNewParcel>
        </b-col>
        <b-col></b-col>
        <b-col></b-col>
        <b-col></b-col>
      </b-row>

      <b-row class="text-center">
        <b-col cols="12">
          <ParcelsList></ParcelsList>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<script>
import ParcelsList from "./components/parceListComponent.vue";
import AddNewParcel from "./components/addNewComponent.vue";
export default {
  components: {
    ParcelsList,
    AddNewParcel,
  },
};
</script>
<style></style>
