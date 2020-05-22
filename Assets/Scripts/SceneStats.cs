using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStats : MonoBehaviour
{
    public ParticleSystem[] ScenesParticles;

    public void TurnOffParticles()
    {
        for (int i = 0; i < ScenesParticles.Length; i++)
        {
            if (ScenesParticles[i].transform.parent.gameObject.activeInHierarchy) //if parent is active
            {
                ScenesParticles[i].Stop();
            }
        }
    }

    public void TurnOnParticles()
    {
        for (int i = 0; i < ScenesParticles.Length; i++)
        {
            if (ScenesParticles[i].transform.parent.gameObject.activeInHierarchy) //if parent is active
            {
                ScenesParticles[i].Play();
            }
        }
    }
}
