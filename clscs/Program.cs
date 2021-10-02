using clscs.EF;
using clscs.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using static System.Console;

namespace clscs
{
    class Program
    {
        static async Task Main(string[] args)
        {
           TestUsingTransactionsTable();
            await Task.CompletedTask;
        }

        private static void TestUsingTransactionsTable()
        {
            using var db = new ResilientContext();
            var strategy = db.Database.CreateExecutionStrategy();
            db.Departments.Add(new Department() { Name = "Dep2", GroupName = "Home", ModifiedDate = DateTime.Now });
            var transaction = new TrackTransaction();
            db.Transactions.Add(transaction);

            strategy.ExecuteInTransaction(db,
                operation: context =>
                    context.SaveChanges(acceptAllChangesOnSuccess: false),
                verifySucceeded: context =>
                    context.Transactions.AsNoTracking().Any(t => t.Id == transaction.Id));
            db.ChangeTracker.AcceptAllChanges();
            db.Transactions.Remove(transaction);
            db.SaveChanges();
        }

        private static void TestStateVerification()
        {
            using (var db = new ResilientContext())
            {
                var strategy = db.Database.CreateExecutionStrategy();
                var depToAdd = new Department() { Name = "Dep1", GroupName = "Home", ModifiedDate = DateTime.Now };
                db.Departments.Add(depToAdd);

                strategy.ExecuteInTransaction(db,
                    operation: context => context.SaveChanges(acceptAllChangesOnSuccess: false),
                    verifySucceeded: context =>
                        context.Departments.AsNoTracking().Any(d => d.DepartmentId == depToAdd.DepartmentId));
                db.ChangeTracker.AcceptAllChanges();
            }
        }

        private static async Task TestExecutionStrategyWithAmbientTransactions()
        {
            using (var context1 = new ResilientContext())
            {
                context1.Departments.Add(new Department()
                {
                    Name = "Dp4", GroupName = "Home", ModifiedDate = DateTime.Now
                });

                var strategy = context1.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var context2 = new ResilientContext())
                    {
                        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            await context2.Departments.AddAsync(new Department()
                            {
                                Name = "Dp5", GroupName = "Home", ModifiedDate = DateTime.Now
                            });
                            await context2.SaveChangesAsync();
                            await context1.SaveChangesAsync();
                            transaction.Complete();
                        }
                    }
                });
            }

            using (var context3 = new ResilientContext())
            {
                foreach (var dep in context3.Departments.Where(d => d.GroupName == "Home").ToList())
                {
                    WriteLine(dep);
                }                
            }
        }
        
        private static void TestExecutionStrategyWithTransactions()
        {
            using var context = new ResilientContext();
            var strategy = context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var ctx = new ResilientContext();
                using var transaction = ctx.Database.BeginTransaction();
                ctx.Departments.Add(new Department()
                {
                    Name = "New Department", GroupName = "New Group", ModifiedDate = DateTime.Now
                });
                ctx.SaveChanges();

                ctx.Departments.Add(new Department()
                {
                    Name = "Eaters", GroupName = "Home", ModifiedDate = DateTime.Now
                });
                ctx.SaveChanges();

                transaction.Commit();
            });
        }

        private static void TestResilientContext()
        {
            using var context = new ResilientContext();
            foreach (var data in context.Departments.Take(10))
            {
                WriteLine(data);
            }
        }
        private static void TestEmployeeDepartmentHistory()
        {
            using var context = new AdventureWorksContext();
            var history = context.EmployeeDepartmentHistories
                .Include(h => h.Department)
                .Include(h => h.Employee)
                .Include(h => h.Shift)
                .Take(2).ToList();

            foreach (var h in history)
            {
                Console.WriteLine(JsonConvert.SerializeObject(h));
            }
        }

        private static void TestEmployees()
        {
            using (var context = new AdventureWorksContext())
            {
                // var persons = context.Persons.Include(p => p.BusinessEntity).Take(5).OrderBy(p => p.FirstName).ToList();
                // foreach (var person in persons)
                // {
                //     Console.WriteLine(JsonConvert.SerializeObject(person));
                // }

                var employees = context.Employees
                    .Include(e => e.Person)
                    .Include(e => e.Person.BusinessEntity).Take(2).OrderBy(e => e.BusinessEntityId)
                    .ToList();
                foreach (var e in employees)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e, Formatting.Indented));
                }
            }       
        }

        private static void TestProducts()
        {
                    using (AdventureWorksContext context = new AdventureWorksContext())
                    {
                        var tenProducts = context.Products.Where(p => p.ProductModelId != null && p.SizeUnitMeasureCode != null && p.WeightUnitMeasureCode != null)
                            .Include(p => p.ProductModel)
                            .Include(p => p.ProductSubcategory) 
                            .Include(p => p.WeightUnitMeasure)
                            .Include(p => p.SizeUnitMeasure)
                            .Take(2).OrderBy(p => p.ProductId).ToList();
                        foreach (var product in tenProducts)
                        {
                            Console.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented));
                        }
                    }    
        }


        private static void BasicThreadSample()
        {
            WriteLine($"Starting dedicated thread to do an asynchronous operation");
            var dedicatedThread = new Thread(ComputeBoundOp);
            dedicatedThread.Start(5);
            
            WriteLine("Main thread: doing other work here...");
            Thread.Sleep(10000);
            dedicatedThread.Join();
            WriteLine("Hit <Enter> to end this program...");
            ReadLine();

            static void ComputeBoundOp(object state)
            {
                WriteLine($"In {nameof(ComputeBoundOp)}: state={state}");
                Thread.Sleep(1000);
            }
        }

        private static void ConditionalWeakTableDemo()
        {
            var o = new object().GCWatch($"My object created at {DateTime.Now}");
            GC.Collect();
            GC.KeepAlive(o);
            o = null;

            GC.Collect();
            //ReadLine();
        }

        public static unsafe void Go()
        {
            for(var x = 0; x < 10000; x++)
            {
                new object();
            }

            IntPtr originalMemoryAddress;
            var bytes = new byte[1000];

            fixed(byte* pbytes = bytes)
            {
                originalMemoryAddress = (IntPtr)pbytes;
            }

            GC.Collect();

            fixed(byte* pbytes = bytes)
            {
                WriteLine($"The byte[] did{(originalMemoryAddress == (IntPtr)pbytes ? " not " : null)} move during the GC");
                WriteLine($"Original address: {originalMemoryAddress}, current address: {(IntPtr)pbytes}");
            }
        }
    }
}
