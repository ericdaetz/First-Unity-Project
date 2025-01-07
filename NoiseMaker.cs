using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    AudioSource good_sound;
    AudioSource bad_sound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] my_audiosources = GetComponents<AudioSource>();
        bad_sound = my_audiosources[0];
        good_sound = my_audiosources[1];

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayNoise(int sound_code){
        switch(sound_code){
            case 0:
                bad_sound.Play();
                break;
            case 1:
                good_sound.Play();
                break;
            default:
                bad_sound.Play();
                break;
        }
    }
}
