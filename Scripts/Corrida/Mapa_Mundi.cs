using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Mapa_Mundi : MonoBehaviour {

    public GameObject mapaCorrida, btnVolta, mapinha, canvasMapaMundi;
    public Image img_Race;
    public Color[] corBolinhas; // cor 1 é quando o player venceu e a bolinha fica cinza a cor 2 significa que liberou uma nova corrida
    public Image[] img_Bolinhas; // pega as bolinas e muda a cor delas
    public Sprite[] imagemCorrida;
    private Loading loading;
    private int btnEscolhido;
    private LiberarCorrida liberarCorrida;
    public TextMeshProUGUI txt_MensagemCorrida;
    public GameObject botaoSair, botaoVoltar, txt_Aviso;
    private GameController gameController;
    private ItensCarro itensCarro;
    [SerializeField]
    private bool podeApertar = true, isLoading;
    [SerializeField]
    private float tempoApertar;
    private GamepadCanvas gamepadCanvas;

    public AudioClip sound_Back;
    private AudioSource audioSource;


    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();

        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        txt_Aviso.SetActive(false);
        itensCarro = FindObjectOfType(typeof(ItensCarro)) as ItensCarro;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        mapaCorrida.SetActive(false);
        btnVolta.SetActive(false);
        mapinha.SetActive(false);

        for(int i = 0; i < liberarCorrida.ganhouCorrida.Length; i++) {
            if(liberarCorrida.ganhouCorrida[i]) {
                img_Bolinhas[i].color = corBolinhas[0];
            } else {
                img_Bolinhas[i].color = corBolinhas[1];
            }
        }

	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (Input.GetButton("Controle 01 X") && podeApertar && canvasMapaMundi.activeInHierarchy)
        {
            podeApertar = false;
            StartCoroutine("tempoEspera");
            if (mapaCorrida.activeInHierarchy)
            {
                if (itensCarro.carroEscolhido != null)
                {
                    switch (btnEscolhido)
                    {
                        case 1:
                            gameController.changeState(GameState.GAMEPLAY);
                            canvasMapaMundi.SetActive(false);
                            loading.carregarLoading("Mapa da Ponte");
                            break;
                        case 2:
                            if (liberarCorrida.ganhouCorrida[1])
                            {
                                canvasMapaMundi.SetActive(false);
                                gameController.changeState(GameState.GAMEPLAY);
                                loading.carregarLoading("Mapa Montanha");
                            }
                            else
                            {

                            }
                            break;
                        case 3:
                            if (liberarCorrida.ganhouCorrida[2])
                            {
                                canvasMapaMundi.SetActive(false);
                                gameController.changeState(GameState.GAMEPLAY);
                                loading.carregarLoading("Mapa do Gelo");
                            }
                            else
                            {

                            }
                            break;
                        case 4:
                            if (liberarCorrida.ganhouCorrida[3])
                            {
                                canvasMapaMundi.SetActive(false);
                                gameController.changeState(GameState.GAMEPLAY);
                                loading.carregarLoading("Mapa do Deserto");
                            }
                            else
                            {

                            }
                            break;
                    }
                }
                else
                {
                    txt_Aviso.SetActive(true);
                    StartCoroutine("aviso");
                    StartCoroutine("tempoEspera");
                }
            }
        }
        else if (Input.GetButton("Controle 01 Bolinha") && podeApertar && canvasMapaMundi.activeInHierarchy)
        {
            audioSource.PlayOneShot(sound_Back);
            if (mapaCorrida.activeInHierarchy)
            {
                podeApertar = false;
                img_Race.sprite = null;
                btnVolta.SetActive(true);
                mapinha.SetActive(true);
                mapaCorrida.SetActive(false);
                EventSystem.current.SetSelectedGameObject(botaoSair);
                StartCoroutine("tempoEspera");
            }
            else
            {
                podeApertar = false;
                EventSystem.current.SetSelectedGameObject(botaoSair);
                StartCoroutine("tempoEspera");
            }
        }
	}

    // chama a função quando eu clico na bolinha
    public void btnEscolherCorrida(int escolha) {
        btnEscolhido = escolha;
        EventSystem.current.SetSelectedGameObject(botaoVoltar);
        switch (escolha) {
            case 1:
                podeApertar = false;
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_MensagemCorrida.SetText("Corrida com 4 participantes \n1/3 voltas");
                } else {
                    txt_MensagemCorrida.SetText("Race with 4 players \n1/3 laps");
                }
                btnVolta.SetActive(false);
                mapinha.SetActive(false);
                mapaCorrida.SetActive(true);
                img_Race.sprite = imagemCorrida[0];
                StartCoroutine("tempoEspera");
                break;
            case 2:
                podeApertar = false;
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_MensagemCorrida.SetText("Corrida com 5 participantes \n1/3 voltas");
                } else {
                    txt_MensagemCorrida.SetText("Race with 5 players \n1/3 laps");
                }
                btnVolta.SetActive(false);
                mapinha.SetActive(false);
                mapaCorrida.SetActive(true);
                img_Race.sprite = imagemCorrida[1];
                StartCoroutine("tempoEspera");
                break;
            case 3:
                podeApertar = false;
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_MensagemCorrida.SetText("Corrida com 5 participantes \n1/4 voltas");
                } else {
                    txt_MensagemCorrida.SetText("Race with 5 players \n1/4 laps");
                }
                btnVolta.SetActive(false);
                mapinha.SetActive(false);
                mapaCorrida.SetActive(true);
                img_Race.sprite = imagemCorrida[2];
                StartCoroutine("tempoEspera");
                break;
            case 4:
                podeApertar = false;
                btnVolta.SetActive(false);
                mapinha.SetActive(false);
                mapaCorrida.SetActive(true);
                img_Race.sprite = imagemCorrida[3];
                StartCoroutine("tempoEspera");
                break;
        }
    }

    IEnumerator aviso() {
        yield return new WaitForSecondsRealtime(5);
        txt_Aviso.SetActive(false);
    }

    IEnumerator tempoEspera() {

        yield return new WaitForSecondsRealtime(tempoApertar);
        podeApertar = true;
    }

}
