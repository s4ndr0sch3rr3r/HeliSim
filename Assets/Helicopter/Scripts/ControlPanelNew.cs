using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelNew : MonoBehaviour {
    public AudioSource MusicSound;
    public GameObject firstPersonCam;
    public GameObject thirdPersonCam;
    [SerializeField]
    KeyCode ThrottleUp = KeyCode.W;
    [SerializeField]
    KeyCode ThrottleDown = KeyCode.S;
    [SerializeField]
    KeyCode YawLeft = KeyCode.A;
    [SerializeField]
    KeyCode YawRight = KeyCode.D;
    [SerializeField]
    KeyCode MusicOffOn = KeyCode.M;
    [SerializeField]
    KeyCode ChangeCamera = KeyCode.C;

    private KeyCode[] keyCodes;

    public Action<PressedKeyCodeNew[]> KeyPressedNew;
    private void Awake()
    {
        keyCodes = new[] {
                            ThrottleUp,
                            ThrottleDown,
                            YawLeft,
                            YawRight,
                            MusicOffOn,
                            ChangeCamera
                        };

    }

    void Start () {
	
	}

	void FixedUpdate ()
	{
	    var pressedKeyCodeNew = new List<PressedKeyCodeNew>();
	    for (int index = 0; index < keyCodes.Length; index++)
	    {
	        var keyCode = keyCodes[index];
	        if (Input.GetKey(keyCode))
                pressedKeyCodeNew.Add((PressedKeyCodeNew)index);
	    }

	    if (KeyPressedNew != null)
	        KeyPressedNew(pressedKeyCodeNew.ToArray());

        // for test
        if (Input.GetKey(MusicOffOn))
        {
           if (  MusicSound.volume == 1) return;
/*            if (MusicSound.isPlaying)
                MusicSound.Stop();
            else*/
                MusicSound.volume = 1;
                MusicSound.Play();
        }

        if (Input.GetKey(ChangeCamera))
        {
            if (firstPersonCam.activeInHierarchy)
            {
                thirdPersonCam.SetActive(true);
                firstPersonCam.SetActive(false);
            }
            else
            {
                firstPersonCam.SetActive(true);
                thirdPersonCam.SetActive(false);
            }
        }
      
	}
}
