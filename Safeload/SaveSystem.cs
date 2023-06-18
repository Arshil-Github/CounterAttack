using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(DataToSave ds)
    {
        //This is called when we need to save data
        //Called after every turn
        //Create a Binary Formatter
        BinaryFormatter formatter = new BinaryFormatter();

        //This is the path where the binary file is stored
        string path = Application.dataPath + "/youCantHackChad.getout";

        //Open a File "Stream"
        FileStream stream = new FileStream(path, FileMode.Create);

        //Put DatatoSave in it using PlayerData(Acting as a intermidiate)
        PlayerData data = new PlayerData(ds);

        formatter.Serialize(stream, data);
        //Close Stream or Problems
        stream.Close();

    }
    public static DataToSave Load()
    {
        //Used for loading data
        //Give the path of saved file
        string path = Application.dataPath + "/youCantHackChad.getout";


        if (File.Exists(path))//To avoid not found error
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream streeam = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(streeam) as PlayerData;

            DataToSave tobeLoaded = new DataToSave();
            //Here PlayerData in The dave file in converted in DatatoSave for ease of use
            //Add Any future component below

            tobeLoaded.coins = data.coins;
            tobeLoaded.xp = data.xp;

            tobeLoaded.grenades = data.grenades;
            tobeLoaded.max_grenades = data.max_grenades;
            tobeLoaded.rocketLauncher = data.rocketLauncher;
            tobeLoaded.max_rocketLauncher = data.max_rocketLauncher;
            tobeLoaded.phantomSword = data.phantomSword;
            tobeLoaded.max_phantomSword = data.max_phantomSword;

            streeam.Close();

            return tobeLoaded;
        }
        else
        {
            return null;
        }
    }
}
