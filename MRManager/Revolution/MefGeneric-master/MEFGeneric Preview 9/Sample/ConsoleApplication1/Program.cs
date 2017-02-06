﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.ComponentModel.Composition.Hosting.Extension;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			new Startup().CatalogSetup();
		}
	}

	class Startup
	{
		public void CatalogSetup()
		{
			var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			var genericCatalog = new GenericCatalog(catalog);
			var container = new CompositionContainer(genericCatalog);

			
			container.GetExportedValue<TEMP>().Say();

			container.GetExportedValueOrDefault<IUMC<string>>();
			container.GetExportedValueOrDefault<IUMC<int>>();

            var res = container.GetExportedValueOrDefault<IEntityCacheViewModel<IVisitType>>();

            container.GetExportedValueOrDefault<IUMC<IEntity<IId>>>().Say();


             container.GetExportedValueOrDefault<IUMC<IEntity<IId>>>().Say();

            container.GetExportedValueOrDefault<IUMC<string>>().Say();

			container.GetExportedValue<ITwoInterface<int, string>>().Say();
			container.GetExportedValue<ITwoInterface<string, int>>().Say();

			Console.WriteLine("-------------------------");
			var c = container.GetExports(typeof(IUMC<>), null, null);
			Console.WriteLine(c.Count());

		    Console.ReadLine();

		}
	}

    public interface IVisitType : IEntity
    {
    }

    public interface IEntityCacheViewModel<T> where T : IEntity
    {
    }

    [Export(typeof(IEntityCacheViewModel<>))]
    public class ViewModel<TEntity> : IEntityCacheViewModel<TEntity> where TEntity : IEntity
    {

    }

    public interface IEntity
    {
    }

    internal interface IEntity<TId> where TId:IId
    {

    }

    internal interface IId
    {
    }

    public class foo : IId
    {
        
    }

    #region TEMP
	[Export(typeof(TEMP))]
	public class TEMP
	{
		public void Say() { Console.WriteLine("TEMP"); }
	}
	#endregion

	#region IUMC<T>
	public interface IUMC<T>
	{
		void Say();
	}

    [Export(typeof(IUMC<>))]
    public class UMC<T> : IUMC<T>
    {
        [ImportingConstructor]
        public UMC(TEMP temp)
        {
            if (temp == null) throw new NullReferenceException("temp");
        }

        #region IUMC<T> 멤버

        public void Say()
        {
            Console.WriteLine(typeof(T).FullName);
        }

        #endregion
    }
    #endregion

    #region ITwoInterface<T1,T2>
    public interface ITwoInterface<T1, T2>
	{
		void Say();
	}

	[Export(typeof(ITwoInterface<,>))]
	public class TwoInterface<T1, T2> : ITwoInterface<T1, T2>
	{
		[ImportingConstructor]
		public TwoInterface(IUMC<object> umc)
		{
			if (umc == null) throw new NullReferenceException("umc");
		}

		#region ITwoInterface<T1,T2> 멤버

		public void Say()
		{
			Console.WriteLine("{0}, {1}", typeof(T1), typeof(T2));
		}

		#endregion
	}
	#endregion
}
