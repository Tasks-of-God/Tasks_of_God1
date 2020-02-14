using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingSoft : MonoBehaviour
{
    //　問題の日本語文
	private string[] qJ  = {"問題", "テスト", "タイピング","神","ゲーム","作業","進歩","締め切り","もも","あか","夜景","資源",
                            "お茶","酸素","メガネ","指揮者","コーヒー","たこ焼き","琵琶湖","騎馬戦","台風","梅酒","水彩画",
                            "日本地図","カツカレー","うどん","腕時計","都道府県","県庁所在地","もんじゃ焼き","国語辞典","死後現金","美少年",
                            "ポテトチップス","トランペット","電子辞書","電話","天気予報","支離滅裂","ごめんなさい","回転寿司",
                            "タイムカード","チョコレート","バウムクーヘン","万葉集","結婚します","どんでん返し","春","夏",
                            "秋","冬","井戸","月","ネギ","地下","壁","皿","ささみチーズカツ","オムライス","ネギトロ丼"};
	//　問題のローマ字文
	private string[] qR = {"monndai", "tesuto", "taipinngu","kami","ga-mu","sagyou","sinnpo","simekiri","momo","aka","yakei","sigen",
                            "otya","sannso","megane","sikisya","ko-hi-","takoyaki","biwako","kibasen","taifuu","umesyu","suisaiga",
                            "nihonntizu","katukare-","udonn","udedokei","todoufuken","kenntyousyozaiti","monnjayaki","kokugozitenn","sigogennkin",
                            "bisyounenn","potetotippusu","torannpetto","dennsizisyo","dennwa","tennkiyohou","sirimeturetu","gomennnasai",
                            "kaitennzusi","taimuka-do","tyokore-to","baumuku-henn","mannyousyuu","kekkonnsimasu","donndenngaesi","haru",
                            "natu","aki","fuyu","ido","tuki","negi","tika","kabe","sara","sasamiti-zukatu","omuraisu","negitorodonn"};
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

    bool[] answeredQ = new bool[500];

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

    public AudioClip progSound;
    public AudioClip bugSound;
    AudioSource audioSource;

 
 
	void Start () {
        

		//　テキストUIを取得
		UIJ = GameObject.Find("QuestionJ").GetComponent<Text>();
		UIR = GameObject.Find("QuestionR").GetComponent<Text>();

        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        progressBar = GameObject.Find("progressBar").GetComponent<Image>();
        bugBar = GameObject.Find("bugBar").GetComponent<Image>();
        HPBar = GameObject.Find("hpBar").GetComponent<Image>();

        //　データ初期化処理
        for(int n=0; n<qJ.Length; n++){
            answeredQ[n] = false;
        }

        num = Random.Range(0, qJ.Length);
        answeredQ[num] = true;


 
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

        audioSource = GetComponent<AudioSource>();

    }
	

    void OutputQ() {
	//　問題数内でランダムに選ぶ
        int count = 0;
        do{
            if(count>=qJ.Length){
                for(int n=0; n<qJ.Length; n++){
                    answeredQ[n] = false;
                }
            }
            //　問題数内でランダムに選ぶ
            num = Random.Range(0, qJ.Length);
            count++;
        }while(answeredQ[num]);
        answeredQ[num] = true;
    
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

        if(bugBar.fillAmount  >= 1){
            mode = "debug";
        }else if(bug == 0 && mode == "debug"){
            mode = "work";
        }

        if(progress >= Max_prog)
            SceneManager.LoadScene("ClearScene");

        if(HP <= 0){
            life -= 1;
            if(life == 2){
                life_1.gameObject.SetActive(false);
            }else if(life == 1){
                life_2.gameObject.SetActive(false);
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
                audioSource.PlayOneShot(progSound);
            }else if(mode == "debug"){
                bug -= (float)Max_bug*0.2f;
                if(bug > Max_bug) bug = Max_bug;
            }

            correctString = "";
            index = 0;
            OutputQ();
        }



    }

    //　タイピング失敗時の処理
    void Mistake() {
        HP -= 1;
        HPBar.fillAmount = HP/Max_HP;

        bug = Max_bug;
        audioSource.PlayOneShot(bugSound);
        showBar();
    }

    //　正解率の計算処理
    void CorrectAnswerRate() {

    }

    void CountDown(){
        //　制限時間が0秒以下なら何もしない
		if (totalTime <= 0f) {
            SceneManager.LoadScene("GameOverScene");
			return;
		}
		//　一旦トータルの制限時間を計測；
		totalTime = hour * 3600 + minute * 60 + seconds;
		totalTime -= Time.deltaTime * 60;
        HP -= Time.deltaTime;
 
		//　再設定
        hour = (int) totalTime / 3600;
		minute = (int) ((int)(totalTime- hour * 3600) / 60) ;
		seconds = totalTime - hour * 3600 - minute * 60;
		//　タイマー表示用UIテキストに時間を表示する
		if((int)seconds != (int)oldSeconds) {
			timeText.text = "納期まであと... " + hour.ToString("00") + ":" + minute.ToString("00") + ":" + ((int) seconds).ToString("00");
            string testText = hour.ToString("00") + ":"  + minute.ToString("00") + ":" + ((int) seconds).ToString("00");
		}
		oldSeconds = seconds;

        
        
    }

    void showBar(){
        
        HPBar.fillAmount = HP/Max_HP;

        if(progressBar.fillAmount < progress/Max_prog){
            if((progressBar.fillAmount + Time.deltaTime/5) > Max_prog)
                progressBar.fillAmount = progress/Max_prog;
            else
                progressBar.fillAmount += Time.deltaTime/5;
        }

        if(bugBar.fillAmount < bug/Max_bug && mode == "work"){
            if((bugBar.fillAmount + Time.deltaTime) > Max_bug)
                bugBar.fillAmount =Max_bug;
            else
                bugBar.fillAmount += Time.deltaTime;
        }else if(bugBar.fillAmount > bug/Max_bug && mode == "debug"){

            if((bugBar.fillAmount - Time.deltaTime) < 0)
                bugBar.fillAmount = 0;
            else{
                bugBar.fillAmount -= Time.deltaTime;
                            Debug.Log(bugBar.fillAmount);
            }
        }
    }



    // IEnumerator Logging(){
    //     while (true) {
    //         // yield return new WaitForSeconds (0.5f);
    //         // Debug.LogFormat ("{0}秒経過", 0.5f);
    //     }
    // }

}
