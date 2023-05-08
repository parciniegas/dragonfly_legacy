using System;
using Dragonfly.Core;

namespace Dragonfly.TestApps.TestDataAccess
{
    internal class InitDragonflyDb
    {
        public static void DoInit()
        {
            var env = new DragonflyAppContext();
            var context = new GeneralContext(env);

            context.Courses.ForEach(c => Console.WriteLine("Course: {0}".Inject(c.Name)));
            Console.ReadLine();
        }
    }
}