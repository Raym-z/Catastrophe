using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStore : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    SaveManager saveManager;
    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }
    public void CloseStorePanel()
    {
        storePanel.SetActive(false);
        saveManager.SaveGame();
    }
}
