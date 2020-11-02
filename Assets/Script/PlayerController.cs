using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;
    float xLimit = 20.0f;
    float zLimit = 20.0f;
    public SphereCollider col;
    public LayerMask groundlayers;
    public bool sphereIsOnTheGround = true;
  
    private int currentJump = 0;

    float gravityModifier = 2.5f;
    int spaceTrack = 0;

    float initYPos = 5;

    public Material[] playerMtrs;

    Rigidbody playerRb;
    Renderer playerRdr;
    // Start is called before the first frame update
    void Start()
    {
        float initYPos = 1000;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
        col = GetComponent<SphereCollider>();
        Physics.gravity *= gravityModifier;

        Debug.Log(initYPos);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playerMtrs[3].color;
        }
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (sphereIsOnTheGround ))
        {
            playerRb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            sphereIsOnTheGround = false;
            currentJump++;
            playerRdr.material.color = playerMtrs[0].color;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            sphereIsOnTheGround = true;
            currentJump = 0;
            spaceTrack = 0;

            playerRdr.material.color = playerMtrs[1].color;
            
            
          
        }
    }

    void MovePlayer()
    {

    }

   



}





