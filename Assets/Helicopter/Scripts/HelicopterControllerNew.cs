using UnityEngine;
using UnityEngine.UI;

public class HelicopterControllerNew : MonoBehaviour
{
    public AudioSource HelicopterSound;
    public ControlPanelNew ControlPanelNew;
    public Rigidbody HelicopterModel;
    public HeliRotorController MainRotorController;
    public HeliRotorController SubRotorController;

    public float rollSpeed = .5f;
    public float yawSpeed = .5f;
    public float pitchSpeed = .5f;
    public float EffectiveHeight = 100f;
    public float horizontalMouseSpeed = 20f;
    public float verticalMouseSpeed = 20f;

    public float turnTiltForcePercent = 1.5f;
    public float turnForcePercent = 1.3f;

    private float _engineForce;
    private float pitch = 0f;
    private float yaw = 0f;
    private float roll = 0f;
    private float pitchLerp = 0f;
    private float yawLerp = 0f;
    private float rollLerp = 0f;
    public float EngineForce
    {
        get { return _engineForce; }
        set
        {
            MainRotorController.RotarSpeed = value * 80;
            SubRotorController.RotarSpeed = value * 40;
            HelicopterSound.pitch = Mathf.Clamp(value / 40, 0, 1.2f);
            if (UIGameController.runtime.EngineForceView != null)
                UIGameController.runtime.EngineForceView.text = string.Format("Engine value [ {0} ] ", (int)value);

            _engineForce = value;
        }
    }
    public bool IsOnGround = true;

    // Use this for initialization
	void Start ()
	{
        ControlPanelNew.KeyPressedNew += OnKeyPressedNew;
	}

	void Update () {
	}
  
    void FixedUpdate()
    {
        LiftProcess();
        alignHelicopter();
        
    }



    private void LiftProcess()
    {
        var upForce = 1 - Mathf.Clamp(HelicopterModel.transform.position.y / EffectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0f, EngineForce, upForce) * HelicopterModel.mass;
        HelicopterModel.AddRelativeForce(Vector3.up * upForce);
    }

    
    private void alignHelicopter()
    {
        pitch -= Mathf.Clamp(verticalMouseSpeed * Input.GetAxis("Mouse Y"), -1*pitchSpeed, 1*pitchSpeed);
        pitchLerp = Mathf.Lerp(pitchLerp, pitch, Time.deltaTime);
        roll += Mathf.Clamp(horizontalMouseSpeed * Input.GetAxis("Mouse X"), -1*rollSpeed, 1*rollSpeed);
        rollLerp = Mathf.Lerp(rollLerp, roll, Time.deltaTime);
        yawLerp = Mathf.Lerp(yawLerp, yaw, Time.deltaTime);
        transform.localRotation = Quaternion.Euler(pitchLerp, yawLerp, rollLerp);
        if (pitch > 360 || pitch < -360) pitch %= 360;
        if (roll > 360 || roll < -360) roll %= 360;
        if (yaw > 360 || yaw < -360) yaw %= 360;
    }

    private void OnKeyPressedNew(PressedKeyCodeNew[] obj)
    {
        if (Input.GetKey(KeyCode.A))
        {

        }
        if (EngineForce > 11.5f)
        {
            EngineForce -= .08f;
        }
        if(EngineForce < 11.5 && !IsOnGround) 
        {
            EngineForce += 0.05f;
        }
        /**
        float tempY = 0;
        float tempX = 0;

        // stable forward
        if (hMove.y > 0)
            tempY = - Time.fixedDeltaTime;
        else
            if (hMove.y < 0)
                tempY = Time.fixedDeltaTime;

        // stable lurn
        if (hMove.x > 0)
            tempX = -Time.fixedDeltaTime;
        else
            if (hMove.x < 0)
                tempX = Time.fixedDeltaTime;
        **/

        foreach (var pressedKeyCodeNew in obj)
        {
            switch (pressedKeyCodeNew)
            {
                case PressedKeyCodeNew.ThrottleUpPressed:

                    EngineForce += 0.18f;
                    break;

                case PressedKeyCodeNew.ThrottleDownPressed:

                    EngineForce -= 0.14f;
                    if (EngineForce < 0) EngineForce = 0;
                        break;

                case PressedKeyCodeNew.YawLeftPressed:
                    yaw -= 1f;
                        break;


                case PressedKeyCodeNew.YawRightPressed:
                    yaw += 1f;
                    break;


            }
        }
        /**
        hMove.x += tempX;
        hMove.x = Mathf.Clamp(hMove.x, -1, 1);

        hMove.y += tempY;
        hMove.y = Mathf.Clamp(hMove.y, -1, 1);
        **/
    }

    private void OnCollisionEnter()
    {
        IsOnGround = true;
    }

    private void OnCollisionExit()
    {
        IsOnGround = false;
    }
}