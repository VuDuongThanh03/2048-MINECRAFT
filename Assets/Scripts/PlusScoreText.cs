using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    float timelife = 1;
    float nowtime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nowtime < timelife)
        {
            nowtime += Time.deltaTime;
            gameObject.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0f,1f,0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
