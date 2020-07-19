﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Destiny.Core.Api
{

    public class DistributedCache<TCacheData> : DistributedCache<string,TCacheData>, IDistributedCache<TCacheData>
               where TCacheData : class
    {
       
    }
    public class DistributedCache<TKey, TCacheData> : IDistributedCache<TKey, TCacheData>
         where TCacheData : class
    {




        private string GetKey(TKey key)
        {

            return key.ToString();
        }

        public TCacheData Get(TKey key)
        {


         
            return RedisHelper.Get<TCacheData>(GetKey(key));
        }

        public async Task<TCacheData> GetAsync(TKey key, CancellationToken token = default)
        {
            return await RedisHelper.GetAsync<TCacheData>(GetKey(key));
        }

        public TCacheData GetOrAdd(TKey key, Func<TCacheData> factory)
        {
            var value = this.Get(key);
        

            if (!Equals(value, default(TCacheData)))
            {
                return value;
            }

            value =  factory();

            if (Equals(value, default(TCacheData)))
            {
                return default;
            }

            Set(key, value);
            return value;
        }

        public async Task<TCacheData> GetOrAddAsync([NotNull] TKey key, Func<Task<TCacheData>> factory, CancellationToken token = default)
        {
            var value =await this.GetAsync(key);


            if (!Equals(value, default(TCacheData)))
            {
                return value;
            }

            value =await factory();

            if (Equals(value, default(TCacheData)))
            {
                return default;
            }

            await SetAsync(key, value);
            return value;
        }

        public void Remove(TKey key)
        {

            RedisHelper.Del(this.GetKey(key));
        }

        public async Task RemoveAsync(TKey key, CancellationToken token = default)
        {
           await RedisHelper.DelAsync(this.GetKey(key));
        }

        public void Set(TKey key, TCacheData value)
        {

            RedisHelper.Set(GetKey(key), value);
        }

        public async Task SetAsync(TKey key, TCacheData value, CancellationToken token = default)
        {
            await RedisHelper.SetAsync(GetKey(key), value);
        }
    }
}
