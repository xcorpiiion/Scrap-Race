using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Corrida : MonoBehaviour {

    public GameObject[] carros;
    public GameObject carroAtivo;
    public Transform respawn;
    private ItensCarro itensCarro;
    private CarroPower carroPower;
    public Sprite[] numeroSprite;
    private GameController gameController;
    private int contarTempo;
    public Image img_ContadorCorrida;
    public GameObject img_ContarTempoPanel;
    public AudioClip[] sound_Contagem;
    private AudioSource audioSource;
    private Scene cena;


    // Use this for initialization
    void Start () {
        cena = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt("TutorialCidade", 1);
        audioSource = GetComponent<AudioSource>();
        contarTempo = numeroSprite.Length;
        img_ContadorCorrida.sprite = null;
        itensCarro = FindObjectOfType(typeof(ItensCarro)) as ItensCarro;
        itensCarro.carroEscolhido.SetActive(true);
        carroPower = FindObjectOfType(typeof(CarroPower)) as CarroPower;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;

        for (int i = 0; i < carros.Length; i++)
        {
            if (carros[i].name == itensCarro.carroEscolhido.name)
            {

                carros[i].SetActive(true);
                carroAtivo = carros[i];
                carros[i].GetComponent<RCC_CarControllerV3>().engineTorque = itensCarro.carroEscolhido.GetComponent<CarroPower>().maxTorque;
                carros[i].GetComponent<RCC_CarControllerV3>().brakeTorque = itensCarro.carroEscolhido.GetComponent<CarroPower>().maxFreio;
                carros[i].GetComponent<RCC_CarControllerV3>().rigid.mass = itensCarro.carroEscolhido.GetComponent<CarroPower>().maxMassa;
                itensCarro.carroEscolhido.SetActive(false);
            } else {

            }
        }
        img_ContarTempoPanel.SetActive(true);
        gameController.changeState(GameState.INTERACAO);
        StartCoroutine("tempoCorrida");
    }
	
	IEnumerator tempoCorrida() {
        gameController.changeState(GameState.INTERACAO);

        if (contarTempo > 0)
        {

            img_ContadorCorrida.sprite = numeroSprite[contarTempo - 1];
            audioSource.PlayOneShot(sound_Contagem[contarTempo - 1]);
        }
        yield return new WaitForSecondsRealtime(1);
        contarTempo--;
        if(contarTempo <= 0) {
            img_ContarTempoPanel.SetActive(false);
            gameController.changeState(GameState.GAMEPLAY);
            if(cena.name == "Mapa da Ponte") {
                PlayerPrefs.SetInt("Fazer tuturial", 1);
            }
            StopCoroutine("tempoCorrida");
        } else {
            StartCoroutine("tempoCorrida");
        }
        
    }
}
