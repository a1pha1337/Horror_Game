using UnityEngine;

public class CollisionEnter : MonoBehaviour {
    public static bool col;

    void OnCollisionEnter(Collision collis)
    {
       if (collis.gameObject.tag == "Terrain")
            col = true;
    }

    void OnCollisionExit(Collision collis)
    {
        if (collis.gameObject.tag == "Terrain")
            col = false;
    }
}
