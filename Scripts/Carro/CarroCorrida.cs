using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarroCorrida : MonoBehaviour {

    public int pontosPartida, pontosAcumulados;
    private Scene cena;
    private LiberarCorrida liberarCorrida;
    public int numPosicao = 1;
    public TextMeshProUGUI txt_Posicao;

	// Use this for initialization
	void Start () {
        cena = SceneManager.GetActiveScene();
        liberarCorrida = FindObjectOfType(typeof(LiberarCorrida)) as LiberarCorrida;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void liberarMapa() {
        if (cena.name == "Mapa da Ponte")
        {
            if (!liberarCorrida.ganhouCorrida[1])
            {
                liberarCorrida.ganhouCorrida[1] = true;
            }
        } else if(cena.name == "Mapa Montanha") {
            if (!liberarCorrida.ganhouCorrida[2])
            {
                liberarCorrida.ganhouCorrida[2] = true;
            }
        } else if(cena.name == "Mapa do Gelo") {
            if (!liberarCorrida.ganhouCorrida[3])
            {
                liberarCorrida.ganhouCorrida[3] = true;
            }
        } else if(cena.name == "Mapa do Deserto") {
            if (!liberarCorrida.ganhouCorrida[4])
            {
                liberarCorrida.ganhouCorrida[4] = true;
            }
        }
    }
}
