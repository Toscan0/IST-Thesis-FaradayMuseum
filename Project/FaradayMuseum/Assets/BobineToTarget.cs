using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobineToTarget : MonoBehaviour {
    private bool canMove;
    public float speed;
    public ParticleSystem starsPS;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "BobineLeft")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().isInsideFinCollider = true;
            other.GetComponent<Animator>().SetBool("canGreen", true);
        }
            
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "BobineLeft")
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().canReturnOriginalPlace == false)//para asegurar que não vai para o lugar se o jogador deixar num sitio e o caminho de retorno tocar no collider ele vai tentar ir na mesma, mas como tem a flag de retornar para o sitio, ele faz o que o algoritmo do collider diz e depois volta ao lugar
            {
                //apenas pode mudar quando o objecto n esta a ser clicado quer dizer que o objecto é para fazer snap 
                canMove = !GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().flagObjectToMouse;


                if (canMove)
                {
                    moveToPlace(other);
                }
                //Se a posição do objecto for a mm que o target então retiramos o trigger para não haver Extra computação
                if (other.gameObject.transform.position == transform.position)
                {
                    ////Debug.Log("entrei aqui nesta cena da flag a true");
                    Destroy(GameObject.FindGameObjectWithTag("FadeBobine"));
                    GetComponent<BoxCollider>().isTrigger = false;
                    //GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().canReturnOriginalPlace = false;
                    Instantiate(starsPS);

                    GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().finishHandHeldDemo = true; //mete flag a true para dizer que ja acabou o demo para puder passar a outras coisas
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().interactFalseFirstDemoBool = true;
                    
                    // GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().interactFalseFirstDemoBool = true;
                    //GameObject.FindGameObjectWithTag("GameController").GetComponent<EletricityController>().telefonou = false;
                    // GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().flagObjectToMouse = false;
                }
            }
        }
      

    }

    void OnTriggerExit(Collider other)
    {
        // //Debug.Log("sai no Trigger");
        if (other.name == "BobineLeft")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().isInsideFinCollider = false;
            other.GetComponent<Animator>().SetBool("canGreen", false);
        }
    }

    void moveToPlace(Collider other)
    {
        float step = speed * Time.deltaTime;
        other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, transform.position, step);
        
    }
}
