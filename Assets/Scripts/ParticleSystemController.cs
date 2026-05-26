using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        particle.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (particle.isPlaying == false)
        {
            gameObject.SetActive(false);
        }
    }
}
