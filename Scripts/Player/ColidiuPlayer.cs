using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ColidiuPlayer : MonoBehaviour {

    public bool colidiuContainer = false, colidiuBarco = false;
    private Player player;
    public GameObject[] canvasReciclagem;
    public GameObject cameraPlayer, txt_Barco;
    [Header ("Interage com o Mapa da Cidade")]
    public TextMeshProUGUI txt_Interacao;
    private Loading loading;
    public Mapa_Mundi mapaMundi;
    private InterfaceReciclagem interfaceReciclagem;
    public byte colidiuNumber;
    private GameController gameController;
    private GamepadCanvas gamepadCanvas;
    private LiberarCorrida liberarCorrida;
    [Header("Pega o texto de interacao")]
    public GameObject txt_Interagir;
    Scene cena;
    public bool podeApertar = true;
    public float tempoEspera;
    private TutorialGeral tutorialGeral;

    private void Start()
    {
        tutorialGeral = FindObjectOfType(typeof(TutorialGeral)) as TutorialGeral;
        liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        player = FindObjectOfType(typeof(Player)) as Player;
        txt_Interagir.SetActive(false);
        cena = SceneManager.GetActiveScene();
        if (cena.name == "Cidade Rica")
        {
            for (int i = 0; i < canvasReciclagem.Length; i++)
            {
                canvasReciclagem[i].SetActive(false);
            }
        }
    }


    private void LateUpdate()
    {
        if (cena.name == "Cidade Rica")
        {
            if (Input.GetButton("Controle 01 Bolinha"))
            {
                if (gameController.currentState == GameState.GAMEPLAY)
                {

                    if (colidiuNumber == 1)
                    {
                        gameController.changeState(GameState.INTERACAO);
                        gamepadCanvas.isCanvas = true;
                        txt_Interagir.SetActive(false);
                        interfaceReciclagem.btnNormal();
                        canvasReciclagem[0].SetActive(true);
                        canvasReciclagem[1].SetActive(true);
                        canvasReciclagem[2].SetActive(false);
                        canvasReciclagem[3].SetActive(false);
                        cameraPlayer.SetActive(false);
                        gameObject.SetActive(false);
                        txt_Interacao.SetText("");
                        EventSystem.current.SetSelectedGameObject(interfaceReciclagem.botaoRecilar);
                        colidiuNumber = 0;
                        if(!tutorialGeral.fezTutorialReciclar) {
                            tutorialGeral.fezTutorialReciclar = true;
                            tutorialGeral.telasCidade[1].SetActive(true);
                            gamepadCanvas.isCanvas = false;
                            tutorialGeral.contadorTexto = 1;

                        }
                    }
                    else if (colidiuNumber == 2)
                    {
                        gameController.changeState(GameState.INTERACAO);
                        gamepadCanvas.isCanvas = true;
                        txt_Interagir.SetActive(false);
                        mapaMundi.btnVolta.SetActive(true);
                        mapaMundi.mapinha.SetActive(true);
                        canvasReciclagem[0].SetActive(false);
                        canvasReciclagem[1].SetActive(false);
                        canvasReciclagem[2].SetActive(true);
                        canvasReciclagem[3].SetActive(true);
                        cameraPlayer.SetActive(false);
                        gameObject.SetActive(false);
                        txt_Interacao.SetText("");
                        EventSystem.current.SetSelectedGameObject(mapaMundi.botaoSair);
                        colidiuNumber = 0;
                        if (!tutorialGeral.fezTutorialMapa)
                        {
                            tutorialGeral.fezTutorialMapa = true;
                            tutorialGeral.telasCidade[5].SetActive(true);
                            gamepadCanvas.isCanvas = false;
                            tutorialGeral.contadorTexto = 5;

                        }
                    }
                    else if (colidiuNumber == 3)
                    {
                        gameController.changeState(GameState.INTERACAO);
                        txt_Interacao.SetText("");
                        txt_Interagir.SetActive(false);
                        loading.carregarLoading("Garagem");
                        colidiuNumber = 0;
                        gameController.changeState(GameState.GAMEPLAY);
                    }
                    else if (colidiuNumber == 4)
                    {
                        gameController.changeState(GameState.INTERACAO);
                        txt_Interacao.SetText("");
                        txt_Interagir.SetActive(false);
                        liberarCorrida.novoJogo = true;
                        PlayerPrefs.SetInt("TutorialCidade", 1);
                        PlayerPrefs.SetInt("TutorialExploracao", 1);
                        SaveObject.Save();
                        gameController.changeState(GameState.GAMEPLAY);
                        colidiuNumber = 0;
                        
                    }
                    else if (colidiuNumber == 5)
                    {
                        gameController.changeState(GameState.INTERACAO);
                        txt_Interacao.SetText("");
                        txt_Interagir.SetActive(false);
                        loading.carregarLoading("Mapa Exploracao");
                        colidiuNumber = 0;
                    }

                }
                else
                {
                    colidiuNumber = 0;
                }
            }
            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Conteiner") {
            player.conteinerInteracao = other.gameObject;
            txt_Interagir.SetActive(true);
            colidiuContainer = true;
        }

        if (other.tag == "Barco")
        {
            player.conteinerInteracao = other.gameObject;
            colidiuBarco = true;
            txt_Barco.SetActive(true);
        }

        if (other.tag == "Reciclagem")
        {
            txt_Interagir.SetActive(true);
            interfaceReciclagem = FindObjectOfType(typeof(InterfaceReciclagem)) as InterfaceReciclagem;
            colidiuNumber = 1;
            if ((PlayerPrefs.GetInt("Idioma")) == 0) {
                txt_Interacao.SetText("Para usar a Máquina de Reciclagem");
            } else {
                txt_Interacao.SetText("To use the Recycling Machine");
            }
            EventSystem.current.SetSelectedGameObject(interfaceReciclagem.botaoRecilar);
            
               
            
        }


        if (other.tag == "Mapa")
        {
            colidiuNumber = 2;
            txt_Interagir.SetActive(true);
            if ((PlayerPrefs.GetInt("Idioma")) == 0)
            {
                txt_Interacao.SetText("Para ver o Mapa");
            }
            else
            {
                txt_Interacao.SetText("To see the Map");
            }
            mapaMundi = FindObjectOfType(typeof(Mapa_Mundi)) as Mapa_Mundi;

            EventSystem.current.SetSelectedGameObject(mapaMundi.btnVolta.gameObject);
                
            
        }

        if (other.tag == "Garagem")
        {
            colidiuNumber = 3;
            txt_Interagir.SetActive(true);
            if ((PlayerPrefs.GetInt("Idioma")) == 0)
            {
                txt_Interacao.SetText("Para entrar na Garagem");
            } else {
                txt_Interacao.SetText("To enter in the Garage");
            }
            
        }

        if (other.tag == "Save Game")
        {
            colidiuNumber = 4;
            txt_Interagir.SetActive(true);
            if ((PlayerPrefs.GetInt("Idioma")) == 0)
            {
                txt_Interacao.SetText("Para Salvar o Jogo");
            } else {
                txt_Interacao.SetText("To Save the Game");
            }
            
        }

        if (other.tag == "Exploracao")
        {
            colidiuNumber = 5;
            txt_Interagir.SetActive(true);
            if ((PlayerPrefs.GetInt("Idioma")) == 0)
            {
                txt_Interacao.SetText("Para Voltar para à Exploração");
            } else {
                txt_Interacao.SetText("To Back to the Exploration");
            }

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Conteiner")
        {

            player.conteinerInteracao = null;
            colidiuContainer = false;
            player.porcentagemBarraItem = 0;
            txt_Interagir.SetActive(false);
            player.barraPorcentagem.SetActive(false);
        }

        if (other.tag == "Barco")
        {
            player.conteinerInteracao = other.gameObject;
            txt_Interagir.SetActive(false);
            colidiuBarco = false;
            txt_Barco.SetActive(false);
        }

        if (other.tag == "Reciclagem")
        {
            txt_Interacao.SetText("");
            txt_Interagir.SetActive(false);
            colidiuNumber = 0;
        }

        if (other.tag == "Mapa")
        {
            txt_Interacao.SetText("");
            colidiuNumber = 0;
            txt_Interagir.SetActive(false);
            mapaMundi.btnVolta.SetActive(true);
            mapaMundi.mapinha.SetActive(true);
        }

        if (other.tag == "Garagem")
        {
            txt_Interacao.SetText("");
            txt_Interagir.SetActive(false);
            colidiuNumber = 0;
        }

        if (other.tag == "Exploracao")
        {
            colidiuNumber = 0;
            txt_Interagir.SetActive(false);
            txt_Interacao.SetText("");

        }

        if (other.tag == "Save Game")
        {
            colidiuNumber = 0;
            txt_Interagir.SetActive(false);

            txt_Interacao.SetText("");
        }

    }

}
