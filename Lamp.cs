using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {
    bool used = false;
    bool attached = false;

    public float lampRadius;
    private SpriteRenderer render;
    LineRenderer lineRender;
    GameObject player;

    public bool getUsed()
    {
        return used;
    }

    public void use()
    {
        this.used = true;
        this.attached = true;
    }

    public void detatch()
    {
        this.attached = false;
    }

    public void reset()
    {
        //make sure the renderer is found
        render = GetComponent<SpriteRenderer>();

        this.used = false;
        transform.Find("bounding box").GetComponent<LampRange>().reset();

        render.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
            render.color = Color.green;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!used)
        {
            render.color = Color.red;
        }
    }

    // Use this for initialization
    void Start () {
        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        coll.size = new Vector3(lampRadius * 2, lampRadius * 2, 0);

        lineRender = GetComponent<LineRenderer>();

        render = GetComponent<SpriteRenderer>();
        render.color = Color.red;

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (used)
        {
            transform.Find("bounding box").localScale = new Vector3(0, 0, 0);

            if (attached)
            {
                lineRender.SetPosition(0, new Vector3(player.transform.position.x, player.transform.position.y, -2));
                lineRender.SetPosition(1, new Vector3(transform.position.x, transform.position.y, -2));
            }
            else
            {
                lineRender.SetPosition(0, new Vector3(0, 0, -2));
                lineRender.SetPosition(1, new Vector3(0, 0, -2));
            }
        }
	}
}
