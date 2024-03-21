using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveLoadControls
{
    public static void saveControls(ControlSettings controls)
    {
        //for the data to be saves more securely it need to be formatted to binary 
        BinaryFormatter formatter = new BinaryFormatter();
        //The selected place where the data is being saved (persistentDataPath==AppData)
        string path = Application.persistentDataPath + "/Controls/";
        //create the directory for the desired place
        Directory.CreateDirectory(path);
        //create the file in the desired directory with a name that is wanted(.dat can be anything you want)
        FileStream stream = new FileStream(path + "controls.dat", FileMode.Create);
        //take the data that that you want to save
        ControlData data = new ControlData(controls);
        //format the data to binary and put it in the file
        formatter.Serialize(stream, data);
        //IMPORTANT remember to close the file!!!
        stream.Close();
    }
    public static ControlData loadControls()
    {
        //The selected place where the data is being saved (persistentDataPath==AppData)
        string path = Application.persistentDataPath + "/Controls/";
        //if the file does exist the data can be loaded
        if (File.Exists(path + "controls.dat"))
        {
            //the binary formatter is needed to deserialize the data
            BinaryFormatter formatter = new BinaryFormatter();
            //the file need to be opened to deserialize it
            FileStream stream = new FileStream(path + "controls.dat", FileMode.Open);
            //deserialize the data and specify that it is players data
            ControlData data = formatter.Deserialize(stream) as ControlData;
            //IMPORTANT remember to close the file!!!
            stream.Close();
            //return the deserialized data
            return data;
        }
        //if the files doesn't exist log an error and return null
        else
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
