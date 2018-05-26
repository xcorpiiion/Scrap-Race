using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour {

    [SerializeField]
    private GameObject canva_Config, canvas_Main, canva_Sair, canvas_Pause;
    private Loading loading;
    public GameObject btn_ResumeCanvas, btn_ConfigCanvas;
    private GameController gameController;
    private GamepadCanvas gamepadCanvas;
    public bool podeApertar = true;
    public float tempoEspera;
    public AudioClip audioClip;
    public bool ativarInventario;
    public AudioSource audioSource;
    private LiberarCorrida liberarCorrida;

    // Use this for initialization
    void Start () {

        liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        gameController.setarPreferencias();
        gameController.changeState(GameState.GAMEPLAY);
        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        gamepadCanvas.isCanvas = false;
        audioSource = GetComponent<AudioSource>();
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        canvas_Main.SetActive(false);
        canva_Config.SetActive(false);
        canva_Sair.SetActive(false);
        canvas_Pause.SetActive(true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canva_Config.activeInHierarchy)
        {
            if (Input.GetButton("Controle 01 Bolinha") && podeApertar)
            {
                audioSource.PlayOneShot(audioClip);
                podeApertar = false;
                canvas_Pause.SetActive(true);
                canva_Sair.SetActive(false);
                canva_Config.SetActive(false);
                EventSystem.current.SetSelectedGameObject(btn_ResumeCanvas);
                StartCoroutine("tempoApertar");
            }
        }
        else if (canva_Sair.activeInHierarchy)
        {
            if (Input.GetButton("Controle 01 Bolinha") && podeApertar)
            {
                audioSource.PlayOneShot(audioClip);
                podeApertar = false;
                canvas_Pause.SetActive(true);
                canva_Sair.SetActive(false);
                canva_Config.SetActive(false);
                EventSystem.current.SetSelectedGameObject(btn_ResumeCanvas);
                StartCoroutine("tempoApertar");
            } else if(Input.GetButton("Controle 01 X") && podeApertar) {
                print("Entrou aqui");
                gameController.changeState(GameState.GAMEPLAY);
                loading = FindObjectOfType(typeof(Loading)) as Loading;
                loading.carregarLoading("MainMenu");
                canvas_Main.SetActive(false);
                canva_Config.SetActive(false);
                canvas_Main.SetActive(false);
                canva_Sair.SetActive(false);
                gameController.usandoGameController = true;
                PlayerPrefs.SetInt("usouSave", 1);
                Destroy(gameController.gameObject);
            }
        }
        else if (canvas_Pause.activeInHierarchy)
        {
            if (Input.GetButton("Controle 01 Bolinha") && podeApertar)
            {
                ativarInventario = false;
                audioSource.PlayOneShot(audioClip);
                canva_Config.SetActive(false);
                canva_Sair.SetActive(false);
                gamepadCanvas.isCanvas = false;
                canvas_Main.SetActive(ativarInventario);
                gameController.changeState(GameState.GAMEPLAY);
            }
        }


        // pausa o jogo
        if (Input.GetButton("Controle 01 Start") && podeApertar && gameController.currentState != GameState.INTERACAO)
        {
            audioSource.PlayOneShot(audioClip);
            gamepadCanvas.isCanvas = true;
            podeApertar = false;
            StartCoroutine("tempoApertar");
            ativarInventario = !ativarInventario;
            gamepadCanvas.isCanvas = ativarInventario;
            canvas_Main.SetActive(ativarInventario);
            EventSystem.current.SetSelectedGameObject(btn_ResumeCanvas);
            if (ativarInventario) {

                gameController.changeState(GameState.INTERACAO);

            }  else {

                gameController.changeState(GameState.GAMEPLAY);

            }
        }


    }

    public void btn_Resume() {
        print("Foi resume");
        gameController.changeState(GameState.GAMEPLAY);
        canvas_Main.SetActive(false);
        canva_Config.SetActive(false);
        canvas_Main.SetActive(false);
        gamepadCanvas.isCanvas = false;
    }

    public void btn_Config()
    {
        EventSystem.current.SetSelectedGameObject(btn_ConfigCanvas);
        canvas_Pause.SetActive(false);
        canva_Config.SetActive(true);
    }

    public void btn_Sair()
    {
        canva_Config.SetActive(false);
        canvas_Pause.SetActive(false);
        canva_Sair.SetActive(true);
        podeApertar = false;
        StartCoroutine("tempoApertar");

    }

    IEnumerator tempoApertar()
    {
        yield return new WaitForSecondsRealtime(tempoEspera);
        podeApertar = true;
    }

}
