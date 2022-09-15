using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work1
{
    public class EnemyDefault : CharacterBase
    {
        public EnemyDefault(string name, int HP, int MP, int AP, int DP) : base(name, HP, MP, AP, DP)
        {

        }

        //攻撃しかできない
        public override void Do(CharacterBase targetCharacter)
        {
            GiveAttack(targetCharacter,AP);
        }
    }
}
