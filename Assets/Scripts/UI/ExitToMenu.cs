using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    SaveManager saveManager;

    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }
    public void BackToMenu()
    {
        Debug.Log("Back to Menu");
        saveManager.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}
