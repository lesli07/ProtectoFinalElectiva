using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicGame : MonoBehaviour
{

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
    private GameObject select;
    public Text option1;
    public Text option2;
    public Text option3;
    public Text option4;
    public Text labelInformativo;
    private int[] visitados;
    private int value;
    private int correct;
    private AudioSource audioOk;
    private AudioSource audioFail;
    private AudioSource[] sounds;
    private int points;
    //Controls
    public GameObject repeat;
    public GameObject buttonOption1;
    public GameObject buttonOption2;
    public GameObject buttonOption3;
    public GameObject buttonOption4;
    public int sceneActive;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        audioOk = sounds[0];
        audioFail = sounds[1];
        tryAgain();
    }

    private void tryAgain()
    {
        repeat.SetActive(false);

        buttonOption1.SetActive(true);
        buttonOption2.SetActive(true);
        buttonOption3.SetActive(true);
        buttonOption4.SetActive(true);
        points = 0;
        visitados = new int[10];
        for (int index = 0; index < visitados.Length; index++)
        {
            visitados[index] = index;
        }
        value = 11;
        correct = 0;
        labelInformativo.text = "Points : " + points;
        selectGameObject();
    }
    // Update is called once per frame
    void Update()
    {
        //Validacion de los objetos impactados
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("Se Detecto un Touch");
            Ray rayo = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit objetoImpacto;

            if (Physics.Raycast(rayo, out objetoImpacto))
            {
                Debug.Log("Objeto Impactado " + objetoImpacto.transform.name);
                if (objetoImpacto.transform.gameObject.CompareTag("repeatAudio"))
                {
                    sendTest();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_1") && correct == 1)
                {
                    option_ok();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_2") && correct == 2)
                {
                    option_ok();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_3") && correct == 3)
                {
                    option_ok();
                }
                else if (objetoImpacto.transform.gameObject.CompareTag("option_4") && correct == 4)
                {
                    option_ok();
                }
                else
                {
                    option_fail();
                }
            }
            else
            {
                Debug.Log("No se encontro impacto");
            }
        }
    }

    public void sendTest()
    {
        if (points < 850 && sceneActive == 0)
        {
            tryAgain();
        }
        else if (points > 850 && sceneActive == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (points > 850 && sceneActive == 1)
        {
            SceneManager.LoadScene(0);
        }else{
            tryAgain();
        }
        
    }

    private void option_ok()
    {
        audioOk.Play();
        points = points + 100;
        labelInformativo.text = "Points : " + points;
        StartCoroutine("runNext");
    }
    private void option_fail()
    {
        if (points > 49)
        {
            points = points - 50;
        }
        labelInformativo.text = "Points : " + points;
        audioFail.Play();
    }

    private IEnumerator runNext()
    {
        yield return new WaitForSecondsRealtime(1);
        selectGameObject();
    }

    public void option(int index)
    {
        if (index == 0)
        {
            select.GetComponent<AudioSource>().Play();
        }
        else if (index == 1 && correct == 1)
        {
            option_ok();
        }
        else if (index == 2 && correct == 2)
        {
            option_ok();
        }
        else if (index == 3 && correct == 3)
        {
            option_ok();
        }
        else if (index == 4 && correct == 4)
        {
            option_ok();
        }
        else
        {
            option_fail();
        }
    }
    private void selectGameObject()
    {
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

        if (visitados.Length > 0)
        {
            //Buscamos el objeto aleatoriamente
            value = Random.Range(0, (visitados.Length - 1));
            //Mostramos el objeto
            activateObject();
            //Eliminamos el indice del arreglo
            int[] tmp = new int[(visitados.Length - 1)];
            int index = 0;
            for (int aux = 0; aux < visitados.Length; aux++)
            {
                if (value != aux)
                {
                    tmp[index] = visitados[aux];
                    index = index + 1;
                }
            }
            //se mueven al vector original
            visitados = new int[tmp.Length];
            for (int aux = 0; aux < tmp.Length; aux++)
            {
                visitados[aux] = tmp[aux];
            }
        }
        else
        {
            repeat.SetActive(true);

            buttonOption1.SetActive(false);
            buttonOption2.SetActive(false);
            buttonOption3.SetActive(false);
            buttonOption4.SetActive(false);
            option1.text = "";
            option2.text = "";
            option3.text = "";
            option4.text = "";
            if (points < 850)
            {
                labelInformativo.text = "Total points " + points;
            }
            else
            {
                if(sceneActive == 0)
                {
                    labelInformativo.text = "Start Test? ";
                }
                else
                {
                    labelInformativo.text = "Total points " + points;
                }
            }
        }
    }

    private void activateObject()
    {
        switch (visitados[value])
        {
            case 0:
                option1.text = "Apple";
                option2.text = "Aple";
                option3.text = "Eippol";
                option4.text = "Epple";
                correct = 1;
                select = apple;
                apple.SetActive(true);
                apple.GetComponent<AudioSource>().Play();
                break;
            case 1:
                option1.text = "Bannana";
                option2.text = "Banana";
                option3.text = "Bananan";
                option4.text = "Bannanna";
                correct = 2;
                banana.SetActive(true);
                select = banana;
                banana.GetComponent<AudioSource>().Play();
                break;
            case 2:
                option1.text = "Bair";
                option2.text = "Beir";
                option3.text = "Bear";
                option4.text = "Bir";
                correct = 3;

                bear.SetActive(true);
                select = bear;
                bear.GetComponent<AudioSource>().Play();
                break;
            case 3:
                option1.text = "Catt";
                option2.text = "Caet";
                option3.text = "Caat";
                option4.text = "Cat";
                correct = 4;
                cat.SetActive(true);
                select = cat;
                cat.GetComponent<AudioSource>().Play();
                break;
            case 4:
                option1.text = "Dog";
                option2.text = "Dhog";
                option3.text = "Dohg";
                option4.text = "Dogh";
                correct = 1;
                dog.SetActive(true);
                select = dog;
                dog.GetComponent<AudioSource>().Play();
                break;
            case 5:
                option1.text = "Huorse";
                option2.text = "Hourse";
                option3.text = "Hurse";
                option4.text = "Horse";
                correct = 2;
                hourse.SetActive(true);
                select = hourse;
                hourse.GetComponent<AudioSource>().Play();
                break;
            case 6:
                option1.text = "Donky";
                option2.text = "Donkey";
                option3.text = "Monkey";
                option4.text = "Monky";
                correct = 3;
                monkey.SetActive(true);
                select = monkey;
                monkey.GetComponent<AudioSource>().Play();
                break;
            case 7:
                option1.text = "Horge";
                option2.text = "Orang";
                option3.text = "Horange";
                option4.text = "Orange";
                correct = 4;
                orange.SetActive(true);
                select = orange;
                orange.GetComponent<AudioSource>().Play();
                break;
            case 8:
                option1.text = "Pineapple";
                option2.text = "Pinepple";
                option3.text = "Pineaple";
                option4.text = "Pinapple";
                correct = 1;
                pineapple.SetActive(true);
                select = pineapple;
                pineapple.GetComponent<AudioSource>().Play();
                break;
            case 9:
                option1.text = "Strawbery";
                option2.text = "Strawberry";
                option3.text = "Straberry";
                option4.text = "Stawbery";
                correct = 2;
                strawberry.SetActive(true);
                select = strawberry;
                strawberry.GetComponent<AudioSource>().Play();
                break;
        }
    }
}
