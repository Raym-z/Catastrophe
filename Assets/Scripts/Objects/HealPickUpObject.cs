using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] private AudioClip healSoundClip;
    [SerializeField] int healAmount;

    public void OnPickUp(Character character)
    {
        SFXManager.instance.PlaySoundFXClip(healSoundClip, transform, 1f);
        character.Heal(healAmount);
    }
}


