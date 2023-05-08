namespace Dragonfly.TestApps.TestDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            InitDb();
            //TestSequencer();
        }

        private static void TestSequencer()
        {
            var sequencer = new TestSequencer();
            sequencer.DoTest();
        }

        private static void InitDb()
        {
            var test = new InitDragonflyDb();
            InitDragonflyDb.DoInit();
        }

        private static void Test()
        {
            var test = new TestEF();
            test.DoTest();
        }
    }
}
