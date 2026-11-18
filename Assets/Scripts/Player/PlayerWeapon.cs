using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform weapon;
    public Transform decal;
    public Transform spawnPos;
    public GameObject projectile;
    public AudioClip WeaponThrow;

    [SerializeField] int _poolSize = 10;
    List<GameObject> _pool = new List<GameObject>();
    List<GameObject> _available = new List<GameObject>();
    private object temp;

    void Start()
    {
        InitPool();
    }

    void InitPool()
    {
        for (int i = 0; i < _poolSize; ++i)
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileController>()._weapon = this;
            proj.SetActive(false);
            _pool.Add(proj);
            _available.Add(proj);
        }
    }

    public void SpawnProjectile(Vector2 shootDir, int damageLevel)
    {
        GameObject proj;
        if (_available.Count > 0)
        {
            proj = _available[0];
            _available.Remove(proj);
        }
        else
        {
            proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileController>()._weapon = this;
            _pool.Add(proj);
        }

        proj.SetActive(true);
        proj.transform.position = spawnPos.position;
        proj.transform.rotation = Quaternion.FromToRotation(Vector3.right, shootDir);
        proj.GetComponent<ProjectileController>().direction = shootDir.normalized;
        proj.GetComponent<ProjectileController>().UpgradeDamage(2 * damageLevel);
    }

    public void DespawnProjectile(GameObject projectile)
    {
        _available.Add(projectile);
    }

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

    public void DecalOnOff(bool on)
    {
        if (decal != null)
        {
            decal.gameObject.SetActive(on);
        }
    }
}
