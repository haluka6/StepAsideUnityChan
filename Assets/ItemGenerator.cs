using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //Unityちゃんのオブジェクト
    private GameObject unitychan;

    //coneの位置を保存するlistlistを保存
    private List<Vector3> coneArray = new List<Vector3>();

    //coinの位置を保存するlistlistを保存
    private List<Vector3> coinArray = new List<Vector3>();

    //carの位置を保存するlistlistを保存
    private List<Vector3> carArray = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find ("unitychan");

        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i+=15)
        {
            //print(i);
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range (1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    // coneの位置をcone_arrayに追加
                    this.coneArray.Add(new Vector3 (4*j, (float) 0.8, (float) i));
                }
            }
            else
            {
                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range (1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        // coneの位置をcone_arrayに追加
                        this.coinArray.Add(new Vector3 (posRange * j, (float) 0.2, (float) i + offsetZ));
                    }
                    else if (7 <= item && item <= 9)
                    {
                        // carの位置をcone_arrayに追加
                        this.carArray.Add(new Vector3 (posRange * j, (float) 0.2, (float) i + offsetZ));
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<coneArray.Count; i++)
        {
            if (coneArray[i].z - this.unitychan.transform.position.z < 50)
            {
                GameObject cone = Instantiate (conePrefab) as GameObject;
                cone.transform.position = coneArray[i];
                coneArray.Remove(coneArray[i]);
            }
        }
        for (int i = 0; i<coinArray.Count; i++)
        {
            if (coinArray[i].z - this.unitychan.transform.position.z < 50)
            {
                GameObject coin = Instantiate (coinPrefab) as GameObject;
                coin.transform.position = coinArray[i];
                coinArray.Remove(coinArray[i]);
            }
        }
        for (int i = 0; i<carArray.Count; i++)
        {
            if (carArray[i].z - this.unitychan.transform.position.z < 50)
            {
                GameObject car = Instantiate (carPrefab) as GameObject;
                car.transform.position = carArray[i];
                carArray.Remove(carArray[i]);
            }
        }
    }
}
