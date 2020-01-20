using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebJobApp.Filters
{
    using Hangfire.Common;
    using Hangfire.States;
    using Hangfire.Storage;

    public class JobTransitionFilter: JobFilterAttribute, IElectStateFilter, IApplyStateFilter
    {
        public void OnStateElection(ElectStateContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            //throw new NotImplementedException();
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            //throw new NotImplementedException();
        }
    }
}
