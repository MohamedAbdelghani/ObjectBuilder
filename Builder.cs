using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace ObjectBuilder
{
  public class Builder<T>
  {
    private T _obj;
    private static readonly ConcurrentDictionary<string, object> _propertySetters = new ConcurrentDictionary<string, object>();

    private Builder()
    {
      _obj = CreateInstance();
    }

    public Builder<T> SetProperty<TProp>(Expression<Func<T, TProp>> property, TProp value)
    {
      var expression = (MemberExpression)property.Body;

      var setter = (PropertySetter<T, TProp>)_propertySetters.GetOrAdd(expression.Member.Name, new PropertySetter<T, TProp>(property));
      setter.Set(_obj, value);

      return this;
    }

    public static Builder<T> New()
    {
      return new Builder<T>();
    }

    public T Build()
    {
      return _obj;
    }

    private static T CreateInstance()
    {
      return (T)Activator.CreateInstance(typeof(T));
    }
  }
}