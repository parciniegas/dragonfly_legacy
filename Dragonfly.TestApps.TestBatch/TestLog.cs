using System;
using System.IO;
using CsvHelper;
using Dragonfly.Batch;
using Dragonfly.Batch.Processer;
using Dragonfly.Batch.Writers;

namespace Dragonfly.TestApps.TestBatch
{
    internal class TestLog
    {
        public void DoTest1()
        {
            using (var reader = new StreamReader(@"D:\Temp\Logs2.csv"))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = false;
                new DataFlow<InputLog, InputLog>()
                    .SetReader(new CsvHelperReader<InputLog>(csvReader))
                    .SetWriter(new CustomWriterIn())
                    .SetProcesser(new CastProcesser<InputLog, InputLog>())
                    .Execute();
            }
        }

        public void DoTest2()
        {
            using (var reader = new StreamReader(@"D:\Temp\Logs2.csv"))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = false;
                
                new DataFlow<InputLog, OutputLog>()
                    .SetReader(new CsvHelperReader<InputLog>(csvReader))
                    .SetWriter(new ConsoleWriter<OutputLog>())
                    .SetProcesser(new LogProcesser())
                    .Execute();
            }
        }

        public void DoTest3()
        {
            using (var reader = new StreamReader(@"D:\Temp\Logs2.csv"))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = false;
                new DataFlow<InputLog, OutputLog>()
                    .SetReader(new CsvHelperReader<InputLog>(csvReader))
                    .SetWriter(new SqlWriter())
                    .SetProcesser(new LogProcesser())
                    .AddPreFilter(new PreFilter())
                    .Execute();
            }
        }

        public void DoTest4()
        {
            using (var reader = new StreamReader((@"D:\Temp\Logs2.csv")))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = false;
                new DataFlow<InputLog, OutputLog>()
                    .SetReader(new CsvHelperReader<InputLog>(csvReader))
                    .SetWriter(new CustomWriterOut())
                    .SetWriter(new SqlWriter())
                    .SetProcesser(new LogProcesser())
                    //.AddPreFilter(new PreFilter())
                    .Execute(0, 5000);
            }
        }

        public void DoTest5()
        {
            var count = 0;
            using (var reader = new StreamReader((@"D:\Temp\Logs2.csv")))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = false;
                new DataFlow<InputLog, OutputLog>()
                    .SetReader(new CsvHelperReader<InputLog>(csvReader))
                    .SetWriter(new CustomWriterOut())
                    .SetWriter(new SqlBulkWriter())
                    .SetProcesser(new LogProcesser())
                    .AddPreFilter(new PreFilter(log => { count++; Console.CursorLeft = 0; Console.Write($"{count} items discarted."); }))
                    .Execute(0, 50000);
            }
        }

        public void DoTest6()
        {
            using (var reader = new StreamReader(@"D:\Temp\Logs.csv"))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = false;
                new DataFlow<InputLog, OutputLog>()
                    .SetReader(new CsvHelperReader<InputLog>(csvReader))
                    .SetWriter(new ConsoleWriter<OutputLog>())
                    .SetProcesser(new LogProcesser())
                    .AddPreFilter(new PreFilter())
                    .Execute();
            }
        }

        private int _count;
        private void ShowFiltered(InputLog input)
        {
            _count++;
            Console.CursorLeft = 0;
            Console.Write($"{_count} items discarted.");
        }
    }
}

