using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeSystem : MonoBehaviour
{
    //Use Enum Switch Statement
    public enum RecipeType { Regular, Espresso, Capucino, DarkRoast, FrenchVanilla  }
    private RecipeType recipe;
    private StringBuilder sb = new StringBuilder();
    public enum IngredientType { Water, Coffee, Milk, Sugar, Cream, Vanilla, EspressoShot }
    private int[][] recipeIngredients = new int[7][];
    private IngredientType[] ingredients = new IngredientType[7];
    [SerializeField] private List<int> ingredientBuild = new List<int>();
    [SerializeField] private List<int> selectedIngredients = new List<int>();
    [SerializeField] private Text startText;
    [SerializeField] private Text recipeText;
    [SerializeField] private Text ingredientText;
    [SerializeField] private Text statusText;
    private string[] ingredientNames = new string[7]
    {
        "Water",
        "Coffee",
        "Milk",
        "Sugar",
        "Cream",
        "Vanilla",
        "EspressoShot"
    };
    private string[] recipeNames = new string[5]
    {
        "Regular",
        "Espresso",
        "Capucino",
        "DarkRoast",
        "FrenchVanilla"
    };
    private string startMessage = "Coffee Game\nPress F1-F5 to select Recipe\nPress 0-6 to select ingredient\nPress D to Dump Ingredients\nPress C to pour Coffee";
    void Start()
    {
        BuildIngredients();
    }
    private void Update()
    {
        //for testing purposes.
        if (Input.GetKeyDown(KeyCode.F1)) SetRecipe(0);
        if (Input.GetKeyDown(KeyCode.F2)) SetRecipe(1);
        if (Input.GetKeyDown(KeyCode.F3)) SetRecipe(2);
        if (Input.GetKeyDown(KeyCode.F4)) SetRecipe(3);
        if (Input.GetKeyDown(KeyCode.F5)) SetRecipe(4);

        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) AddIngredient(0);
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) AddIngredient(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) AddIngredient(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) AddIngredient(3);
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) AddIngredient(4);
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) AddIngredient(5);
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) AddIngredient(6);

        if (Input.GetKeyDown(KeyCode.Alpha0) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad0) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(0);
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad1) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad2) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad3) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(3);
        if (Input.GetKeyDown(KeyCode.Alpha4) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad4) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(4);
        if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad5) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(5);
        if (Input.GetKeyDown(KeyCode.Alpha6) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad6) && Input.GetKeyDown(KeyCode.Space)) RemoveIngredient(6);

        if (Input.GetKeyDown(KeyCode.C)) 
        {
            if (selectedIngredients.Count < 1) return;
            if (CheckIngredients(recipe))
            {
                statusText.color = Color.cyan;
                statusText.text = "That's some good Coffee!";
                selectedIngredients.Clear();
                ingredientBuild.Clear();
                ingredientText.text = null;
            }
            else
            {
                statusText.color = Color.red;
                statusText.text = "Ew Yuck! Dumping coffee...";
                selectedIngredients.Clear();
                ingredientBuild.Clear();
                ingredientText.text = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ClearIngredients();
        }
    }
    private void BuildIngredients()
    {
        startText.text = startMessage;
      
        recipeIngredients = new int[5][]
        {
            new int[4] { 0, 1, 2, 3 },
            new int[5] { 1, 2, 3, 4, 5 },
            new int[5] { 1, 2, 3, 4, 6 },
            new int[2] { 0, 1 },
            new int[6] { 0, 1, 2, 3, 4, 5 }
        };

        for(int i = 0; i < ingredients.Length; i++)
        {
            switch (i)
            {
                case 0: ingredients[i] = IngredientType.Water; break;
                case 1: ingredients[i] = IngredientType.Coffee; break;
                case 2: ingredients[i] = IngredientType.Milk; break;
                case 3: ingredients[i] = IngredientType.Sugar; break;
                case 4: ingredients[i] = IngredientType.Cream; break;
                case 5: ingredients[i] = IngredientType.Vanilla; break;
                case 6: ingredients[i] = IngredientType.EspressoShot; break;
            }
        }
        SetRecipe(0);
    }
    public void SetRecipe(int id)
    {
        if (sb.Length > 0) sb.Clear();
        switch (id)
        {
            case 0: recipe = RecipeType.Regular; break;
            case 1: recipe = RecipeType.Espresso; break;
            case 2: recipe = RecipeType.Capucino; break;
            case 3: recipe = RecipeType.DarkRoast; break;
            case 4: recipe = RecipeType.FrenchVanilla; break;
        }
        for(int i = 0; i < recipeIngredients[id].Length; i++)
        {
            string ingredientName = "";
            switch (recipeIngredients[id][i])
            {
                case 0: ingredientName = "Water"; break;
                case 1: ingredientName = "Coffee"; break;
                case 2: ingredientName = "Milk"; break;
                case 3: ingredientName = "Sugar"; break;
                case 4: ingredientName = "Cream"; break;
                case 5: ingredientName = "Vanilla"; break;
                case 6: ingredientName = "EspressoShot"; break;
            }
            sb.Append(" [" + ingredientName + "]" );
        }
        recipeText.text = "Recipe [" + recipeNames[id] + "] Selected." + "  Needed Ingredients: " + sb.ToString();
    }
    public void AddIngredient(int id)
    {
        if (id > 6 || id < 0) { statusText.color = Color.red; statusText.text = "Ingredient ID not valid."; return; } //id must be within ingredient enum range.
        if (selectedIngredients.Contains(id)) { statusText.color = Color.yellow; statusText.text = "Ingredient already added!"; return; }
        selectedIngredients.Add(id);
        ingredientText.color = Color.green; ingredientText.text = "Ingredient [" + ingredientNames[id] +  "] added!"; //add ingredient to tthe selectedIngredient list.
        statusText.text = null;
    }
    public void RemoveIngredient(int id)
    {
        if (id > 6 || id < 0) { statusText.color = Color.red; statusText.text = "Ingredient ID not valid."; return; } //id must be within ingredient enum range.
        if (selectedIngredients.Contains(id)) { selectedIngredients.Remove(id); ingredientText.color = Color.red; ingredientText.text = "Ingredient [" + ingredientNames[id] + "] removed!"; statusText.text = null; }
        else { statusText.color = Color.yellow; statusText.text = "Ingredient was not added!"; return; }
    }
    public bool CheckIngredients(RecipeType recipe)
    {
        //Create an ID number
        int id = 0;
        //Set the id number based on the recipe
        switch (recipe)
        {
            case RecipeType.Regular: id = 0; break;
            case RecipeType.Capucino: id = 2; break;
            case RecipeType.Espresso: id = 1; break;
            case RecipeType.DarkRoast: id = 3; break;
            case RecipeType.FrenchVanilla: id = 4; break;
        }
        //Create a temporary list to store contained values
    
        //If there's more selected ingredients than the recipe's amount return false.
        if (selectedIngredients.Count > recipeIngredients[id].Length) 
        {
            statusText.color = Color.red;
            statusText.text = "Too many ingredients!"; 
            return false; 
        }
        //If there's less selected ingredients than the recipe's amount return false.
        else if (selectedIngredients.Count < recipeIngredients[id].Length)
        {
            statusText.color = Color.red;
            statusText.text = "Not enough ingredients!";
            return false;
        }
        //Check if the recipe ingredients IDs are contained in the selected ingredients .
        for (int r = 0; r < recipeIngredients[id].Length; r++)
        {
            if (selectedIngredients.Contains(recipeIngredients[id][r]))
            {
                //if the build recipe ingredient IDs was added again return false. (IE: too much milk/sugar/water added etc.)
                if (ingredientBuild.Count > 0 && ingredientBuild.Contains(recipeIngredients[id][r])) return false;
                //Add the correct ID into new build recipe list
                ingredientBuild.Add(recipeIngredients[id][r]);
            }
            //if the selected ingredient ID doesnt match any IDs in the recipe return false. (IE: Wrong ingredients for recipe.)
            else return false;
        }
        //turn the list into an array
        int[] addedIngredients = new int[ingredientBuild.Count];
        addedIngredients = ingredientBuild.ToArray();
        //if the loop finishes and these arrays match the recipe is true.
        for(int i = 0; i < recipeIngredients[id].Length; i++) 
        {
            if (addedIngredients[i] != recipeIngredients[id][i])  
                return false; //Fail safe if something goes wrong
        }
        //Finished pouring so clear the selected ingredients
        selectedIngredients.Clear();
        return true; 
    }
    public void ClearIngredients()
    {
        //Do nothing if the count is 0
        if (selectedIngredients.Count < 1) { statusText.color = Color.yellow; statusText.text = "Ingredient list is already cleared!"; return; }
        //clears the list of ssleected ingredients
        selectedIngredients.Clear();
        ingredientBuild.Clear();
        statusText.color = Color.white;
        statusText.text = "Ingredient list cleared!";
        ingredientText.text = null;
    }
}
