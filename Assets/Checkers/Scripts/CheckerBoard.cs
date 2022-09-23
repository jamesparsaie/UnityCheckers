using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoard : MonoBehaviour
{
    public PlayerController[] myPieces = new PlayerController[16];
    public PlayerController selectedPlayer;
    public GameObject whitePiece;  //gameObject representing white checker
    public GameObject blackPiece; //gameObject representing black checker
    public bool isWhite;  //bool to determine which object to instantiate when filling board

    private Vector2 mouseOver;
    private int index = 0;
    private int whiteCount = 1;
    private int blackCount = 1;

    private bool turnEngaged;
    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();  //Generate full board on game start
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Implement Legal move checker
        //       Likely need to create function specifically for code below as well for cleanup


        //Check if piece is selected, i.e turn has begun and player deciding moves
        if(!turnEngaged){
            SelectPiece();
        }
        //Player has picked their piece and right clicks for final location
        if(turnEngaged){
            //User intends to make movement
            if(Input.GetMouseButtonDown(1)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit)){
                    selectedPlayer.gameObject.transform.position = hit.point;
                    selectedPlayer.isSelected = false;
                    turnEngaged = false;
                }
            }
            //If they click onto another game piece, allow them to get out of movement select mode
            else if(Input.GetMouseButtonDown(0)){
                SelectPiece();
            }
        }
    }

    /**
        Function to handle piece selection and changing material
    **/
    void SelectPiece() {
        //TODO: At latter point will need to verify if clicking on own pieces or enemy pieces
        if(Input.GetMouseButtonDown(0)){        
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);          
            if(hit){
                for(int i=0; i<myPieces.Length; i++){
                    //Select our piece, if selected object then show possible moves
                    if(hitInfo.transform.gameObject.CompareTag(myPieces[i].tag)){
                        myPieces[i].isSelected = true;
                        selectedPlayer = myPieces[i];
                        turnEngaged = true;
                    } else {
                        myPieces[i].isSelected = false;
                    }
                }
            }
        }
    }
    /**
        Method popualtes the board with all pieces (black and white)
    **/
    private void GenerateBoard() {
        //Generate white pieces
        float zPos = -1.96f;
        float xPos = -1.937f;
        for(int x = 0; x<2; x++){
            if(x == 1){
                xPos = -1.38f;
                zPos = -1.39f;
            }
            for(int z=0; z<4; z++){
                GeneratePiece(xPos, zPos, isWhite);
                zPos += 1.12f;
            }
        }

        //Generate black pieces (Need seperate unique x and z coordinatee)
        zPos = 1.96f;
        xPos = 1.937f;
        for(int x = 0; x<2; x++){
            if(x == 1){
                xPos = 1.38f;
                zPos = 1.39f;
            }
            for(int z=0; z<4; z++){
                GeneratePiece(xPos, zPos, !isWhite);
                zPos -= 1.12f;
            }
        }
    }

    /**
        Method to generate and prepare each piece on the board
    **/
    private void GeneratePiece(float x, float z, bool isWhite){
        //Instatiate and prepare game objects
        GameObject go = null;
        if(isWhite){
            go = Instantiate(whitePiece) as GameObject;
        } else {
            go = Instantiate(blackPiece) as GameObject;
        }
        go.transform.SetParent(transform);

        //Assign to array and give unique tag
        PlayerController p = go.GetComponent<PlayerController>();
        if(isWhite){
            p.tag = "White"+whiteCount.ToString();
            p.isWhite = true;
            whiteCount++;
        } else {
            p.tag = "Black"+blackCount.ToString();
            p.isWhite = false;
            blackCount++;
        }
        myPieces[index] = p;
        index++;
        MovePiece(p, x, z);
    }
    /**
        Method for piece placement on the board and movement
    **/
    private void MovePiece(PlayerController p, float x, float z){
        p.transform.position = new Vector3(x, 0, z);
    }
}
