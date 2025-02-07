using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject weapon;
    private List <GameObject> buffs;

    public void Start()
    {
        buffs = new List<GameObject>();
    }

    public void Attack()
    {
        //weapon.GetComponent<WeaponController>().Attack();
    }

    public void AddBuff(GameObject obj)
    {
        buffs.Add(obj);
    }
}
