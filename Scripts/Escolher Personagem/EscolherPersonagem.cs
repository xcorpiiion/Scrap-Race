using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscolherPersonagem : MonoBehaviour {

    public Transform pontoAdan, pontoSelena;
    public GameObject painelEscolha, painelBotoes, painelMenu;
    public TextMeshProUGUI txt_Personagem, txt_VoltarMenu;
    private byte numPersonagemEscolhido = 1; // adan é o 0 e a selena é a 1
    public float tempoEspera;
    private bool podeApertar = true, escolheuPlayer;
    private Loading loading;
    public TextMeshProUGUI[] txt_Botoes;
    private AudioSource audioSource;
    public AudioClip[] sound_Botoes, vozes;
    public Animator[] animacaoPersonagens;
    public GameObject[] personagem;
    private Pecas pecas;

    // Use this for initialization
    void Start () {

        pecas = FindObjectOfType(typeof(Pecas)) as Pecas;
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        animacaoPersonagens[0] = personagem[0].GetComponent<Animator>();
        animacaoPersonagens[1] = personagem[1].GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, transform.position.y, pontoSelena.position.z);

        audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("Idioma") == 0) {
            txt_VoltarMenu.SetText("Você quer voltar ao menu ?");
            txt_Botoes[0].SetText("Sim");
            txt_Botoes[1].SetText("Não");
            txt_Botoes[2].SetText("Escolher Personagem");
            txt_Botoes[3].SetText("Voltar ao Menu");
            txt_Botoes[4].SetText("Sim");
            txt_Botoes[5].SetText("Não");

        } else {
            txt_VoltarMenu.SetText("Do you want to back menu ?");
            txt_Botoes[0].SetText("Yes");
            txt_Botoes[1].SetText("No");
            txt_Botoes[2].SetText("Choice Player");
            txt_Botoes[3].SetText("Back to Menu");
            txt_Botoes[4].SetText("Yes");
            txt_Botoes[5].SetText("No");
        }

        painelEscolha.SetActive(false);
        painelMenu.SetActive(false);

    }
	
	// Update is called once per frame
	void LateUpdate () {
		if((Input.GetAxis("Controle 01 Direcional X") > 0 || Input.GetAxis("Horizontal") > 0) && painelBotoes.activeInHierarchy && podeApertar) {
            audioSource.PlayOneShot(sound_Botoes[2]);
            podeApertar = false;
            numPersonagemEscolhido = 0;
            StartCoroutine("tempoApertar");
        } else if ((Input.GetAxis("Controle 01 Direcional X") < 0 || Input.GetAxis("Horizontal") < 0) && painelBotoes.activeInHierarchy && podeApertar) {
            audioSource.PlayOneShot(sound_Botoes[2]);
            podeApertar = false;
            numPersonagemEscolhido = 1;
            StartCoroutine("tempoApertar");
        }

        if (numPersonagemEscolhido == 0)
        {
            if (transform.position.z != pontoAdan.position.z)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, pontoAdan.position.z), 1f * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.z != pontoSelena.position.z)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, pontoSelena.position.z), 1f * Time.deltaTime);
            }
        }


        if(Input.GetButton("Controle 01 X") && podeApertar && !escolheuPlayer) {
            audioSource.PlayOneShot(sound_Botoes[0]);
            podeApertar = false;
            if(painelBotoes.activeInHierarchy) {
                painelBotoes.SetActive(false);
                painelEscolha.SetActive(true);
                if(numPersonagemEscolhido == 0) {
                    if(PlayerPrefs.GetInt("Idioma") == 0) {
                        txt_Personagem.SetText("Você deseja escolher o Adan ?");
                    } else {
                        txt_Personagem.SetText("Do you want to choice the Adan ?");
                    }
                } else {
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        txt_Personagem.SetText("Você deseja escolher a Selena ?");
                    }
                    else
                    {
                        txt_Personagem.SetText("Do you want to choice the Selena ?");
                    }
                }
            } else if(painelEscolha.activeInHierarchy) {
                if(numPersonagemEscolhido == 0) {
                    PlayerPrefs.SetInt("Personagem", 0);
                    painelEscolha.SetActive(false);
                    animacaoPersonagens[0].SetBool("escolheuJogador", true);
                    StartCoroutine("tempoAnimacao");
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(vozes[2]);
                    }
                    else
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(vozes[3]);
                    }

                } else {
                    PlayerPrefs.SetInt("Personagem", 1);
                    painelEscolha.SetActive(false);
                    animacaoPersonagens[1].SetBool("escolheuJogador", true);
                    StartCoroutine("tempoAnimacao");
                    if (PlayerPrefs.GetInt("Idioma") == 0)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(vozes[0]);
                    }
                    else
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(vozes[1]);
                    }

                }
            } else {
                
                loading.carregarLoading("MainMenu");
            }
            StartCoroutine("tempoApertar");
        }

        if(Input.GetButton("Controle 01 Bolinha") && podeApertar) {
            podeApertar = false;
            audioSource.PlayOneShot(sound_Botoes[1]);
            if (painelEscolha.activeInHierarchy) {
                painelEscolha.SetActive(false);
                painelBotoes.SetActive(true);
            } else if(painelBotoes.activeInHierarchy) {
                painelEscolha.SetActive(false);
                painelBotoes.SetActive(false);
                painelMenu.SetActive(true);
            } else {
                painelBotoes.SetActive(true);
                painelMenu.SetActive(false);
            }
            StartCoroutine("tempoApertar");
        }

    }

    IEnumerator tempoApertar()
    {
        yield return new WaitForSecondsRealtime(tempoEspera);
        podeApertar = true;

    }

    IEnumerator tempoAnimacao()
    {
        escolheuPlayer = true;
        yield return new WaitForSecondsRealtime(5);
        pecas.numeroFerro[0] = 5;
        pecas.numeroAco[0] = 5;
        pecas.numeroAluminio[0] = 5;
        pecas.numBorracha[0] = 5;
        loading.carregarLoading("Mapa Cutscene");

    }



}
