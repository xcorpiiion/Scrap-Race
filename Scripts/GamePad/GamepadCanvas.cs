using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GamepadCanvas : MonoBehaviour {

    private GameObject currentButton;
    private AxisEventData currentAxis;
    public GameObject selectedObj;
    public bool podeApertar, isCanvas;
    public float tempoEspera;
    [SerializeField]
    private AudioClip[] sound_Canvas;
    private AudioSource audioSource;
    private StandaloneInputModule pegaInputs;


    //timer
    private float timeBetweenInputs = 0.15f; //in seconds
    private float timer = 0;

    private void Start()
    {
        pegaInputs = FindObjectOfType(typeof(StandaloneInputModule)) as StandaloneInputModule;
        audioSource = GetComponent<AudioSource>();
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
       selectedObj = EventSystem.current.currentSelectedGameObject;
    }

    void LateUpdate()
    {



        if (isCanvas)
        {
            selectedObj = EventSystem.current.currentSelectedGameObject;

            currentAxis = new AxisEventData(EventSystem.current);
            currentButton = EventSystem.current.currentSelectedGameObject;

            if(Input.GetAxis("Controle 01 Direcional X") != 0 || Input.GetAxis("Controle 01 Direcional Y") != 0) {
                audioSource.PlayOneShot(sound_Canvas[0]);
                pegaInputs.horizontalAxis = "Controle 01 Direcional X";
                pegaInputs.verticalAxis = "Controle 01 Direcional Y";
            } else if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
                audioSource.PlayOneShot(sound_Canvas[0]);
                pegaInputs.horizontalAxis = "Horizontal";
                pegaInputs.verticalAxis = "Vertical";
            }

            if (selectedObj != null)
            {
                if (Input.GetButton("Controle 01 X") && !podeApertar) // seleciona
                {

                    podeApertar = true;
                    ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.submitHandler);
                    selectedObj = null;
                    audioSource.PlayOneShot(sound_Canvas[1]);
                    StartCoroutine("tempoApertar");
                }
            }
        }
            

    }

    IEnumerator tempoApertar() {
        yield return new WaitForSecondsRealtime(tempoEspera);
        podeApertar = false;

    }


}

