using Domain.Core;
using Domain.Infrastructure;
using Domain.Interfaces;
using DomainDatabaseEF.Extensions;
using DomainDatabaseManagerEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SandBoxConsole
{
    class Program
    {
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

        static void Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DomainDatabaseContext>();

            var option = builder.UseNpgsql("Host=localhost;Port=5432;Database=DomainBase;Username=postgres;Password=password")
                .UseLoggerFactory(_myLoggerFactory)  //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging()
                .Options;

            string tbl = "";


            var unit1 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 1" };
            var unit2 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 2" };
            var unit3 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 3" };
            var unit4 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 4" };
            var unit5 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 5" };
            var unit6 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 6" };
            var unit7 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 7" };
            var unit8 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 8" };
            var unit9 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 9" };
            var unit10 = new Unit { Active = true, Created = DateTimeOffset.Now, Draft = false, Guid = Guid.NewGuid(), Modified = DateTimeOffset.Now, Title = "Title Unit 10" };


            var node1 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit1 };
            var node2 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit2 };
            var node3 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit3 };
            var node4 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit4 };
            var node5 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit5 };
            var node6 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit6 };
            var node7 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit7 };
            var node8 = new Node { Created = DateTimeOffset.Now, Modified = DateTimeOffset.Now, Order = 1, Unit = unit8 };

            node1.Children.Add(node2);
            node1.Children.Add(node3);

            node2.Children.Add(node4);
            node2.Children.Add(node5);

            node4.Children.Add(node6);
            node4.Children.Add(node7);


            IEnumerable<Domain.Core.Node> res = new List<Domain.Core.Node>();

            using (var db = new DomainDatabaseContext(option))
            {
                //db.Units.AddRange(unit1, unit2, unit3, unit4, unit5, unit6, unit7, unit8, unit9, unit10 );

                //db.SaveChanges();

                //db.UnitLayouts.AddRange(node1, node2, node3, node4, node5, node6, node7, node8);

                //db.SaveChanges();

                res = db.UnitLayouts.GetHierachy()
                    .Include(x => x.Unit)
                    .ToList()
                    .Where(x=>x.Parent == null);


                var arr = new ComponentTreeBuilder().Build(res);

                Prnt2(arr);
            }



            void Prnt(IEnumerable<Domain.Core.Node> units)
            {
                tbl += "|_";

                foreach (var unit in units)
                {
                    Console.WriteLine(tbl + unit.Unit.Title);

                    if (unit.Children?.Count > 0) Prnt(unit.Children);
                }

                tbl = tbl.Substring(0, 1);

            }          


            void Prnt2(IEnumerable<IComponent> units)
            {
                tbl += "-";

                foreach (var unit in units)
                {
                    line(unit);
                }

                tbl = tbl.Substring(0, 1);
            }

            void line(IComponent component)
            {
                if (component is Composit)
                {
                    var item = (Composit)component;

                    Console.WriteLine(tbl + item.Title + " | " + item.GetType().Name);

                    if (item.Children?.Count > 0)
                    {
                        Prnt2(item.Children);
                    }
                }
                else
                {
                    var item = (Module)component;

                    Console.WriteLine(tbl + item.Title + " | " + item.GetType().Name);
                }
            }

        }
    }
}
