using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LockedDoor : MonoBehaviour, ISwappable
{
    static private LockedDoor[,] _LOCKED_DOORS;
    static private Dictionary<int, DoorInfo> _DOOR_INFO_DICT;
    public Vector2Int mapLoc;
    const int LOCKED_R = 73;
    const int LOCKED_UR = 57;
    const int LOCKED_UL = 56;
    const int LOCKED_L = 72;
    const int LOCKED_DL = 88;
    const int LOCKED_DR = 89;

    public class DoorInfo {
        public int tileNum;
        public Vector2Int otherHalf;

        public DoorInfo(int tN, Vector2Int oH) {
            tileNum = tN;
            otherHalf = oH;
            if (_DOOR_INFO_DICT != null) {
                _DOOR_INFO_DICT.Add(tileNum, this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_LOCKED_DOORS == null) {
            BoundsInt mapBounds = MapInfo.GET_MAP_BOUNDS();
            _LOCKED_DOORS = new LockedDoor[mapBounds.size.x, mapBounds.size.y];
            InitDorInfoDict();
        }
        mapLoc = Vector2Int.FloorToInt(transform.position);
        _LOCKED_DOORS[mapLoc.x, mapLoc.y] = this;
    }

    void InitDoorInfoDict()
    {
        _DOOR_INFO_DICT = new Dictionary<int, DoorInfo>();

        new DoorInfo(LOCKED_R, Vector2Int.zero);
        new DoorInfo(LOCKED_UR, Vector2Int.left);
        new DoorInfo(LOCKED_UL, Vector2Int.right);
        new DoorInfo(LOCKED_L, Vector2Int.zero);
        new DoorInfo(LOCKED_DL, Vector2Int.right);
        new DoorInfo(LOCKED_DR, Vector2Int.left);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Dray>() != null) Destroy(gameObject);
    }

    public GameObject guaranteedDrop { get; set; }
    public int tileNum { get; private set; }
    public void Init(int fromTileNum, int tileX, int tileY)
    {
        tileNum = fromTileNum;
        SpriteRenderer sRend = GetComponent<SpriteRenderer>();
        sRend.sprite = TilemapManager.DELVER_TILES[fromTileNum].sprite;
        transform.position = new Vector3(tileX, tileY, 0) + MapInfo.OFFSET;
    }

}
