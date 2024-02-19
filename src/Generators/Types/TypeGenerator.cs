﻿using Soenneker.Reflection.Cache.Types;
using Soenneker.Utils.AutoBogus.Context;
using Soenneker.Utils.AutoBogus.Generators.Abstract;
using Soenneker.Utils.AutoBogus.Services;

namespace Soenneker.Utils.AutoBogus.Generators.Types;

internal sealed class TypeGenerator<TType> : IAutoFakerGenerator
{
    object IAutoFakerGenerator.Generate(AutoFakerContext context)
    {
        // Note that all instances are converted to object to cater for boxing and struct population
        // When setting a value via reflection on a struct a copy is made
        // This means the changes are applied to a different instance to the one created here
        CachedType cachedType = CacheService.Cache.GetCachedType(typeof(TType));

        object instance = context.Binder.CreateInstance<TType>(context, cachedType);

        if (instance == null)
            return null!;

        // Populate the generated instance
        context.Binder.PopulateInstance<TType>(instance, context, cachedType);

        return instance;
    }
}