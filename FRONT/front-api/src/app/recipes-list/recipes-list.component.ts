import { Component, OnInit } from '@angular/core';
import {RecipeItem} from "../interface/recipe";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-recipes-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.scss']
})
export class RecipesListComponent implements OnInit {
  public recipes?:RecipeItem[];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getRecipes().subscribe(recipes =>{
      this.recipes = recipes;
    })
  }

  public getRecipes(){
    return this.http.get<RecipeItem[]>("https://localhost:5001/api/recipe");
  }
}
