using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Player;
    public float smoothRate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        Vector3 PlayerPos = Player.position;
        Vector3 cameraPos = transform.position;

        //cameraPos.x = PlayerPos.x;
        cameraPos.y = PlayerPos.y;
        //cameraPos = new Vector3(PlayerPos,PlayerPos,0);

        //cameraPos = PlayerPos;
        cameraPos.x = Mathf.Lerp(cameraPos.x, PlayerPos.x, smoothRate); //linear interpolate
        transform.position = cameraPos;
    }
}
