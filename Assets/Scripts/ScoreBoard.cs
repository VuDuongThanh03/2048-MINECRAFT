using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreBoard scoreBoard;
    int score = 0;
    TextMeshProUGUI scoretext;
    public GameObject text;
    public TextMeshProUGUI bestScore;
    public ScoreSave scoreBest;
    private void Awake()
    {
        scoreBoard = this;
        bestScore.text = scoreBest.score.ToString();
    }
    void Start()
    {
        scoretext = GetComponent<TextMeshProUGUI>();
        scoreBoard = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setScore(int x)
    {
        if (x == 0)
        {
            return;
        }
        score += x;
        
        GameObject a = Instantiate(text,gameObject.transform);
        a.GetComponent<RectTransform>().position += new Vector3(0f, 100f,0f);
        a.GetComponent<TextMeshProUGUI>().text = "+"+x.ToString();

        if (score > scoreBest.score)
        {
            scoreBest.score = this.score;
            bestScore.text = score.ToString();
        }
        scoretext.text = score.ToString();
    }
}
