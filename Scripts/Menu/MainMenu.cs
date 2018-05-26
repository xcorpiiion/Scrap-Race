using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public Image[] img_Splash, img_Qualidade;
    public Text[] txt_Config, txt_IniciarGame;
    public Button[] img_MainMenu, btn_Config, btn_InicioJogo, btn_NovoJogo;
    public GameObject background, mainMenu, galeria, menuConfiguracao, protecaoTela, inicioJogo, canvas2;
    public GameObject[] tutorialExploracao, tutorialCorrida;
    public Color corAlpha;
    public bool apertouButton, voltarTela = true, apertarBotaoGamepad = false;
    private Loading load;
    public TextMeshProUGUI[] configTexto;
    private GamepadCanvas gamepadCanvas;
    [SerializeField]
    private AudioClip sound_Voltar;
    private AudioSource audioSource;
    public TextMeshProUGUI txt_PrecioneStart;
    public TextMeshProUGUI[] txt_MainMenu, txt_Galeria;
    public GameObject novoJogoPanel;
    public LiberarCorrida liberarCorrida;
    public GameObject[] img_Botoes;
    public float tempoOpacidade;

    

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("Personagem") == 0 || PlayerPrefs.GetInt("Personagem") == 1)
        {
            btn_InicioJogo[1].enabled = true;
        }
        else
        {
            btn_InicioJogo[1].enabled = false;
        }


        img_Botoes[0].SetActive(false);
        img_Botoes[1].SetActive(false);
        novoJogoPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        canvas2.SetActive(false);
        load = FindObjectOfType(typeof(Loading)) as Loading;
        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        // deixa os buttons invisiveis
        corAlpha = img_MainMenu[0].image.color;
        corAlpha.a = 0;
        for(int i = 0; i < img_MainMenu.Length; i++) {
            img_MainMenu[i].image.color = corAlpha; // tira o alpha dos botoes
        }
        
        

        for (int i = 0; i < configTexto.Length; i++)
        {
            configTexto[i].color = corAlpha;
        }

        for (int i = 0; i < img_Qualidade.Length; i++)
        {
            img_Qualidade[i].color = corAlpha;
        }

        for(int i = 0; i < txt_Galeria.Length; i++) {
            txt_Galeria[i].color = corAlpha;
        }



        for (int i = 0; i < txt_IniciarGame.Length; i++)
        {
            txt_IniciarGame[i].color = corAlpha;
        }

        for (int i = 0; i < btn_InicioJogo.Length; i++)
        {
            btn_InicioJogo[i].image.color = corAlpha;
        }

        galeria.SetActive(false);
        mainMenu.SetActive(false);
        inicioJogo.SetActive(false);

    }

    private void LateUpdate()
    {
        // faz entrar no menu principal
        if (Input.GetButton("Controle 01 Start") && background.activeInHierarchy && !apertarBotaoGamepad)
        {
            protecaoTela.SetActive(true);
            apertarBotaoGamepad = true;
            liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;
            StartCoroutine("opacidadeSplash");
        }

        if((Input.GetButton("Controle 01 Bolinha") && !apertarBotaoGamepad)&& (inicioJogo.activeInHierarchy || galeria.activeInHierarchy || menuConfiguracao.activeInHierarchy) && voltarTela) {
            protecaoTela.SetActive(true);
            apertarBotaoGamepad = true;
            audioSource.PlayOneShot(sound_Voltar);
            gamepadCanvas.podeApertar = true;
            StartCoroutine("opacidadeSplash");
        }
    }

    // diminui a opacidade ******************************************************************
    IEnumerator opacidadeSplash() {

        yield return new WaitForSeconds(tempoOpacidade);

        if (mainMenu.activeInHierarchy) // diminui a opacidade do menu
        {
            corAlpha = img_MainMenu[0].image.color;
            corAlpha.a -= 0.1f;
            for (int i = 0; i < img_MainMenu.Length; i++)
            {
                img_MainMenu[i].image.color = corAlpha;
            }

            for (int i = 0; i < txt_MainMenu.Length; i++)
            {
                txt_MainMenu[i].color = corAlpha;
            }

            if (corAlpha.a <= 0) // desativa o menu depois que a opacidade chegar a zero
            {
                mainMenu.SetActive(false);
            }
        }
        else if (galeria.activeInHierarchy) // diminui a opacidade da galeria
        {
            corAlpha = txt_Galeria[0].color;
            corAlpha.a -= 0.1f;
            

            for (int i = 0; i < txt_Galeria.Length; i++)
            {
                txt_Galeria[i].color = corAlpha;
            }

            if (corAlpha.a <= 0) // desativa a galeria depois que a opacidade chegar a zero
            {
                galeria.SetActive(false);
                mainMenu.SetActive(true);
            }

        } else if (menuConfiguracao.activeInHierarchy) {
            corAlpha.a -= 0.1f;

            for (int i = 0; i < configTexto.Length; i++)
            {
                configTexto[i].color = corAlpha;
            }

            for (int i = 0; i < img_Qualidade.Length; i++)
            {
                img_Qualidade[i].color = corAlpha;
            }

            


            if (corAlpha.a <= 0)
            {
                menuConfiguracao.SetActive(false);
                mainMenu.SetActive(true);
                canvas2.SetActive(false);
            }

        } else if (inicioJogo.activeInHierarchy) {
            corAlpha.a -= 0.1f;
            for (int i = 0; i < txt_IniciarGame.Length; i++)
            {
                txt_IniciarGame[i].color = corAlpha;
            }

            for (int i = 0; i < btn_InicioJogo.Length; i++)
            {
                btn_InicioJogo[i].image.color = corAlpha;
            }

            if (corAlpha.a <= 0)
            {
                inicioJogo.SetActive(false);
                mainMenu.SetActive(true);
            }
        } else { // diminui a opacidade antes do menu
            corAlpha = img_Splash[0].color;
            corAlpha.a -= 0.1f;
            for (int i = 0; i < img_Splash.Length; i++)
            {
                img_Splash[i].color = corAlpha;
                txt_PrecioneStart.color = corAlpha;
            }
            if (corAlpha.a <= 0) // desativa o background depois que a opacidade chegar a zero
            {
                background.SetActive(false);
                img_Botoes[0].SetActive(true);
                img_Botoes[1].SetActive(true);
                mainMenu.SetActive(true);
            }
        } 

        if (corAlpha.a >= 0) {
            StartCoroutine("opacidadeSplash");
        } else {
            StartCoroutine("aumentarOpacidadeMenu");
        }

    }

    // aumenta a opacidade do menu
    IEnumerator aumentarOpacidadeMenu()
    {

        yield return new WaitForSeconds(tempoOpacidade);
        
        if (mainMenu.activeInHierarchy)
        {
            
            corAlpha = img_MainMenu[0].image.color;
            EventSystem.current.SetSelectedGameObject(img_MainMenu[0].gameObject);
            corAlpha.a += 0.1f;
            for (int i = 0; i < img_MainMenu.Length; i++)
            {
                img_MainMenu[i].image.color = corAlpha;
            }

            for (int i = 0; i < txt_MainMenu.Length; i++)
            {
                txt_MainMenu[i].color = corAlpha;
            }

        } else if(galeria.activeInHierarchy) {
            corAlpha = txt_Galeria[0].color;
            corAlpha.a += 0.1f;
            
            for (int i = 0; i < txt_Galeria.Length; i++)
            {
                txt_Galeria[i].color = corAlpha;
            }

        } else if(menuConfiguracao.activeInHierarchy) {
            corAlpha = configTexto[0].color;
            corAlpha.a += 0.1f;
            EventSystem.current.SetSelectedGameObject(btn_Config[0].gameObject);

            for (int i = 0; i < configTexto.Length; i++)
            {
                configTexto[i].color = corAlpha;
            }

            for (int i = 0; i < img_Qualidade.Length; i++)
            {
                img_Qualidade[i].color = corAlpha;
            }

            

        } else if (inicioJogo.activeInHierarchy) {
            corAlpha.a += 0.1f;
            EventSystem.current.SetSelectedGameObject(btn_InicioJogo[0].gameObject);
            for (int i = 0; i < txt_IniciarGame.Length; i++)
            {
                txt_IniciarGame[i].color = corAlpha;
            }

            for (int i = 0; i < btn_InicioJogo.Length; i++)
            {
                btn_InicioJogo[i].image.color = corAlpha;
            }

        }


        if (corAlpha.a <= 1)
        {
            StartCoroutine("aumentarOpacidadeMenu");
        } else {
            protecaoTela.SetActive(false);
            voltarTela = true;
            gamepadCanvas.podeApertar = false;
            apertarBotaoGamepad = false;
        }
    }

    public void modoHistoria() {
        voltarTela = false;
        inicioJogo.SetActive(true);
        protecaoTela.SetActive(true);
        StartCoroutine("opacidadeSplash");
        
    }

    public void galeriaMenu() {
        voltarTela = false;
        galeria.SetActive(true);
        protecaoTela.SetActive(true);
        StartCoroutine("opacidadeSplash");
    }

    public void configuracoes() {
        canvas2.SetActive(true);
        voltarTela = false;
        menuConfiguracao.SetActive(true);
        protecaoTela.SetActive(true);
        StartCoroutine("opacidadeSplash");
    }

    public void sair() {
        Application.Quit();
    }

    public void btn_NewGame() {
        liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;
        EventSystem.current.SetSelectedGameObject(btn_NovoJogo[0].gameObject);
        if (liberarCorrida.novoJogo || PlayerPrefs.GetInt("Personagem") == 0 || PlayerPrefs.GetInt("Personagem") == 1) {
            novoJogoPanel.SetActive(true);
            inicioJogo.SetActive(false);
        } else {
            SaveObject.NewGame();
            SaveObject.Save();
            load.carregarLoading("Escolha Personagem");
        }
    }

    public void btn_Continuar() {
        load.carregarLoading("Cidade Rica");
    }

    public void btn_Sim() {
        SaveObject.NewGame();
        liberarCorrida.novoJogo = false;
        SaveObject.Save();
        PlayerPrefs.SetInt("Personagem", 2);
        PlayerPrefs.SetInt("TutorialExploracao", 0);
        PlayerPrefs.SetInt("TutorialCidade", 0);
        PlayerPrefs.SetInt("TutorialCorrida", 0);
        gamepadCanvas.isCanvas = false;
        load.carregarLoading("Escolha Personagem");
    }

    public void btn_Nao() {
        EventSystem.current.SetSelectedGameObject(btn_InicioJogo[0].gameObject);
        novoJogoPanel.SetActive(false);
        inicioJogo.SetActive(true);
    }

}
