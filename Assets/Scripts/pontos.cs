using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pontos : MonoBehaviour
{
    public GameObject player;
    public Text pont;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       pont.text = "Pontos: " + player.GetComponent<playerMove>().pontuacao.ToString() + " de 7";
    }
}
