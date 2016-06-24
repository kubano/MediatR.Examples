﻿
namespace KnightFrank.Antares.Domain.AttributeConfiguration.Common.Extensions
{
    using System.Collections.Generic;

    using KnightFrank.Antares.Domain.AttributeConfiguration.Enums;
    using KnightFrank.Antares.Domain.AttributeConfiguration.Fields;

    public static class DictionaryExtensions
    {
        public static void AddControl(this IList<Control> list, PageType pageType, ControlCode controlCode)
        {
            list.Add(new Control(pageType, controlCode));
        }
    }
}
