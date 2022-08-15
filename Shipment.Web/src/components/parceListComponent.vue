<template>
  <b-table
    :items="parcels"
    :fields="fields"
  >
    <template #cell(id)="data">
      <span>{{ data.item.id }}</span>
    </template>
    <template #cell(weight)="data">
      <b-form-input
        type="text"
        v-model="parcels[data.items.indexOf(data.item)].weight"
        v-if="parcels[data.items.indexOf(data.item)].edit"
        style="width: 6em"
      ></b-form-input>
      <span
        style="width: 6em"
        v-else
      >{{ data.item.weight }}</span>
    </template>
    <template #cell(length)="data">
      <b-form-input
        type="text"
        v-model="parcels[data.items.indexOf(data.item)].length"
        v-if="parcels[data.items.indexOf(data.item)].edit"
        style="width: 6em"
      ></b-form-input>
      <span
        style="width: 6em"
        v-else
      >{{ data.item.length }}</span>
    </template>
    <template #cell(width)="data">
      <b-form-input
        type="text"
        v-model="parcels[data.items.indexOf(data.item)].width"
        v-if="parcels[data.items.indexOf(data.item)].edit"
        style="width: 6em"
      ></b-form-input>
      <span
        style="width: 6em"
        v-else
      >{{ data.item.width }}</span>
    </template>
    <template #cell(height)="data">
      <b-form-input
        type="text"
        v-model="parcels[data.items.indexOf(data.item)].height"
        v-if="parcels[data.items.indexOf(data.item)].edit"
        style="width: 6em"
      ></b-form-input>
      <span
        style="width: 6em"
        v-else
      >{{ data.item.height }}</span>
    </template>
    <template #cell(value)="data">
      <b-form-input
        type="text"
        v-model="parcels[data.items.indexOf(data.item)].value"
        v-if="parcels[data.items.indexOf(data.item)].edit"
        style="width: 6em"
      ></b-form-input>
      <span
        style="width: 6em"
        v-else
      >{{ data.item.value }}</span>
    </template>
    <template #cell(actions)="data">
      <p>
        <!-- edit  -->
        <font-awesome-icon
          icon="pen"
          @click.native="editData(data)"
          v-if="!parcels[data.items.indexOf(data.item)].edit"
        /> &nbsp;
        <!-- delete  -->
        <font-awesome-icon
          icon="trash-can"
          @click.native="askToDeleteData(data)"
          v-if="!parcels[data.items.indexOf(data.item)].edit"
        /> &nbsp;
        <!-- undo  -->
        <font-awesome-icon
          icon="xmark"
          @click.native="discardData(data)"
          v-if="parcels[data.items.indexOf(data.item)].newItem || parcels[data.items.indexOf(data.item)].edit"
        /> &nbsp;
        <!-- save  -->
        <font-awesome-icon
          icon="floppy-disk"
          @click.native="saveData(data)"
          v-if="parcels[data.items.indexOf(data.item)].edit"
        />
      </p>
    </template>
  </b-table>
  <b-modal
    v-model="modalShow"
    @ok="deleteData"
  >Do you really want to delete?</b-modal>

  <b-modal
    v-model="showErrorMessage"
    @hidden="closeErrorMessage"
  >{{errorMessage}}

  </b-modal>
</template>
<script>
export default {
  data() {
    return {
      modalShow: false,
      wantToDeleteId:null,
      fields: [
        {
          key: "id",
          label: "Id",
        },
        {
          key: "weight",
          label: "Weight",
        },
        {
          key: "length",
          label: "Length",
        },
        {
          key: "width",
          label: "Width",
        },
        {
          key: "height",
          label: "Height",
        },
        {
          key: "value",
          label: "Value",
        },
        {
          key: "actions",
          label: "Actions",
        },
      ],
    };
  },
  created() {
    this.$store.dispatch("loadParcels");
  },
  computed: {
    parcels() {
      return this.$store.getters.parcels;
    },
    showErrorMessage(){
      return this.$store.getters.showErrorMessage;
    },
    errorMessage(){
      return this.$store.getters.errorMessage;
    },

  },
  methods: {
    editData(data) {
      this.parcels[data.items.indexOf(data.item)].edit = !this.parcels[
        data.items.indexOf(data.item)
      ].edit;
      var tempData = { ...this.parcels[data.items.indexOf(data.item)] };
      this.$store.dispatch("addToParcelBag",tempData);
    },
    saveData(data) {
      if(data.item.newItem){
        this.$store.dispatch("saveData",data.item);
      }else{
        this.$store.dispatch("updateData",data.item);
      }
    },
    discardData(data) {
      var discardData={
        newItem:data.item.newItem,
        id:data.item.id,
        edit:data.item.edit
      };
      this.$store.dispatch("discardEditing",discardData);
    },
    askToDeleteData(data){
      this.wantToDeleteId = data.item.id;
      this.modalShow=true;
    },
    deleteData() {
      this.$store.dispatch("deleteData",this.wantToDeleteId);
      this.wantToDeleteId=null;
    },
    closeErrorMessage(){
       var message = {
        showErrorMessage:false,
      }
      this.$store.dispatch("triggerErrorMessage",message);

    }
  },
  watch:{
 
  }
};
</script>

<style>
#app {
  text-align: left;
  margin-top: 60px;
}
thead,
tbody,
tfoot,
tr,
td,
th {
  text-align: left;
}
.svg-inline--fa:hover {
  color: grey;
  cursor: pointer;
}
</style>
