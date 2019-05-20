using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGame : MonoBehaviour{

    public GameObject apple;
    public GameObject banana;
    public GameObject bear;
    public GameObject cat;
    public GameObject dog;
    public GameObject hourse;
    public GameObject monkey;
    public GameObject orange;
    public GameObject pineapple;
    public GameObject strawberry;
    public Text labelInformativo;
    public int[] visitados;
    // Start is called before the first frame update
    void Start(){
        visitados = new int[10];
        for(int index = 0; index < visitados.Length ; index++){
            visitados[index] = index;
        }
        selectGameObject();
    }
    // Update is called once per frame
    void Update(){
        
    }
    public void selectGameObject(){
        //Ocultamos los otros objetos
        apple.SetActive(false);
        banana.SetActive(false);
        bear.SetActive(false);
        cat.SetActive(false);
        dog.SetActive(false);
        hourse.SetActive(false);
        monkey.SetActive(false);
        orange.SetActive(false);
        pineapple.SetActive(false);
        strawberry.SetActive(false);

        if(visitados.Length > 0){
            //Buscamos el objeto aleatoriamente
            int value = Random.Range(0, (visitados.Length-1));
            //Mostramos el objeto
            switch(visitados[value]){
                case 0:
                    labelInformativo.text = "Apple";
                    apple.SetActive(true);
                    apple.GetComponent<AudioSource>().Play();
                break;
                case 1:
                    labelInformativo.text = "Banana";
                    banana.SetActive(true);
                    banana.GetComponent<AudioSource>().Play();
                break;
                case 2:
                    labelInformativo.text = "Bear";
                    bear.SetActive(true);
                    bear.GetComponent<AudioSource>().Play();
                break;
                case 3:
                    labelInformativo.text = "Cat";
                    cat.SetActive(true);
                    cat.GetComponent<AudioSource>().Play();
                break;
                case 4:
                    labelInformativo.text = "Dog";
                    dog.SetActive(true);
                    dog.GetComponent<AudioSource>().Play();
                break;
                case 5:
                    labelInformativo.text = "Hourse";
                    hourse.SetActive(true);
                    hourse.GetComponent<AudioSource>().Play();
                break;
                case 6:
                    labelInformativo.text = "Monkey";
                    monkey.SetActive(true);
                    monkey.GetComponent<AudioSource>().Play();
                break;
                case 7:
                    labelInformativo.text = "Orange";
                    orange.SetActive(true);
                    orange.GetComponent<AudioSource>().Play();
                break;
                case 8:
                    labelInformativo.text = "Pineapple";
                    pineapple.SetActive(true);
                    pineapple.GetComponent<AudioSource>().Play();
                break;
                case 9:
                    labelInformativo.text = "Strawberry";
                    strawberry.SetActive(true);
                    strawberry.GetComponent<AudioSource>().Play();
                break;
            }
            //Eliminamos el indice del arreglo
            int[] tmp = new int[(visitados.Length-1)];
            int index = 0;
            for(int aux = 0 ; aux < visitados.Length ; aux ++){
                if(value != aux){
                    tmp[index] = visitados[aux];
                    index = index + 1;
                }
            }
            //se mueven al vector original
            visitados = new int[tmp.Length];
            for(int aux = 0 ; aux < tmp.Length ; aux ++){
                visitados[aux] = tmp[aux];
            }
        }else{
            labelInformativo.text = "Prueba en la sección de conocimientos tu aprendizaje";
        }
    }
}
