using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isSelected;
    public bool isWhite;
    public Material selectedRender;
    public Material blackRender;
    public Material whiteRender;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected) {
            gameObject.GetComponent<Renderer>().material = selectedRender;
        } else {
            if(isWhite){
                gameObject.GetComponent<Renderer>().material = whiteRender;
            } else if(!isWhite){
                gameObject.GetComponent<Renderer>().material = blackRender;
            }

        }
    }
}
