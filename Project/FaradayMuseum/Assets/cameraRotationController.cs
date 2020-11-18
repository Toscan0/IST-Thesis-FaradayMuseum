using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraRotationController : MonoBehaviour {

    
    public GameObject cameraGameObject;
    public Animator cameraAnimator;
    private  Text infoText;
    public GameObject infoPanel;
    public Text panelText;
    public GameObject PageController;
    Sprite[] BOOKsprites;
    // Use this for initialization

    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    void Start () {
        cameraAnimator = cameraGameObject.GetComponent<Animator>();
        BOOKsprites = Resources.LoadAll<Sprite>("GowerBellInfoImagesATLAS");
    }

   
    // The target marker.
    Transform target;
    private bool canMove = false;

    // Angular speed in radians per sec.
    float speed;
   
    void Update()
    {
        


    }
    
    public void resetCameraPos()
    {
        cameraAnimator.SetBool("zoomQuestion1", false);
        cameraAnimator.SetBool("zoomQuestion2", false);
        cameraAnimator.SetBool("zoomQuestion3", false);
    }
    public void showInfo(GameObject info)
    {
        if(infoPanel.GetComponent<Animator>().GetBool("clickedOrTimeOut") == true)
        {
            infoPanel.SetActive(false);
        }
        if (info.transform.name == "Info1")
        {
            singleton.AddGameEvent(LogEventType.Info1);

            infoPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().stopBoolean = true;
            //panelText.text = info.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            infoPanel.transform.Find("InfoImage").GetComponent<Image>().sprite = BOOKsprites[0];
            infoPanel.transform.Find("InfoImage").GetComponent<Image>().preserveAspect = true;
            panelText.text = "" +
                "1. Utilizador (Emissor) roda a manivela para gerar sinal de chamada na central com operador (Telefonista)." + System.Environment.NewLine +
                "2. Utilizador(Emissor) levanta auscultador. O circuito do microfone / auscultador fica ativado. O Utilizador(Emissor) pode falar então com o(a) Telefonista, depois de este / a atender." + System.Environment.NewLine +
                "3.O Utilizador(Emissor) indica ao(à) Telefonista qual o número da extensão pretendido." + System.Environment.NewLine +
                "4.Telefonista marca o número indicado e chama, através do sinal - recetor, o Utilizador(Recetor) pretendido." + System.Environment.NewLine +
                "5.O Utilizador(Recetor) levanta o microfone / auscultador e fica em contacto com o(a) Telefonista.Entretanto a linha do Utilizador(Emissor) é mantida em espera." + System.Environment.NewLine +
                "6.Telefonista põe o Utilizador(Emissor) e o Utilizador(Recetor) em contacto.";
            //infoPanel.GetComponent<Animator>().SetBool("clickedOrTimeOut", true);
        }

        if (info.transform.name == "Info2")
        {
            singleton.AddGameEvent(LogEventType.Info2);

            infoPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().stopBoolean = true;
            infoPanel.transform.Find("InfoImage").GetComponent<Image>().sprite = BOOKsprites[1];
            infoPanel.transform.Find("InfoImage").GetComponent<Image>().preserveAspect = true;
            panelText.text = "A Bateria garante a presença de uma corrente no circuito que é modelada pelo Microfone, sem esta não é possivel ouvir o Utilizador(Recetor) nem ele ouvir o Utilizador (Emissor)." + System.Environment.NewLine +
                System.Environment.NewLine +
                " A figura abaixo representa uma numeração das partes importantes do telefone gower bell" + System.Environment.NewLine +
                System.Environment.NewLine +
                "1. Manivela do magneto gerador de chamada" + System.Environment.NewLine +
                "2. Campainha de chamada" + System.Environment.NewLine +
                "3. Placa de madeira suporte do microfone" + System.Environment.NewLine +
                "4. Tubos acústicos para audição" + System.Environment.NewLine +
                "5. Caixa da bateria" + System.Environment.NewLine +
                "6. Microfone" + System.Environment.NewLine +
                "7. Auscultador" + System.Environment.NewLine +
                "8. Transformador do microfone";
           // infoPanel.GetComponent<Animator>().SetBool("clickedOrTimeOut", true);
        }

        if (info.transform.name == "Info3")
        {
            singleton.AddGameEvent(LogEventType.Info3);

            infoPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("Book").GetComponent<PageController>().stopBoolean = true;
            //panelText.text = info.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            infoPanel.transform.Find("InfoImage").GetComponent<Image>().sprite = BOOKsprites[9];
            
            infoPanel.transform.Find("InfoImage").GetComponent<Image>().preserveAspect = true; 
            panelText.text = "A peça de madeira no meio do telefone com uma placa preta é o diaframa do microfone, este é um Transdutor Acústico, ou seja um conversor das vibrações mecânicas associadas à voz do Utilizador (Emissor) em variações de resistência." + System.Environment.NewLine +
                "A corrente ao variar nos Auscultadores atua um Transdutor Eletroacústico, transformando-a em variações de pressão acústica (som) que é recebido pelo Utilizador (Recetor)." + System.Environment.NewLine +
                System.Environment.NewLine +
                "A figura abaixo representa a ligação entre utilizadores";
            //infoPanel.GetComponent<Animator>().SetBool("clickedOrTimeOut", true);
        }
    }
    public void moveCamera(GameObject info)
    {
        //Debug.Log("Entrei aqui no move camera");
        //Debug.Log(info.name);
        if (info.transform.name == "Info1")
        {
            /*if (cameraAnimator.GetBool("zoomQuestion2") == true)
            {
                cameraAnimator.SetBool("zoomQuestion1",true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }

            else if (cameraAnimator.GetBool("zoomQuestion3") == true)
            {
                cameraAnimator.SetBool("zoomQuestion1", true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            */
            if (cameraAnimator.GetBool("zoomQuestion1") == true)
            {
                cameraAnimator.SetBool("zoomQuestion1", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            


        }
        else if (info.transform.name == "Info2")
        {
            if (cameraAnimator.GetBool("zoomQuestion2") == true)
            {
                cameraAnimator.SetBool("zoomQuestion2", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", false);
                cameraAnimator.SetBool("zoomQuestion2", true);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            
        }
        else if (info.transform.name == "Info3")
        {
            if (cameraAnimator.GetBool("zoomQuestion3") == true)
            {
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", false);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", true);
            }
           
        }
        
    }
}
