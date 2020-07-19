using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Destiny.Core.Api
{

    public interface IDistributedCache<TCacheData> : IDistributedCache<string, TCacheData> where TCacheData : class { 
    
    } 

    public interface IDistributedCache<TKey,TCacheData>
         where TCacheData : class
    {

        TCacheData Get(TKey key);

        TCacheData GetOrAdd(
        TKey key,
        Func<TCacheData> factory);

        Task<TCacheData> GetAsync(TKey key, CancellationToken token = default);

        Task<TCacheData> GetOrAddAsync(
             [NotNull] TKey key,
             Func<Task<TCacheData>> factory,
             CancellationToken token = default
         );


        #region 设置
        void Set(TKey key,  TCacheData value);
        Task SetAsync(TKey key, TCacheData value, CancellationToken token = default);
        #endregion



        #region 删除

        void Remove(TKey key);

        Task RemoveAsync(TKey key, CancellationToken token = default);
        #endregion
    }
}
