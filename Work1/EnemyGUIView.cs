using System.ComponentModel.Design;

namespace Work1
{
    public class EnemyGUIView
    {
        private string viewEnemyName = "てすと";
        private int hp = 0;
        public EnemyGUIView(string enemyName)
        {
            viewEnemyName = enemyName;
        }

        //ステータス更新
        public void StatusUpdate(int hp)
        {
            this.hp = hp;
        }

        //UI更新
        public void ViewUpdate()
        {
            WriteBar(19);
            Console.WriteLine();
            if (hp == 0)
            {
                Console.Write("[Dead] " + viewEnemyName);
            }
            else
            {
                Console.Write(viewEnemyName);
            }
            Console.WriteLine();
            WriteBar(19);
            Console.WriteLine();
            Console.WriteLine();
        }

        //バー入れる
        private void WriteBar(int barNum)
        {
            for (int i = 0; i < barNum; i++)
            {
                Console.Write("-");
            }
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
    }
}
