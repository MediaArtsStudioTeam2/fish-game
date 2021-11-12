using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip eatFoodSound;
    public static AudioClip BackgroundMusic;
    static AudioSource audioSrc;
//    static AudioSource bgmChannel;
  //  static AudioSource sfxChannel;

    // Start is called before the first frame update
    void Start()
    {
        eatFoodSound = Resources.Load<AudioClip> ("eatFoodSound");
        BackgroundMusic = Resources.Load<AudioClip>("bg1");
        audioSrc = GetComponent<AudioSource> ();

        audioSrc.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip) {
        Debug.Log("Yes!");
        switch (clip) {
        case "eatFoodSound":
            audioSrc.PlayOneShot (eatFoodSound);
            break;
        }
    }
}
