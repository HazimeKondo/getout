using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip OnClickDownSfx;
    public AudioClip OnClickUpSfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCickDown()
    {
        AudioSource.PlayOneShot(OnClickDownSfx);
    }

    public void OnClickUp()
    {
        AudioSource.PlayOneShot(OnClickUpSfx);
    }
}
