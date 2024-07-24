import { createWebHistory, createRouter } from 'vue-router'
import { msalInstance, state } from '@/config/msalConfig'
import { msalService } from '@/config/useAuth'
const { login, logout, handleRedirect, registerAuthorizationHeaderInterceptor } = msalService()



import Jobs from '@/components/Jobs.vue'
import About from "@/components/About.vue"
import JobEdit from "@/components/JobEdit.vue"
import JobView from "@/components/JobView.vue"
import Login from '@/components/Login.vue'

const routes = [
    { 
        path: '/', 
        name: "Home",
        component: Jobs,
        meta: {
            requiresAuth: true
        }
    },
    { 
        path: '/login', 
        name: "Login",
        component: Login
    },
    { 
        path: '/about', 
        name: "About",
        component: About 
    },
    { 
        path: '/job/edit', 
        name: "Job Edit",
        alias: "/job/edit/:id",
        component: JobEdit,
        meta: {
            requiresAuth: true
        }
    },
    { 
        path: '/job/view/:id?', 
        name: "Job View",
        component: JobView,
        meta: {
            requiresAuth: true
        }
    },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach(async (to, from, next) => {
    await msalInstance.initialize()
    await handleRedirect()
    if (to.matched.some(record => record.meta.requiresAuth)) {
      // this route requires auth, check if logged in
      // if not, redirect to login page.


      if (!msalInstance.getAllAccounts().length > 0) {
        next({ name: 'Login' })
      } else {
        next() // go to wherever I'm going
      }
    } else {
      next() // does not require auth, make sure to always call next()!
    }
  })

export default router