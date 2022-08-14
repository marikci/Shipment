import App from './App.vue'
import { createApp } from 'vue'
import BootstrapVue3 from 'bootstrap-vue-3'
import store from './stores/index';
import { library } from "@fortawesome/fontawesome-svg-core";
import { faFloppyDisk, faPen, faTrashCan, faXmark } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css'
import './assets/main.css'

library.add(faPen, faTrashCan, faXmark, faFloppyDisk);

const app = createApp(App)
app.component("font-awesome-icon", FontAwesomeIcon)
app.use(BootstrapVue3)
app.use(store)
app.mount('#app')

