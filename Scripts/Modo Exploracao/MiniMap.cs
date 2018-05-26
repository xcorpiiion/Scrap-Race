using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public Transform player;
    public Transform[] playerAtivo = new Transform[3];

    private void Start()
    {

        for (int i = 0; i < playerAtivo.Length; i++)
        {
            if (playerAtivo[i].gameObject.activeInHierarchy)
            {
                player = playerAtivo[i];
            }
        }


    }

    private void LateUpdate()
    {
        if(player == null) {
            for (int i = 0; i < playerAtivo.Length; i++)
            {
                if (playerAtivo[i].gameObject.activeInHierarchy)
                {
                    player = playerAtivo[i];
                }
            }
        }
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

    }
}
