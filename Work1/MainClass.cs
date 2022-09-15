using Work1;

public class MainClass
{
    static void Main(string[] Args)
    {
        #region Args
        //Index0はHP
        int HP = 30;
        if (Args.Length > 0)
        {
            int parsedHP = 30;
            if (int.TryParse(Args[0], out parsedHP))
            {
                HP = parsedHP;
            }
            else
            {
                HP = 30;
            }
        }

        //Index1はMP
        int MP = 30;
        if (Args.Length > 1)
        {
            int parsedMP = 10;
            if (int.TryParse(Args[1], out parsedMP))
            {
                MP = parsedMP;
            }
            else
            {
                MP = 10;
            }
        }

        //Index2はAP
        int AP = 10;
        if (Args.Length > 2)
        {
            int parsedAP = 10;
            if (int.TryParse(Args[2], out parsedAP))
            {
                AP = parsedAP;
            }
            else
            {
                AP = 10;
            }
        }

        //Index3はDP
        int DP = 3;
        if (Args.Length > 3)
        {
            int parsedDP = 3;
            if (int.TryParse(Args[3], out parsedDP))
            {
                DP = parsedDP;
            }
            else
            {
                DP = 3;
            }
        }

        //Index4はNeedMP
        int NeedMP = 2;
        if (Args.Length > 4)
        {
            int parsedNeedMP = 2;
            if (int.TryParse(Args[4], out parsedNeedMP))
            {
                NeedMP = parsedNeedMP;
            }
            else
            {
                NeedMP = 2;
            }
        }

        //Index5はSpellAP
        int SpellAP = 15;
        if (Args.Length > 5)
        {
            int parsedSpellAP = 15;
            if (int.TryParse(Args[5], out parsedSpellAP))
            {
                SpellAP = parsedSpellAP;
            }
            else
            {
                SpellAP = 15;
            }
        }



        #endregion

        bool isLoop = false;
        //繰り返し遊ぶ用
        do
        {
            //ゲーム中かどうか
            bool gamePlaying = true;

            //プレイヤー
            PlayerCharacter player = new PlayerCharacter("プレイヤー", HP, MP, AP, DP, NeedMP, SpellAP);
            //敵
            CharacterBase enemy = EnemyGenerator.GetRandomEnemy();

            //カーソルの位置
            CursorPos nowCursorPos = CursorPos.Attack;
            //コマンドUI
            CommandGUIView commandGUI = new CommandGUIView(nowCursorPos);
            //敵UI
            EnemyGUIView enemyGUI = new EnemyGUIView(enemy.GetName());

            //UIとプレイヤーを紐付け
            player.SetCommandGUIView(commandGUI);
            //UIと敵を紐付け
            enemy.SetCommandGUIView(commandGUI);

            //入力
            InputController inputController = new InputController();

            #region クロージャ定義

            //UI表示共通処理
            var UIUpdate = () =>
            {
                //敵UI
                enemyGUI.StatusUpdate(enemy.GetHP());
                enemyGUI.ConsoleColorUpdate(enemy.GetMaxHP());
                enemyGUI.ViewUpdate();

                //コマンドUI
                commandGUI.CursorPosUpdate(nowCursorPos);
                commandGUI.ViewStatusUpdate(player.GetHP(), player.GetMP());
                commandGUI.ConsoleColorUpdate(player.GetMaxHP());
                commandGUI.ViewUpdate();
            };

            //GUI表示
            var GUIViewUpdate = () =>
            {
                //UI描画
                UIUpdate();

                //UI描画終了
                commandGUI.GUIUpdateFinish();
            };

            //アクション
            var GameUpdate = () =>
            {
                //プレイヤーアクション
                player.PlayerDo(enemy, nowCursorPos);


                //敵死亡判定
                if (enemy.GetHP() == 0)
                {
                    commandGUI.WinView(enemy.GetName(), true);
                    gamePlaying = false;
                    return;
                }

                //敵のアクション
                enemy.Do(player);

                //プレイヤー死亡判定
                if (player.GetHP() == 0)
                {
                    commandGUI.WinView(enemy.GetName(), false);
                    gamePlaying = false;
                    return;
                }

                //UI描画
                UIUpdate();

                //ターン終了
                commandGUI.TurnFinish();

                //UI描画終了
                commandGUI.GUIUpdateFinish();


            };

            //入力を購読
            var InputSubscribe = () =>
            {
                //下入力
                inputController
                    .GetArrowInputObservable(CursorMoveDir.Up)
                    .Subscribe(_ =>
                    {
                        nowCursorPos = CommandGUIView.MoveCursorPos(nowCursorPos, CursorMoveDir.Up);
                        GUIViewUpdate();
                    });

                //上入力
                inputController
                    .GetArrowInputObservable(CursorMoveDir.Down)
                    .Subscribe(_ =>
                    {
                        nowCursorPos = CommandGUIView.MoveCursorPos(nowCursorPos, CursorMoveDir.Down);
                        GUIViewUpdate();
                    });
                //決定
                inputController
                    .GetDecisionInputObservable()
                    .Subscribe(_ =>
                    {
                    //決定キーのイベント
                    //アクション
                    GameUpdate();
                    });


                //強制終了
                inputController
                    .GetEndInputObservable()
                    .Subscribe(_ =>
                    {
                        gamePlaying = false;
                    });
            };

            //ゲーム終了
            var GameEnd = () =>
            {
                inputController.InputFinish();
            };
            #endregion

            #region ゲーム

            //初期表示
            GUIViewUpdate();

            //購読開始
            InputSubscribe();

            //ゲームループ
            while (gamePlaying)
            {
                inputController.InputUpdate();
            }

            //ゲーム終了
            GameEnd();

            #endregion


            //↓繰り返し遊ぶかどうかの処理


            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("もう一度遊びますか？ y/n");
            Console.WriteLine("-----------------------------------------");

            string loopInput = Console.ReadLine();
            if (loopInput[0] == 'y')
            {
                isLoop = true;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            else if (loopInput[0] == 'n')
            {
                isLoop = false;
            }
        } while (isLoop);
        
    }
}