using System;
using UnityEngine;

public static class GameState
{
    public static int CurrentCharacterIndex = 0;
    public static bool IsSinglePlayer = true;
    public static bool WasMultiplayer = false;
    public static int CurrentLevel = 1;

    public static void SetActiveObject(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i == CurrentCharacterIndex);
        }
    }

    public static GameObject[] GetActiveObjects(GameObject[] gameObjects)
    {
        return Array.FindAll(gameObjects, go => go.activeInHierarchy);
    }
}