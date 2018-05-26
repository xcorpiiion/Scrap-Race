using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Race : MonoBehaviour {

    public GameObject canvasCarro, canvasFeedback, venceu, perdeu;
    private Loading loading;
    public int pontosLap;
    public GameObject botaoVoltar;
    private GamepadCanvas gamepadCanvas;
    private GameController gameController;
    public TextMeshProUGUI txt_NumeroLap, txt_NumeroVoltas;


	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        txt_NumeroLap.SetText(pontosLap.ToString());
        txt_NumeroVoltas.SetText("0");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {


            if(this.gameObject.tag == "Inicio") {
                other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados = 1;
            }

            if(this.gameObject.tag == "Meio" && other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados == 1) {
                other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados = 2;
            }

            if (this.gameObject.tag == "Fim" && other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados == 2)
            {
                other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados = 3;

            }

            if (this.gameObject.tag == "Chegada")
            {
                if (other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados == 3)
                {
                    other.gameObject.GetComponent<CarroCorrida>().pontosPartida++;
                    txt_NumeroVoltas.SetText(other.gameObject.GetComponent<CarroCorrida>().pontosPartida.ToString());

                    if (other.gameObject.GetComponent<CarroCorrida>().pontosPartida >= pontosLap)
                    {
                        canvasCarro.SetActive(false);
                        other.gameObject.GetComponent<CarroCorrida>().liberarMapa();
                        gamepadCanvas.isCanvas = true;
                        PlayerPrefs.SetInt("TutorialCorrida", 1);
                        loading.carregarLoading("Feedback");
                        PlayerPrefs.SetInt("ResultadoCorrida", 0);
                    }
                    other.gameObject.GetComponent<CarroCorrida>().pontosAcumulados = 0;
                }
            }
        } else if(other.gameObject.tag == "Inimigo") {


            if (this.gameObject.tag == "Inicio")
            {
                other.gameObject.GetComponent<AICarroCorrida>().pontosAcumulados = 1;
            }

            if (this.gameObject.tag == "Meio" && other.gameObject.GetComponent<AICarroCorrida>().pontosAcumulados == 1)
            {
                other.gameObject.GetComponent<AICarroCorrida>().pontosAcumulados = 2;
            }

            if (this.gameObject.tag == "Fim" && other.gameObject.GetComponent<AICarroCorrida>().pontosAcumulados == 2)
            {
                other.gameObject.GetComponent<AICarroCorrida>().pontosAcumulados = 3;

            }

            if (this.gameObject.tag == "Chegada" && other.gameObject.GetComponent<AICarroCorrida>().pontosAcumulados == 3)
            {

                other.gameObject.GetComponent<AICarroCorrida>().pontosPartida++;


                if (other.gameObject.GetComponent<AICarroCorrida>().pontosPartida >= pontosLap)
                {
                    canvasCarro.SetActive(false);
                    gamepadCanvas.isCanvas = true;
                    PlayerPrefs.SetInt("TutorialCorrida", 1);
                    loading.carregarLoading("Feedback");
                    PlayerPrefs.SetInt("ResultadoCorrida", 1);

                }
            }
        }
    }


}
