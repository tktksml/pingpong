using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayer : MonoBehaviour
{
    [SerializeField] private PhotonView PhotonView = null;
    [Header("Settings")]
    public float moveSpeed = 2;


    public void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            //Move to another side
            Vector3 currentPos = transform.position;
            currentPos.y *= -1;
            transform.position = currentPos;
        }
    }
    public void FixedUpdate()
    {
        if (!PhotonView.IsMine)
        {
            return;
        }
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp((MovementController.currentDir * moveSpeed * Time.deltaTime) + currentPos.x, -5.25f, 5.25f);

        transform.position = currentPos;
    }
}
