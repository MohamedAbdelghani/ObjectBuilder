using System;
using System.Linq.Expressions;

namespace ObjectBuilder
{
  internal class PropertySetter<TSource, TProp>
  {
    private readonly Action<TSource, TProp> _setter;

    public PropertySetter(Expression<Func<TSource, TProp>> property)
    {
      _setter = GetPropertySetter(property);
    }

    public void Set(TSource obj, TProp value)
    {
      _setter(obj, value);
    }

    private static Action<TSource, TProp> GetPropertySetter(Expression<Func<TSource, TProp>> property)
    {
      var expression = (MemberExpression)property.Body;
      var name = expression.Member.Name;

      var propertyInfo = typeof(TSource).GetProperty(name);
      var targetExp = Expression.Parameter(typeof(TSource), "target");
      var valueExp = Expression.Parameter(typeof(TProp), "value");

      var propertyExp = Expression.Property(targetExp, propertyInfo);
      var assignExp = Expression.Assign(propertyExp, valueExp);

      var setter = Expression.Lambda<Action<TSource, TProp>>
          (assignExp, targetExp, valueExp).Compile();

      return setter;
    }
  }
}