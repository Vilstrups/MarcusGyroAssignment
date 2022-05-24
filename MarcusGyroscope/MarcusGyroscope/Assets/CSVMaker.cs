using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System.IO;

public class CSVMaker : MonoBehaviour
{
    Gyroscope gyro;



    string filename = "";

    private volatile bool dataSave = false;

    List<string> data = new List<string>();
    private int counter = 0;

    void Start()
    {
        
        Debug.Log(filename);

        // Getting access to the gyroscope through Input. manegement. 
        gyro = Input.gyro;
        if (SystemInfo.supportsGyroscope)
        {
            //Enabeling the gyroscope after checking if unity and the device is capable.
            gyro.enabled = true;
        }
    }

    bool buttonClicked = false;

    void Update()
    {
        // All datasets are included
        if (!dataSave && buttonClicked)
            // i'll seperate all x,y,z values with a comma
            data.Add(gyro.gravity.x + "," + gyro.gravity.y + "," + gyro.gravity.z);
    }


    private void FileCSV()
    {
        dataSave = true;
        filename = Application.dataPath + "/tryout" + (++counter) + ".csv";
        //Creating a stream writer to take notes of incoming values, 
        //writing it all down in a file. 
        StreamWriter sw = new StreamWriter(filename, false);
        //writing the "x,y,z" as the header for the file.
        sw.WriteLine("x,y,z");



        //sw = new StreamWriter(filename, true);

        for (int i = 0; i < data.Count; i++)
        {
            sw.WriteLine(data[i]);
        }
        //closing the file to assure the content.

        sw.Close();
        //Clear the current data, to make space for new files 
        data.Clear();
        dataSave = false;
    }

    public void buttonPress()
    {
        Debug.Log("Button Clicked");
        buttonClicked = !buttonClicked;

        if (!buttonClicked)
            FileCSV();
    }
}











