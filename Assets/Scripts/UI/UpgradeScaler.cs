using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScaler : MonoBehaviour
{
    [SerializeField] Slider slider;


    public void UpdateStoreSlider(int current, int target)
    {
        slider.maxValue = target;
        slider.value = current;
    }
}
