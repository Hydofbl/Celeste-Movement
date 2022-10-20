using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetTableButton : MonoBehaviour, IPointerDownHandler
{
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
        string readFromFilePath = Application.streamingAssetsPath + "/High Score/" + "HighScore" + ".txt";
        //StreamWriter writer = new StreamWriter(readFromFilePath, true);
        using (var stream = new FileStream(readFromFilePath, FileMode.Truncate))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write("");
            }
        }
    }
}
