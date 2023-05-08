using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragonfly.Core;
using Dragonfly.DataAccess.EF;

namespace Dragonfly.TestApps.TestDataAccess
{
    class TestEF
    {
        public void DoTest()
        {
            var quantity = 10000;
            var env = new AppContext();
            var db = new TestContext(env);
            //db.Configuration.AutoDetectChangesEnabled = false;

            var uw = new UnitOfWork(db);

            Console.WriteLine("Creando Formas: {0}".Inject(DateTime.Now.ToLocalTime()));
            var forms = new List<Form>();
            for (var i = 1; i <= quantity; i++)
            {
                forms.Add(new Form
                {
                    Id = i,
                    Cost = 1000,
                    FormStateId = 1,
                    FormSubtypeId = 1,
                    FormTypeId = 1,
                    Number = i,
                    OfficeId = 1,
                    SequenceNumber = i
                });
            }

            Console.WriteLine("Prueba con  db.Configuration.AutoDetectChangesEnabled {0}".Inject(db.Configuration.AutoDetectChangesEnabled ? "habilitado" : "deshabilitado"));
            var start = DateTime.Now;
            Console.WriteLine("Adicionando {0} formas al repositorio.".Inject(quantity));
            forms.ForEach(f => uw.Repository<Form>().Add(f));
            var end = DateTime.Now - start;
            Console.WriteLine("{0} formas adicionadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));

            start = DateTime.Now;
            Console.WriteLine("Guardando {0} formas".Inject(quantity));
            uw.SaveChanges();
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas guardadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));

            start = DateTime.Now;
            Console.WriteLine("Adicionando {0} formas con AddRange".Inject(quantity));
            uw.Repository<Form>().AddRange(forms);
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas adicionadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));

            start = DateTime.Now;
            Console.WriteLine("Guardando {0} formas".Inject(quantity));
            uw.SaveChanges();
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas guardadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));


            db.Configuration.AutoDetectChangesEnabled = false;
            Console.WriteLine();
            Console.WriteLine("Prueba con  db.Configuration.AutoDetectChangesEnabled {0}".Inject(db.Configuration.AutoDetectChangesEnabled ? "habilitado" : "deshabilitado"));
            start = DateTime.Now;
            Console.WriteLine("Adicionando {0} formas al repositorio.".Inject(quantity));
            forms.ForEach(f => uw.Repository<Form>().Add(f));
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas adicionadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));

            start = DateTime.Now;
            Console.WriteLine("Guardando {0} formas".Inject(quantity));
            uw.SaveChanges();
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas guardadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));

            start = DateTime.Now;
            Console.WriteLine("Adicionando {0} formas con AddRange".Inject(quantity));
            uw.Repository<Form>().AddRange(forms);
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas adicionadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));

            start = DateTime.Now;
            Console.WriteLine("Guardando {0} formas".Inject(quantity));
            uw.SaveChanges();
            end = DateTime.Now - start;
            Console.WriteLine("{0} formas guardadas en {1}:{2} minutos".Inject(quantity, end.Minutes, end.Seconds));


            Console.ReadLine();
        }
    }
}
