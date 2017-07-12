using Core.Common;
using Core.Common.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Core.Common.Client.Entities
{
    public abstract class BaseEntity<T> : IEntity, ICreateEntityFromString<T>, INotifyPropertyChanged where T : IEntity
    {
        
        
       // public DateTime EntryDateTime { get; set; }


        public virtual string EntityId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string EntityName
        {
            get
            {
                return EntityId.ToString();
            }
            set
            {
                //throw new NotImplementedException();
            }
        }

        public virtual T CreateEntityFromString(string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Event for notification of property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fire PropertyChanged event.
        /// </summary>
        /// <typeparam name="TResult">Property return type</typeparam>
        /// <param name="property">Lambda expression for property</param>
        protected void NotifyPropertyChanged<TResult>
            (Expression<Func<TResult>> property)
        {
            string propertyName = ((MemberExpression)property.Body).Member.Name;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Fire PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
