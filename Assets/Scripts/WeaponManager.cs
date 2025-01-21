using Unity.Mathematics;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int minDamage,maxDamage;
    public Camera PlayerCamera;
    public float Range = 300f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private Enemy_Manager enemyManager;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
            muzzleFlash.Play();

        }
    }
    void Fire()
    {
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.transform.position,PlayerCamera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
            enemyManager = hit.transform.GetComponent<Enemy_Manager>();
            Instantiate(impactEffect, hit.point ,Quaternion.LookRotation(hit.normal));
            if(enemyManager != null )
            {
                enemyManager.EnemyTakeDamage(UnityEngine.Random.Range(minDamage, maxDamage));
            }
            
        }
    }
}
