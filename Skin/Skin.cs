using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSkinComp", menuName = "Skin Component")]
public class Skin : ScriptableObject
{
    public Sprite sprite;

    public enum Rarity { Common, Rare, Epic };
    public Rarity skinRarity;

    public string name;
    public Sprite skinPieceSprite;

    public int skinPiecesneeded;
    public int skinPiecesHave;
}