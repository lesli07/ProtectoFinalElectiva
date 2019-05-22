using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public string[] options = new string[4];
    public int correct;
    private Vector3 sizeInit;
    // Start is called before the first frame update
    void Start()
    {
        sizeInit = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] getOptions(){
        return options;
    }

    public int getCorrect(){
        return correct;
    }

    public void hiddenObject(){
        Debug.Log("APP - hidden ");
        transform.localScale = new Vector3(0,0,0);
    }

    public void showObject(){
        Debug.Log("APP - hidden ");
        transform.localScale = new Vector3(sizeInit.x, sizeInit.y,sizeInit.z);
    }
}
