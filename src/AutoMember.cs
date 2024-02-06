using System;
using System.Reflection;
using Soenneker.Reflection.Cache.Types;
using Soenneker.Utils.AutoBogus.Config;
using Soenneker.Utils.AutoBogus.Services;

namespace Soenneker.Utils.AutoBogus;

internal sealed class AutoMember
{
    internal readonly string Name;

    internal readonly CachedType CachedType;

    internal readonly bool IsReadOnly;

    internal readonly Func<object, object?> Getter;

    internal readonly Action<object, object?> Setter;

    internal readonly bool ShouldSkip = false;

    internal readonly bool IsDictionary = false;

    internal readonly bool IsCollection = false;

    internal AutoMember(FieldInfo fieldInfo, AutoFakerConfig config)
    {
        Name = fieldInfo.Name;

        // Extract the required member info

        CachedType = CacheService.Cache.GetCachedType(fieldInfo.FieldType);
        IsReadOnly = !fieldInfo.IsPrivate && fieldInfo.IsInitOnly;
        Getter = fieldInfo.GetValue;

        if (!IsReadOnly)
            Setter = fieldInfo.SetValue;

        IsDictionary = CachedType.IsDictionary;
        IsCollection = CachedType.IsCollection;

        if (config.SkipTypes != null && config.SkipTypes.Contains(CachedType.Type))
        {
            ShouldSkip = true;
        }

        // Skip if the path is found
        if (config.SkipPaths != null && config.SkipPaths.Contains($"{CachedType.Type.FullName}.{Name}"))
        {
            ShouldSkip = true;
        }
    }

    internal AutoMember(PropertyInfo propertyInfo, AutoFakerConfig config)
    {
        Name = propertyInfo.Name;

        CachedType = CacheService.Cache.GetCachedType(propertyInfo.PropertyType);
        IsReadOnly = !propertyInfo.CanWrite;
        Getter = obj => propertyInfo.GetValue(obj, []);

        if (!IsReadOnly)
            Setter = (obj, value) => propertyInfo.SetValue(obj, value, []);

        IsDictionary = CachedType.IsDictionary;
        IsCollection = CachedType.IsCollection;

        if (config.SkipTypes != null && config.SkipTypes.Contains(CachedType.Type))
        {
            ShouldSkip = true;
        }

        // Skip if the path is found
        if (config.SkipPaths != null && config.SkipPaths.Contains($"{CachedType.Type.FullName}.{Name}"))
        {
            ShouldSkip = true;
        }
    }
}