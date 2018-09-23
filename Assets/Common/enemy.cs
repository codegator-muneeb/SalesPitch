using System.Collections;
using System.Threading;
using UnityEngine;

public class enemy : MonoBehaviour {
    public Transform[] target; //targets to define path
    public const float speed = 1.5f; //speed of rotation
    public float maxDist; //maxDist between targets
    private int current;
    Vector3 origPlace; //position of master card
    Renderer myRenderer;
    Color origColor;  //original colour

    void ChangeColor()
    {
        float dis = Mathf.Abs(Vector3.Distance(origPlace, transform.position));
        float alpha = 1f - dis / maxDist;
        myRenderer.material.color = new Color(origColor.r, origColor.g, origColor.b, alpha);
    }


    void Start()
    {
        GameObject masterTarget = GameObject.FindGameObjectWithTag("MainTarget");
        origPlace = masterTarget.transform.position;
        maxDist = 6.0f;
        myRenderer = GetComponent<Renderer>();
        origColor = myRenderer.material.color;

        //set initialcolor as per position
        ChangeColor();
    }

    void Update () {

        //set color as per position
        ChangeColor();

        //move towards next target
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            
        }
        else
        {
            current = (current + 1) % target.Length;
        }
    }

}
