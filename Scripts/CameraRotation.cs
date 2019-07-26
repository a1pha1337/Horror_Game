using UnityEngine;

public class CameraRotation : MonoBehaviour {
    [SerializeField]
    public float sensitivity_y;
    public Camera _cam;

	// Use this for initialization
	void Start () {
        _cam = GetComponent<Camera>();
        sensitivity_y = StaticValues.sens_y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        sensitivity_y = StaticValues.sens_y;
        Rotation();
	}

    private void Rotation()     //Поворот камеры по X
    {
        float angle = Vector3.Angle(Vector3.up, _cam.transform.forward);
        float xRot = Input.GetAxis("Mouse Y");
        Vector3 camRotation = new Vector3(xRot, 0f, 0f) * sensitivity_y;

        if ((angle > 20f && xRot > 0f) || (angle < 160f && xRot < 0f))
                _cam.transform.Rotate(-camRotation);
    }
}
