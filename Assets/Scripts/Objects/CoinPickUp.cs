using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour, IPickUpObject
{
    [SerializeField] int amount;
    [SerializeField] public AudioClip coinPickupSoundClip;


    public void OnPickUp(Character character)
    {
        SFXManager.instance.PlaySoundFXClip(coinPickupSoundClip,transform,1f);
        character.coins.Add(amount);
    }
}
