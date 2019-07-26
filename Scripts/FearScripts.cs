using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class FearScripts : MonoBehaviour{
    private AudioSource screamer;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            screamer = this.GetComponent<AudioSource>();
            screamer.Play();
            Destroy(this);
        }
    }
}
