﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager _instance = null;
    public static RecipeManager GetInstance() { return _instance; }

    [SerializeField]
    private List<ObjectTypeName> Recipe1;
    private string _recipeCode1;

    [SerializeField]
    private List<ObjectTypeName> Recipe2;
    private string _recipeCode2;

    [SerializeField]
    private List<ObjectTypeName> Recipe3;
    private string _recipeCode3;

    [SerializeField]
    private List<ObjectTypeName> Recipe4;
    private string _recipeCode4;

    [SerializeField]
    private List<ObjectTypeName> Recipe5;
    private string _recipeCode5;

    [SerializeField]
    private List<ObjectTypeName> Recipe6;
    private string _recipeCode6;

    private Dictionary<string, bool> _recipeTable = new Dictionary<string, bool>();

    private StringBuilder _stringBuilder = new StringBuilder();

    public bool IsAvailableRecipe(string recipeData)
    {
        bool cache;
        return _recipeTable.TryGetValue(recipeData, out cache) && cache;
    }

    public string GetRandomRecipeCode()
    {
        return GetRecipeCodeByIntID(Random.Range(1, 4));
    }

    public string GetRecipeCodeByIntID(int id)
    {
        switch (id)
        {
            case 1:
                return _recipeCode1;

            case 2:
                return _recipeCode2;

            case 3:
                return _recipeCode3;

            case 4:
                return _recipeCode4;

            case 5:
                return _recipeCode5;

            case 6:
                return _recipeCode6;

            default:
                return null;
        }
    }

    private void Awake()
    {
        _instance = this;

        _recipeCode1 = GenerateRecipeCode(Recipe1);
        _recipeTable[_recipeCode1] = true;

        _recipeCode2 = GenerateRecipeCode(Recipe2);
        _recipeTable[_recipeCode2] = true;

        _recipeCode3 = GenerateRecipeCode(Recipe3);
        _recipeTable[_recipeCode3] = true;

        _recipeCode4 = GenerateRecipeCode(Recipe4);
        _recipeTable[_recipeCode4] = true;

        _recipeCode5 = GenerateRecipeCode(Recipe5);
        _recipeTable[_recipeCode5] = true;

        _recipeCode6 = GenerateRecipeCode(Recipe6);
        _recipeTable[_recipeCode6] = true;
    }

    private string GenerateRecipeCode(List<ObjectTypeName> recipe)
    {
        _stringBuilder.Clear();
        recipe.ForEach<ObjectTypeName>((ObjectTypeName otn) => _stringBuilder.Append((int)otn));

        return _stringBuilder.ToString();
    }
}