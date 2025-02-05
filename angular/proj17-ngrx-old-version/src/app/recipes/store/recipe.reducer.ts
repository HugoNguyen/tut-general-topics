import { createReducer, on } from '@ngrx/store';

import { Recipe } from '../recipe.model';
import * as RecipesActions from './recipe.actions';

export interface State {
  recipes: Recipe[];
}

const initialState: State = {
  recipes: [],
};

export const recipeReducer = createReducer(
  initialState,
  on(RecipesActions.setRecipes, (state, action) => {
    return {
      ...state,
      recipes: [...action.recipes],
    };
  }),
  on(RecipesActions.addRecipe, (state, action) => {
    return {
      ...state,
      recipes: [...state.recipes, action.recipe],
    };
  }),
  on(RecipesActions.updateRecipe, (state, action) => {
    const updatedRecipe = {
      ...state.recipes[action.index],
      ...action.newRecipe,
    };

    const updatedRecipes = [...state.recipes];
    updatedRecipes[action.index] = updatedRecipe;

    return {
      ...state,
      recipes: updatedRecipes,
    };
  }),
  on(RecipesActions.deleteRecipe, (state, action) => {
    return {
      ...state,
      recipes: state.recipes.filter((recipe, index) => {
        return index !== action.index;
      }),
    };
  })
);
