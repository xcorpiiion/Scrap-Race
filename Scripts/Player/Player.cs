using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.CharacterController;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    private Pecas pecas;
    public Image[] img_Engrenagens;
    public Color[] cor = new Color[3];
    private vThirdPersonMotor thirPerson; 
    public bool running = true; // verifica se estou correndo
    public float porcentagemCor; // mostra a porcentagem do alpha
    public Image porcentagemItem; //  mostra a porcentagem da barra
    public float porcentagemBarraItem = 0; // pega o scale da porcentagemItem
    public GameObject conteinerInteracao, barraPorcentagem, cameraPlayer;
    private ColidiuPlayer colidiuPlayer;
    private GameController gameController;
    private Loading loading;
    private vThirdPersonController inputs;
    Scene cena;
    public float carregarBarraItens;
    [Header("Pega os personagens")]
    public GameObject[] personagens;
    public Image img_Personagem;
    public Sprite[] sprite_Personagem;
    private TutorialGeral tutorialGeral;
    private ModoExploracao modoExploracao;


    private void Awake()
    {
        if(PlayerPrefs.GetInt("Personagem") == 0) {
            personagens[0].SetActive(true);
            personagens[1].SetActive(false);
            img_Personagem.sprite = sprite_Personagem[0];
        } else {
            personagens[0].SetActive(false);
            personagens[1].SetActive(true);
            img_Personagem.sprite = sprite_Personagem[1];
        }
    }

    // Use this for initialization
    void Start () {

        modoExploracao = FindObjectOfType(typeof(ModoExploracao)) as ModoExploracao;
        tutorialGeral = FindObjectOfType(typeof(TutorialGeral)) as TutorialGeral;
        loading = FindObjectOfType(typeof(Loading)) as Loading;

        inputs = FindObjectOfType(typeof(vThirdPersonController)) as vThirdPersonController;

        pecas = FindObjectOfType(typeof(Pecas)) as Pecas;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        
        colidiuPlayer = FindObjectOfType(typeof(ColidiuPlayer)) as ColidiuPlayer;

        cena = SceneManager.GetActiveScene();
        barraPorcentagem.SetActive(false);
        thirPerson = FindObjectOfType(typeof(vThirdPersonMotor)) as vThirdPersonMotor;
        for(int i = 0; i < img_Engrenagens.Length; i++) {
            cor[i] = img_Engrenagens[i].color;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void LateUpdate()
    {

        if (cena.name == "Mapa Exploracao")
        {
            if (Input.GetButton("Controle 01 L2") && running)
            {
                inputs.Sprint(running);
                running = true;
                tempoCorrida();
            }
            else if (!running)
            {
                thirPerson.freeSprintSpeed = 15;
                voltarCorrida();
                inputs.Sprint(running);
            } else {
                inputs.Sprint(false);
            }


            if(Input.GetButton("Controle 01 Bolinha") && colidiuPlayer.colidiuBarco) {
                colidiuPlayer.colidiuBarco = false;
                loading.carregarLoading("Cidade Rica");
                PlayerPrefs.SetInt("TutorialExploracao", 1);
            }

            if (Input.GetButton("Controle 01 Bolinha") && colidiuPlayer.colidiuContainer)
            {
                colidiuPlayer.txt_Interagir.SetActive(false);
                barraPorcentagem.SetActive(true);
                porcentagemBarraItem += carregarBarraItens * Time.deltaTime;
                porcentagemItem.transform.localScale = new Vector3(porcentagemBarraItem, 1, 1);
                if (porcentagemBarraItem >= 1)
                {
                    porcentagemBarraItem = 1;
                    if (conteinerInteracao.GetComponent<Containers>().normal)
                    {
                        for (int i = 0; i < conteinerInteracao.GetComponent<Containers>().itensNoConteiner.Length; i++)
                        {
                            gameController.itensNormais.Add(conteinerInteracao.GetComponent<Containers>().itensNoConteiner[i]);
                        }
                    }
                    else if (conteinerInteracao.GetComponent<Containers>().raro)
                    {
                        for (int i = 0; i < conteinerInteracao.GetComponent<Containers>().itensNoConteiner.Length; i++)
                        {
                            gameController.itensRaros.Add(conteinerInteracao.GetComponent<Containers>().itensNoConteiner[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < conteinerInteracao.GetComponent<Containers>().itensNoConteiner.Length; i++)
                        {
                            gameController.itensLendarios.Add(conteinerInteracao.GetComponent<Containers>().itensNoConteiner[i]);
                        }
                    }
                    porcentagemBarraItem = 0;
                    barraPorcentagem.SetActive(false);
                    conteinerInteracao.tag = "Untagged";
                    conteinerInteracao.GetComponent<Containers>().luz.SetActive(false);
                    conteinerInteracao = null;
                    colidiuPlayer.colidiuContainer = false;
                    colidiuPlayer.txt_Interagir.SetActive(false);
                    // faz aparecer o tutorial do container na tela
                    if(modoExploracao.tempo <= 0) {
                        modoExploracao.numeroContainerAtivo--;
                        modoExploracao.dialogoContainer();
                    }
                    if (!tutorialGeral.fezTutorialContainer)
                    {
                        tutorialGeral.contadorTexto = 4;
                        if (!tutorialGeral.coletouContainer)
                        {
                            tutorialGeral.canvasTutorial.SetActive(true);

                            tutorialGeral.canvasPlayer.SetActive(false);
                            tutorialGeral.txt_tutorialExplicacao.SetText(tutorialGeral.txt_explicacaoExploracao[4]);
                            gameController.changeState(GameState.INTERACAO);
                        }
                    }
                }
            }
        
        }

    }

    private void tempoCorrida() {


        if (cor[0].a >= 0) {
            cor[0].a -= 0.2f * Time.deltaTime;
            img_Engrenagens[0].color = cor[0];
        } else if (cor[1].a >= 0) {
            cor[1].a -= 0.2f * Time.deltaTime;
            img_Engrenagens[1].color = cor[1];
        } else if(cor[2].a >= 0) {
            cor[2].a -= 0.2f * Time.deltaTime;
            img_Engrenagens[2].color = cor[2];
        } else {
            running = false;
            for(int i = 0; i < cor.Length; i++) {
                cor[i].a = 0;
                img_Engrenagens[i].color = cor[i];
            }
        }

    }

    private void voltarCorrida() {
        if (cor[0].a < 1)
        {
            cor[0].a += 0.2f * Time.deltaTime;
            if (cor[0].a >= 1) {
                cor[0].a = 1;
            }
            img_Engrenagens[0].color = cor[0];
        }
        else if (cor[1].a < 1)
        {
            cor[1].a += 0.2f * Time.deltaTime;
            if (cor[1].a >= 1)
            {
                cor[1].a = 1;
            }
            img_Engrenagens[1].color = cor[1];
        }
        else if (cor[2].a < 1)
        {
            cor[2].a += 0.2f * Time.deltaTime;
            if (cor[1].a >= 1)
            {
                cor[1].a = 1;
            }
            img_Engrenagens[2].color = cor[2];
        } else {
            running = true;
            thirPerson.freeSprintSpeed = 22;
        }


    }
    
}
