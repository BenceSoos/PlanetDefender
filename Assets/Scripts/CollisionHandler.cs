using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    [SerializeField] ParticleSystem crashParticle;

    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Crashed();
    }

    void Crashed()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", delay);
        crashParticle.Play();
    }

    void ReloadLevel()
    {
        int levelReload = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelReload);
        crashParticle.Play();
    }


}
