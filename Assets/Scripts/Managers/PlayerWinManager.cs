using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    PauseManager pauseManager;
    [SerializeField] DataContainer dataContainer;

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }
    public void Win()
    {
        winMessagePanel.SetActive(true);
        pauseManager.PauseGame();
        dataContainer.stageComplete(0);
    }
}
