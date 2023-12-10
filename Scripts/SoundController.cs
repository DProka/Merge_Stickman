using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource mainSource;

    [Header("Units")]
    
    public AudioClip throwProjectile;
    public AudioClip swordAttack;
    public AudioClip unitDeath;
    public AudioClip unitUpgrade;

    [Header("UI")]

    public AudioClip startButton;
    public AudioClip OptionsButton;
    public AudioClip BuyButton;
    public AudioClip Win;
    public AudioClip Defeat;
    public AudioClip coins;


    public void PlayUnitAttack(bool isMelee)
    {
        mainSource.PlayOneShot(isMelee == true ? swordAttack : throwProjectile);
    }

    public void PlayUnitDeath()
    {
        mainSource.PlayOneShot(unitDeath);
    }
    
    public void PlayUnitUp()
    {
        mainSource.PlayOneShot(unitUpgrade);
    }

    public void PlayStart()
    {
        mainSource.PlayOneShot(startButton);
    }

    public void PlayButtonClick(bool isBuy)
    {
        mainSource.PlayOneShot(isBuy == true ? BuyButton : OptionsButton);
    }

    public void PlayWin(bool win)
    {
        mainSource.PlayOneShot(win == true ? Win : Defeat);
    }

    public void PlayCasinoReward()
    {
        mainSource.PlayOneShot(coins);
    }
}
