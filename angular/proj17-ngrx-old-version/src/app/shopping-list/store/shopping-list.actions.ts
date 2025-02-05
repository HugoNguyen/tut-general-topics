import { createAction, props } from '@ngrx/store';

import { Ingredient } from 'src/app/shared/ingredient.model';

export const AddIngredient = createAction(
  '[Shopping List] Add Ingredient',
  props<{ ingredient: Ingredient }>()
);

export const AddIngredients = createAction(
  '[Shopping List] Add Ingredients',
  props<{ ingredients: Ingredient[] }>()
);

export const UpdateIngredient = createAction(
  '[Shopping List] Update Ingredient',
  props<{ ingredient: Ingredient }>()
);

export const DeleteIngredient = createAction(
  '[Shopping List] Delete Ingredient'
);

export const StartEdit = createAction(
  '[Shopping List] Start Edit',
  props<{ index: number }>()
);

export const StopEdit = createAction('[Shopping List] Stop Edit');
