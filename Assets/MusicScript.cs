using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public GameObject path;
    public AudioSource music;
    Photon.Pun.PhotonView myView;
    GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerInstance");
        for (int i = 0; i < players.Length; i++)
        {
            Photon.Pun.PhotonView view = players[i].GetComponent<Photon.Pun.PhotonView>();
            if (view != null)
            {
                if (view.IsMine)
                {
                    myPlayer = players[i];
                    myView = view;
                    break;
                }
            }
        }
        if (myView == null) { return; }
        SimplePlayerController controller = myPlayer.GetComponent<SimplePlayerController>();
        if (controller.distanceTravelled < 0.5f)
        {
            music.Stop();
            music.time = 0;
        }
        else if (!music.isPlaying && controller.distanceTravelled > 0.5f)
        {
            music.Play();
        }
    }
}
