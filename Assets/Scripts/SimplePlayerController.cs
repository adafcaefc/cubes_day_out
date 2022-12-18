using UnityEngine;
using PathCreation;
using Photon.Pun;

public class SimplePlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public float distanceTravelled;

    public Player.PlayerColliderCheck playerColliderCheck;
    public GameObject colliderx;

    // Update is called once per frame
    public PathCreator pathCreator;

    private bool _mouseDown;
    public bool _jumpTicket = true;

    private bool endShown = false;

    private float timeTotal = 0;

    private void CheckAndJump()
    {
        if (_mouseDown && playerColliderCheck.isGrounded && _jumpTicket)
        {
            Debug.Log("Jumping...");
            _jumpTicket = false;

            transform.Rotate(0, 0, 0);
            colliderx.GetComponent<Rigidbody>().transform.Rotate(0, 0, 0);
            colliderx.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            colliderx.GetComponent<Rigidbody>().velocity = new Vector3(colliderx.GetComponent<Rigidbody>().velocity.x, jumpForce * 3.0f, colliderx.GetComponent<Rigidbody>().velocity.z);
        }
        else if ((playerColliderCheck.isGrounded && colliderx.GetComponent<Rigidbody>().velocity.y > -0.5 && colliderx.GetComponent<Rigidbody>().velocity.y < 0.5) || distanceTravelled == 0)
        {
            colliderx.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log("Done jumping...");
            _jumpTicket = true;
        }
        else
        {
            
        }
    }

    void FixedUpdate()
    {
        colliderx.GetComponent<Rigidbody>().AddForce(Vector3.down * (jumpForce * 3.5f), ForceMode.Acceleration);
    }

    public PhotonView view;

    [PunRPC]
    void EndGame(string senderName, string messageText)
    {
        if (!endShown)
        {
            MessageBox.Show("You lost.", "INFO", (result) => { PhotonNetwork.LeaveRoom(); });
            endShown = true;
        }
    }

    void Update()
    {
        if (endShown) { return; }
        _mouseDown = Input.GetMouseButton(0);

        if (pathCreator == null)
        {
            GameObject gameObjectPath = GameObject.FindGameObjectWithTag("GeometryPath");
            pathCreator = gameObjectPath.GetComponent<PathCreator>();
            return;
        }

        CheckAndJump();

        distanceTravelled += speed * Time.deltaTime;
        if (distanceTravelled >= pathCreator.path.length)
        {
            if (!endShown)
            {
                MessageBox.Show("You won, your time is " + timeTotal + " seconds.", "INFO", (result) => { PhotonNetwork.LeaveRoom(); });
                endShown = true;
                view.RPC("EndGame", RpcTarget.Others, PhotonNetwork.LocalPlayer.NickName, "" + timeTotal);
            }
            return;
        }

        timeTotal += Time.deltaTime;

        Vector3 pos = pathCreator.path.GetPointAtDistance(distanceTravelled);
        pos.y = transform.position.y;
        transform.position = pos;

        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        transform.Rotate(0, 0, 90, Space.Self);
    }
}