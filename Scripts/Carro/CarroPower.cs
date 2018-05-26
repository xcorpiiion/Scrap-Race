using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarroPower : MonoBehaviour {

    private RCC_CarControllerV3 carController;
    [Header ("velocidade do carro")]
    public int maxTorqueMotor;
    public int maxTorqueChassi, maxTorqueTransmissao;

    [Header("peso do carro")]
    public int massaMotor;
    public int massaChassi, massaTransmissao, massaPneu, massaFreio;

    [Header ("freio do carro")]
    public int freioTransmissao;
    public int freioPneu, freioChassi, freio;

    [Header ("velocidade maxima em KM")]
    public int maxSpeed;

    [Header("Resistencia das peças do carro")]
    public int resistenciaMotor;
    public int resistenciaChassi, resistenciaFreio, resistenciaPneu, resistenciaTransmissao;

    [Header("status que o carro vai ter durante a corrida")]
    public int maxTorque;
    public int maxMassa, maxFreio;
    private ItensCarro itensCarro;

    private Scene cena;

    private void Awake()
    {
        this.gameObject.tag = "Player";
    }

    // Use this for initialization
    void Start () {
        itensCarro = FindObjectOfType(typeof(ItensCarro)) as ItensCarro;
        carController = FindObjectOfType(typeof(RCC_CarControllerV3)) as RCC_CarControllerV3;
        carController.engineTorque = maxTorque;
        carController.brakeTorque = maxFreio;
        carController.rigid.mass = 1300;



        cena = SceneManager.GetActiveScene();

        
        // ao iniciar o jogo, o carro ganha esses valores iniciais
        if(cena.name != "Garagem")
        {

            carroCorrida();
            print("Foi");
        }
	}
	

    public void powerCar() {
        maxTorque = maxTorqueMotor + maxTorqueChassi + maxTorqueTransmissao;
        maxMassa = massaMotor + massaChassi + massaTransmissao + massaPneu + massaFreio;
        maxFreio = freioTransmissao + freioPneu + freioChassi + freio;
        this.carController.engineTorque = maxTorque;
        this.carController.brakeTorque = maxFreio;
        this.carController.rigid.mass = maxMassa;
    }

    // pega o carro que eu escolhi na garagem e aplica esses valores ao mesmo
    public void carroCorrida() {
        this.carController.engineTorque = itensCarro.carroEscolhido.GetComponent<CarroPower>().maxTorque;
        this.carController.brakeTorque = itensCarro.carroEscolhido.GetComponent<CarroPower>().maxFreio;
        this.carController.rigid.mass = itensCarro.carroEscolhido.GetComponent<CarroPower>().maxMassa;
    }

}
