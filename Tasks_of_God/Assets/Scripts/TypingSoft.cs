using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingSoft : MonoBehaviour
{
    //　問題の日本語文
	private string[] qJ  = {"問題", "テスト", "タイピング","神","ゲーム","作業","進歩","締め切り"};
	//　問題のローマ字文
	private string[] qR = {"monndai", "tesuto", "taipinngu","kami","ga-mu","sagyou","sinnpo","simekiri"};
	//　日本語表示テキスト
	private Text UIJ;
	//　ローマ字表示テキスト
	private Text UIR;
	//　日本語問題
	private string nQJ;
	//　ローマ字問題
	private string nQR;
	//　問題番号
	private int num;

    //　正解した文字列を入れておく
    private string correctString;


    //　トータル制限時間
	private float totalTime;
    //　制限時間（分）
	[SerializeField]
	private int hour;
	//　制限時間（分）
	[SerializeField]
	private int minute;
	//　制限時間（秒）
	[SerializeField]
	private float seconds;
	//　前回Update時の秒数
	private float oldSeconds;

    private Text timeText;

    private float progress = 0;
    private Image progressBar;
    private int Max_prog = 50;

    private float bug = 0;
    private Image bugBar;
    private int Max_bug = 100;

    private float HP = 500;
    private Image HPBar;
    private int Max_HP = 500;


    private string mode;

    private int life = 3;
    private Image life_1;
    private Image life_2;

 
 
	void Start () {
        

		//　テキストUIを取得
		UIJ = GameObject.Find("QuestionJ").GetComponent<Text>();
		UIR = GameObject.Find("QuestionR").GetComponent<Text>();

        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        progressBar = GameObject.Find("progressBar").GetComponent<Image>();
        bugBar = GameObject.Find("bugBar").GetComponent<Image>();
        HPBar = GameObject.Find("hpBar").GetComponent<Image>();
        //　データ初期化処理



		//　問題数内でランダムに選ぶ
		num = Random.Range(0, qJ.Length);
 
		//　選択した問題をテキストUIにセット
		nQJ = qJ[num];
		nQR = qR[num];
		UIJ.text = nQJ;
		UIR.text = nQR;



        //　問題出力メソッドを呼ぶ
	    OutputQ();

        // StartCoroutine ("Logging");

        hour = 2;
        minute = 00;
        seconds = 00;
        totalTime = hour * 3600 + minute * 60 + seconds;
		oldSeconds = 0f;

        mode = "work";

        life_1 = GameObject.Find("Life_1").GetComponent<Image>();
        life_2 = GameObject.Find("Life_2").GetComponent<Image>();

        life_1.gameObject.SetActive(false);
        life_2.gameObject.SetActive(false);
    }
	

    void OutputQ() {
	//　問題数内でランダムに選ぶ
        num = Random.Range(0, qJ.Length);
    
        //　選択した問題をテキストUIにセット
        nQJ = qJ[num];
        nQR = qR[num];
        UIJ.text = nQJ;
        UIR.text = nQR;
    }


    //　問題の何文字目か
    private int index;

    void Update () {

        //　キーを押しているかどうか
        if(Input.anyKeyDown
        && (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        ) {
            //　今見ている文字とキーボードから打った文字が同じかどうか
            if(Input.GetKeyDown(nQR[index].ToString())) {
                //　正解時の処理を呼び出す
                Correct();
            } else {
                //　失敗時の処理を呼び出す
                Mistake();
            }
        }

        CountDown();
        showBar();

        if(bug >= Max_bug){
            mode = "debug";
        }else if(bug == 0 && mode == "debug"){
            mode = "work";
        }

        if(progress >= Max_prog)
            SceneManager.LoadScene("ClearScene");

        if(HP <= 0){
            life -= 1;
            if(life == 2){
                life_1.gameObject.SetActive(true);
            }else if(life == 1){
                life_2.gameObject.SetActive(true);
            }else{
                SceneManager.LoadScene("GameOverScene");
            }
            HP = Max_HP;
        }

    }

    //　タイピング正解時の処理
    void Correct() {

        //　正解した文字を表示
        correctString += nQR[index].ToString();
        
        //　次の文字を指す
        index++;
        UIR.text =  "<color=#313131>" + correctString + "</color>" + nQR.Substring(index);
        //　問題を入力し終えたら次の問題を表示
        if(index >= nQR.Length) {
            if(mode == "work"){
                progress += 1;
            }else if(mode == "debug"){
                bug -= (float)Max_bug*0.2f;
                if(bug > Max_bug) bug = Max_bug;
            }
            progressBar.fillAmount = 1-(progress/Max_prog);

            correctString = "";
            index = 0;
            OutputQ();
        }
        Debug.Log("正解");


    }

    //　タイピング失敗時の処理
    void Mistake() {
        HP -= 1;
        HPBar.fillAmount = 1 - (HP/Max_HP);

        bug += 1;
        bugBar.fillAmount = 1-(bug/Max_bug);
    }

    //　正解率の計算処理
    void CorrectAnswerRate() {

    }

    void CountDown(){
        //　制限時間が0秒以下なら何もしない
		if (totalTime <= 0f) {
			return;
		}
		//　一旦トータルの制限時間を計測；
		totalTime = hour * 3600 + minute * 60 + seconds;
		totalTime -= Time.deltaTime * 12;
        Debug.Log(totalTime);
        HP -= Time.deltaTime;
 
		//　再設定
        hour = (int) totalTime / 3600;
        Debug.Log("hour: " + hour);
		minute = (int) ((int)(totalTime- hour * 3600) / 60) ;
        Debug.Log("minute: " + minute);
		seconds = totalTime - hour * 3600 - minute * 60;
        Debug.Log("seconds: " + seconds);
		//　タイマー表示用UIテキストに時間を表示する
		if((int)seconds != (int)oldSeconds) {
			timeText.text = "納期まであと... " + hour.ToString("00") + ":" + minute.ToString("00") + ":" + ((int) seconds).ToString("00");
            string testText = hour.ToString("00") + ":"  + minute.ToString("00") + ":" + ((int) seconds).ToString("00");
            Debug.Log(testText);
		}
		oldSeconds = seconds;
		//　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
		if(totalTime <= 0f) {
			Debug.Log("制限時間終了");
		}
        
        
    }

    void showBar(){
        progressBar.fillAmount = 1-(progress/Max_prog);
        bugBar.fillAmount = 1-(bug/Max_bug);
        HPBar.fillAmount = 1-(HP/Max_HP);
    }

    // IEnumerator Logging(){
    //     while (true) {
    //         // yield return new WaitForSeconds (0.5f);
    //         // Debug.LogFormat ("{0}秒経過", 0.5f);
    //     }
    // }

}
