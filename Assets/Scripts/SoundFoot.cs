using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFoot : MonoBehaviour
{
    public AudioSource audi;

    [Header("FootSteps Sources")]
    public AudioClip[] footStepSound;

    private AudioClip GetRandomFootStep()
    {
        return footStepSound[Random.Range(0, footStepSound.Length)];
    }

    private void Step()
    { 
        AudioClip clip = GetRandomFootStep();
        audi.PlayOneShot(clip);
    }
}
