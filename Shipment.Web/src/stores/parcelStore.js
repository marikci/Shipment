export const parcelStore = {
  state: () => ({
    parcels: [],
    newParcelIds: [0],
    showErrorMessage: false,
    errorMessage: null
  }),
  getters: {
    parcels: state => state.parcels,
    showErrorMessage: state => state.showErrorMessage,
    errorMessage: state => state.errorMessage,

  },
  mutations: {
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
    DISCARD_EDITING(state, id) {
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
    TRIGGER_ERROR_MESSAGE(state, data) {
      state.showErrorMessage = data.showErrorMessage;
      state.errorMessage = data.errorMessage;
    }
  },
  actions: {
    loadParcels({ commit }) {
      var parcels = [{
        id: 1,
        weight: 12,
        length: 13,
        width: 14,
        height: 15,
        value: 16
      },
      {
        id: 2,
        weight: 22,
        length: 23,
        width: 24,
        height: 25,
        value: 26
      }
      ];
      commit('SET_PARCELS', parcels);
    },
    addNewRow({ commit }) {
      commit('ADD_NEW_ROW');
    },
    deleteData({ commit }, id) {
      commit('REMOVE_PARCEL', id);
    },
    discardEditing({ commit }, id) {
      commit('DISCARD_EDITING', id);
    },
    saveData({ commit, dispatch }, parcel) {
      var data = {
        oldId: parcel.id,
        data: parcel
      };

      //if success 
      data.data.edit = false;
      data.data.newItem = false;
      commit('SAVE_PARCEL', data);

      //if not success show error
      var message = {
        showErrorMessage: true,
        errorMessage: "There is an issue on saving data!"
      };
      dispatch('triggerErrorMessage', message);
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
