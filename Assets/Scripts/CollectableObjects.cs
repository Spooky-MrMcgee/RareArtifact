using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableObjects : MonoBehaviour
{
    [SerializeField] private GameObject Collectable;
    [SerializeField] public float durability, hungerCost;
    [SerializeField] private float damageTillSpawn, maxDamageTillSpawn;
    [SerializeField] Vector3 spawnPoint;
    public bool isBroken;
    
    void Start()
    {
        damageTillSpawn = maxDamageTillSpawn;
    }
    
    public void DamageDurability(float damage)
    {
        if (durability <= 0)
        {
            isBroken = true;
        }

        durability -= damage;
        damageTillSpawn -= damage;
        if (damageTillSpawn <= 0)
        {
            spawnPoint = new Vector3(transform.position.x + Random.Range(0.3f, 1.3f), transform.position.y + 5f, transform.position.z + Random.Range(0.3f, 1.3f));
            Instantiate(Collectable, spawnPoint, Quaternion.identity);
            damageTillSpawn = maxDamageTillSpawn;
        }
    }
}
