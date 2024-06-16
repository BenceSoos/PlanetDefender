using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject crash;
    [SerializeField] GameObject hitVFX;
    GameObject parent;
    [SerializeField] int hitPoints = 2;

    [SerializeField] int death = 15;

    ScoreBoard scoreboard;

    void Start()
    {
        scoreboard = FindObjectOfType<ScoreBoard>();
        AddParent();
        AddRB();
    }

    void AddParent()
    {
        parent = GameObject.FindWithTag("Parent");
    }

    void AddRB()
    {
        gameObject.AddComponent<Rigidbody>().useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ScoreCounter();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(crash, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }

    void ScoreCounter()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        hitPoints = hitPoints - 1;
        scoreboard.IncreaseScore(death); 
    }
}
