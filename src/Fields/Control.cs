﻿namespace Fields
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Fields.Enums;
    using Fields.Extensions;

    public class Control
    {
        public readonly ControlCode ControlCode;
        public readonly IList<InnerField> Fields;
        public readonly PageType PageType;

        public bool IsReadonly(object entity) => entity != null && ((bool?)this.isReadonlyExpression?.DynamicInvoke(entity) ?? false);
        public bool IsHidden(object entity) => entity != null && ((bool?)this.isHiddenExpression?.DynamicInvoke(entity) ?? false);

        private Delegate isHiddenExpression;
        private Delegate isReadonlyExpression;

        public Control(PageType pageType, ControlCode controlCode, IList<InnerField> fields)
        {
            this.ControlCode = controlCode;
            this.Fields = fields;
            this.PageType = pageType;
        }

        public Control(PageType pageType, ControlCode controlCode, InnerField create)
        {
            this.Fields = new List<InnerField> { create };
            this.ControlCode = controlCode;
            this.PageType = pageType;
        }

        public void SetReadonlyRule(LambdaExpression expression)
        {
            this.isReadonlyExpression = expression.Compile();
        }

        public void SetHiddenRule(LambdaExpression expression)
        {
            this.isHiddenExpression = expression.Compile();
        }

        public void SetFieldHiddenRule(LambdaExpression fieldExpression, LambdaExpression expression)
        {
            this.SetFieldExpression(fieldExpression, expression,false);
        }

        public void SetFieldReadonlyRule(LambdaExpression fieldExpression, LambdaExpression expression)
        {
            this.SetFieldExpression(fieldExpression, expression, true);
        }

        private void SetFieldExpression(LambdaExpression fieldExpression, LambdaExpression expression, bool readonlyExpression)
        {
            foreach (InnerField innerField in this.Fields)
            {
                if (innerField.expression.ToString() == fieldExpression.ToString())
                {
                    if (readonlyExpression)
                    {
                        innerField.SetReadonlyRule(expression);
                    }
                    else
                    {
                        innerField.SetHiddenRule(expression);
                    }
                }
            }
        }

        public IList<InnerFieldState> GetFieldStates(object entity)
        {
            IList<InnerFieldState> states = new List<InnerFieldState>();
            foreach (var field in this.Fields)
            {
                var state = new InnerFieldState
                {
                    PropertyType = field.propertyType,
                    ContainerType = field.containerType,
                    Compiled = field.compiled,
                    Validators = field.validators,
                    Expression = field.expression,
                    IsReadonly = this.IsReadonly(entity) || field.IsReadonly(entity),
                    IsHidden = this.IsHidden(entity) || field.IsHidden(entity),
                    Name = field.expression.GetMemberName()
                };

                states.Add(state);
            }
            return states;
        }
    }
}