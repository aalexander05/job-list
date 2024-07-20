import { createWebHistory, createRouter } from 'vue-router'


import Jobs from '@/components/Jobs.vue'
import About from "@/components/About.vue"
import JobEdit from "@/components/JobEdit.vue"
import JobView from "@/components/JobView.vue"

const routes = [
    { 
        path: '/', 
        name: "Home",
        component: Jobs 
    },
    { 
        path: '/about', 
        name: "About",
        component: About 
    },
    { 
        path: '/job/edit/:id?', 
        name: "Job Edit",
        alias: "/job/edit/:id",
        component: JobEdit 
    },
    { 
        path: '/job/view/:id?', 
        name: "Job View",
        component: JobView 
    },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router