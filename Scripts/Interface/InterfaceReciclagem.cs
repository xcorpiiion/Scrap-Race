using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent (typeof(AudioSource))]
public class InterfaceReciclagem : MonoBehaviour
{

    private ItensCarro itensCarro;
    private Pecas pecas;
    private GameController gameController;

    private GamepadCanvas gamepadCanvas;

    private LiberarCorrida liberarCorrida;

    // pega as cores dos buttons e aplica a mudança de cor
    [Header("Pega as cores do background e muda de acordo com a raridade")]
    public Color[] corRaridade;

    [Header("Pega as imagens do backgrond")]
    public Image[] imgBackground;
    private byte numBtnApertado = 0;

    // pega os itens que serão reciclados e jogam nessa variavel
    public int[] numeroFerroReciclar = new int[3], numeroAcoReciclar = new int[3], numeroAluminioReciclar = new int[3], numBorrachaReciclar = new int[3];

    private Player player;

    [Header("Pega os textos e muda eles de acordo com o numero de itens pegos")]
    public TextMeshProUGUI txtFerro, txtAco, txtAluminio, txtBorracha;

    // pega o nome dos textos
    public TextMeshProUGUI txtFerroReciclagem, txtAcoReciclagem, txtAluminioReciclagem, txtBorrachaReciclagem, txt_Mensagem;

    // pega os textos das informações do item e joga nas variaveis
    public TextMeshProUGUI txt_Descricao, txt_NomeItem;

    // verifica se eu posso aperta o botão
    private bool podeApertarButton = false;

    [Header ("Pega os elementos da tela te desativa")]
    public GameObject cameraPlayer, personagem, cameraCanvas, canvas, canvasMapa, cameraMapa, informacaoItem, canvasReciclarPanel;

    [Header ("Ajusta o peso das peças")]
    public int[] pesoFerro = new int[3], pesoAco = new int[3], pesoAluminio = new int[3], pesoBorracha = new int[3];

    public GameObject txt_Aviso;

    public GameObject botaoRecilar, botaoVoltar, botaoSair; // serve para eu selecionar o botao

    private int random;
    public float tempoApertar;
    private bool podeApertar = true;

    [Header("Armazena os nomes dos carros")]
    public string[] nomeCarro;

    public AudioClip sound_Back;
    private AudioSource audioSource;

    [Header("Armazena os personagens")]
    public GameObject adan;
    public GameObject selena;


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;

        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;

        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        imgBackground[0].color = corRaridade[0];
        imgBackground[1].color = corRaridade[0];

        txt_Aviso.SetActive(false);

        Time.timeScale = 1;
        player = FindObjectOfType(typeof(Player)) as Player;
        itensCarro = FindObjectOfType(typeof(ItensCarro)) as ItensCarro;

        pecas = FindObjectOfType(typeof(Pecas)) as Pecas;

        numBtnApertado = 1;


        if (PlayerPrefs.GetInt("Personagem") == 0)
        {
            personagem = adan;
        }
        else
        {
            personagem = selena;
        }
        personagem.SetActive(true);

        txtAco.SetText("0");
        txtAluminio.SetText("0");
        txtBorracha.SetText("0");
        txtFerro.SetText("0");

        txtAcoReciclagem.SetText("0");
        txtAluminioReciclagem.SetText("0");
        txtBorrachaReciclagem.SetText("0");
        txtFerroReciclagem.SetText("0");

        txt_Mensagem.SetText("");


        txtFerro.SetText(pecas.numeroFerro[0].ToString());
        txtAco.SetText(pecas.numeroAco[0].ToString());
        txtAluminio.SetText(pecas.numeroAluminio[0].ToString());
        txtBorracha.SetText(pecas.numBorracha[0].ToString());


        foreach (string raro in gameController.itensRaros)
        {
            switch (raro)
            {
                case "Ferro":
                    pecas.numeroFerro[0]++;
                    break;
                case "Aco":
                    pecas.numeroAco[0]++;
                    break;
                case "Aluminio":
                    pecas.numeroAluminio[0]++;
                    break;
                case "Borracha":
                    pecas.numBorracha[0]++;
                    break;
            }
        }


        foreach (string raro in gameController.itensRaros)
        {
            switch (raro)
            {
                case "Ferro":
                    pecas.numeroFerro[1]++;
                    break;
                case "Aco":
                    pecas.numeroAco[1]++;
                    break;
                case "Aluminio":
                    pecas.numeroAluminio[1]++;
                    break;
                case "Borracha":
                    pecas.numBorracha[1]++;
                    break;
            }
        }

