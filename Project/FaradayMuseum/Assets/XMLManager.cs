using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Collections.Generic; //lets use lists
using System.Xml;                 //basic xml attributes     
using System.Xml.Serialization;   //access xmlSerializer
using System.IO;                  //file management
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XMLManager : MonoBehaviour {
    
    ///////////////////////////////////////INI Singleton pattern/////////////////////////
     
    public static XMLManager ins;
    private static bool created = false;
    

    // Use this for initialization

   
    private void Awake()
    {
        ins = this;
        if (!created)
         {
             DontDestroyOnLoad(this.gameObject);
        }
        //SaveItems();
        //Debug.Log("entrei aqui no start do XMLManager");
        if (!FileChk())// se o ficheiro não existe, passa a exisitr
        {
            ResetDatabase();
            SaveItemsFirstTime(); //é pelo filestream para ter o xml arranjadinho porque o streamwriter escreve tudo afrente e para ir editar o xml fica um bocado dificil
        }
            /*
            //só para fazer os testezs quero que seja sempre refeito o ficheiro
            ResetDatabase();
            SaveItemsFirstTime();
            //caso contrario comentar as duas linhas acima e descomentar o LoadItems();
            //FileChk();
            //LoadItems();
            
            //Debug.Log(itemDB.list[0].objectName);
            */

        else
        {
            
            LoadItems();
            //debugText.text += "Nome do primeiro objecto: " + itemDB.list[0].objectName + "\n";
            //Debug.Log(itemDB.list[0].objectName);
        }
        
        //LoadItems();
    }

    private void Start()
    {
        //Debug.Log("entrei aqui no start do XMLManager");
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            //LoadItems();
            //debugManager.debugInst.debugText.text += "Nome do primeiro objecto: " + itemDB.list[0].objectName + "\n";
          //  //Debug.Log(itemDB.list[0].objectName);
        }
        else
        {
            //LoadItems();
            //debugManager.debugInst.debugText.text += "Nome do primeiro objecto: " + itemDB.list[0].objectName + "\n";
            ////Debug.Log(itemDB.list[0].objectName);
        }
    }
    ///////////////////////////////////////FIM Singleton pattern/////////////////////////

    //list of items
    public ItemDatabase itemDB;

    //save functions
    public void SaveItemsFirstTime()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
       
        string filename = "";
        //ANDROID
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            filename = Application.persistentDataPath + "/item_data.xml";
        }
        //DESKTOP
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            filename = Application.streamingAssetsPath + "/XML/item_data.xml";
        }
        
        FileStream stream = new FileStream(filename, FileMode.Create);
        //StreamWriter stream = new StreamWriter(filename); //mete default utf-8 
        serializer.Serialize(stream, itemDB);
        stream.Close();



    }

    public void SaveItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));

        string filename = "";
        //ANDROID
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            filename = Application.persistentDataPath + "/item_data.xml";
        }
        //DESKTOP
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            filename = Application.streamingAssetsPath + "/XML/item_data.xml";
        }

        //FileStream stream = new FileStream(filename, FileMode.Create);
        StreamWriter stream = new StreamWriter(filename); //mete default utf-8 
        serializer.Serialize(stream, itemDB);
        stream.Close();



    }

    public void BuildFirstAndroidXMLFile(string objectName, bool win, bool discoveredFirstInfo, bool discoveredSecondInfo, bool discoveredThirdInfo, string objectInfoText1, string objectInfoText2, string objectInfoText3)
    {
        MuseumObject obj = new MuseumObject();
        obj.objectName = objectName;
        obj.earnedSticker = win;
        obj.objectInfoText1 = objectInfoText1;
        obj.objectInfoText2 = objectInfoText2;
        obj.objectInfoText3 = objectInfoText3;
        obj.discoveredFirstInfo = discoveredFirstInfo;
        obj.discoveredSecondInfo = discoveredSecondInfo;
        obj.discoveredThirdInfo = discoveredThirdInfo;
        itemDB.list.Add(obj);

        
    }
    public void ResetDatabase()
    {
        
        itemDB.list.Clear();
        string objectName = "Telephone";
        bool win = false;
        bool discoveredFirstInfo = false;
        bool discoveredSecondInfo = false;
        bool discoveredThirdInfo = false;
        string objectInfoText1 = "Antigamente, quando se queria telefonar a alguem, era necessário rodar uma manivela de maneira a produzir energia suficiente para gerar um sinal de chamada na central com o operador, de maneira a ser possivel entrar em contacto com outro Utilizador (Recetor).";
        string objectInfoText2 = "A bateria é a responsável por fornecer energia ao microfone situado dentro da caixa com a placa preta. Agora que já tens a bateria carregada, tenta ligar ao telefone";
        string objectInfoText3 = "Esta placa de madeira é o Microfone, que é um transdutor acústico, ou seja, um conversor das vibrações mecânicas associadas à voz do Utilizador(Emissor) em variações de resistência. A corrente ao variar nos Auscultadores atua um transdutor Eletroacústico, transformando-a em variações de pressão acústica(som) que é recebido pelo Utilizador(Recetor).";
        BuildFirstAndroidXMLFile(objectName, win, discoveredFirstInfo, discoveredSecondInfo, discoveredThirdInfo, objectInfoText1, objectInfoText2, objectInfoText3);

        string objectName1 = "FreqMachine";
        bool win1 = false;
        bool discoveredFirstInfo1 = false;
        bool discoveredSecondInfo1 = false;
        bool discoveredThirdInfo1 = false;
        string objectInfoText11 = "Info1";
        string objectInfoText21 = "Info2";
        string objectInfoText31 = "Info3";
        BuildFirstAndroidXMLFile(objectName1, win1, discoveredFirstInfo1, discoveredSecondInfo1, discoveredThirdInfo1, objectInfoText11, objectInfoText21, objectInfoText31);


    }
    public void LoadResetItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_dataReset.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();



    }
    public bool FileChk()
    {
        string filePath = Application.persistentDataPath + "/item_data.xml";

        if (System.IO.File.Exists(filePath))
        {
            // The file exists -> run event
            //debugManager.debugInst.debugText.text += "FICHEIRO EXISTE";
            return true;
        }
        else
        {

            //debugManager.debugInst.debugText.text += "FICHEIRO NÃO EXISTE";
            return false;
            // The file does not exist -> run event
        }
        
    }

    //load function
    public void LoadItems()
    {
        //open a new xmlFile
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));

        string filename = "";
        //ANDROID
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            filename = Application.persistentDataPath + "/item_data.xml";
           // debugManager.debugInst.debugText.text += "Nome do caminho no android: " + Application.persistentDataPath + "/item_data.xml" + "\n";
        }
        //DESKTOP
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            filename = Application.streamingAssetsPath + "/XML/item_data.xml";
            //debugManager.debugInst.debugText.text += "Nome do caminho no windows: " + Application.streamingAssetsPath + "/XML/item_data.xml" + "\n";
        }
        //WWW www = new WWW(filename);
        FileStream stream = new FileStream(filename, FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();
    }

    public void SetVariableInDatabase(string objectName,bool win)
    {

        foreach(MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }
            
        }
        
    }
    public void SetVariableInDatabase(string objectName, bool discoveredInfoBool,int discoveredInfoNumber)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                switch(discoveredInfoNumber){
                    case 0:
                        item.discoveredFirstInfo = discoveredInfoBool;
                        break;
                    case 1:
                        item.discoveredSecondInfo = discoveredInfoBool;
                        break;
                    case 2:
                        item.discoveredThirdInfo = discoveredInfoBool;
                        break;
                }
                
                
            }

        }

    }
    public void SetVariableInDatabase(string objectName, bool win, string objectInfoText1)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (objectInfoText1 != null)
                {
                    item.objectInfoText1 = objectInfoText1;
                }
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }

        }

    }
    public void SetVariableInDatabase(string objectName, bool win, string objectInfoText1, string objectInfoText2)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (objectInfoText1 != null)
                {
                    item.objectInfoText1 = objectInfoText1;
                }
                if (objectInfoText2 != null)
                {
                    item.objectInfoText2 = objectInfoText2;
                }
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }

        }

    }
    public void SetVariableInDatabase(string objectName, bool win, string objectInfoText1, string objectInfoText2, string objectInfoText3)
    {

        foreach (MuseumObject item in itemDB.list)
        {
            if (item.objectName == objectName)
            {
                if (objectInfoText1 != null)
                {
                    item.objectInfoText1 = objectInfoText1;
                }
                if (objectInfoText2 != null)
                {
                    item.objectInfoText2 = objectInfoText2;
                }
                if (objectInfoText3 != null)
                {
                    item.objectInfoText2 = objectInfoText3;
                }
                if (win != false)
                {
                    item.earnedSticker = win;
                }
            }

        }

    }


    public void LoadScene(string sceneName) // salvar as alterações para o xml antes de fazer load da scene e depois vais load do xml para que a próxima cena tenha as coisas actualizadas
    {
        //SaveItems();
        LoadItems();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName(sceneName));
    }
    /*
    public void SceneManager()
    {
        if (SceneManager.GetActiveScene().name == "Telephone")
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
    }
    */
}

[System.Serializable]
public class MuseumObject
{
    public string objectName;
    public bool earnedSticker;
    public string objectInfoText1;
    public bool discoveredFirstInfo;
    public string objectInfoText2;
    public bool discoveredSecondInfo;
    public string objectInfoText3;
    public bool discoveredThirdInfo;



}

[System.Serializable]
public class ItemDatabase
{
    [XmlArray("Objects")]
    public List<MuseumObject> list = new List<MuseumObject>();
}

// tenho de usar Application.persistentDataPath algures