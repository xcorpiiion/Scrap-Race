using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensCarro : MonoBehaviour {

    #region responsavel pelos status do carros pelos carros

    public GameObject carroEscolhido;

    [Header("Responsavel pelo Chassi")]
    #region responsavel pelo chassi
    public List<string> chassiNormal = new List<string>();
    public List<int> pesoChassiNormal = new List<int>();
    public List<int> aceleracaoChassiNormal = new List<int>();
    public List<int> freioChassiNormal = new List<int>();
    public List<float> resistenciaChassiNormal = new List<float>();

    public List<string> chassiRaro = new List<string>();
    public List<int> pesoChassiRaro = new List<int>();
    public List<int> aceleracaoChassiRaro = new List<int>();
    public List<int> freioChassiRaro = new List<int>();
    public List<float> resistenciaChassiRaro = new List<float>();

    public List<string> chassiLendario = new List<string>();
    public List<int> pesoChassiLendario = new List<int>();
    public List<int> aceleracaoChassiLendario = new List<int>();
    public List<int> freioChassiLendario = new List<int>();
    public List<float> resistenciaChassiLendario= new List<float>();

    #endregion

    [Space (2)]
    [Header ("Responsavel pelo Motor")]
    #region responsavel pelo motor
    public List<string> motorNormal = new List<string>();
    public List<int> pesoMotorNormal = new List<int>();
    public List<int> aceleracaoMotorNormal = new List<int>();
    public List<float> resistenciaMotorNormal = new List<float>();

    public List<string> motorRaro = new List<string>();
    public List<int> pesoMotorRaro = new List<int>();
    public List<int> aceleracaoMotorRaro = new List<int>();
    public List<float> resistenciaMotorRaro = new List<float>();

    public List<string> motorLendario = new List<string>();
    public List<int> pesoMotorLendario = new List<int>();
    public List<int> aceleracaoMotorLendario = new List<int>();
    public List<float> resistenciaMotorLendario = new List<float>();

    #endregion

    [Space(2)]
    [Header("Responsavel pelo Transmissão")]
    #region responsavel pela transmissão
    public List<string> transmissaoNormal = new List<string>();
    public List<int> pesoTransmissaoNormal = new List<int>();
    public List<int> aceleracaoTransmissaoNormal = new List<int>();
    public List<int> FreioTransmissaoNormal = new List<int>();
    public List<float> resistenciaTransmissaoNormal = new List<float>();

    public List<string> transmissaoRaro = new List<string>();
    public List<int> pesoTransmissaoRaro = new List<int>();
    public List<int> aceleracaoTransmissaoRaro = new List<int>();
    public List<int> FreioTransmissaoRaro = new List<int>();
    public List<float> resistenciaTransmissaoRaro = new List<float>();

    public List<string> transmissaoLendario = new List<string>();
    public List<int> pesoTransmissaoLendario = new List<int>();
    public List<int> aceleracaoTransmissaoLendario= new List<int>();
    public List<int> FreioTransmissaoLendario = new List<int>();
    public List<float> resistenciaTransmissaoLendario = new List<float>();

    #endregion

    [Space(2)]
    [Header("Responsavel pelo Pneu")]
    #region responsavel pelo pneu
    public List<string> pneuNormal = new List<string>();
    public List<int> pesoPneuNormal = new List<int>();
    public List<int> freioPneuNormal = new List<int>();
    public List<float> resistenciaPneuNormal = new List<float>();

    public List<string> pneuRaro = new List<string>();
    public List<int> pesoPneuRaro = new List<int>();
    public List<int> freioPneuRaro = new List<int>();
    public List<float> resistenciaPneuRaro= new List<float>();

    public List<string> pneuLendario = new List<string>();
    public List<int> pesoPneuLendario = new List<int>();
    public List<int> freioPneuLendario = new List<int>();
    public List<float> resistenciaPneuLendario = new List<float>();

    #endregion

    [Space(2)]
    [Header("Responsavel pelos tipos de carros")]
    #region responsavel pelos carros
    public List<string> carrosNormais = new List<string>();
    public List<string> carrosRaros = new List<string>();
    public List<string> carrosLendarios = new List<string>();
    #endregion

    [Space(2)]
    [Header("Responsavel pelo Freio")]
    #region responsavel pelo freio
    public List<string> freioNormal = new List<string>();
    public List<int> forcaFreioNormal = new List<int>();
    public List<int> pesoFreioNormal = new List<int>();
    public List<float> resistenciaFreioNormal = new List<float>();

    public List<string> freioRaro = new List<string>();
    public List<int> forcaFreioRaro = new List<int>();
    public List<int> pesoFreioRaro= new List<int>();
    public List<float> resistenciaFreioRaro= new List<float>();

    public List<string> freioLendario = new List<string>();
    public List<int> forcaFreioLendario = new List<int>();
    public List<int> pesoFreioLendario = new List<int>();
    public List<float> resistenciaFreioLendario = new List<float>();

    #endregion

    public int[] aceleracao = new int[3], velocidadeMaxima = new int[3];

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	
}
