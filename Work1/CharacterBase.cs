namespace Work1
{
    public abstract class CharacterBase
    {
        protected string name;
        protected int HP, MP, AP, DP;
        protected int MaxHP, MaxMP;
        protected CommandGUIView GUIView;

        protected CharacterBase(string name,int HP,int MP,int AP,int DP)
        {
            this.name = name;
            this.HP = HP;
            this.MP = MP;
            this.AP = AP;
            this.DP = DP;

            MaxHP = HP;
            MaxMP = MP;
        }

        //GUIViewをセット
        public void SetCommandGUIView(CommandGUIView GUIView)
        {
            this.GUIView = GUIView;
        }

        //基本的にこれをゲーム側から呼ぶ
        public abstract void Do(CharacterBase targetCharacter);
        
        //攻撃する
        protected void GiveAttack(CharacterBase targetCharacter,int AP)
        {
            targetCharacter.ReceiveAttack(AP);
        }

        //攻撃される
        protected void ReceiveAttack(int receiveAP)
        {
            int damage = (receiveAP / 2) - (DP / 4);
            if (damage <= 0)
            {
                damage = 1;
            }
            HP -= damage;
            GUIView.DamageView(this,damage);
            if (HP <= 0)
            {
                HP = 0;
            }
        }

        public int GetHP()
        {
            return HP;
        }

        public int GetMaxHP()
        {
            return MaxHP;
        }

        public string GetName()
        {
            return name;
        }
    }

    //防御できるよ
    public interface IDefensable
    {
        const int defenseMultiplier = 3;
        void DoDefense();
        void DefenseCheck();
    }

    //魔法使えるよ
    public interface ISpellable
    {
        void DoSpell(CharacterBase targetCharacter);
    }
    
}
