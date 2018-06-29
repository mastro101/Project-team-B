using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour {
	
    public float XPosition, ZPosition;
    public bool CorrectMove;
    public Player[] player = new Player[4];
    public GameObject Menu;

    float Size;

    Vector3 TilePosition,CellPosition;

    public TheGrid grid;

    int _x, _z;

    public string NameDO;
    public GamePlayManager Gpm;

    public UIManager UIM;

    // Update is called once per frame
    void Update () {

        if (!Menu)
        {

        


            if (Input.GetMouseButtonDown(0) && NameDO == Gpm.Name && UIM._isInfo == false)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                   
                   
                        XPosition = hit.transform.position.x;
                        ZPosition = hit.transform.position.z;

                        /**
                         * TilePosition è un oggetto di tipo Vector3 al quale vado a inserire i parametri di posizione dell'Object Colpito
                         */

                        TilePosition = new Vector3(XPosition, hit.transform.position.y, ZPosition);

                        /**
                         * Il For mi permette di controllare ogni cella all'interno della lista e verificare che la WorldPosition della cella[x,z]
                         * sia uguale al Vector3 [TilePosition]
                         * 
                         * Se questa condizione si dovesse risultare vera allora vado a salvare le variabili _x _z per capire quale cella della lista io abbia selezionato
                         */
                        for (int x=0; x<13; x++) {
                            for (int z=0; z<13; z++) {
                                CellPosition = grid.GetWorldPosition(x,z);
                                if (CellPosition == TilePosition) {
                                    _z = z;
                                    _x = x;
                                    CorrectMove = true;
                                    break;
                                }
                                
                            }
                        }
                    
                    }
                }
            }
        }

    }

    //Metodi GET

    public float GetPositionX() {
        float _X = XPosition;
        return _X;
    }

    public float GetPositionZ()
    {
        float _Z = ZPosition;
        return _Z;
    }

    public int GetZ()
    {
        return _z;
    }

    public int GetX()
    {
        return _x;
    }

}
