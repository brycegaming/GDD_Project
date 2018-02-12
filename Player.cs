using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    const float movementSpeed = 6.0f;
    const float jumpVelocity = 9.0f;
    Rigidbody2D body;

    bool isJumping = false;

    GameObject[] lamps;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        lamps = GameObject.FindGameObjectsWithTag("Lamp");
        reset();
	}

    public void reset()
    {
        //reset the level
        for (int i = 0; i < lamps.Length; i++)
        {
            lamps[i].GetComponent<Lamp>().reset();
        }

        GetComponent<LampMechanics>().reset();

        GameObject startPlatform = GameObject.FindGameObjectWithTag("StartPlatform");
        transform.position = startPlatform.transform.position + new Vector3(0,startPlatform.transform.localScale.y/2+.1f,0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground"){
            reset();
        }

        isJumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "StartPlatform" || collision.gameObject.tag == "EndPlatform") 
            isJumping = true;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            if(!isJumping)
                body.velocity = new Vector3(-movementSpeed, body.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if(!isJumping)
                body.velocity = new Vector3(movementSpeed, body.velocity.y, 0);
        }
        else
        {
            if(!isJumping)
                body.velocity = new Vector3(0, body.velocity.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJumping)
                body.velocity = new Vector3(body.velocity.x, jumpVelocity, 0);

            isJumping = true;
        }
	}
}
