using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Weapon", menuName = "New Player Weapon")]
[System.Serializable]
public class PlayerWeapon : MonoBehaviour
{
    public Transform weapon;
    public Transform decal;
    public Transform spawnPos;
    public GameObject projectile;
    public AudioClip WeaponThrow;

    public void FlipWeapon()
    {
        if (decal != null)
        {
            decal.localScale = new Vector3(-decal.localScale.x, decal.localScale.y, decal.localScale.z);
        }
        if (weapon.tag == "WeaponFlip")
        {
            weapon.localScale = new Vector3(weapon.localScale.x, -weapon.localScale.y, weapon.localScale.z);
            weapon.localPosition = new Vector3(weapon.localPosition.x, -weapon.localPosition.y, weapon.transform.localPosition.z);
        }
    }

    public void SpawnProjectile(Vector2 shootDir, int damageLevel)
    {
        GameObject temp = Instantiate(projectile, spawnPos.position, Quaternion.LookRotation(Vector3.forward, shootDir));
        temp.GetComponent<ProjectileController>().direction = shootDir.normalized;
        temp.GetComponent<ProjectileController>().UpgradeDamage(2 * damageLevel);
    }

    public void DecalOnOff(bool on)
    {
        if (decal != null)
        {
            decal.gameObject.SetActive(on);
        }
    }
}
