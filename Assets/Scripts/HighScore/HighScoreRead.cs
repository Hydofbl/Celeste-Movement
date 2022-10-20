using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreRead : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public int totalPersonWillShowInHighScore;
    // Start is called before the first frame update
    void Start()
    {
        string readFromFilePath = Application.streamingAssetsPath + "/High Score/" + "HighScore" + ".txt";
        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();
        int index = 0;
        foreach(string line in fileLines)
        {
            if (line != null && highScoreText != null && index< totalPersonWillShowInHighScore)
            {
                highScoreText.text += (index+1)+": "+ line + "\n";
                index++;
            }
            /*recallTextObject.GetComponent<Text>().text = line;
            Instantiate(recallTextObject, contentWindow);*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
