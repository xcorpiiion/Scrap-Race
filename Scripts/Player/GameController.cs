using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GAMEPLAY, INVENTARIO, ITEMINFO, INTERACAO, CUTSCENE
}

public class GameController : MonoBehaviour {

    public GameState currentState;
    public Terrain terrenoAtivo;
    public bool usandoOpcoes;
    public GameObject carroUsado;
    public List<string> itensNormais = new List<string>(), itensRaros = new List<string>(), itensLendarios = new List<string>();
    public bool usandoGameController;

    private void Awake()
    {
        if(usandoGameController) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

    private void Start()
    {
        setarPreferencias();
    }

    // muda o estado do jogo
    public void changeState(GameState newState)
    {
        currentState = newState; // recebe o novo estado do jogo
        // verifica se o inventario está aberto
        if (currentState == GameState.INVENTARIO || currentState == GameState.INTERACAO || currentState == GameState.CUTSCENE)
        {
            Time.timeScale = 0; // encerra a passagem de tempo do jogo
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Update()
    {
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
       
    }

    public void setarPreferencias()
    {

        int telaResolution = PlayerPrefs.GetInt("resolucaoTela");
        int graficConfig = PlayerPrefs.GetInt("qualidadeGrafica");
        int detail = PlayerPrefs.GetInt("nivelDetalhe");
        int fullScren = PlayerPrefs.GetInt("telaCheia");
        int idioma = PlayerPrefs.GetInt("Idioma");
        int antialising = PlayerPrefs.GetInt("antialising");
        int widgetTela = 1280, heightTela = 720;

        bool gcTelaCheia = false;

        if (fullScren == 1)
        {
            gcTelaCheia = true;
        }
        else
        {
            gcTelaCheia = false;
        }


        switch (telaResolution)
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

        switch (detail)
        {
            case 0:
                terrenoAtivo = Terrain.activeTerrain;
                if (terrenoAtivo != null)
                {
                    terrenoAtivo.detailObjectDensity = 0.1f;
                }
                break;
            case 1:
                terrenoAtivo = Terrain.activeTerrain;
                if (terrenoAtivo != null)
                {
                    terrenoAtivo.detailObjectDensity = 0.3f;
                }
                break;
            case 2:
                terrenoAtivo = Terrain.activeTerrain;
                if (terrenoAtivo != null)
                {
                    terrenoAtivo.detailObjectDensity = 0.6f;
                }
                break;
            case 3:
                terrenoAtivo = Terrain.activeTerrain;
                if (terrenoAtivo != null)
                {
                    terrenoAtivo.detailObjectDensity = 1.0f;
                }
                break;
        }


        Screen.SetResolution(widgetTela, heightTela, gcTelaCheia);
        QualitySettings.SetQualityLevel(graficConfig, true);
    }

    public void pegarCarro() {
        DontDestroyOnLoad(carroUsado);
    }

}
