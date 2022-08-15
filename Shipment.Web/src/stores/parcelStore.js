import axiosService from "../services/axiosService";
export const parcelStore = {
  state: () => ({
    parcels: [],
    newParcelIds: [0],
    showErrorMessage: false,
    errorMessage: null,
    editParcelBag: []
  }),
  getters: {
    parcels: state => state.parcels,
    showErrorMessage: state => state.showErrorMessage,
    errorMessage: state => state.errorMessage,
    editParcelBag: state => state.editParcelBag,

  },
  mutations: {
    ADD_TO_PARCEL_BAG(state, parcel) {
      state.editParcelBag.push(parcel);
    },
    REMOVE_FROM_PARCEL_BAG(state, id) {
      state.editParcelBag = state.editParcelBag.filter(parcel => parcel.id !== id);
    },
    DISCARD_EDITING(state, data) {
      if (data.newItem) {
        state.parcels = state.parcels.filter(parcel => parcel.id !== data.id);
      }
      else if (data.edit) {
        var index = state.parcels.findIndex(parcel => {
          return parcel.id == data.id
        });
        var indexOnBag = state.editParcelBag.findIndex(parcel => {
          return parcel.id == data.id
        });
        state.parcels[index] = state.editParcelBag[indexOnBag];
        state.parcels[index].edit = false;
        state.editParcelBag = state.editParcelBag.filter(parcel => parcel.id !== data.id);
      }
    },
    SET_PARCELS(state, parcels) {
      state.parcels = parcels;
    },
    ADD_NEW_ROW(state) {
      var newId = Math.min.apply(Math, state.newParcelIds) - 1;
      state.newParcelIds.push(newId);
      state.parcels.splice(0, 0, {
        id: newId,
        weight: null,
        length: null,
        width: null,
        height: null,
        value: null,
        edit: true,
        newItem: true
      });
    },
    REMOVE_PARCEL(state, id) {
      state.parcels = state.parcels.filter(parcel => parcel.id !== id);
    },

    SAVE_PARCEL(state, data) {
      var oldId = data.oldId;
      var newParcel = data.data;
      var index = state.parcels.findIndex(parcel => {
        return parcel.id == oldId
      });
      state.parcels[index] = newParcel;
    },
    DISCARD_EDIT_MODE(state, id) {
      var index = state.parcels.findIndex(parcel => {
        return parcel.id == id
      });
      state.parcels[index].edit = false;
    },
    TRIGGER_ERROR_MESSAGE(state, data) {
      state.showErrorMessage = data.showErrorMessage;
      state.errorMessage = data.errorMessage;
    }
  },
  actions: {
    addToParcelBag({ commit }, parcel) {
      commit('ADD_TO_PARCEL_BAG', parcel);
    },
    removeFromParcelBag({ commit }, id) {
      commit('REMOVE_FROM_PARCEL_BAG', id);
    },
    loadParcels({ commit }) {
      console.log(import.meta.env.VITE_BASE_URL);
      axiosService.get("api/parcel").then(r => {
        console.log(r.data);
        commit('SET_PARCELS', r.data);
      }).catch((err) => {
        console.error(err);
      });
    },
    addNewRow({ commit }) {
      commit('ADD_NEW_ROW');
    },
    deleteData({ commit, dispatch }, id) {
      axiosService.delete("api/parcel/" + id).then(r => {
        commit('REMOVE_PARCEL', id);
      }).catch((err) => {
        console.error(err);
        var message = {
          showErrorMessage: true,
          errorMessage: "There is an issue on saving data!"
        };
        dispatch('triggerErrorMessage', message);
      });
    },
    discardEditing({ commit }, discardData) {
      commit('DISCARD_EDITING', discardData);
    },
    saveData({ commit, dispatch }, parcel) {
      var data = {
        oldId: parcel.id,
        data: parcel
      };

      axiosService.post("api/parcel", parcel).then(r => {
        data.data = r.data;
        commit('SAVE_PARCEL', data);
      }).catch((err) => {
        console.error(err);
        var message = {
          showErrorMessage: true,
          errorMessage: "There is an issue on saving data!"
        };
        dispatch('triggerErrorMessage', message);
      });
    },
    updateData({ commit, dispatch }, parcel) {

      axiosService.put("api/parcel", parcel).then(r => {
        commit('DISCARD_EDIT_MODE', parcel.id);
      }).catch((err) => {
        console.error(err);
        var message = {
          showErrorMessage: true,
          errorMessage: "There is an issue on updating data!"
        };
        dispatch('triggerErrorMessage', message);
      });
    },
    triggerErrorMessage({ commit }, data) {
      var message = {
        showErrorMessage: data.showErrorMessage,
        errorMessage: data.errorMessage
      }
      commit('TRIGGER_ERROR_MESSAGE', message);
    }
  }
};
