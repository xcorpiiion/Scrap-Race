using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialGeral : MonoBehaviour {

    [Header("Tutorial Modo Exploração")]
    public GameObject[] controle;
    public GameObject canvasPlayer, canvasTutorial; // o canvas tutorial pega a caixinha e destiva. (é a caixinha onde fica os textos)
    private GameController gameController;
    private Scene cena;
    public bool fezTutorialContainer, fezTutorialTempo, fezTutorialReciclar, fezTutorialMapa;
    private GamepadCanvas gamepadCanvas;
    public string[] txt_explicacaoExploracao = new string[7];
    public TextMeshProUGUI txt_tutorialExplicacao;
    public int contadorTexto = 1; // contador do texto que serve para alterar o index do array dos textos
    public float tempoEspera;
    public bool podeApertar = true, fezTutorialControle, coletouContainer, tutorialCidade;
    private Dialogo dialogo;

    [Header("Tutorial Cidade")]
    public GameObject[] telasCidade;

    // Use this for initialization
    void Start () {
        dialogo = FindObjectOfType(typeof(Dialogo)) as Dialogo;
        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        cena = SceneManager.GetActiveScene();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;

        // verifica se eu fiz o tutorial
        if (cena.name == "Mapa Exploracao")
        {
            if (PlayerPrefs.GetInt("TutorialExploracao") == 0) // caso eu não tenha feito o tutorial, o numero fica 0
            {
                contadorTexto = 1;
                canvasPlayer.SetActive(false); // desativa o canvas geral do player caso não tenha feito
                fezTutorialControle = false;
                canvasTutorial.SetActive(true);
                coletouContainer = false;
                gameController.changeState(GameState.INTERACAO);

            } else {
                fezTutorialContainer = true;
                canvasTutorial.SetActive(false);
                fezTutorialTempo = true;
                fezTutorialControle = true;
            }
        } else if(cena.name == "Cidade Rica") {
            if (PlayerPrefs.GetInt("TutorialCidade") == 0)
            {
                tutorialCidade = false;
                telasCidade[0].SetActive(true);
                gameController.changeState(GameState.INTERACAO);
            }
            else
            {
                tutorialCidade = true;
                for (int i = 0; i < telasCidade.Length; i++)
                {
                    telasCidade[i].SetActive(false);
                }
                fezTutorialMapa = true;
                fezTutorialReciclar = true;
            }
        } else if(cena.name == "Mapa da Ponte") {
            if (PlayerPrefs.GetInt("TutorialCorrida") == 0)
            {
                fezTutorialControle = true;
                for (int i = 0; i < telasCidade.Length; i++) {
                    telasCidade[i].SetActive(false);
                }
            }
            else
            {
                fezTutorialControle = false;
                for (int i = 0; i < telasCidade.Length; i++)
                {
                    telasCidade[i].SetActive(false);
                }
            }
        }

        if (cena.name == "Mapa Exploracao")
        {

            if (PlayerPrefs.GetInt("Idioma") == 0)
            {
                txt_explicacaoExploracao[0] = "Nesse modo é onde você irá coletar itens para criar o seu proprio carro.";
                txt_tutorialExplicacao.SetText(txt_explicacaoExploracao[0]);
                txt_explicacaoExploracao[1] = "Neste modo de jogo, seu objetivo é explorar a Ilha e procurar CONTAINERS que contenha luzes caindo sobre eles.";
                txt_explicacaoExploracao[2] = "Os lixos pegos nos CONTAINERS serão reciclados para poder fazer peças para o seu carro.";
                txt_explicacaoExploracao[3] = "Agora vamos começar o jogo.";
                txt_explicacaoExploracao[4] = "Os itens que você coletou estão armazenados dentro da maquina de reciclar no mapa da cidade.";
                txt_explicacaoExploracao[5] = "O tempo acabou, mas você ainda pode explorar a Ilha e verificar se ainda tem CONTAINERS com itens ou apenas conhecer melhor a ilha.";
                txt_explicacaoExploracao[6] = "Você pode ir para a cidade à qualquer momento.\nBasta você ir até um barco para poder ir até o outro mapa.";

            }
            else
            {
                txt_explicacaoExploracao[0] = "In this mode is where you will collect items to create your own car.";
                txt_tutorialExplicacao.SetText(txt_explicacaoExploracao[0]);
                txt_explicacaoExploracao[1] = "In this game mode, your goal is to explore the Island and look for CONTAINERS that contains lights falling on them.";
                txt_explicacaoExploracao[2] = "The garbage collected in CONTAINERS will be recycled to make parts for your car.";
                txt_explicacaoExploracao[3] = "Now let's start the game.";
                txt_explicacaoExploracao[4] = "The items you have collected are stored inside the recycling machine on the city map.";
                txt_explicacaoExploracao[5] = "The time is up but you can still explore the Island and check if you still have CONTAINERS with items or just get to know the island better.";
                txt_explicacaoExploracao[6] = "You can go to town at any time. \nJust go to a boat to be able to go to the other map.";
            }
        }


    }

    private void LateUpdate()
    {
        if (cena.name == "Mapa Exploracao")
        {
            if (PlayerPrefs.GetInt("TutorialExploracao") == 0)
            {
                if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0) && controle[0].activeInHierarchy)
                {
                    controle[0].SetActive(false);
                    controle[1].SetActive(true);
                }

                if (Input.GetButton("Controle 01 X") && controle[1].activeInHierarchy)
                {
                    controle[1].SetActive(false);
                    controle[2].SetActive(true);
                }

                if (Input.GetButton("Controle 01 X") && canvasTutorial.activeInHierarchy && podeApertar)
                {
                    podeApertar = false;
                    if (contadorTexto >= 1 && contadorTexto <= 3)
                    {

                        gameController.changeState(GameState.INTERACAO);
                        txt_tutorialExplicacao.SetText(txt_explicacaoExploracao[contadorTexto]);
                        canvasPlayer.SetActive(false); // desativa o canvas geral do player caso não tenha feito

                        contadorTexto++;
                        if (contadorTexto >= 3)
                        {
                            contadorTexto = 10;


                        }
                    }
                    else if (contadorTexto == 4 && !coletouContainer)
                    {
                        gameController.changeState(GameState.GAMEPLAY);
                        contadorTexto = 9;
                        canvasTutorial.SetActive(false);
                        canvasPlayer.SetActive(true);
                        gameController.changeState(GameState.GAMEPLAY);

                        dialogo.numeroDialogo(1, 2); // pega o numero de texto que tem, e o numero de index
                        coletouContainer = true;
                    }
                    else if (contadorTexto >= 5 && contadorTexto <= 6)
                    {
                        gameController.changeState(GameState.INTERACAO);
                        txt_tutorialExplicacao.SetText(txt_explicacaoExploracao[contadorTexto]);
                        canvasPlayer.SetActive(false); // desativa o canvas geral do player caso não tenha feito
                        contadorTexto++;
                        if (contadorTexto > 6)
                        {
                            contadorTexto = 8;
                        }
                    }
                    else
                    {
                        canvasTutorial.SetActive(false);
                        canvasPlayer.SetActive(true);
                        gameController.changeState(GameState.GAMEPLAY);

                    }

                    StartCoroutine("tempoApertar");
                }

                if (Input.GetButton("Controle 01 L2") && controle[2].activeInHierarchy)
                {
                    controle[2].SetActive(false);
                    fezTutorialControle = true;
                    dialogo.numeroDialogo(0, 0); // pega o numero de texto que tem, e o numero de index
                }

                if (!fezTutorialControle && contadorTexto >= 10 && !canvasTutorial.activeInHierarchy)
                {
                    controle[0].SetActive(true);
                    contadorTexto = 9;
                }

            }

        } else if (cena.name == "Cidade Rica") {
            if(Input.GetButton("Controle 01 X") && podeApertar && !tutorialCidade) {
                podeApertar = false;
                if (telasCidade[0].activeInHierarchy)
                {
                    telasCidade[0].SetActive(false);
                    gameController.changeState(GameState.GAMEPLAY);
                    dialogo.numeroDialogo(5, 6);
                }
                else if (telasCidade[contadorTexto].activeInHierarchy && contadorTexto <= 4)
                {
                    contadorTexto++;
                    telasCidade[contadorTexto].SetActive(true);
                    telasCidade[contadorTexto - 1].SetActive(false); // desativa a tela anterior
                    if (contadorTexto > 4)
                    {
                        telasCidade[contadorTexto].SetActive(false);
                        gamepadCanvas.isCanvas = true;
                        contadorTexto = 0;
                    }
                }
                else if (telasCidade[5].activeInHierarchy && contadorTexto == 5)
                {

                    telasCidade[5].SetActive(false);
                    telasCidade[6].SetActive(true);
                    contadorTexto++;

                }
                else if (telasCidade[6].activeInHierarchy && contadorTexto == 6)
                {
                    telasCidade[6].SetActive(false);
                    gamepadCanvas.isCanvas = true;
                    contadorTexto = 0;
                }
                StartCoroutine("tempoApertar");
            }
        } else if(cena.name == "Mapa da Ponte") {
            if (PlayerPrefs.GetInt("Fazer tuturial") == 1)
            {
                telasCidade[0].SetActive(true);
                fezTutorialControle = true;
                PlayerPrefs.SetInt("Fazer tuturial", 0);
            }

            if (fezTutorialControle)
            {
                if (Input.GetButton("Controle 01 R2") && telasCidade[0].activeInHierarchy)
                {
                    telasCidade[0].SetActive(false);
                    telasCidade[1].SetActive(true);
                }

                if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0) && telasCidade[1].activeInHierarchy)
                {
                    telasCidade[1].SetActive(false);
                    telasCidade[2].SetActive(true);
                }

                if (Input.GetButton("Controle 01 Bolinha") && telasCidade[2].activeInHierarchy)
                {
                    telasCidade[2].SetActive(false);
                    telasCidade[3].SetActive(true);
                }

                if (Input.GetButton("Controle 01 X") && telasCidade[3].activeInHierarchy)
                {
                    telasCidade[3].SetActive(false);
                    PlayerPrefs.SetInt("TutorialCorrida", 1);
                    fezTutorialControle = false;
                    PlayerPrefs.SetInt("Fazer tuturial", 0);
                    dialogo.numeroDialogo(7, 8);
                    this.gameObject.GetComponent<TutorialGeral>().enabled = false;

                }
            }
            

        }

    }


    IEnumerator tempoApertar()
    {
        yield return new WaitForSecondsRealtime(tempoEspera);
        podeApertar = true;

    }

}
