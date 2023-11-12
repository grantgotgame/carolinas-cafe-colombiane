using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArepaStacking : MonoBehaviour
{
    public GameObject arepa;
    public Transform dropPos;
    public GameObject arow;
    public Rigidbody2D RigidArow;
    public float moveSpeed;
    public float maxArrowPos;
    public float maxArepas;
    public GameObject lastArepa; //leave empty, this is left as public to see it in editor

    // Start is called before the first frame update
    void Start()
    {
        RigidArow.velocity = new Vector2(moveSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //print (arow.transform.position.x);

        if (arow.transform.position.x >= maxArrowPos)
        {
            RigidArow.velocity = new Vector2(-moveSpeed, 0f);
        }

        if (arow.transform.position.x <= -maxArrowPos)
        {
            RigidArow.velocity = new Vector2(moveSpeed, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && maxArepas > 0)
        {
            lastArepa = Instantiate(arepa, dropPos.position, Quaternion.identity);
            maxArepas -= 1;
        }

        if (lastArepa.GetComponent<Rigidbody2D>().velocity.magnitude == 0 && maxArepas == 0)
        {
            print("Game Time");
        }

        if (maxArepas < 0)
        {
            print(lastArepa.GetComponent<Rigidbody2D>().velocity.magnitude);
        }
    }
}
