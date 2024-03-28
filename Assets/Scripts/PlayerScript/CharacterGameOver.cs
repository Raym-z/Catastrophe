using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponParent;

    SaveManager saveManager;
    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        weaponParent.SetActive(false);
        saveManager.SaveGame();
    }
}
