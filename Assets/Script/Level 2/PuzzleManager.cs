using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public PuzzlePiece[] pieces;
    public bool isCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPuzzleComplete()
    {
        foreach(var piece in pieces)
        {
            if(Vector2.Distance(piece.transform.position,piece.correctPosition) > 0.1f)
            {
                return;
            }
        }
        if(!isCompleted)
        {
            isCompleted = true;
            Debug.Log("完成");
            AchieveDetect.achieve[1] = true;
            SceneManager.LoadScene("Options");
        }
    }
}
