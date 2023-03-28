using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class FileHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    //Encrypting

    private bool useEncryption = false;

    private readonly string encryptionCode = "hi";

    public FileHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;

        // Encryption
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }

                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data frow file: " + fullPath + "\n" + e);
            }


        }

        return loadedData;
    }

    public void Save(GameData data) 
    {
        // string fullPath = dataDirPath + "/" + dataFileName;

        string fullPath = Path.Combine(dataDirPath, dataFileName);


        try
        {
            // Create directory
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize C# data into JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            if(useEncryption) 
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e) 
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }

    }

    // Encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char) (data[i] ^ encryptionCode [i % encryptionCode.Length]);
        }

        return modifiedData;
    }
}