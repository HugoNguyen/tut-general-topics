import { compareCourses, Course } from '../model/course';
import { createEntityAdapter, EntityState } from '@ngrx/entity';
import { createReducer, on } from '@ngrx/store';
import { CourseActions } from '../action-types';

/**
 * ids: string[] | number[];
 * entities: Dictionary<Course>;
 * allCoursesLoaded: boolean
 */
export interface CoursesState extends EntityState<Course> {
    allCoursesLoaded: boolean
}


export const adapter = createEntityAdapter<Course>({
    sortComparer: compareCourses,
    // selectId: course => course.id, // can be obmited because of course entity has indentifier is id
});

export const initialCoursesState = adapter.getInitialState({
    allCoursesLoaded: false
});


export const coursesReducer = createReducer(

    initialCoursesState,

    on(
        CourseActions.allCoursesLoaded,
        (state, action) =>
            adapter.setAll(action.courses, {
                ...state,
                allCoursesLoaded: true
            })
    ),


    on(CourseActions.courseUpdated, (state, action) =>
        adapter.updateOne(action.update, state))

);


export const {
    selectAll
} = adapter.getSelectors();

