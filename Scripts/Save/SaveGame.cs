using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveGame : MonoBehaviour
{
    public GameObject saveObject;
    private string saveName = "Default";
    /**
	* In order to use the SaveObject, we need to initialize it. 
	* It will create a SaveObject instance, make sure you link 
	* correctly to the save object either with a string with the 
	* Resources folder as root or a direct link to the prefab.
	**/
    void Start()
    {

        /**
		* You need to call SaveObject.Initialize before doing anything with SaveObject.
		**/
        if(PlayerPrefs.GetInt("usouSave") != 1) {
            SaveObject.Initialize(saveObject);
        }
        PlayerPrefs.SetInt("usouSave", 0);

        /**
		* Set a callback for when you load a game like this, used to do complex updates
		* when needed.
		**/
        SaveObject.SetRefreshCallback(OnRefresh);

        /**
		* It is recommended that you Load right after initialization, it makes sure that
		* you always have a game ready to be used. 
		**/
        SaveObject.Load("Saved Game No1");
    }

    /**
	* Use the RefreshCallback to execute the extra logic it takes
	* to properly reflect the updated values of a loaded game or
	* when you manually update values in the inspector then press
	* "Call Refresh"
	**/
    private void OnRefresh()
    {


        saveName = SaveObject.saveName;
    }


    /**
	* Use SaveObject.Get<T>(); to get a saved component (component of the SaveObject)
	* A game must be loaded ino order to be able to use SaveObject.Get<T>()
	**/
    

    /**
	* Save your game like this
	**/
    public void Save()
    {
        SaveObject.Save();
    }
    /**
	* Load your game like this
	**/
    public void Load()
    {
        SaveObject.Load(saveName);
    }

    
    

    

    private void AlternativeLoad()
    {
        string str = PlayerPrefs.GetString("alt" + SaveObject.saveName);

        if (string.IsNullOrEmpty(str))
        {
            Debug.LogWarning("No file found, please previously Alt Save the file you want to Alt Load (press \"Alt. Save\")");
        }
        SaveObject.LoadFromString(str);
        Debug.Log("LOAD STRING TO USE AS YOU WISH:\n" + str);
    }

    private void AlternativeSave()
    {
        string str = SaveObject.GetSaveString();
        PlayerPrefs.SetString("alt" + SaveObject.saveName, str);
        Debug.Log("SAVE STRING TO USE AS YOU WISH:\n" + str);
    }
}
