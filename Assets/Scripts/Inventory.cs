using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List <GameObject> weapons;
    private List <GameObject> buffs;

    public void Start()
    {
        weapons = new List<GameObject>();
        buffs = new List<GameObject>();
    }

    public void Attack()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].GetComponent<WeaponController>().Attack();
        }
    }

    public void AddWeapon(GameObject obj)
    {
        weapons.Add(obj);
    }

    public void AddBuff(GameObject obj)
    {
        buffs.Add(obj);
    }
}
