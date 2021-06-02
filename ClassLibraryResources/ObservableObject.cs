using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using ClassLibraryResources.Annotations;

namespace ClassLibraryResources
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Assert(GetType().GetProperty(propertyName) != null);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propName);
                return true;
            }

            return false;
        }

        protected bool SetProperty<T>(ref T field, T value, Expression<Func<T>> expr)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var lambda = expr as LambdaExpression;
                MemberExpression memberExpression;
                if (lambda.Body is UnaryExpression unaryExpr)
                {
                    memberExpression = unaryExpr.Operand as MemberExpression;
                } 
                else
                {
                    memberExpression = lambda.Body as MemberExpression;
                }
                OnPropertyChanged(memberExpression.Member.Name);
                return true;
            }

            return false;
        }
    }
}