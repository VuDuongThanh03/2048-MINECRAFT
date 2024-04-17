using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardControl : MonoBehaviour
{
    // Start is called before the first frame update
    public CellFrame[,] cells = new CellFrame[4,4];
    public TileState[] tileState;
    public GameObject tilePrefab;
    public int tileInBoard = 0;
    public int lastCell;
    Vector2 startTouchPosition;
    int huong = 0;
    int sumScoreTurn = 0;
    bool add;
    bool gameOver = false;
    bool flag = false;
    public bool vohieuhoa = false;
    public GameObject menu;
    public Button Play;
    public Button Reset;
    public Button Exit;
    public GameObject gameOverBoard;
    private void Awake()
    {
        CellFrame[] allcell = GetComponentsInChildren<CellFrame>();
        for(int x = 0; x < 4; ++x)
        {
            for (int y = 0; y < 4; ++y)
            {
                cells[x, y] = allcell[x * 4 + y];
            }
        }
    }

    void Start()
    {
        CreateTile();
        CreateTile();
        Reset.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        Exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (vohieuhoa)
        {
            return;
        }
        huong = 0;
        checkDrag();
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)||huong == 4)
        {
            Debug.Log("Move Left");
            add = false;
            sumScoreTurn = 0;
            moveTiles(Vector2.right, 1, 3);
            ScoreBoard.scoreBoard.setScore(sumScoreTurn);
            if (add)
            {
                CreateTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)||huong == 2)
        {
            Debug.Log("Move Right");
            add = false;
            sumScoreTurn = 0;
            moveTiles(Vector2.left,2,0);
            ScoreBoard.scoreBoard.setScore(sumScoreTurn);
            if (add)
            {
                CreateTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)||huong == 1)
        {
            Debug.Log("Move Up");
            add = false;
            sumScoreTurn = 0;
            moveTiles(Vector2.up, 1, 3);
            ScoreBoard.scoreBoard.setScore(sumScoreTurn);
            if (add)
            {
                CreateTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)|| huong == 3)
        {
            Debug.Log("Move Down");
            add = false;
            sumScoreTurn = 0;
            moveTiles(Vector2.down, 2, 0);
            ScoreBoard.scoreBoard.setScore(sumScoreTurn);
            if (add)
            {
                CreateTile();
            }
        }

    }
    public void checkGameOver()
    {
        gameOver = true;
        for(int x = 0; x < 4; ++x)
        {
            for (int y = 0; y < 4; ++y)
            {
                if(!checkTile(x, y))
                {
                    gameOver = false;
                    return;
                }
            }
        }
        gameOverBoard.SetActive(true);
        Play.gameObject.SetActive(false);
    }
    public bool checkTile(int x,int y)
    {
        int numberofLastCell = cells[x, y].getNumberOfTile();
        if (x + 1 < 4 && cells[x + 1, y].getNumberOfTile() == numberofLastCell)
        {
            return false;
        }
        if (x - 1 > -1 && cells[x - 1, y].getNumberOfTile() == numberofLastCell)
        {
            return false;
        }
        if (y + 1 < 4 && cells[x, y + 1].getNumberOfTile() == numberofLastCell)
        {
            return false;
        }
        if (y - 1 > -1 && cells[x, y - 1].getNumberOfTile() == numberofLastCell)
        {
            return false;
        }
        return true;
    }
    public void checkDrag()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Lay vi tri bat dau
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Lay vi tri ket thuc
                Vector2 endTouchPosition = touch.position;

                // Tinh toan vector huong vuot
                Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                // Xac dinh huong vuot
                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {
                    // Vuot theo chieu ngang
                    if (swipeDirection.x > 0)
                    {
                        Debug.Log("Swipe Right");
                        huong = 2;
                    }
                    else
                    {
                        Debug.Log("Swipe Left");
                        huong = 4;
                    }
                }
                else
                {
                    // Vuot theo chieu doc
                    if (swipeDirection.y > 0)
                    {
                        Debug.Log("Swipe Up");
                        huong = 1;
                    }
                    else
                    {
                        Debug.Log("Swipe Down");
                        huong = 3;
                    }
                }
            }
        }
    }
    public void moveTiles(Vector2 huong, int batdau, int ketthuc)
    {
        int buocnhay = (int)(huong.x + huong.y);
        if (huong.x == 0f)
        {
            for (int x = batdau; x != ketthuc + buocnhay; x = x + buocnhay)
            {
                for (int y = 0; y < 4 && y > -1; ++y)
                {
                    Debug.Log("x = " + x + " y = " + y + " buocnhay = " + buocnhay);
                    if (cells[x, y].haveTile)
                    {
                        moveTile(x, y, new Vector2(huong.x, -huong.y));
                    }
                }
            }
        }
        else
        {
            for (int x = batdau; x != ketthuc + buocnhay; x = x + buocnhay)
            {
                for (int y = 0; y < 4&&y > -1; ++y)
                {
                    Debug.Log("x = " + x + " y = " + y + " buocnhay = " + buocnhay);
                    if (cells[y, x].haveTile)
                    {
                        moveTile(y, x, new Vector2(-huong.x, huong.y));
                    }

                }
            }
        }
        
    }
    public void moveTile(int x,int y,Vector2 huong)
    {
        Debug.Log("Move Tile X = " + x + " Y = " + y + " Huong " + huong);
        GameObject tile = cells[x, y].tileOfCell;
        int lastmove = x * 4 + y;
        if (huong.x != 0)
        {
            for (; ; )
            {
                int y2 = y + (int)huong.x;
                if (!cells[x, y2].haveTile)
                {
                    cells[x, y2].tileOfCell = cells[x, y].tileOfCell;
                    cells[x, y2].haveTile = true;
                    cells[x, y].haveTile = false;
                    cells[x, y].tileOfCell = null;
                    y = y2;
                    lastmove = x * 4 + y;
                    if (y == 0 || y == 3)
                    {
                        StartCoroutine(Animation(tile, cells[lastmove / 4, lastmove % 4].GetComponent<RectTransform>().position));
                        return;
                    }
                }
                else
                {
                    if (cells[x, y2].tileOfCell.GetComponent<Tile>().number == tile.GetComponent<Tile>().number)
                    {
                        cells[x, y2].tileOfCell.GetComponent<Tile>().number *= 2;
                        sumScoreTurn += cells[x, y2].tileOfCell.GetComponent<Tile>().number;
                        lastmove = x * 4 + y2;
                        flag = false;
                        StartCoroutine(Animation(tile, cells[lastmove / 4, lastmove % 4].GetComponent<RectTransform>().position));
                        //cells[x].tileOfCell.GetComponent<Tile>().setNumber();
                        int number = (int)Mathf.Log(cells[lastmove / 4, lastmove % 4].tileOfCell.GetComponent<Tile>().number, 2) - 1;
                        cells[x, y2].tileOfCell.GetComponent<Tile>().setState(tileState[number]);
                        cells[x, y].haveTile = false;
                        cells[x, y].tileOfCell = null;
                        StartCoroutine(CheckandDestroy(tile));
                        cells[x, y2].tileOfCell.GetComponent<Tile>().setMerge();
                        return;
                    }
                    StartCoroutine(Animation(tile, cells[lastmove / 4, lastmove % 4].GetComponent<RectTransform>().position));
                    return;
                }

            }
        }
        else
        {
            for (; ; )
            {
                int x2 = x + (int)huong.y;
                if (!cells[x2, y].haveTile)
                {
                    cells[x2, y].tileOfCell = cells[x, y].tileOfCell;
                    cells[x2, y].haveTile = true;
                    cells[x, y].haveTile = false;
                    cells[x, y].tileOfCell = null;
                    x = x2;
                    lastmove = x * 4 + y;
                    if (x == 0 || x == 3)
                    {
                        StartCoroutine(Animation(tile, cells[lastmove / 4, lastmove % 4].GetComponent<RectTransform>().position));
                        return;
                    }
                }
                else
                {
                    if (cells[x2, y].tileOfCell.GetComponent<Tile>().number == tile.GetComponent<Tile>().number)
                    {
                        cells[x2, y].tileOfCell.GetComponent<Tile>().number *= 2;
                        sumScoreTurn += cells[x2, y].tileOfCell.GetComponent<Tile>().number;
                        lastmove = x2 * 4 + y;
                        flag = false;
                        StartCoroutine(Animation(tile, cells[lastmove / 4, lastmove % 4].GetComponent<RectTransform>().position));
                        //cells[x].tileOfCell.GetComponent<Tile>().setNumber();
                        int number = (int)Mathf.Log(cells[lastmove / 4, lastmove % 4].tileOfCell.GetComponent<Tile>().number, 2) - 1;
                        cells[x2, y].tileOfCell.GetComponent<Tile>().setState(tileState[number]);
                        cells[x, y].haveTile = false;
                        cells[x, y].tileOfCell = null;
                        StartCoroutine(CheckandDestroy(tile));
                        cells[x2, y].tileOfCell.GetComponent<Tile>().setMerge();
                        return;
                    }
                    StartCoroutine(Animation(tile, cells[lastmove / 4, lastmove % 4].GetComponent<RectTransform>().position));
                    return;
                }

            }
        }
    }
    public IEnumerator Animation(GameObject Tile, Vector2 to)
    {
        if((Vector2)Tile.GetComponent<RectTransform>().position != to) 
        {
            add = true;
            
            yield return null;
        }
        else
        {
            yield break;
        }
        float timedachay = 0;
        float thoigiananimation = 0.1f;
        Vector2 from = Tile.GetComponent<RectTransform>().position;
        while (timedachay < thoigiananimation)
        {
            Tile.GetComponent<RectTransform>().position = Vector2.Lerp(from, to, timedachay/thoigiananimation);
            timedachay += Time.deltaTime;
            yield return null;
        }
        Tile.GetComponent<RectTransform>().position = to;
        flag = true;
    }
    public IEnumerator CheckandDestroy(GameObject tile)
    {
        for( ; ; )
        {
            if (flag)
            {
                Destroy(tile);
                --tileInBoard;
                yield break;
            }
            yield return null;
        }
    }
    public void CreateTile()
    {
        if (tileInBoard < 16)
        {
            ++tileInBoard;
        }
        else
        {
            return;
        }
        CellFrame cell = getRandomCell();
        if (cell != null)
        {
            GameObject tile = Instantiate(tilePrefab,gameObject.transform);
            tile.GetComponent<RectTransform>().position = cell.GetComponent<RectTransform>().position;
            int randomValue = UnityEngine.Random.Range(1, 3);
            if(randomValue == 1)
            {
                tile.GetComponent<Tile>().number = 2;
                //tile.GetComponent<Tile>().setNumber();
                tile.GetComponent<Tile>().setState(tileState[0]);
            }
            else
            {
                tile.GetComponent<Tile>().number = 4;
                //tile.GetComponent<Tile>().setNumber();
                tile.GetComponent<Tile>().setState(tileState[1]);
            }
            
            cell.haveTile = true;
            cell.tileOfCell = tile;
            if (tileInBoard == 16)
            {
                checkGameOver();
                if (gameOver)
                {
                    menu.gameObject.SetActive(true);
                    Play.gameObject.SetActive(false);
                    gameOverBoard.SetActive(true);
                }
            }
        }   
        
    }
    public CellFrame getRandomCell()
    {
        int ran = UnityEngine.Random.Range(0, 16);
        if (!cells[ran/4,ran%4].haveTile)
        {
            lastCell = ran;
            return cells[ran / 4, ran % 4];
        }
        else
        {
            for (int i = ran + 1; i != ran; i++)
            {
                if (i == 16)
                {
                    i = 0;
                }
                if (!cells[i/4,i%4].haveTile)
                {
                    lastCell = i;
                    return cells[i / 4, i % 4];

                }
            }
        } 
        return null;
    }
}
