﻿using System;
using System.Linq.Expressions;

namespace Nelibur.ObjectMapper.Bindings
{
    internal sealed class BindingConfigOf<TSource, TTarget> : BindingConfig, IBindingConfig<TSource, TTarget>
    {
        public void Bind(Expression<Func<TSource, object>> source, Expression<Func<TTarget, object>> target)
        {
            string sourceName = GetMemberInfo(source);
            string targetName = GetMemberInfo(target);

            if (string.Equals(sourceName, targetName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            BindFields(sourceName, targetName);
        }

        public void Bind(Expression<Func<TTarget, object>> target, Type targetType)
        {
            string targetName = GetMemberInfo(target);
            BindType(targetName, targetType);
        }

        public void Ignore(Expression<Func<TSource, object>> expression)
        {
            string memberName = GetMemberInfo(expression);
            IgnoreField(memberName);
        }

        private static string GetMemberInfo<T>(Expression<Func<T, object>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    member = unaryExpression.Operand as MemberExpression;
                }

                if (member == null)
                {
                    throw new ArgumentException("Expression is not a MemberExpression", "expression");
                }
            }
            return member.Member.Name;
        }
    }
}
