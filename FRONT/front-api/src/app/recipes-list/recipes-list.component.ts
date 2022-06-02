import { Component, OnInit } from '@angular/core';
import { RecipeItem} from "../interface/recipe";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.scss']
})
export class RecipesListComponent implements OnInit {
  public recipes!:RecipeItem[];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getRecipes().subscribe(recipes =>{
      this.recipes = recipes;
      console.log(this.recipes);
    })
    // this.recipes = [
    //   {
    //     id : 1,
    //     name : "Gâteau au chocolat",
    //     description: "Ceci est une recette pour un super gâteau au chocolat fondant, gourmand et super pour les diabétiques",
    //     urlPicture: "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png",
    //     ingredients : null,
    //     difficulty: 1,
    //     duration: 50,
    //     score: 1,
    //     budget: 1,
    //     recipe: "string",
    //   },
    //   {
    //     id : 1,
    //     name : "test",
    //     description: "test",
    //     urlPicture: "test",
    //     ingredients : null,
    //     difficulty: 1,
    //     duration: 50,
    //     score: 1,
    //     budget: 1,
    //     recipe: "string",
    //   },
    //   {
    //     id : 1,
    //     name : "test",
    //     description: "test",
    //     urlPicture: "test",
    //     ingredients : null,
    //     difficulty: 1,
    //     duration: 50,
    //     score: 1,
    //     budget: 1,
    //     recipe: "string",
    //   }
    // ]
  }

  public getRecipes(){
    return this.http.get<RecipeItem[]>("https://localhost:5001/api/recipe");
  }
}
