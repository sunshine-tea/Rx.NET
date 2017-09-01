﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(this IAsyncEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Select(x => x);
        }

        public static IAsyncEnumerable<TValue> Return<TValue>(TValue value)
        {
            return new[] { value }.ToAsyncEnumerable();
        }

        public static IAsyncEnumerable<TValue> Throw<TValue>(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            return CreateEnumerable(
                () => CreateEnumerator<TValue>(
                    ct =>
                    {
                        var tcs = new TaskCompletionSource<bool>();
                        tcs.TrySetException(exception);
                        return tcs.Task;
                    },
                    current: null,
                    dispose: null)
            );
        }
    }
}