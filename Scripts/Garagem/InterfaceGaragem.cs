using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InterfaceGaragem : MonoBehaviour {

    [Header("Pega as cores do background e muda de acordo com a raridade")]
    public Color[] corRaridade;

    private ItensCarro itensCarro;

    [Header("Armazena todos os carros")]
    public GameObject[] carrosNormais;
    public GameObject[] carrosRaros, carrosLendarios;

    private GameController gameController;

    [Header("Pega os textos e muda eles de acordo com o numero de itens pegos")]
    public TextMeshProUGUI[] txtPneuNormal, txtPneuRaro, txtPneuLendario;
    public TextMeshProUGUI[] txtChassiNormal, txtChassiRaro, txtChassiLendario;
    public TextMeshProUGUI[] txtFreioNormal, txtFreioRaro, txtFreioLendario;
    public TextMeshProUGUI[] txtMotorNormal, txtMotorRaro, txtMotorLendario;
    public TextMeshProUGUI[] txtTransmissaoNormal, txtTransmissaoRaro, txtTransmissaoLendario;
    public TextMeshProUGUI[] txtSprayNormal, txtSprayRaro, txtSprayLendario;

    private Loading loading;

    [Space (2)]
    [Header ("Verifica se o botão foi apertado")]
    public bool podeApertarButton = false;
    public byte numBtnApertado = 0; // verifica qual raridade eu estou
    public byte numTabelaSelecionada = 0; // verifica a tabela que eu estou selecionando

    [Header("Pega as imagens do backgrond")]
    public Image[] imgBackground;

    [Header ("Responsavel por pegar as tabela das peças do carro")]
    public GameObject[] tabelasItens;

    [Header("Pega os botões e ativa eles dependendo da raridade")]
    public GameObject[] btnFreio = new GameObject[7];
    public GameObject[] btnChassi = new GameObject[7], btnPneu = new GameObject[7], btnTransmissao = new GameObject[7], btnMotor = new GameObject[7], btnSpray = new GameObject[7];

    [Header("Mostra a porcentagem dos itens do carro")]
    public Image img_Freio;
    public Image img_Peso, img_Velocidade;
    [Header("Armazena os valores do carro")]
    public float status_Freio;
    public float status_Peso, status_Velocidade;

    private AudioSource audioSource;
    [Header ("Pega o som de clique")]
    [SerializeField]
    private AudioClip som_Click;
    [SerializeField]
    private AudioClip som_Voltar;

    [Header("Verifica se eu posso apertar o botao")]
    [SerializeField]
    private bool podeApertar = true;
    public float tempoApertar;
    public GameObject botaoRaridade, botaoVoltar;

    private GamepadCanvas gamepadCanvas;

    public GameObject txt_avisoCarro;

    // Use this for initialization
    void Start () {
        txt_avisoCarro.SetActive(false);
        gamepadCanvas = FindObjectOfType(typeof(GamepadCanvas)) as GamepadCanvas;
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        itensCarro = FindObjectOfType(typeof(ItensCarro)) as ItensCarro;
        imgBackground[0].color = corRaridade[0];
        Time.timeScale = 0;
        numBtnApertado = 1;

        if (itensCarro.carroEscolhido != null)
        {
            Destroy(itensCarro.carroEscolhido);
        }

        #region responsavel por colocar os valores iniciais na tabela
        int i = 0;
        foreach (string itens in itensCarro.chassiNormal)
        {
            txtChassiNormal[i].SetText(itens);
            i++;
        }
        i = 0;
        foreach (string itens in itensCarro.pneuNormal)
        {
            txtPneuNormal[i].SetText(itens);
            i++;
        }
        i = 0;
        foreach (string itens in itensCarro.freioNormal)
        {
            txtFreioNormal[i].SetText(itens);
            i++;
        }
        i = 0;
        foreach (string itens in itensCarro.transmissaoNormal)
        {
            txtTransmissaoNormal[i].SetText(itens);
            i++;
        }
        i = 0;
        foreach (string itens in itensCarro.motorNormal)
        {
            txtMotorNormal[i].SetText(itens);
            i++;
        }
        #endregion

        status_Freio = 0;
        status_Peso = 0;
        status_Velocidade = 0;
        img_Freio.transform.localScale = new Vector3(0, 1, 1);
        img_Peso.transform.localScale = new Vector3(0, 1, 1);
        img_Velocidade.transform.localScale = new Vector3(0, 1, 1);

        calculoStatus(); // chama a função para atualizar os status
        imgBackground[0].color = corRaridade[0];
    }


    private void LateUpdate()
    {

        calculoStatus(); // chama a função para atualizar os status
        if(Input.GetButton("Controle 01 R1") && podeApertar) {
            audioSource.PlayOneShot(som_Click);
            podeApertar = false;
            if (tabelasItens[0].activeInHierarchy)
            {

                tabelasItens[0].SetActive(false);
                tabelasItens[5].SetActive(true);

            }
            else if (tabelasItens[1].activeInHierarchy)
            {

                tabelasItens[1].SetActive(false);
                tabelasItens[0].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.motorNormal)
                {
                    txtMotorNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[2].activeInHierarchy)
            {

                tabelasItens[2].SetActive(false);
                tabelasItens[1].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.chassiNormal)
                {
                    txtChassiNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[3].activeInHierarchy)
            {

                tabelasItens[3].SetActive(false);
                tabelasItens[2].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.transmissaoNormal)
                {
                    txtTransmissaoNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[4].activeInHierarchy)
            {

                tabelasItens[4].SetActive(false);
                tabelasItens[3].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.pneuNormal)
                {
                    txtPneuNormal[i].SetText(itens);
                    i++;
                }
            }
            else if (tabelasItens[5].activeInHierarchy)
            {

                tabelasItens[5].SetActive(false);
                tabelasItens[4].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.freioNormal)
                {
                    txtFreioNormal[i].SetText(itens);
                    i++;
                }
            }

            StartCoroutine("tempoEspera");
            EventSystem.current.SetSelectedGameObject(botaoRaridade);

        } else if(Input.GetButton("Controle 01 L1") && podeApertar) {
            audioSource.PlayOneShot(som_Click);
            podeApertar = false;

            if (tabelasItens[0].activeInHierarchy)
            {

                tabelasItens[0].SetActive(false);
                tabelasItens[1].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.chassiNormal)
                {
                    txtChassiNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[1].activeInHierarchy)
            {

                tabelasItens[1].SetActive(false);
                tabelasItens[2].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.transmissaoNormal)
                {
                    txtTransmissaoNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[2].activeInHierarchy)
            {

                tabelasItens[2].SetActive(false);
                tabelasItens[3].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.pneuNormal)
                {
                    txtPneuNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[3].activeInHierarchy)
            {

                tabelasItens[3].SetActive(false);
                tabelasItens[4].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.freioNormal)
                {
                    txtFreioNormal[i].SetText(itens);
                    i++;
                }

            }
            else if (tabelasItens[4].activeInHierarchy)
            {

                tabelasItens[4].SetActive(false);
                tabelasItens[5].SetActive(true);
            }
            else if (tabelasItens[5].activeInHierarchy)
            {

                tabelasItens[5].SetActive(false);
                tabelasItens[0].SetActive(true);
                int i = 0;
                foreach (string itens in itensCarro.motorNormal)
                {
                    txtMotorNormal[i].SetText(itens);
                    i++;
                }
            }

            StartCoroutine("tempoEspera");
            EventSystem.current.SetSelectedGameObject(botaoRaridade);
        } else if(Input.GetButton("Controle 01 Bolinha") && podeApertar) {
            audioSource.PlayOneShot(som_Voltar);
            podeApertar = false;
            EventSystem.current.SetSelectedGameObject(botaoVoltar);
            StartCoroutine("tempoEspera");
            if(gamepadCanvas.selectedObj == botaoVoltar) {
                for (int i = 0; i < carrosNormais.Length; i++)
                {
                    if (carrosNormais[i].activeInHierarchy)
                    {
                        carrosNormais[i].GetComponent<CarroPower>().powerCar();
                    }
                }

                loading.carregarLoading("Cidade Rica");
                DontDestroyOnLoad(itensCarro.carroEscolhido);
                Time.timeScale = 1;
            }
        }

        // ativa o texto se nenhum carro estiver ativo no momento
        if(!carrosNormais[0].activeInHierarchy && !carrosNormais[1].activeInHierarchy && !carrosNormais[2].activeInHierarchy) {
            txt_avisoCarro.SetActive(true);
        } else {
            txt_avisoCarro.SetActive(false);
        }


    }

    // calcula o status e chama para atualizar o satus
    private void calculoStatus()
    {
        // faz o calculo para definir os status do carro
        for (int i = 0; i < carrosNormais.Length; i++)
        {
            if (carrosNormais[i].activeInHierarchy)
            {
                status_Freio = 0;
                status_Peso = 0;
                status_Velocidade = 0;
                if (status_Freio >= 1)
                {
                    img_Freio.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {

                    status_Freio = (carrosNormais[i].GetComponent<CarroPower>().freioTransmissao + carrosNormais[i].GetComponent<CarroPower>().freioChassi +
                        carrosNormais[i].GetComponent<CarroPower>().freioPneu + carrosNormais[i].GetComponent<CarroPower>().freio );
                    status_Freio /= 5000;
                    img_Freio.transform.localScale = new Vector3(status_Freio, 1, 1);
                }

                if (status_Peso > 1)
                {
                    img_Peso.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    status_Peso = carrosNormais[i].GetComponent<CarroPower>().massaMotor + carrosNormais[i].GetComponent<CarroPower>().massaFreio +
                    carrosNormais[i].GetComponent<CarroPower>().massaChassi + carrosNormais[i].GetComponent<CarroPower>().massaPneu + carrosNormais[i].GetComponent<CarroPower>().massaTransmissao;
                    status_Peso /= 3000;
                    img_Peso.transform.localScale = new Vector3(status_Peso, 1, 1);
                }

                if (status_Velocidade > 1)
                {
                    img_Velocidade.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    status_Velocidade = carrosNormais[i].GetComponent<CarroPower>().maxTorqueChassi + carrosNormais[i].GetComponent<CarroPower>().maxTorqueMotor +
                    carrosNormais[i].GetComponent<CarroPower>().maxTorqueTransmissao;
                    status_Velocidade /= 5000;
                    img_Velocidade.transform.localScale = new Vector3(status_Velocidade, 1, 1);
                }

            }
        }
    }

    #region responsavel pelo botão de raridade
    public void btnNormal()
    {
        if (!podeApertarButton)
        {
            numBtnApertado = 1; // indica que o btn normal foi apertado
            for(int contador = 0; contador < imgBackground.Length; contador++) {
                imgBackground[contador].color = corRaridade[0];
            }

            for(int contador = 0; contador < btnChassi.Length; contador++) {
                btnChassi[contador].SetActive(true);
                btnFreio[contador].SetActive(true);
                btnMotor[contador].SetActive(true);
                btnPneu[contador].SetActive(true);
                btnTransmissao[contador].SetActive(true);
            }

            int i = 0;
            foreach(string itens in itensCarro.chassiNormal) {
                txtChassiNormal[i].SetText(itens);
                i++;
            }
            i = 0;
            foreach (string itens in itensCarro.pneuNormal)
            {
                txtPneuNormal[i].SetText(itens);
                i++;
            }
            i = 0;
            foreach (string itens in itensCarro.freioNormal)
            {
                txtFreioNormal[i].SetText(itens);
                i++;
            }
            i = 0;
            foreach (string itens in itensCarro.transmissaoNormal)
            {
                txtTransmissaoNormal[i].SetText(itens);
                i++;
            }
            i = 0;
            foreach (string itens in itensCarro.motorNormal)
            {
                txtMotorNormal[i].SetText(itens);
                i++;
            }
        }

    }

    public void btnRaro()
    {
        if (!podeApertarButton)
        {
            numBtnApertado = 2; // indica que o btn raro foi apertado

            for (int contador = 0; contador < btnChassi.Length; contador++)
            {

                if (contador >= 5)
                {
                    btnChassi[contador].SetActive(false);
                    btnFreio[contador].SetActive(false);
                    btnMotor[contador].SetActive(false);
                    btnPneu[contador].SetActive(false);
                    btnTransmissao[contador].SetActive(false);
                } else {
                    btnChassi[contador].SetActive(true);
                    btnFreio[contador].SetActive(true);
                    btnMotor[contador].SetActive(true);
                    btnPneu[contador].SetActive(true);
                    btnTransmissao[contador].SetActive(true);
                }
            }

            for (int contador = 0; contador < imgBackground.Length; contador++)
            {
                imgBackground[contador].color = corRaridade[1];
            }

        }
    }

    public void btnLendario()
    {
        if (podeApertarButton == false)
        {

            for (int contador = 0; contador < btnChassi.Length; contador++)
            {
                if (contador >= 3)
                {
                    btnChassi[contador].SetActive(false);
                    btnFreio[contador].SetActive(false);
                    btnMotor[contador].SetActive(false);
                    btnPneu[contador].SetActive(false);
                    btnTransmissao[contador].SetActive(false);
                }
                else
                {
                    btnChassi[contador].SetActive(true);
                    btnFreio[contador].SetActive(true);
                    btnMotor[contador].SetActive(true);
                    btnPneu[contador].SetActive(true);
                    btnTransmissao[contador].SetActive(true);
                }
            }

            numBtnApertado = 3; // indica que o btn normal foi apertado
            for (int contador = 0; contador < imgBackground.Length; contador++)
            {
                imgBackground[contador].color = corRaridade[2];
            }

        }
    }
    #endregion

    public void sair()
    {
        
        for(int i = 0; i < carrosNormais.Length; i++) {
            if(carrosNormais[i].activeInHierarchy) {
                carrosNormais[i].GetComponent<CarroPower>().powerCar();
            }
        }

        gamepadCanvas.isCanvas = false;
        loading.carregarLoading("Cidade Rica");
        DontDestroyOnLoad(itensCarro.carroEscolhido);
        Time.timeScale = 1;
        

    }

    #region responsavel por add o motor ao carro
    public void btnMotorCarro(int numero) {
        if(numBtnApertado == 1) {
            int i = 0;
            switch (numero) {
                case 1:

                    i = 0;
                    foreach(int aceleracao in itensCarro.aceleracaoMotorNormal) {
                        if(i == 0) {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        } else {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {

                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 2:
                    print("foi");
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoMotorNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 3:
                    print("foi");
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoMotorNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 4:
                    print("foi");
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoMotorNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 5:
                    print("foi");
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoMotorNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 6:
                    print("foi");
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoMotorNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 7:
                    print("foi");
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoMotorNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueMotor = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int peso in itensCarro.pesoMotorNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaMotor = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaMotorNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaMotor = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
            }
        }
    }
    #endregion

    #region responsavel por add o chassi ao carro
    public void btnChassiCarro(int numero)
    {
        if (numBtnApertado == 1)
        {
            int i = 0;
            int cont = 0;
            switch (numero)
            {
                case 1:

                    i = 0;
                    
                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i == 0)
                        {
                            foreach(string carro in itensCarro.carrosNormais) {
                                if (cont == 0)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        } else {
                            break;
                        }
                        i++;
                    }
                    
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i == 0)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 2:
                    print("foi");
                    i = 0;

                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i <= 1)
                        {
                            foreach (string carro in itensCarro.carrosNormais)
                            {
                                if (cont <= 1)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            print(carro);
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i <= 1)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 3:
                    print("foi");
                    i = 0;

                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i <= 2)
                        {
                            foreach (string carro in itensCarro.carrosNormais)
                            {
                                if (cont <= 2)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i <= 2)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 4:
                    print("foi");
                    i = 0;

                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i <= 3)
                        {
                            foreach (string carro in itensCarro.carrosNormais)
                            {
                                if (cont <= 3)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i <= 3)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 5:
                    print("foi");
                    i = 0;

                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i <= 4)
                        {
                            foreach (string carro in itensCarro.carrosNormais)
                            {
                                if (cont <= 4)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i <= 4)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 6:
                    print("foi");
                    i = 0;

                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i <= 5)
                        {
                            foreach (string carro in itensCarro.carrosNormais)
                            {
                                if (cont <= 5)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i <= 5)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 7:
                    print("foi");
                    i = 0;

                    foreach (int peso in itensCarro.pesoChassiNormal)
                    {
                        if (i <= 6)
                        {
                            foreach (string carro in itensCarro.carrosNormais)
                            {
                                if (cont <= 6)
                                {
                                    for (int contador = 0; contador < carrosNormais.Length; contador++)
                                    {
                                        carrosNormais[0].SetActive(false);
                                        carrosNormais[1].SetActive(false);
                                        carrosNormais[2].SetActive(false);
                                        if (carrosNormais[contador].name == carro)
                                        {
                                            gameController.carroUsado = carrosNormais[contador];
                                            itensCarro.carroEscolhido = carrosNormais[contador];
                                            carrosNormais[contador].SetActive(true);
                                            carrosNormais[contador].GetComponent<CarroPower>().massaChassi = peso;
                                            break;
                                        }
                                        else
                                        {
                                            carrosNormais[contador].SetActive(false);
                                        }
                                    }
                                }
                                cont++;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoChassiNormal)
                    {
                        if (i <= 6)
                        {

                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueChassi = aceleracao;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaChassiNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaChassi = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioChassiNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioChassi = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
            }
        }
    }
    #endregion

    #region responsavel por add um pneu ao carro
    public void btnPneuCarro(int numero)
    {
        if (numBtnApertado == 1)
        {
            int i = 0;
            switch (numero)
            {
                case 1:

                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    
                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 2:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 3:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
                case 4:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 5:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 6:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 7:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoPneuNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaPneu = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaPneuNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaPneu = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.freioPneuNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioPneu = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
            }
        }
    }
    #endregion

    #region responsavel por add uma transmissao ao carro
    public void btnTransmissaoCarro(int numero)
    {
        if (numBtnApertado == 1)
        {
            int i = 0;
            switch (numero)
            {
                case 1:

                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 2:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 3:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 4:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 5:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 6:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
                case 7:
                    print("foi");
                    i = 0;
                    foreach (int peso in itensCarro.pesoTransmissaoNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaTransmissao = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaTransmissaoNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaTransmissao = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.FreioTransmissaoNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freioTransmissao = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int aceleracao in itensCarro.aceleracaoTransmissaoNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().maxTorqueTransmissao = aceleracao;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    break;
            }
        }
    }
    #endregion

    #region responsavel por add um freio ao carro
    public void btnFreioCarro(int numero)
    {
        if (numBtnApertado == 1)
        {
            int i = 0;
            switch (numero)
            {
                case 1:


                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i == 0)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
                case 2:
                    print("foi");

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i <= 1)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
                case 3:
                    print("foi");

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i <= 2)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }


                    break;
                case 4:
                    print("foi");

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i <= 3)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
                case 5:
                    print("foi");

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i <= 4)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
                case 6:
                    print("foi");

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i <= 5)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
                case 7:
                    print("foi");

                    i = 0;
                    foreach (int resistencia in itensCarro.resistenciaFreioNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().resistenciaFreio = resistencia;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i = 0;
                    foreach (int freio in itensCarro.forcaFreioNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().freio = freio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    i = 0;
                    foreach (int peso in itensCarro.pesoFreioNormal)
                    {
                        if (i <= 6)
                        {
                            for (int contador = 0; contador < carrosNormais.Length; contador++)
                            {
                                if (carrosNormais[contador].activeInHierarchy)
                                {
                                    carrosNormais[contador].GetComponent<CarroPower>().massaFreio = peso;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    break;
            }
        }
    }
    #endregion


    IEnumerator tempoEspera() {
        yield return new WaitForSecondsRealtime(tempoApertar);
        podeApertar = true;

    }

}
