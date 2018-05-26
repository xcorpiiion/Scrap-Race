using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarPosicao : MonoBehaviour {

    public CarroCorrida[] carroCorrida;
    public int numCarroCorrida;
    private bool ultrapassou = true;

    // Use this for initialization
    void Start () {
        
       for(int i = 0; i < carroCorrida.Length; i++) {
            carroCorrida[i].numPosicao = numCarroCorrida;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < carroCorrida.Length; i++) {
            if (other.gameObject.tag == "Colisor Frente") {
                if (carroCorrida[i].pontosPartida == this.gameObject.GetComponent<AICarroCorrida>().pontosPartida && !ultrapassou) {
                    carroCorrida[i].numPosicao++;
                    ultrapassou = true;
                    if ((carroCorrida[i].numPosicao <= 1)) {
                        carroCorrida[i].numPosicao = 1;
                        carroCorrida[i].txt_Posicao.SetText(carroCorrida[i].numPosicao.ToString());
                    } else if (carroCorrida[i].numPosicao >= numCarroCorrida) {
                        carroCorrida[i].numPosicao = numCarroCorrida;
                        carroCorrida[i].txt_Posicao.SetText(carroCorrida[i].numPosicao.ToString());
                    } else {
                        carroCorrida[i].txt_Posicao.SetText(carroCorrida[i].numPosicao.ToString());
                    }
                }
            } else if (other.gameObject.tag == "Colisor Tras") {

                if (carroCorrida[i].pontosPartida == this.gameObject.GetComponent<AICarroCorrida>().pontosPartida && ultrapassou)
                {
                    carroCorrida[i].numPosicao--;
                    ultrapassou = false;
                    if (carroCorrida[i].numPosicao <= 1)
                    {
                        carroCorrida[i].numPosicao = 1;
                        carroCorrida[i].txt_Posicao.SetText(carroCorrida[i].numPosicao.ToString());
                    }
                    else if (carroCorrida[i].numPosicao >= numCarroCorrida)
                    {
                        carroCorrida[i].numPosicao = numCarroCorrida;
                        carroCorrida[i].txt_Posicao.SetText(carroCorrida[i].numPosicao.ToString());
                    }
                    else
                    {
                        carroCorrida[i].txt_Posicao.SetText(carroCorrida[i].numPosicao.ToString());
                    }
                }
            }
        }
    }

}
