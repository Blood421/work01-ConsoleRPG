namespace Work1
{
    public class PlayerCharacter : CharacterBase,ISpellable,IDefensable
    {
        
        private CursorPos nowCursorPos;
        bool isDefense = false;
        private readonly int spellNeedMP = 20;
        private readonly int spellAP = 100;
        //守り時の防御力
        private int DefensedDP = 0;


        public PlayerCharacter(string name, int HP, int MP, int AP, int DP, int spellNeedMP, int spellAP) : base(name,HP, MP, AP, DP)
        {
            this.spellNeedMP = spellNeedMP;
            this.spellAP = spellAP;

            DefensedDP = DP * IDefensable.defenseMultiplier;
        }

        //プレイヤーは例外でこれを呼ぶようにする
        public void PlayerDo(CharacterBase targetCharacter,CursorPos cursorPos)
        {
            //これでどのメソッドを呼ぶか決める
            nowCursorPos = cursorPos;
            Do(targetCharacter);
        }

        public override void Do(CharacterBase targetCharacter)
        {
            DefenseCheck();

            switch (nowCursorPos)
            {
                case CursorPos.Attack:
                    Random random = new Random();
                    int rand = random.Next(0, 5);
                    //強攻撃
                    if (rand == 0)
                    {
                        GUIView.StrongAttack();
                        GiveAttack(targetCharacter, AP * 2);
                    }
                    //通常
                    else
                    {
                        GiveAttack(targetCharacter, AP);
                    }
                    break;
                case CursorPos.Spell:
                    DoSpell(targetCharacter);
                    break;
                case CursorPos.Defense:
                    DoDefense();
                    break;
                default:
                    Console.WriteLine("無効な値");
                    break;
            }
        }

        //魔法
        public void DoSpell(CharacterBase targetCharacter)
        {
            if (MP >= spellNeedMP)
            {
                GUIView.MagicView(spellNeedMP);
                //暫定
                MP -= spellNeedMP;
                GiveAttack(targetCharacter, spellAP);
            }
            else
            {
                GUIView.MPLacking();
            }
        }

        //防御状態に
        public void DoDefense()
        {
            //防御するよ
            GUIView.DefenseView();
            isDefense = true;
            DP = DefensedDP;
        }

        //防御状態なら解除
        public void DefenseCheck()
        {
            //防御解除
            if (isDefense)
            {
                isDefense = false;
                DP = DefensedDP / IDefensable.defenseMultiplier;
            }
        }

        public int GetMP()
        {
            return MP;
        }
    }
}
