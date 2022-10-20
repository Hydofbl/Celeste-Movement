using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubmitScoreButton : MonoBehaviour, IPointerDownHandler
{
    public Text nickText;
    public GameObject submitField, tableField;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        string readFromFilePath = Application.streamingAssetsPath + "/High Score/" + "HighScore" + ".txt";
        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();
        foreach (string line in fileLines)
        {
            string[] newPerson = line.Split(char.Parse(":"));
            dictionary.Add(newPerson[0], int.Parse(newPerson[1]));
        }
        int currentHighScore = PlayerPrefs.GetInt("PogScore", 0);
        string currentNick = nickText.text;
        if (currentNick != "")
        {
            //Write some text to the test.txt file
            if(currentHighScore>0)
            dictionary.Add(currentNick, currentHighScore);
            //StreamWriter writer = new StreamWriter(readFromFilePath, true);
            using (var stream = new FileStream(readFromFilePath, FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (KeyValuePair<string, int> author in dictionary.OrderByDescending(key => key.Value))
                    {
                        writer.Write(author.Key + ":" + author.Value + "\n");
                    }
                }
            }
        }
        submitField.SetActive(false);
        tableField.SetActive(true);
    }
}
