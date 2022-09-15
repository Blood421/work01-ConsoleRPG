
namespace Work1
{
    public enum CursorPos
    {
        Attack,
        Spell,
        Defense,
    }

    public enum CursorMoveDir
    {
        Up,
        Down,
    }
    public class CommandGUIView
    {
        private CursorPos nowCursorPos;
        private int hp = 100, mp = 100;

        public CommandGUIView(CursorPos cursorPos)
        {
            nowCursorPos = cursorPos;
        }

        //ステータス更新
        public void ViewStatusUpdate(int hp, int mp)
        {
            this.hp = hp;
            this.mp = mp;
        }

        //カーソルの位置更新
        public void CursorPosUpdate(CursorPos cursorPos)
        {
            nowCursorPos = cursorPos;
        }

        //UI更新
        public void ViewUpdate()
        {
            string cursorAttack = " ", cursorSpell = " ", cursorDefense = " ";
            switch (nowCursorPos)
            {
                case CursorPos.Attack:
                    cursorAttack = "<";
                    break;
                case CursorPos.Spell:
                    cursorSpell = "<";
                    break;
                case CursorPos.Defense:
                    cursorDefense = "<";
                    break;
                default:
                    Console.WriteLine("無効な値");
                    break;
            }

            //status
            WriteBar(1);
            WriteSpace(1);
            Console.Write("HP:");
            Console.Write(hp.ToString("000"));
            WriteSpace(1);
            WriteBar(1);
            WriteSpace(1);
            Console.Write("MP:");
            Console.Write(mp.ToString("000"));
            WriteSpace(1);
            WriteBar(1);

            Console.WriteLine();

            //Bar

            WriteBar(19);

            Console.WriteLine();

            //Attack

            WriteBar(1);
            WriteSpace(1);
            Console.Write("Attack");
            WriteSpace(2);
            Console.Write(cursorAttack);
            WriteSpace(7);
            WriteBar(1);

            Console.WriteLine();

            //Magic

            WriteBar(1);
            WriteSpace(1);
            Console.Write("Spell ");
            WriteSpace(2);
            Console.Write(cursorSpell);
            WriteSpace(7);
            WriteBar(1);

            Console.WriteLine();

            //Defense

            WriteBar(1);
            WriteSpace(1);
            Console.Write("Defense");
            WriteSpace(1);
            Console.Write(cursorDefense);
            WriteSpace(7);
            WriteBar(1);

            Console.WriteLine();

            //End

            WriteBar(19);

            Console.WriteLine();

        }

        //ターンの終了
        public void TurnFinish()
        {
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("ターン終了");

            Console.WriteLine();
            Console.WriteLine();
        }

        //GUIの終了
        public void GUIUpdateFinish()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        //スペース入れる
        private void WriteSpace(int spaceNum)
        {
            for (int i = 0; i < spaceNum; i++)
            {
                Console.Write(" ");
            }
        }

        //バー入れる
        private void WriteBar(int barNum)
        {
            for (int i = 0; i < barNum; i++)
            {
                Console.Write("-");
            }
        }

        //カーソルの位置取得
        public static CursorPos MoveCursorPos(CursorPos cursorPos,CursorMoveDir dir)
        {
            switch (cursorPos)
            {
                case CursorPos.Attack:
                    if (dir == CursorMoveDir.Up)
                    {
                        return CursorPos.Attack;
                    }
                    else if (dir == CursorMoveDir.Down)
                    {
                        return CursorPos.Spell;
                    }
                    break;
                case CursorPos.Spell:
                    if (dir == CursorMoveDir.Up)
                    {
                        return CursorPos.Attack;
                    }
                    else if (dir == CursorMoveDir.Down)
                    {
                        return CursorPos.Defense;
                    }
                    break;
                case CursorPos.Defense:
                    if (dir == CursorMoveDir.Up)
                    {
                        return CursorPos.Spell;
                    }
                    else if (dir == CursorMoveDir.Down)
                    {
                        return CursorPos.Defense;
                    }
                    break;
                default:
                    Console.WriteLine("無効な値です");
                    return CursorPos.Attack;
            }
            Console.WriteLine("無効な値です");
            return CursorPos.Attack;
        }

        //HPによって色変更
        public void ConsoleColorUpdate(int maxHp)
        {
            float hpRate = (float)hp / (float)maxHp;
            if (hpRate > 0.5f)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (hpRate > 0.2f)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
        }

        //MPが足りない表示
        public void MPLacking()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MPが足りない！");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        //強攻撃
        public void StrongAttack()
        {
            Console.WriteLine();
            Console.WriteLine("会心の一撃！");
            Console.WriteLine();
        }

        //防御表示
        public void DefenseView()
        {
            Console.WriteLine();
            Console.WriteLine("防御！");
            Console.WriteLine();
        }

        //魔法表示
        public void MagicView(int mp)
        {
            Console.WriteLine();
            Console.WriteLine("MP" + mp + "を使って魔法発動！");
            Console.WriteLine();
        }

        //ダメージ
        public void DamageView(CharacterBase character,int damage)
        {
            if (character is PlayerCharacter)
            {
                Console.WriteLine();
                Console.WriteLine(damage + "ダメージ受けた！");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(damage + "ダメージ与えた！");
                Console.WriteLine();
            }
        }

        //勝ち負け表示
        public void WinView(string enemyName,bool isWin)
        {
            if (isWin)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("******************************");
                Console.WriteLine();
                Console.WriteLine(enemyName + "を倒した！！！！");
                Console.WriteLine();
                Console.WriteLine("******************************");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("******************************");
                Console.WriteLine();
                Console.WriteLine(enemyName + "に負けた....");
                Console.WriteLine();
                Console.WriteLine("******************************");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
