using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        // Debug.Log($"Setting upgrade {upgradeData.Name}");
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
