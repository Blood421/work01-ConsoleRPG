using System.Reactive;
using System.Reactive.Subjects;

namespace Work1
{

    internal class InputController
    {
        private Subject<Unit> upInputSubject = new Subject<Unit>();
        private Subject<Unit> downInputSubject = new Subject<Unit>();
        private Subject<Unit> endInputSubject = new Subject<Unit>();
        private Subject<Unit> decisionInputSubject = new Subject<Unit>();

        public void InputUpdate()
        {
            //上キー入力
            if (Console.ReadKey().Key == ConsoleKey.UpArrow)
            {
                upInputSubject.OnNext(Unit.Default);
            }
            //下キー入力
            if (Console.ReadKey().Key == ConsoleKey.DownArrow)
            {
                downInputSubject.OnNext(Unit.Default);
            }
            //エンターキー入力
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                decisionInputSubject.OnNext(Unit.Default);
            }

            //ESCキー入力
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                endInputSubject.OnNext(Unit.Default);
            }

        }

        public void InputFinish()
        {
            upInputSubject.Dispose();
            downInputSubject.Dispose();
            decisionInputSubject.Dispose();
            endInputSubject.Dispose();
        }
        public IObservable<Unit> GetArrowInputObservable(CursorMoveDir dir)
        {
            if (dir == CursorMoveDir.Up)
            {
                return upInputSubject;
            }
            else if (dir == CursorMoveDir.Down)
            {
                return downInputSubject;
            }

            Console.WriteLine("無効な値 : " + dir);
            return upInputSubject;
        }
        public IObservable<Unit> GetDecisionInputObservable()
        {
            return decisionInputSubject;
        }
        public IObservable<Unit> GetEndInputObservable()
        {
            return endInputSubject;
        }
    }
}
