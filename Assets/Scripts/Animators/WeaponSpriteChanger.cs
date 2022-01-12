using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteChanger : MonoBehaviour
{
    public List<Sprite> weapons;

    public void ChangeWeapon(Weapon weapon)
    {
        GetComponent<SpriteRenderer>().sprite = weapons[(int) weapon];
    }
}
