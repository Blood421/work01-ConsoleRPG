using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work1
{
    public class StrongEnemy : CharacterBase
    {
        public StrongEnemy(string name, int HP, int MP, int AP, int DP) : base(name, HP, MP, AP, DP)
        {

        }

        //攻撃しかできない
        public override void Do(CharacterBase targetCharacter)
        {
            Random random = new Random();
            int rand = random.Next(0, 3);

            //強攻撃
            if (rand == 0)
            {
                GUIView.StrongAttack();
                GiveAttack(targetCharacter, AP * 2);
            }
            //普通の攻撃
            else
            {
                GiveAttack(targetCharacter, AP);
            }
        }
    }
}
