using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;
    [SerializeField] GameWinPanel levelCompletePanel;
    StageTime stageTime;
    PauseManager pauseManager; 
    private void Awake() 
    {
        stageTime = GetComponent<StageTime>();
        pauseManager = FindObjectOfType<PauseManager>(); 
        levelCompletePanel = FindObjectOfType<GameWinPanel>(true); 
    }

    public void Update() 
    {
            if (stageTime.time > timeToCompleteLevel) 
            {
                pauseManager.PauseGame();
                levelCompletePanel.gameObject.SetActive(true);
            }
    }
}
