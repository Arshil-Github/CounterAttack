using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Chest")]
public class Chest : ScriptableObject
{

    public enum Rarity {Common,Rare, Epic};
    public Rarity chestRarity;

    public int cost;

    //Coin
    public int min_coins;
    public int max_coins;

    //Diamond
    public int min_diamonds;
    public int max_diamonds;

    //SkinPieces
    public int skinPieces;

    //PowerUps
    public int numberOfPowerUps;

    public Sprite sprite;
}
