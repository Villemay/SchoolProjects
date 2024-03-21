using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    //these are for making multiple levels and saveslots
    public static string saveSlot = "Slot1";
    public static string currentLevel = "Level1";
    public static string enteredWaypoint;
    public static bool dash = false;
    public static bool burrow = false;
    public static bool groundpound = false;
    //save the player data
    public static void SavePlayer(PlayerMovvement player)
    {
        //for the data to be saves more securely it need to be formatted to binary 
        BinaryFormatter formatter = new BinaryFormatter();
        //The selected place where the data is being saved (persistentDataPath==AppData)
        string path = Application.persistentDataPath + "/" + saveSlot + "/Saves/"+ currentLevel + "/";
        //create the directory for the desired place
        Directory.CreateDirectory(path);
        //create the file in the desired directory with a name that is wanted(.dat can be anything you want)
        FileStream stream = new FileStream(path + "player.dat", FileMode.Create);
        //take the data that that you want to save
        PlayerData data = new PlayerData(player);
        //format the data to binary and put it in the file
        formatter.Serialize(stream, data);
        //IMPORTANT remember to close the file!!!
        stream.Close();
    }
    //load the player data
    public static PlayerData loadPlayer()
    {
        //The selected place where the data is being saved (persistentDataPath==AppData)
        string path = Application.persistentDataPath + "/" + saveSlot + "/saves/" + currentLevel + "/";
        //if the file does exist the data can be loaded
        if (File.Exists(path +"player.dat"))
        {
            //the binary formatter is needed to deserialize the data
            BinaryFormatter formatter = new BinaryFormatter();
            //the file need to be opened to deserialize it
            FileStream stream = new FileStream(path + "player.dat", FileMode.Open);
            //deserialize the data and specify that it is players data
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
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
