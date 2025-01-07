using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    AudioSource my_audiosource;

    // Start is called before the first frame update
    void Start()
    {
        my_audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayNoise(){
        my_audiosource.Play();
    }
}
