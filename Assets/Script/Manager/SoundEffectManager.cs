using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    public AudioSource SoundEffect;

    public AudioClip CreditTile, CombatTile, EventTile, BuyMetal, 
                        BuyPoison, BuyOil, BuyGem, Move, Click, Attack, Heal, Sell, 
                        LoseCombat, WinCombat, PlayerDeath, City, ActiveEvent, Mercante;

    public void PlayEffect(AudioClip _clip)
    {
        SoundEffect.clip = _clip;
        SoundEffect.Play();
    }
}