        foreach (string lendario in gameController.itensLendarios)
        {
            switch (lendario)
            {
                case "Ferro":
                    pecas.numeroFerro[2]++;
                    break;
                case "Aco":
                    pecas.numeroAco[2]++;
                    break;
                case "Aluminio":
                    pecas.numeroAluminio[2]++;
                    break;
                case "Borracha":
                    pecas.numBorracha[2]++;
                    break;
            }
        }

    }

    // remove os itens do inventario 
    private void LateUpdate()
    {
        for (int i = 0; i < gameController.itensLendarios.Count; i++)
        {
            gameController.itensLendarios.RemoveAt(i);
        }

        for (int i = 0; i < gameController.itensNormais.Count; i++)
        {
            gameController.itensNormais.RemoveAt(i);
        }

        for (int i = 0; i < gameController.itensRaros.Count; i++)
        {
            gameController.itensRaros.RemoveAt(i);
        }


        if (Input.GetButton("Controle 01 Bolinha") && podeApertar && canvas.activeInHierarchy)
        {
            podeApertar = false;
            audioSource.PlayOneShot(sound_Back);
            if (informacaoItem.activeInHierarchy)
            {
                canvasReciclarPanel.SetActive(true);
                informacaoItem.SetActive(false);
                EventSystem.current.SetSelectedGameObject(botaoRecilar);
            } else {
                EventSystem.current.SetSelectedGameObject(botaoSair);
            }
            StartCoroutine("tempoEspera");
        }


    }

    public void btnNormal()
    {
        if (!podeApertarButton)
        {
            numBtnApertado = 1; // indica que o btn normal foi apertado
            imgBackground[0].color = corRaridade[0];
            imgBackground[1].color = corRaridade[0];
            txtFerro.SetText(pecas.numeroFerro[0].ToString());
            txtBorracha.SetText(pecas.numBorracha[0].ToString());
            txtAluminio.SetText(pecas.numeroAluminio[0].ToString());
            txtAco.SetText(pecas.numeroAco[0].ToString());
        }

    }

    public void btnRaro()
    {
        if (!podeApertarButton)
        {
            numBtnApertado = 2; // indica que o btn raro foi apertado
            imgBackground[0].color = corRaridade[1];
            imgBackground[1].color = corRaridade[1];
            txtFerro.SetText(pecas.numeroFerro[1].ToString());
            txtBorracha.SetText(pecas.numBorracha[1].ToString());
            txtAluminio.SetText(pecas.numeroAluminio[1].ToString());
            txtAco.SetText(pecas.numeroAco[1].ToString());
        }
    }

    public void btnLendario()
    {
        if (podeApertarButton == false)
        {
            numBtnApertado = 3; // indica que o btn normal foi apertado
            imgBackground[0].color = corRaridade[2];
            imgBackground[1].color = corRaridade[2];
            txtFerro.SetText(pecas.numeroFerro[2].ToString());
            txtBorracha.SetText(pecas.numBorracha[2].ToString());
            txtAluminio.SetText(pecas.numeroAluminio[2].ToString());
            txtAco.SetText(pecas.numeroAco[2].ToString());
        }
    }

    public void sair() {

        if(numeroAcoReciclar[0] <= 0 && numeroFerroReciclar[0] <= 0 && numeroAluminioReciclar[0] <= 0 && numBorrachaReciclar[0] <= 0) {
            if(numeroAcoReciclar[1] <= 0 && numeroFerroReciclar[1] <= 0 && numeroAluminioReciclar[1] <= 0 && numBorrachaReciclar[1] <= 0) {
                if(numeroAcoReciclar[2] <= 0 && numeroFerroReciclar[2] <= 0 && numeroAluminioReciclar[2] <= 0 && numBorrachaReciclar[2] <= 0) {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    canvas.SetActive(false);
                    cameraCanvas.SetActive(false);
                    personagem.SetActive(true);
                    cameraPlayer.SetActive(true);
                    canvasMapa.SetActive(false);
                    cameraMapa.SetActive(false);
                    txt_Aviso.SetActive(false);
                    gamepadCanvas.isCanvas = false;
                    Time.timeScale = 0;
                    gameController.changeState(GameState.GAMEPLAY);

                } else {
                    txt_Aviso.SetActive(true);
                    txt_Mensagem.SetText("");
                    StartCoroutine("mensagemAviso");
                }
            } else {
                txt_Aviso.SetActive(true);
                txt_Mensagem.SetText("");
                StartCoroutine("mensagemAviso");
            }
        } else {
            txt_Aviso.SetActive(true);
            txt_Mensagem.SetText("");
            StartCoroutine("mensagemAviso");
        }

    }

    /// ///////////////////////////////////////////////////////////////////////////////////
    ///  responsavel pelos buttons de mais e menos

        
    public void btnMaisFerro()
    {
        if (numBtnApertado == 1) // verifica a raridade do item
        {
            if (pecas.numeroFerro[0] > 0) // pega o item normal e verifica se ele tem algum item dentro
            {
                podeApertarButton = true; // não posso abertar o button de raridade
                numeroFerroReciclar[0]++; // adiciona um item no menu de reciclar
                txtFerroReciclagem.SetText(numeroFerroReciclar[0].ToString()); // altera o texto

                pecas.numeroFerro[0]--; // tira um item dos que eu tenho coletado
                txtFerro.SetText(pecas.numeroFerro[0].ToString()); // altera o texto

            }


        }
        else if (numBtnApertado == 2)
        {
            if (pecas.numeroFerro[1] > 0)
            {
                podeApertarButton = true;
                numeroFerroReciclar[1] ++;
                txtFerroReciclagem.SetText(numeroFerroReciclar[1].ToString());

                pecas.numeroFerro[1]--;
                txtFerro.SetText(pecas.numeroFerro[1].ToString());
            }


        }
        else
        {
            if (pecas.numeroFerro[2] > 0)
            {
                podeApertarButton = true;
                numeroFerroReciclar[2]++;
                txtFerroReciclagem.SetText(numeroFerroReciclar[2].ToString());

                pecas.numeroFerro[2]--;
                txtFerro.SetText(pecas.numeroFerro[2].ToString());
            }

        }

    }

    public void btnMenosFerro()
    {
        if (numBtnApertado == 1) // verifica a raridade do item
        {
            if (numeroFerroReciclar[0] > 0) // verifica se tem algum item dentro do menu de reciclar
            {
                numeroFerroReciclar[0]--; // tira um item do menu de reclicar
                txtFerroReciclagem.SetText(numeroFerroReciclar[0].ToString()); // altera o texto

                pecas.numeroFerro[0]++; // aumenta um item no menu de itens coletaveis
                txtFerro.SetText(pecas.numeroFerro[0].ToString()); // altera o texto
                if (numeroFerroReciclar[0] <= 0) // verifica se o menu de reciclar tem nenhum item
                {
                    podeApertarButton = false; // posso apertar o button
                }
            }
        }
        else if (numBtnApertado == 2)
        {
            if (numeroFerroReciclar[1] > 0)
            {
                numeroFerroReciclar[1]--;
                txtFerroReciclagem.SetText(numeroFerroReciclar[1].ToString());

                pecas.numeroFerro[1]++;
                txtFerro.SetText(pecas.numeroFerro[1].ToString());

                if (numeroFerroReciclar[1] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else
        {
            if (numeroFerroReciclar[2] > 0)
            {
                numeroFerroReciclar[2]--;
                txtFerroReciclagem.SetText(numeroFerroReciclar[2].ToString());

                pecas.numeroFerro[2]++;
                txtFerro.SetText(pecas.numeroFerro[2].ToString());

                if (numeroFerroReciclar[2] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }


    }

    public void btnMaisAco()
    {
        if (numBtnApertado == 1)
        {
            if (pecas.numeroAco[0] > 0)
            {
                numeroAcoReciclar[0] ++;
                txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString());

                podeApertarButton = true;

                pecas.numeroAco[0]--;
                txtAco.SetText(pecas.numeroAco[0].ToString());
            }
        }
        else if (numBtnApertado == 2)
        {
            if (pecas.numeroAco[1] > 0)
            {
                numeroAcoReciclar[1] ++;
                txtAcoReciclagem.SetText(numeroAcoReciclar[1].ToString());

                podeApertarButton = true;

                pecas.numeroAco[1]--;
                txtAco.SetText(pecas.numeroAco[1].ToString());

            }
        }
        else
        {
            if (pecas.numeroAco[2] > 0)
            {
                numeroAcoReciclar[2] ++;
                txtAcoReciclagem.SetText(numeroAcoReciclar[2].ToString());

                podeApertarButton = true;

                pecas.numeroAco[2]--;
                txtAco.SetText(pecas.numeroAco[2].ToString());
            }
        }

    }

    public void btnMenosAco()
    {
        if (numBtnApertado == 1)
        {
            if (numeroAcoReciclar[0] > 0)
            {
                numeroAcoReciclar[0]--;
                txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString());

                pecas.numeroAco[0]++;
                txtAco.SetText(pecas.numeroAco[0].ToString());
                if (numeroAcoReciclar[0] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else if (numBtnApertado == 2)
        {
            if (numeroAcoReciclar[1] > 0)
            {
                numeroAcoReciclar[1]--;
                txtAcoReciclagem.SetText(numeroAcoReciclar[1].ToString());

                pecas.numeroAco[1]++;
                txtAco.SetText(pecas.numeroAco[1].ToString());
                if (numeroAcoReciclar[1] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else
        {
            if (numeroAcoReciclar[2] > 0)
            {
                numeroAcoReciclar[2]--;
                txtAcoReciclagem.SetText(numeroAcoReciclar[2].ToString());

                pecas.numeroAco[2]++;
                txtAco.SetText(pecas.numeroAco[2].ToString());
                if (numeroAcoReciclar[2] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }


    }

    public void btnMaisAluminio()
    {
        if (numBtnApertado == 1)
        {
            if (pecas.numeroAluminio[0] > 0)
            {
                numeroAluminioReciclar[0] ++;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString());

                podeApertarButton = true;

                pecas.numeroAluminio[0]--;
                txtAluminio.SetText(pecas.numeroAluminio[0].ToString());
            }
        }
        else if (numBtnApertado == 2)
        {
            if (pecas.numeroAluminio[1] > 0)
            {
                numeroAluminioReciclar[1]++;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[1].ToString());

                podeApertarButton = true;

                pecas.numeroAluminio[1]--;
                txtAluminio.SetText(pecas.numeroAluminio[1].ToString());
            }
        }
        else
        {
            if (pecas.numeroAluminio[2] > 0)
            {
                numeroAluminioReciclar[2]++;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[2].ToString());

                podeApertarButton = true;

                pecas.numeroAluminio[2]--;
                txtAluminio.SetText(pecas.numeroAluminio[2].ToString());
            }
        }

    }

    public void btnMenosAluminio()
    {
        if (numBtnApertado == 1)
        {
            if (numeroAluminioReciclar[0] > 0)
            {
                numeroAluminioReciclar[0]--;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString());

                pecas.numeroAluminio[0]++;
                txtAluminio.SetText(pecas.numeroAluminio[0].ToString());
                if (numeroAluminioReciclar[0] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else if (numBtnApertado == 2)
        {
            if (numeroAluminioReciclar[1] > 0)
            {
                numeroAluminioReciclar[1]--;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[1].ToString());

                pecas.numeroAluminio[1]++;
                txtAluminio.SetText(pecas.numeroAluminio[1].ToString());
                if (numeroAluminioReciclar[1] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else
        {
            if (numeroAluminioReciclar[2] > 0)
            {
                numeroAluminioReciclar[2]--;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[2].ToString());

                pecas.numeroAluminio[2]++;
                txtAluminio.SetText(pecas.numeroAluminio[2].ToString());
                if (numeroAluminioReciclar[2] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
    }

    public void btnMaisBorracha()
    {
        if (numBtnApertado == 1)
        {
            if (pecas.numBorracha[0] > 0)
            {
                numBorrachaReciclar[0] ++;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[0].ToString());

                podeApertarButton = true;

                pecas.numBorracha[0]--;
                txtBorracha.SetText(pecas.numBorracha[0].ToString());
            }
        }
        else if (numBtnApertado == 2)
        {
            if (pecas.numBorracha[1] > 0)
            {
                numBorrachaReciclar[1]++;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[1].ToString());

                podeApertarButton = true;

                pecas.numBorracha[1]--;
                txtBorracha.SetText(pecas.numBorracha[1].ToString());
            }
        }
        else
        {
            if (pecas.numBorracha[2] > 0)
            {
                numBorrachaReciclar[2]++;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[2].ToString());

                podeApertarButton = true;

                pecas.numBorracha[2]--;
                txtBorracha.SetText(pecas.numBorracha[2].ToString());
            }
        }

    }

    public void btnMenosBorracha()
    {
        if (numBtnApertado == 1)
        {
            if (numBorrachaReciclar[0] > 0)
            {
                numBorrachaReciclar[0]--;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[0].ToString());

                pecas.numBorracha[0]++;
                txtBorracha.SetText(pecas.numBorracha[0].ToString());
                if (numeroAluminioReciclar[0] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else if (numBtnApertado == 2)
        {
            if (numBorrachaReciclar[1] > 0)
            {
                numBorrachaReciclar[1]--;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[0].ToString());

                pecas.numBorracha[1]++;
                txtBorracha.SetText(pecas.numBorracha[1].ToString());
                if (numeroAluminioReciclar[1] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }
        else
        {
            if (numBorrachaReciclar[2] > 0)
            {
                numBorrachaReciclar[2]--;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[2].ToString());

                pecas.numBorracha[2]++;
                txtBorracha.SetText(pecas.numBorracha[2].ToString());
                if (numeroAluminioReciclar[2] <= 0)
                {
                    podeApertarButton = false;
                }
            }
        }

    }

    //**************************************************
    // fim do buttons mais e menos

    #region responsavel por reciclar os itens
    // resposnsavel pelo button de reciclar
    public void btnReciclar() {
        if (numBtnApertado == 1)
        {

            // formulas para criar os itens
            if (numeroAluminioReciclar[0] > 0 && numeroAcoReciclar[0] > 0 && numeroFerroReciclar[0] > 0 && numBorrachaReciclar[0] <= 0)
            {
                if (itensCarro.motorNormal.Count < 7)
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você criou um MOTOR NORMAL");
                    } else {
                        txt_Mensagem.SetText("You created a NORMAL ENGINE");
                    }
                    StartCoroutine("mensagemAviso");
                    print("criou um motor normal");
                    // cria os bonuts do motor
                    int bonusPeso = 0, bonusAceleracao = 0, bonusResistencia = 0;
                    itensCarro.motorNormal.Add("Motor");

                    // da o bonus ao peso do carro
                    // faz o calculo da porcentagem
                    bonusPeso = (pesoAco[0] * numeroAcoReciclar[0]) + (pesoAluminio[0] * numeroAluminioReciclar[0]) + (pesoFerro[0] * numeroFerroReciclar[0]);
                    float receberBonusPeso = (8 * numeroAcoReciclar[0]); // faz o calculo da porcentagem
                    receberBonusPeso /= 100; // divide pela porcentagem
                    int receberResultadoPeso = (int)(bonusPeso * receberBonusPeso); // pega o valor inteiro da soma

                    // add ao carro o peso junto com o bonus
                    itensCarro.pesoMotorNormal.Add(bonusPeso + receberResultadoPeso);

                    // da o bonus a resistencia do carro
                    bonusResistencia = (((pesoAco[0] * numeroAcoReciclar[0]) + (pesoAluminio[0] * numeroAluminioReciclar[0]) + (pesoFerro[0] * numeroFerroReciclar[0])) / 2);
                    float receberBonusResistencia = (8 * numeroAluminioReciclar[0]); // faz o calculo da porcentagem
                    receberBonusResistencia /= 100; // divide pela porcentagem
                    int receberResultadoResistencia = (int)(bonusResistencia * receberBonusResistencia); // pega o valor inteiro da soma

                    // add ao carro a resistencia junto com o bonus
                    itensCarro.resistenciaMotorNormal.Add(bonusResistencia + receberResultadoResistencia);

                    // da o bonus a aceletacao do carro
                    bonusAceleracao = 500;
                    float receberBonusAceleracao = (5 * numeroFerroReciclar[0]); // faz o calculo da porcentagem
                    receberBonusAceleracao /= 100; // divide pela porcentagem
                    int receberResultadoAceleracao = (int)(bonusAceleracao * receberBonusAceleracao); // pega o valor inteiro da soma

                    // add ao carro a aceleracao junto com o bonus
                    itensCarro.aceleracaoMotorNormal.Add(bonusAceleracao + receberResultadoAceleracao);

                    podeApertarButton = false; // posso apertar o button
                    numeroAcoReciclar[0] = 0; // zera o numero de acos usado
                    txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString()); // altera o texto
                    numeroAluminioReciclar[0] = 0; // zera o numero de aluminio usado
                    txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString()); // altera o texto
                    numeroFerroReciclar[0] = 0; // zera o numero de ferros usado
                    txtFerroReciclagem.SetText(numeroFerroReciclar[0].ToString()); // altera o texto
                }
                else
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você atingiu o número maximo de MOTOR NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You have reached the maximum number of NORMAL ENGINE");
                    }
                    
                    StartCoroutine("mensagemAviso");
                }

            }
            else if (numeroAluminioReciclar[0] > 0 && numeroAcoReciclar[0] > 0 && numBorrachaReciclar[0] <= 0 && numeroFerroReciclar[0] <= 0)
            {
                if (itensCarro.transmissaoNormal.Count < 7)
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você criou uma TRANSMISSÃO NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You created a NORMAL TRANSMISSION");
                    }
                    StartCoroutine("mensagemAviso");
                    print("criou um transmissao normal");
                    // cria os bonuts do motor
                    int bonusAceleracao = 0, bonusFreio = 0;
                    itensCarro.transmissaoNormal.Add("Transmissão");

                    // add ao carro o peso 
                    itensCarro.pesoTransmissaoNormal.Add((pesoAco[0] * numeroAcoReciclar[0]) + (pesoAluminio[0] * numeroAluminioReciclar[0]));

                    // da bonus ao freio do carro
                    bonusFreio = 500;

                    // faz o calculo da porcentagem
                    float testeFreio = (5 * numeroAluminioReciclar[0]);
                    testeFreio /= 100;
                    int testeFreio2 = (int)(bonusFreio * testeFreio);

                    // aplica o bonus no freio do carro e soma com o freio atual
                    itensCarro.FreioTransmissaoNormal.Add(bonusFreio + testeFreio2);

                    // da o bonus a aceletacao do carro
                    bonusAceleracao = 500;
                    // add ao carro a aceleracao junto com o bonus
                    float teste = (8 * numeroAcoReciclar[0]);
                    teste /= 100;
                    int teste2 = (int) (bonusAceleracao * teste);
                    itensCarro.aceleracaoTransmissaoNormal.Add(bonusAceleracao + teste2);

                    podeApertarButton = false; // posso apertar o button
                    numeroAcoReciclar[0] = 0;
                    txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString());
                    numeroAluminioReciclar[0] = 0;
                    txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString());
                }
                else
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você atingiu o número maximo de TRANSMISSÃO NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You have reached the maximum number of NORMAL TRANSMISSION");
                    }
                    StartCoroutine("mensagemAviso");
                }

            }
            else if (numeroAluminioReciclar[0] > 0 && numeroAcoReciclar[0] > 0 && numBorrachaReciclar[0] > 0 && numeroFerroReciclar[0] > 0)
            {
                if (itensCarro.chassiNormal.Count < 7)
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você criou uma CHASSI NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You created a NORMAL BODY");
                    }
                    StartCoroutine("mensagemAviso");
                    print("criou um chassi normal");
                    itensCarro.chassiNormal.Add("Chassi");

                    random = Random.Range(0, nomeCarro.Length);

                    itensCarro.carrosNormais.Add(nomeCarro[random]);

                    // cria os bonuts do motor
                    int bonusAceleracao = 0, bonusPeso = 0, bonusResistencia = 0;
                   
                    bonusPeso = ((pesoAco[0] * numeroAcoReciclar[0]) + (pesoAluminio[0] * numeroAluminioReciclar[0]) + (pesoBorracha[0] * numBorrachaReciclar[0]) + (pesoFerro[0] * numeroFerroReciclar[0])) / 2;
                    float receberBonusPeso = (5 * numeroFerroReciclar[0]); // faz o calculo da porcentagem
                    receberBonusPeso /= 100; // divide pela porcentagem
                    int receberResultadoPeso = (int)(bonusPeso * receberBonusPeso) + (bonusPeso * (5 * numeroAluminioReciclar[0]) / 100); // pega o valor inteiro da soma

                    // add ao carro o peso 
                    itensCarro.pesoChassiNormal.Add(bonusPeso + receberResultadoPeso);

                    // da bonus ao freio do carro
                    bonusResistencia = ((pesoAco[0] * numeroAcoReciclar[0]) + (pesoAluminio[0] * numeroAluminioReciclar[0]) + (pesoFerro[0] * numeroFerroReciclar[0]) + (pesoBorracha[0] * numBorrachaReciclar[0])) / 2;

                    // aplica o bonus no freio do carro e soma com o freio atual
                    itensCarro.freioChassiNormal.Add(bonusResistencia + (bonusResistencia * ((10 * numeroAcoReciclar[0]) / 100)));

                    // da o bonus a aceletacao do carro
                    bonusAceleracao = 500;
                    float receberBonusAceleracao = (5 * numeroFerroReciclar[0]); // faz o calculo da porcentagem
                    receberBonusAceleracao /= 100; // divide pela porcentagem
                    int receberResultadoAceleracao = (int)(bonusAceleracao * receberBonusAceleracao); // pega o valor inteiro da soma

                    // add ao carro a aceleracao junto com o bonus
                    itensCarro.aceleracaoChassiNormal.Add(bonusAceleracao + receberResultadoAceleracao);

                    podeApertarButton = false; // posso apertar o button
                    numeroAcoReciclar[0] = 0;
                    txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString());
                    numeroAluminioReciclar[0] = 0;
                    txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString());
                    numeroFerroReciclar[0] = 0;
                    txtFerroReciclagem.SetText(numeroFerroReciclar[0].ToString());
                    numBorrachaReciclar[0] = 0;

                    txtBorrachaReciclagem.SetText(numBorrachaReciclar[0].ToString());
                }
                else
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você atingiu o número maximo de CHASSI NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You have reached the maximum number of NORMAL BODY");
                    }
                    StartCoroutine("mensagemAviso");
                }
            }
            else if (numeroAluminioReciclar[0] <= 0 && numeroAcoReciclar[0] <= 0 && numBorrachaReciclar[0] > 0 && numeroFerroReciclar[0] > 0)
            {
                if (itensCarro.pneuNormal.Count < 7)
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você criou uma PNEU NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You created a NORMAL WHEEL");
                    }
                    StartCoroutine("mensagemAviso");
                    print("criou um pneu normal");
                    itensCarro.pneuNormal.Add("Pneu");

                    int bonusResistencia = 0, bonusFreio = 0;
                    // da o bonus ao peso do carro

                    itensCarro.pesoPneuNormal.Add((pesoBorracha[0] * numBorrachaReciclar[0]) + (pesoFerro[0] * numeroFerroReciclar[0]));

                    // da bonus ao freio do carro
                    bonusFreio = 500;

                    // aplica o bonus no freio do carro e soma com o freio atual
                    itensCarro.freioPneuNormal.Add(bonusFreio + (bonusFreio * ((5 * numBorrachaReciclar[0]) / 100)));

                    // da o bonus a aceletacao do carro
                    bonusResistencia = ((pesoFerro[0] * numeroFerroReciclar[0]) + (pesoBorracha[0] * numBorrachaReciclar[0])) / 2;
                    // add ao carro a aceleracao junto com o bonus
                    itensCarro.resistenciaPneuNormal.Add(bonusResistencia + (bonusResistencia * ((10 * numeroFerroReciclar[0]) / 100)));

                    podeApertarButton = false; // posso apertar o button
                    numeroAcoReciclar[0] = 0;
                    txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString());
                    numeroAluminioReciclar[0] = 0;
                    txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString());
                    numeroFerroReciclar[0] = 0;
                    txtFerroReciclagem.SetText(numeroFerroReciclar[0].ToString());
                    numBorrachaReciclar[0] = 0;
                    txtBorrachaReciclagem.SetText(numBorrachaReciclar[0].ToString());
                }
                else
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você atingiu o número maximo de PNEU NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You have reached the maximum number of NORMAL WHEEL");
                    }
                    StartCoroutine("mensagemAviso");
                }
            }
            else if (numeroAcoReciclar[0] > 0 && numeroFerroReciclar[0] > 0 && numeroAluminioReciclar[0] <= 0 && numBorrachaReciclar[0] <= 0)
            {
                if (itensCarro.freioChassiNormal.Count < 7)
                {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você criou um FREIO NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You created a NORMAL BRAKE");
                    }
                    StartCoroutine("mensagemAviso");
                    print("criou um freio normal");
                    itensCarro.freioNormal.Add("Freio");

                    // add bonus ao freio
                    int bonusResistencia = 0, bonusFreio = 0;


                    // da bonus ao freio do carro
                    bonusFreio = 500;

                    // aplica o bonus no freio do carro e soma com o freio atual
                    itensCarro.forcaFreioNormal.Add(bonusFreio + (bonusFreio * ((8 * numeroAcoReciclar[0]) / 100)));

                    // add peso ao freio
                    itensCarro.pesoFreioNormal.Add((numeroAcoReciclar[0] * pesoAco[0]) + (pesoFerro[0] * numeroFerroReciclar[0]));

                    // da o bonus a aceletacao do carro
                    bonusResistencia = ((pesoFerro[0] * numeroFerroReciclar[0]) + (pesoAco[0] * numeroAcoReciclar[0])) / 2;
                    // add ao carro a aceleracao junto com o bonus
                    itensCarro.resistenciaFreioNormal.Add(bonusResistencia + (bonusResistencia * ((10 * numeroFerroReciclar[0]) / 100)));

                    podeApertarButton = false; // posso apertar o button
                    numeroAcoReciclar[0] = 0;
                    txtAcoReciclagem.SetText(numeroAcoReciclar[0].ToString());
                    numeroAluminioReciclar[0] = 0;
                    txtAluminioReciclagem.SetText(numeroAluminioReciclar[0].ToString());
                    numeroFerroReciclar[0] = 0;
                    txtFerroReciclagem.SetText(numeroFerroReciclar[0].ToString());
                    numBorrachaReciclar[0] = 0;
                    txtBorrachaReciclagem.SetText(numBorrachaReciclar[0].ToString());
                }
                else
                {

                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Mensagem.SetText("Você atingiu o número maximo de FREIO NORMAL");
                    }
                    else
                    {
                        txt_Mensagem.SetText("You have reached the maximum number of NORMAL BRAKE");
                    }
                    StartCoroutine("mensagemAviso");
                }
            }

        }
        else if (numBtnApertado == 2 && liberarCorrida.reciclarCarroRaro)
        { // so posso usar esse carro, caso eu tenha liberado eles com a corrida
            // formulas para criar os itens
            if (numeroAluminioReciclar[1] > 0 && numeroAcoReciclar[1] > 0 && numeroFerroReciclar[1] > 0 && numBorrachaReciclar[1] <= 0)
            {
                print("criou um motor raro");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[1] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[1].ToString());
                numeroAluminioReciclar[1] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[1].ToString());
                numeroFerroReciclar[1] = 0;
                txtFerroReciclagem.SetText(numeroFerroReciclar[1].ToString());
            }
            else if (numeroAluminioReciclar[1] > 0 && numeroAcoReciclar[1] > 0 && numBorrachaReciclar[1] <= 0 && numeroFerroReciclar[1] <= 0)
            {
                print("criou um transmissor raro");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[1] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[1].ToString());
                numeroAluminioReciclar[1] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[1].ToString());
            }
            else if (numeroAluminioReciclar[1] > 0 && numeroAcoReciclar[1] > 0 && numBorrachaReciclar[1] > 0 && numeroFerroReciclar[1] > 0)
            {
                print("criou um chassi raro");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[1] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[1].ToString());
                numeroAluminioReciclar[1] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[1].ToString());
                numeroFerroReciclar[1] = 0;
                txtFerroReciclagem.SetText(numeroFerroReciclar[1].ToString());
                numBorrachaReciclar[1] = 0;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[1].ToString());
            }
            else if (numeroAluminioReciclar[1] <= 0 && numeroAcoReciclar[1] <= 0 && numBorrachaReciclar[1] > 0 && numeroFerroReciclar[1] > 0)
            {
                print("criou um pneu raro");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[1] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[1].ToString());
                numeroAluminioReciclar[1] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[1].ToString());
                numeroFerroReciclar[1] = 0;
                txtFerroReciclagem.SetText(numeroFerroReciclar[1].ToString());
                numBorrachaReciclar[1] = 0;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[1].ToString());
            }
        }
        else if (numBtnApertado == 3 && liberarCorrida.reciclarCarroLendario)
        {

            // formulas para criar os itens
            if (numeroAluminioReciclar[2] > 0 && numeroAcoReciclar[2] > 0 && numeroFerroReciclar[2] > 0 && numBorrachaReciclar[2] <= 0)
            {
                print("criou um motor lendario");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[2] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[2].ToString());
                numeroAluminioReciclar[2] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[2].ToString());
                numeroFerroReciclar[2] = 0;
                txtFerroReciclagem.SetText(numeroFerroReciclar[2].ToString());
            }
            else if (numeroAluminioReciclar[2] > 0 && numeroAcoReciclar[2] > 0 && numBorrachaReciclar[2] <= 0 && numeroFerroReciclar[2] <= 0)
            {
                print("criou um transmissor lendario");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[2] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[2].ToString());
                numeroAluminioReciclar[2] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[2].ToString());
            }
            else if (numeroAluminioReciclar[2] > 0 && numeroAcoReciclar[2] > 0 && numBorrachaReciclar[2] > 0 && numeroFerroReciclar[2] > 0)
            {
                print("criou um chassi lendario");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[2] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[2].ToString());
                numeroAluminioReciclar[2] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[2].ToString());
                numeroFerroReciclar[2] = 0;
                txtFerroReciclagem.SetText(numeroFerroReciclar[2].ToString());
                numBorrachaReciclar[2] = 0;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[2].ToString());
            }
            else if (numeroAluminioReciclar[2] <= 0 && numeroAcoReciclar[2] <= 0 && numBorrachaReciclar[2] > 0 && numeroFerroReciclar[2] > 0)
            {
                print("criou um pneu lendario");
                podeApertarButton = false; // posso apertar o button
                numeroAcoReciclar[2] = 0;
                txtAcoReciclagem.SetText(numeroAcoReciclar[2].ToString());
                numeroAluminioReciclar[2] = 0;
                txtAluminioReciclagem.SetText(numeroAluminioReciclar[2].ToString());
                numeroFerroReciclar[2] = 0;
                txtFerroReciclagem.SetText(numeroFerroReciclar[2].ToString());
                numBorrachaReciclar[2] = 0;
                txtBorrachaReciclagem.SetText(numBorrachaReciclar[2].ToString());
            }
        }
        else
        {
            txt_Mensagem.SetText("");
            if (PlayerPrefs.GetInt("Idioma") == 0)
            {
                txt_Mensagem.SetText("Você precisa liberar essa raridade");
            } else {
                txt_Mensagem.SetText("You need unlock this rarity");
            }
            StartCoroutine("mensagemAviso");
        }
    }
    #endregion

    // chama a função quando eu clicar no nome dos itens
    public void btnClicouItens(int numberItem) {
        // verifica qual dos itens eu cliquei e aparece as informacoes dele
        EventSystem.current.SetSelectedGameObject(botaoVoltar);
        switch (numberItem) {
            case 1:
                canvasReciclarPanel.SetActive(false);
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_NomeItem.SetText("Ferro");
                    txt_Descricao.SetText("O ferro pode ser usado para construir um 'CHASSI', 'PNEU', 'FREIO', 'MOTOR'. \n" +
                    "O Ferro influência nos seguintes aspectos: \n" +
                    "Chassi: 10% de peso a mais. \n" +
                    "Pneu: 10% de resistência a mais. \n" +
                    "Freio: 5% de trava a mais. \n" +
                    "Motor: 5% a mais de aceleração. \n" +
                    "Peso deste ferro: Lendario: 50 Kg, Raro: 35 Kg, Normal: 20 Kg.");
                } else {
                    txt_NomeItem.SetText("Iron");
                    txt_Descricao.SetText("The Iron can be used to build a 'BODY', 'WHEEL', 'BRAKE', 'ENGINE'. \n" +
                    "The Iron influence on the following aspects: \n" +
                    "Body: 10% more weight. \n" +
                    "Wheel: 10% more resistance. \n" +
                    "Brake: 5% more bracking. \n" +
                    "Engine: 5% more acceleration. \n" +
                    "Weight of the iron: Legendary: 50 Kg, Rare: 35 Kg, Normal: 20 Kg.");
                }
                informacaoItem.SetActive(true);
                break;
            case 2:
                canvasReciclarPanel.SetActive(false);
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_NomeItem.SetText("Aço");
                    txt_Descricao.SetText("O aço pode ser usado para construir um “CHASSI”, “TRANSMISSÃO”, “FREIO”, “MOTOR”. \n" +
                    "O Aço influência nos seguintes aspectos: \n" +
                    "Chassi: 10% de resistencia a mais. \n" +
                    "Transmissão: 8 % a mais de aceleração. \n" +
                    "Freio: 4% de melhoria. \n" +
                    "Motor: 8% de peso a mais. \n" +
                    "Peso deste aço: Lendario: 70 Kg, Raro: 40 Kg, Normal: 25 Kg.");
                } else {
                    txt_NomeItem.SetText("Steel");
                    txt_Descricao.SetText("The steel can be used to build a  “BODY”, “TRANSMISSION”, “BRAKE”, “ENGINE”. \n" +
                    "The Steel influence on the following aspects: \n" +
                    "Body: 10% more resistance. \n" +
                    "Transmission: 8 % more acceleration. \n" +
                    "Brake: 4% improvement. \n" +
                    "Engine: 8% more weight. \n" +
                    "Weight of the steel: Legendary: 70 Kg, Rare: 40 Kg, Normal: 25 Kg.");
                }
                informacaoItem.SetActive(true);

                break;

            case 3:
                canvasReciclarPanel.SetActive(false);
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_NomeItem.SetText("Aluminio");
                    txt_Descricao.SetText("O alumínio pode ser usado para construir um “CHASSI”, “MOTOR”, “TRANSMISSÃO”. \n" +
                    "O Aluminio influência nos seguintes aspectos: \n" +
                    "Chassi: 5% de peso a mais. \n" +
                    "Transmissão: 10 % de resistência a mais. \n" +
                    "Motor: 8% de resistencia. \n" +
                    "Freio: 3% de melhoria nos freios. \n" +
                    "Peso deste aluminio: Lendario: 40 Kg, Raro: 25 Kg, Normal: 15 Kg.");
                } else {
                    txt_NomeItem.SetText("Aluminum");
                    txt_Descricao.SetText("The Aluminum can be used to build a  “BODY”, “ENGINE”, “TRANSMISSION”. \n" +
                    "The Aluminum influence on the following aspects: \n" +
                    "Body: 5% more weight. \n" +
                    "Transmission: 10 % more resistance. \n" +
                    "Engine: 8% more resistance. \n" +
                    "Brake: 3% improvement on brake. \n" +
                    "Weight of the aluminum: Legendary: 40 Kg, Rare: 25 Kg, Normal: 15 Kg.");
                }
                informacaoItem.SetActive(true);
                break;

            case 4:
                canvasReciclarPanel.SetActive(false);
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_NomeItem.SetText("Borracha");
                    txt_Descricao.SetText("A borracha pode ser usada para criar um  “CHASSI”, “PNEU”. \n" +
                    "A Borracha influência nos seguintes aspectos: \n" +
                    "Chassi: 5% a mais de aceleração. \n" +
                    "Pneu: 5% a mais de força na freagem. \n" +
                    "Peso desta borracha: Lendario 15 Kg, Raro: 10 Kg, Normal: 5 Kg.");
                } else {
                    txt_NomeItem.SetText("Rubber");
                    txt_Descricao.SetText("The rubber can be used to build a  “BODY”, “WHEEL”. \n" +
                    "The Rubber influence on the following aspects: \n" +
                    "Body: 5% more acceleration. \n" +
                    "Pneu: 5% more braking. \n" +
                    "Weight of the rubber: Legendary 15 Kg, Rare: 10 Kg, Normal: 5 Kg.");
                }
                informacaoItem.SetActive(true);
                break;
        }
    }

    // tira a informação dos itens da tela
    public void btnVoltar() {
        canvasReciclarPanel.SetActive(true);
        informacaoItem.SetActive(false);
        EventSystem.current.SetSelectedGameObject(botaoRecilar);
    }

    IEnumerator mensagemAviso() {
        yield return new WaitForSecondsRealtime(5);
        txt_Aviso.SetActive(false);
        txt_Mensagem.SetText("");
    }

    IEnumerator tempoEspera() {
        yield return new WaitForSecondsRealtime(tempoApertar);
        podeApertar = true;
        
    }

}
