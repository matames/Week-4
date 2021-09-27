using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMove : MonoBehaviour { 

    public GameObject Player;
    public float yPos;
    public float zPos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, yPos,zPos);
        
    }
}
