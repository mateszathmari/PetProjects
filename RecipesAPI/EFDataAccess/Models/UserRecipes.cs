﻿using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class UserRecipes
    {
        [Key] public int Id { get; set; }
        [MaxLength(100)] public string UserName { get; set; }
        public User User { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public UserRecipes()
        {
            
        }

        public UserRecipes(User user, Recipe recipe)
        {

            UserName = user.UserName;
            User = user;
            RecipeId = recipe.id;
            Recipe = recipe;
        }
    }
}