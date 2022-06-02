export interface RecipeItem{
  id : number,
  name : string,
  description: string,
  urlPicture: string,
  ingredients : Ingredient[]|null,
  difficulty: number,
  duration: number,
  score: number,
  budget: number,
  recipe: string,
}

export interface Ingredient{
  name : string,
  quantity : number,
}
