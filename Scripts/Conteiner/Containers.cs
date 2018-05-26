using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containers : MonoBehaviour {

    public bool normal, raro, lendario;
    public int quantidadeItem, jaPodeColetar, raridade;
    public string[] nomeItens = new string[5], itensNoConteiner = new string[3];
    public bool podeColetar = false;
    private ModoExploracao modoExploracao;
    public GameObject luz;
    public byte tempoItens;

    private void Awake()
    {
        nomeItens[0] = "Aco";
        nomeItens[1] = "Aluminio";
        nomeItens[2] = "Borracha";
        nomeItens[3] = "Ferro";
        nomeItens[4] = "Spray";
    }

    // Use this for initialization
    void Start () {

        modoExploracao = FindObjectOfType(typeof(ModoExploracao)) as ModoExploracao;

        coletarOuNaoColetar();
        definirRaridade();
        definirItens();
        StartCoroutine("tempoEspera");

    }
	
    // responsavel por definir os tipos de itens no conteiners
	private void definirItens() {
        if(normal) {
            for(int i = 0; i < itensNoConteiner.Length; i++) {
                quantidadeItem = Random.Range(1, 6);
                switch(quantidadeItem) {
                    case 1:
                        itensNoConteiner[i] = "Aco";
                        break;
                    case 2:
                        itensNoConteiner[i] = "Aluminio";
                        break;
                    case 3:
                        itensNoConteiner[i] = "Borracha";
                        break;
                    case 4:
                        itensNoConteiner[i] = "Ferro";
                        break;
                    
                }
            }

        } else if(raro) {
            for (int i = 0; i < itensNoConteiner.Length; i++)
            {
                quantidadeItem = Random.Range(1, 6);
                switch (quantidadeItem)
                {
                    case 1:
                        itensNoConteiner[i] = "Aco";
                        break;
                    case 2:
                        itensNoConteiner[i] = "Aluminio";
                        break;
                    case 3:
                        itensNoConteiner[i] = "Borracha";
                        break;
                    case 4:
                        itensNoConteiner[i] = "Ferro";
                        break;
                    
                }
            }
        } else {
            for (int i = 0; i < itensNoConteiner.Length; i++)
            {
                quantidadeItem = Random.Range(1, 6);
                switch (quantidadeItem)
                {
                    case 1:
                        itensNoConteiner[i] = "Aco";
                        break;
                    case 2:
                        itensNoConteiner[i] = "Aluminio";
                        break;
                    case 3:
                        itensNoConteiner[i] = "Borracha";
                        break;
                    case 4:
                        itensNoConteiner[i] = "Ferro";
                        break;
                    
                }
            }
        }
    }

    public void coletarOuNaoColetar() {
        jaPodeColetar = Random.Range(0, 2);
        if(jaPodeColetar == 0) {
            this.gameObject.tag = "Untagged";
            luz.SetActive(false);
        } else {
            this.gameObject.tag = "Conteiner";
            luz.SetActive(true);
        }
    }

    public void definirRaridade() {
        raridade = Random.Range(0, 101);

        if (raridade >= 0 && raridade <= 50)
        {
            raro = false;
            lendario = false;
            normal = true;

        }
        else if (raridade >= 51 && raridade <= 85)
        {
            normal = false;
            lendario = false;
            raro = true;

        }
        else
        {
            normal = false;
            raro = false;
            lendario = true;

        }
    }

    IEnumerator tempoEspera()
    {
        if (modoExploracao.tempo <= 0)
        {
            StopCoroutine("tempoEspera");
        }
        yield return new WaitForSeconds(tempoItens);
        if (modoExploracao.tempo > 0)
        {
            definirRaridade();
            definirItens();
            coletarOuNaoColetar();
            StartCoroutine("tempoEspera");
        }

    }
}
