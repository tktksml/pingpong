using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PhotonView PhotonView;

    [SerializeField] private Rigidbody ballRb = null;
    [SerializeField] private MeshRenderer ballRenderer = null;
    private Vector3 lastVelocity;
    private BallSettings currentSettings;
    private Vector3 defaultScale;


    public void Awake()
    {
        defaultScale = transform.localScale;
    }



    [PunRPC]
    public void Init(BallSettings settings)
    {
        currentSettings = settings;

        ballRenderer.sharedMaterial.color = settings.color;
        transform.localScale = defaultScale * settings.scale;
        transform.position = Vector3.zero;
        if (PhotonNetwork.IsMasterClient)
        {
            //Don't use typical random - Y can be equals ~0!
            float y, x;
            if (Random.Range(0, 2) == 0)
            {
                y = Random.Range(-1f, -3f);
            }
            else
            {
                y = Random.Range(1f, 3f);
            }
            x = Random.Range(-3f, 3f);

            Vector3 dir = new Vector3(x, y, 0).normalized;
            ballRb.velocity = dir * settings.speed;
        }
    }


    public void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        lastVelocity = ballRb.velocity;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (collision.gameObject.tag == "TopLoseZone")
        {
            PhotonView.RPC("OnWin", RpcTarget.All);
        }
        else if (collision.gameObject.tag == "BotLoseZone")
        {
            PhotonView.RPC("OnLose", RpcTarget.All);
        }
        else if (collision.gameObject.tag == "Player")
        {
            float x = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            float y = -lastVelocity.y;
            Vector2 dir = new Vector2(x, y).normalized;
            ballRb.velocity = dir * currentSettings.speed;
        }
        else
        {
            Vector3 dir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal).normalized;
            ballRb.velocity = dir * currentSettings.speed;
        }
    }

    [PunRPC]
    public void OnWin()
    {
        GameController.LoseZoneTriggered(true);
    }
    [PunRPC]
    public void OnLose()
    {
        GameController.LoseZoneTriggered(false);
    }

    /// <summary>
    /// Returns value of touch position
    /// </summary>
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth * 3;
    }
}
