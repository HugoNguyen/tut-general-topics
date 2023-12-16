import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {HomeComponent as HomeV2Component} from './home.v2/home.component';
import {AboutComponent} from "./about/about.component";
import {CourseComponent} from "./course/course.component";
import {CourseComponent as CourseV2Component} from "./course.v2/course.component";

const routes: Routes = [
    {
        path: "",
        component: HomeComponent

    },
    {
        path: "about",
        component: AboutComponent
    },
    {
        path: 'courses/:id',
        component: CourseComponent
    },
    {
        path: "v2",
        component: HomeV2Component

    },
    {
        path: 'v2/courses/:id',
        component: CourseV2Component
    },
    {
        path: "**",
        redirectTo: '/'
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
