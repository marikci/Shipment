import { createStore } from "vuex";
import { parcelStore } from "./parcelStore";

export default createStore({
  modules: {
    parcelStore
  }
});
