using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicGame : MonoBehaviour
{
    // Objetos de interaccion
    public GameObject[] objects;
    // Botones para interactuar
    public GameObject[] buttons;
    // Text de Opciones
    public Text[] options;
    // Label Informativo de Accion siguiente
    public Text labelInformativo;
    // Boton de Accion siguiente
    public GameObject test;
    private GameObject select;
    private int[] visitados;
    private int value;
    private AudioSource audioOk;
    private AudioSource audioFail;
    private AudioSource[] sounds;
    private int points;

    private bool inTest;
    // Start is called before the first frame update
    void Start(){
        sounds = GetComponents<AudioSource>();
        audioOk = sounds[0];
        audioFail = sounds[1];
        inTest = false;
        points = 0;
        labelInformativo.text = "Points : " + points;
        initGame();
    }
    // Update is called once per frame
    void Update(){
        //Validacion de los objetos impactados
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
            Ray rayo = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit objetoImpacto;
            if (Physics.Raycast(rayo, out objetoImpacto)){
                if (objetoImpacto.transform.gameObject.CompareTag("repeatAudio")){
                    inTest = !inTest;
                    selectObject();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_1") && select.GetComponent<Options>().getCorrect() == 1){
                    option_ok();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_2") && select.GetComponent<Options>().getCorrect() == 2){
                    option_ok();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_3") && select.GetComponent<Options>().getCorrect() == 3){
                    option_ok();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_4") && select.GetComponent<Options>().getCorrect() == 4){
                    option_ok();
                }
                else{
                    option_fail();
                }
            }
            else
            {
                Debug.Log("No se encontro impacto");
            }
        }
    }
    // Activar o desactivar Botones
    private void desactivateButtons(bool viewNext){
        test.SetActive(!viewNext);
        for(int index  = 0 ; index < buttons.Length ; index ++){
            buttons[index].SetActive(viewNext);
        }
    }
    // Inicializando los indices de los visitados dependiendo de la cantidad de objetos
    private void initIndex(){
        visitados = new int[objects.Length];
        for(int index = 0 ; index < objects.Length ; index ++){
            visitados[index] = index;
        }
    }
    // Ocultando inicialmente todos los objetos
    private void noViewObjects(){
        for(int index  = 0 ; index < objects.Length ; index ++){
            objects[index].SetActive(false);
        }
    }
    // Generando un nuevo objeto seleccionado
    private void selectObject(){
        Debug.Log("inTest " + inTest);
        if(visitados.Length == 0){
            desactivateButtons(false);
            labelInformativo.text = "Start Test?";
            points = 0;
        }else{

            if(select){
                select.SetActive(false);
            }
            // Random para escoger el objeto
            value = Random.Range(0, (visitados.Length - 1));
            //Seleccionado el objeto
            select = objects[visitados[value]];
            //Mostrando las opciones del objeto
            for(int indexAux = 0 ; indexAux < options.Length ; indexAux++){
                options[indexAux].text = select.GetComponent<Options>().getOptions()[indexAux];
            }
            //Activando visualmente el objeto
            select.SetActive(true);
            //validamos si se encuentra en test
            select.GetComponent<Options>().hiddenObject(inTest);
            //Colocando el sonido
            select.GetComponent<AudioSource>().Play();
            //Eliminando el indice de la prueba
            int[] tmp = new int[(visitados.Length - 1)];
            int index = 0;
            for (int aux = 0; aux < visitados.Length; aux++){
                if (value != aux){
                    tmp[index] = visitados[aux];
                    index = index + 1;
                }
            }
            //se mueven al vector original
            visitados = new int[tmp.Length];
            for (int aux = 0; aux < tmp.Length; aux++){
                visitados[aux] = tmp[aux];
            }
        }

    }

    private void initGame(){
        noViewObjects();
        initIndex();
        selectObject();
        desactivateButtons(true);
    }

    private void option_ok(){
        audioOk.Play();
        points = points + 100;
        labelInformativo.text = "Points : " + points;
        StartCoroutine("runNext");
    }
    private void option_fail(){
        if (points > 49){
            points = points - 50;
        }
        labelInformativo.text = "Points : " + points;
        audioFail.Play();
    }

    private IEnumerator runNext(){
        yield return new WaitForSecondsRealtime(1);
        selectObject();
    }
}
