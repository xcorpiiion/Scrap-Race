using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour {

    [Header("Valores iniciais")]
    public int resolucaoTela;
    public bool telaCheia;
    public int qualidadeGrafica;
    public int nivelDetalhe;
    public int numIdioma;
    public int numAntiAliasing;

    private int widgetTela, heightTela, intTelaCheia;

    public Terrain terrenoAtivo;

    private AudioSource audioS;

    public bool aplicouConfig;

    // cuida do novo config
    private GamepadCanvas gamepadCanvas;

    [Header("Pega os textos do tela cheia")]
    public GameObject txt_LigadoTela;
    public GameObject txt_DesligadoTela;

    [Header("Pega os textos da Linguagem")]
    public GameObject txt_Portugues;
    public GameObject txt_Ingles;

    [Header("Pega os textos da Resolução")]
    public GameObject txt_1920;
    public GameObject txt_1366, txt_1280;

    [Header("Pega os textos da Qualidade")]
    public GameObject txt_Ultra;
    public GameObject txt_Alto, txt_Medio, txt_Baixo;

    [Header("Pega os textos dos Detalhes")]
    public GameObject txt_UltraDetalhes;
    public GameObject txt_AltoDetalhes, txt_MedioDetalhes, txt_BaixoDetalhes;

    [Header("Pega os textos do Anti Aliasing")]
    public GameObject txt_AntiAliasingLigado;
    public GameObject txt_AntiAliasingDesligado;

    public float tempoEspera;
    public bool podeApertar = true;

    private MudarIdioma mudarIdioma;
    private GameController gameController;

    // Use this for initialization
    void Start () {

        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        mudarIdioma = FindObjectOfType(typeof(MudarIdioma)) as MudarIdioma;

        QualitySettings.vSyncCount = 0;

        audioS = GetComponent<AudioSource>();

        int telaResolution = PlayerPrefs.GetInt("resolucaoTela");
        int graficConfig = PlayerPrefs.GetInt("qualidadeGrafica");
        int detail = PlayerPrefs.GetInt("nivelDetalhe");
        int fullScren = PlayerPrefs.GetInt("telaCheia");
        int idioma = PlayerPrefs.GetInt("Idioma");
        int antialising = PlayerPrefs.GetInt("antialising");

        numIdioma = idioma;

        resolucaoTela = telaResolution;
        qualidadeGrafica = graficConfig;
        nivelDetalhe = detail;
        numAntiAliasing = antialising;

        if(numIdioma == 0) {
            txt_Portugues.SetActive(true);
            txt_Ingles.SetActive(false);
        } else {
            txt_Portugues.SetActive(false);
            txt_Ingles.SetActive(true);
        }

        if (qualidadeGrafica == 3) {
            txt_Ultra.SetActive(true);
            txt_Alto.SetActive(false);
            txt_Medio.SetActive(false);
            txt_Baixo.SetActive(false);
        } else if(qualidadeGrafica == 2) {
            txt_Ultra.SetActive(false);
            txt_Alto.SetActive(true);
            txt_Medio.SetActive(false);
            txt_Baixo.SetActive(false);
        } else if(qualidadeGrafica == 1) {
            txt_Ultra.SetActive(false);
            txt_Alto.SetActive(false);
            txt_Medio.SetActive(true);
            txt_Baixo.SetActive(false);
        } else {
            txt_Ultra.SetActive(false);
            txt_Alto.SetActive(false);
            txt_Medio.SetActive(false);
            txt_Baixo.SetActive(true);
        }


        if (nivelDetalhe == 3)
        {
            txt_UltraDetalhes.SetActive(true);
            txt_AltoDetalhes.SetActive(false);
            txt_MedioDetalhes.SetActive(false);
            txt_BaixoDetalhes.SetActive(false);
        }
        else if (nivelDetalhe == 2)
        {
            txt_UltraDetalhes.SetActive(false);
            txt_AltoDetalhes.SetActive(true);
            txt_MedioDetalhes.SetActive(false);
            txt_BaixoDetalhes.SetActive(false);
        }
        else if (nivelDetalhe == 1)
        {
            txt_UltraDetalhes.SetActive(false);
            txt_AltoDetalhes.SetActive(false);
            txt_MedioDetalhes.SetActive(true);
            txt_BaixoDetalhes.SetActive(false);
        }
        else
        {
            txt_UltraDetalhes.SetActive(false);
            txt_AltoDetalhes.SetActive(false);
            txt_MedioDetalhes.SetActive(false);
            txt_BaixoDetalhes.SetActive(true);
        }


        if (resolucaoTela == 0) {
            txt_1920.SetActive(true);
            txt_1366.SetActive(false);
            txt_1280.SetActive(false);
        } else if(resolucaoTela == 1) {
            txt_1920.SetActive(false);
            txt_1366.SetActive(true);
            txt_1280.SetActive(false);
        } else {
            txt_1920.SetActive(false);
            txt_1366.SetActive(false);
            txt_1280.SetActive(true);
        }

        if (fullScren == 1)
        {
            telaCheia = true;
        }
        else
        {
            telaCheia = false;
        }

        if(telaCheia) {
            txt_LigadoTela.SetActive(true);
            txt_DesligadoTela.SetActive(false);
        } else {
            txt_LigadoTela.SetActive(false);
            txt_DesligadoTela.SetActive(true);
        }

        gameController.setarPreferencias();

    }

    private void LateUpdate()
    {
        // verifica se eu estou no canvas no momento
        if(gamepadCanvas.isCanvas) {
            if(gamepadCanvas.selectedObj.name == "Setas Tela Cheia") {
                if((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && podeApertar) {
                    podeApertar = false;
                    if(txt_LigadoTela.activeInHierarchy) {
                        telaCheia = false;
                        txt_LigadoTela.SetActive(false);
                        txt_DesligadoTela.SetActive(true);
                    } else {
                        telaCheia = true;
                        txt_LigadoTela.SetActive(true);
                        txt_DesligadoTela.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");
                } else if((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && podeApertar) {
                    podeApertar = false;
                    if (txt_DesligadoTela.activeInHierarchy)
                    {
                        telaCheia = true;
                        txt_LigadoTela.SetActive(true);
                        txt_DesligadoTela.SetActive(false);
                    }
                    else
                    {
                        telaCheia = false;
                        txt_LigadoTela.SetActive(false);
                        txt_DesligadoTela.SetActive(true);
                    }
                    StartCoroutine("tempoApertar");

                }

            } else if(gamepadCanvas.selectedObj.name == "Setas Linguagem") {
                if ((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_Portugues.activeInHierarchy)
                    {
                        numIdioma = 0;
                        txt_Portugues.SetActive(false);
                        txt_Ingles.SetActive(true);
                    }
                    else
                    {
                        numIdioma = 1;
                        txt_Portugues.SetActive(true);
                        txt_Ingles.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }
                else if ((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_Ingles.activeInHierarchy)
                    {
                        numIdioma = 0;
                        txt_Portugues.SetActive(true);
                        txt_Ingles.SetActive(false);
                    }
                    else
                    {
                        numIdioma = 1;
                        txt_Portugues.SetActive(false);
                        txt_Ingles.SetActive(true);
                    }
                    StartCoroutine("tempoApertar");

                }
            } else if (gamepadCanvas.selectedObj.name == "Setas Resolução") {
                if ((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_1920.activeInHierarchy)
                    {
                        resolucaoTela = 0;
                        txt_1920.SetActive(false);
                        txt_1366.SetActive(true);
                        txt_1280.SetActive(false);

                    }
                    else if (txt_1366.activeInHierarchy)
                    {
                        resolucaoTela = 1;
                        txt_1920.SetActive(false);
                        txt_1366.SetActive(false);
                        txt_1280.SetActive(true);

                    }
                    else
                    {
                        resolucaoTela = 2;
                        txt_1920.SetActive(true);
                        txt_1366.SetActive(false);
                        txt_1280.SetActive(false);

                    }
                    StartCoroutine("tempoApertar");

                }
                else if ((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_1366.activeInHierarchy)
                    {
                        resolucaoTela = 2;
                        txt_1920.SetActive(false);
                        txt_1366.SetActive(false);
                        txt_1280.SetActive(true);

                    }
                    else if (txt_1280.activeInHierarchy)
                    {
                        resolucaoTela = 0;
                        txt_1920.SetActive(true);
                        txt_1366.SetActive(false);
                        txt_1280.SetActive(false);

                    }
                    else
                    {
                        resolucaoTela = 1;
                        txt_1920.SetActive(false);
                        txt_1366.SetActive(true);
                        txt_1280.SetActive(false);

                    }
                    StartCoroutine("tempoApertar");

                }
            } if (gamepadCanvas.selectedObj.name == "Setas Qualidade") {
                if ((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_Ultra.activeInHierarchy)
                    {
                        qualidadeGrafica = 0;
                        txt_Ultra.SetActive(false);
                        txt_Alto.SetActive(false);
                        txt_Medio.SetActive(false);
                        txt_Baixo.SetActive(true);

                    }
                    else if (txt_Alto.activeInHierarchy)
                    {
                        qualidadeGrafica = 3;
                        txt_Ultra.SetActive(true);
                        txt_Alto.SetActive(false);
                        txt_Medio.SetActive(false);
                        txt_Baixo.SetActive(false);

                    }
                    else if (txt_Medio.activeInHierarchy)
                    {
                        qualidadeGrafica = 2;
                        txt_Ultra.SetActive(false);
                        txt_Alto.SetActive(true);
                        txt_Medio.SetActive(false);
                        txt_Baixo.SetActive(false);

                    }
                    else
                    {
                        qualidadeGrafica = 1;
                        txt_Ultra.SetActive(false);
                        txt_Alto.SetActive(false);
                        txt_Medio.SetActive(true);
                        txt_Baixo.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }
                else if ((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_Ultra.activeInHierarchy)
                    {
                        qualidadeGrafica = 2;
                        txt_Ultra.SetActive(false);
                        txt_Alto.SetActive(true);
                        txt_Medio.SetActive(false);
                        txt_Baixo.SetActive(false);

                    }
                    else if (txt_Alto.activeInHierarchy)
                    {
                        qualidadeGrafica = 1;
                        txt_Ultra.SetActive(false);
                        txt_Alto.SetActive(false);
                        txt_Medio.SetActive(true);
                        txt_Baixo.SetActive(false);

                    }
                    else if(txt_Medio.activeInHierarchy)
                    {
                        qualidadeGrafica = 0;
                        txt_Ultra.SetActive(false);
                        txt_Alto.SetActive(false);
                        txt_Medio.SetActive(false);
                        txt_Baixo.SetActive(true);

                    } else {
                        qualidadeGrafica = 3;
                        txt_Ultra.SetActive(true);
                        txt_Alto.SetActive(false);
                        txt_Medio.SetActive(false);
                        txt_Baixo.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }

            } else if(gamepadCanvas.selectedObj.name == "Setas Detalhes") {
                if ((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_UltraDetalhes.activeInHierarchy)
                    {
                        nivelDetalhe = 0;
                        txt_UltraDetalhes.SetActive(false);
                        txt_AltoDetalhes.SetActive(false);
                        txt_MedioDetalhes.SetActive(false);
                        txt_BaixoDetalhes.SetActive(true);

                    }
                    else if (txt_AltoDetalhes.activeInHierarchy)
                    {
                        nivelDetalhe = 3;
                        txt_UltraDetalhes.SetActive(true);
                        txt_AltoDetalhes.SetActive(false);
                        txt_MedioDetalhes.SetActive(false);
                        txt_BaixoDetalhes.SetActive(false);

                    }
                    else if (txt_MedioDetalhes.activeInHierarchy)
                    {
                        nivelDetalhe = 2;
                        txt_UltraDetalhes.SetActive(false);
                        txt_AltoDetalhes.SetActive(true);
                        txt_MedioDetalhes.SetActive(false);
                        txt_BaixoDetalhes.SetActive(false);

                    }
                    else
                    {
                        nivelDetalhe = 1;
                        txt_UltraDetalhes.SetActive(false);
                        txt_AltoDetalhes.SetActive(false);
                        txt_MedioDetalhes.SetActive(true);
                        txt_BaixoDetalhes.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }
                else if ((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_UltraDetalhes.activeInHierarchy)
                    {
                        nivelDetalhe = 2;
                        txt_UltraDetalhes.SetActive(false);
                        txt_AltoDetalhes.SetActive(true);
                        txt_MedioDetalhes.SetActive(false);
                        txt_BaixoDetalhes.SetActive(false);

                    }
                    else if (txt_AltoDetalhes.activeInHierarchy)
                    {
                        nivelDetalhe = 1;
                        txt_UltraDetalhes.SetActive(false);
                        txt_AltoDetalhes.SetActive(false);
                        txt_MedioDetalhes.SetActive(true);
                        txt_BaixoDetalhes.SetActive(false);

                    }
                    else if (txt_MedioDetalhes.activeInHierarchy)
                    {
                        nivelDetalhe = 0;
                        txt_UltraDetalhes.SetActive(false);
                        txt_AltoDetalhes.SetActive(false);
                        txt_MedioDetalhes.SetActive(false);
                        txt_BaixoDetalhes.SetActive(true);

                    }
                    else
                    {
                        nivelDetalhe = 3;
                        txt_UltraDetalhes.SetActive(true);
                        txt_AltoDetalhes.SetActive(false);
                        txt_MedioDetalhes.SetActive(false);
                        txt_BaixoDetalhes.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }
            } else if (gamepadCanvas.selectedObj.name == "Setas Anti Aliasing") {
                if ((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_AntiAliasingLigado.activeInHierarchy)
                    {
                        numAntiAliasing = 0;
                        txt_AntiAliasingLigado.SetActive(false);
                        txt_AntiAliasingDesligado.SetActive(true);
                    }
                    else
                    {
                        numAntiAliasing = 1;
                        txt_AntiAliasingLigado.SetActive(true);
                        txt_AntiAliasingDesligado.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }
                else if ((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && podeApertar)
                {
                    podeApertar = false;
                    if (txt_AntiAliasingLigado.activeInHierarchy)
                    {
                        numIdioma = 0;
                        txt_AntiAliasingLigado.SetActive(false);
                        txt_AntiAliasingDesligado.SetActive(true);
                    }
                    else
                    {
                        numIdioma = 1;
                        txt_AntiAliasingLigado.SetActive(true);
                        txt_AntiAliasingDesligado.SetActive(false);
                    }
                    StartCoroutine("tempoApertar");

                }
            }
        }
    }

    public void aplicar() {

        aplicouConfig = true;

        switch (resolucaoTela)
        {
            case 0:
                widgetTela = 1920;
                heightTela = 1080;
                break;
            case 1:
                widgetTela = 1366;
                heightTela = 768;
                break;
            case 2:
                widgetTela = 1280;
                heightTela = 720;
                break;

        }

        if(txt_Portugues.activeInHierarchy) {
            PlayerPrefs.SetInt("Idioma", 0);
            mudarIdioma.trocaIdioma();
        } else {
            PlayerPrefs.SetInt("Idioma", 1);
            mudarIdioma.trocaIdioma();
        }

        if (terrenoAtivo != null)
        {
            switch (nivelDetalhe)
            {
                case 0:
                    terrenoAtivo = Terrain.activeTerrain;
                    terrenoAtivo.detailObjectDensity = 0.1f;
                    break;
                case 1:
                    terrenoAtivo = Terrain.activeTerrain;
                    terrenoAtivo.detailObjectDensity = 0.3f;
                    break;
                case 2:
                    terrenoAtivo = Terrain.activeTerrain;
                    terrenoAtivo.detailObjectDensity = 0.6f;
                    break;
                case 3:
                    terrenoAtivo = Terrain.activeTerrain;
                    terrenoAtivo.detailObjectDensity = 1f;
                    break;
            }
        }


        Screen.SetResolution(widgetTela, heightTela, telaCheia);
        QualitySettings.SetQualityLevel(qualidadeGrafica, true);

        armazenarPreferencias();
        
        
        


    }


    void armazenarPreferencias() {
        print("armazenarPreferencias");
        PlayerPrefs.SetInt("resolucaoTela", resolucaoTela);
        PlayerPrefs.SetInt("qualidadeGrafica", qualidadeGrafica);
        PlayerPrefs.SetInt("nivelDetalhe", nivelDetalhe);
        PlayerPrefs.SetInt("idioma", numIdioma); 
        PlayerPrefs.SetInt("antialising", numAntiAliasing);

        if (telaCheia) {
            intTelaCheia = 1;
        } else {
            intTelaCheia = 0;
        }

        PlayerPrefs.SetInt("telaCheia", intTelaCheia);



    }

    IEnumerator tempoApertar()
    {
        yield return new WaitForSecondsRealtime(tempoEspera);
        podeApertar = true;

    }

}
