using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAPITest : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase tile;

    public GameObject explosionPrefab;
    private Grid grid;

	// Use this for initialization
	void Start ()
	{
	    grid = GetComponent<Grid>();
	    return;
        //清空所有的格子
	    //tilemap.ClearAllTiles();

        //根据格子坐标获取瓦片
	    TileBase tile1 = tilemap.GetTile(new Vector3Int(-4,-2,0));
        Debug.Log(tile1);

        //根据格子坐标设置瓦片
	    tilemap.SetTile(Vector3Int.zero, tile);

	    //删除指定格子的瓦片
        tilemap.SetTile(Vector3Int.zero,null);

        //将所有的参数1 ，替换成参数2
        tilemap.SwapTile(tile1, tile);

        //tilemap.SetTiles();
        //tilemap.BoxFill();
        //tilemap.GetTilesBlock()
        //tilemap.SetTilesBlock();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
	    {
            //屏幕坐标转世界坐标
	        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //世界坐标转格子坐标
	        Vector3Int cellPos = grid.WorldToCell(worldPos);

	        ExplosionLogic(cellPos);
	        ExplosionLogic(cellPos+new Vector3Int(1,0,0));
	        ExplosionLogic(cellPos + new Vector3Int(-1, 0, 0));
	        ExplosionLogic(cellPos + new Vector3Int(0, 1, 0));
	        ExplosionLogic(cellPos + new Vector3Int(0, -1, 0));
        }
	}

    void ExplosionLogic(Vector3Int cellPos)
    {
        tilemap.SetTile(cellPos, null);

        //格子坐标转世界坐标
        //Vector3 worldPos = grid.CellToWorld(cellPos);
        Vector3 worldPos = grid.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f, 0f));
        Instantiate(explosionPrefab, worldPos, Quaternion.identity);
    }
}
