using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shedule.Common
{
    namespace Extensions
    {
        using System.Data;
        using System.Data.Objects;

        public static class Extension
        {
            public static IEnumerable<T> Local<T>(this ObjectSet<T> objectSet) where T : class // позволяет обращаться к локально добавленным объектам, ещё не сохранённым
            {
                return from stateEntry in objectSet.Context.ObjectStateManager
                                                   .GetObjectStateEntries(EntityState.Added |
                                                                          EntityState.Modified |
                                                                          EntityState.Unchanged)
                       where stateEntry.Entity != null && stateEntry.EntitySet == objectSet.EntitySet
                       select stateEntry.Entity as T;
            }
        }
    }
}
