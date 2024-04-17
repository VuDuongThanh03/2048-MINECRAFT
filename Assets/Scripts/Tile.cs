using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState TileState { get; private set; }
    public int number;
    TextMeshProUGUI tileValue;
    Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        tileValue = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        animator.SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setNumber()
    {
        tileValue.text = number.ToString();
    }
    public void setState(TileState state)
    {
        //tileValue.color = state.textColor;
        //gameObject.GetComponent<Image>().color = state.backGroundColor;
        gameObject.GetComponent<Image>().sprite = state.hinhanh;

    }
    public void setMerge()
    {
        animator.SetTrigger("Merge");
    }
}
