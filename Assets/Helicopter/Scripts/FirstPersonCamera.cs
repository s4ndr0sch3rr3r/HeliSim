using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float PositionFolowForce = 5f;
    public float RotationFolowForce = 5f;
	void Start ()
	{

	}

    void FixedUpdate()
	{
        var vector = Vector3.forward;
        var dir = Target.rotation * Vector3.forward;
        if (dir.magnitude > 0f) vector = dir / dir.magnitude;

        transform.position = Target.position;
        transform.rotation = Target.rotation;
	}
}

