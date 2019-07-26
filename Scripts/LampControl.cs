using UnityEngine;

public class LampControl : MonoBehaviour {      //Скрипт для переключения фонаря
    private Light l;
    private AudioSource Audio;

	void Start () {
        l = GetComponent<Light>();
        Audio = GetComponent<AudioSource>();
	}
	
	void Update () {
        Switching();
	}

    private void Switching()
    {
       if (l.enabled && Input.GetButtonDown("Lamp Switching"))
        {
            l.enabled = false;
            Audio.Play();
        } 
       else if (!l.enabled && Input.GetButtonDown("Lamp Switching"))
        {
            l.enabled = true;
            Audio.Play();
        }
    }
}
