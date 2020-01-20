using System;

namespace WebJobApp.Contracts
{
    using WebJobApp.Model;

    public interface IBackgroundJobManager
    {
        void EnqueueJob(IBackgroundJobParam bgJobParam);
    }
}
