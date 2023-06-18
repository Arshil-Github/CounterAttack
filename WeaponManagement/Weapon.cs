using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite gunSprite;
    public int ammo;

    public Color color;
    public string weaponColor;

    public Vector3[] DotLocations;
}
