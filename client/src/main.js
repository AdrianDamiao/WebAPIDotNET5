import Vue from 'vue';
import VueRouter from 'vue-router'
import App from './App.vue';

import Home from './components/Home';
import Login from './views/Login'

Vue.use(VueRouter);

Vue.config.productionTip = false;

const routes = [
  { path: '/', component: Home },
  { path: '/login', component: Login }
]

const router = new VueRouter({
  routes
})

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
