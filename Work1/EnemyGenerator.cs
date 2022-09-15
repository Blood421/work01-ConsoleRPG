using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work1
{
    public class EnemyGenerator
    {
        public static CharacterBase GetRandomEnemy()
        {
            Random random = new Random();
            int rand = random.Next(0, 3);
            switch (rand)
            {
                case 0:
                    return new EnemyDefault("スライム", 10, 0, 5, 2);
                case 1:
                    return new EnemyDefault("ゴブリン", 15, 0, 8, 4);
                case 2:
                    return new StrongEnemy("ゴーレム", 30, 0, 5, 8);
                default:
                    return new EnemyDefault("スライム", 10, 0, 5, 2);
            }
        }
    }
}
