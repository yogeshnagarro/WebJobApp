namespace WebJobApp.BAL
{
    using System;
    using Hangfire;
    using WebJobApp.Common;
    using WebJobApp.Contracts;
    using WebJobApp.Model;

    public class BackgroundJobManager : IBackgroundJobManager
    {
        #region Methods

        #region Public Methods

        public void EnqueueJob(IBackgroundJobParam bgJobParam)
        {
            IBackgroundJobType bgJobType = GetBackgroundJobType(bgJobParam.BackgroundJobTypeID);
            IBackgroundJobHandler bgBackgroundJobHandler =
                (IBackgroundJobHandler) Helper.LoadObject(bgJobType.HandlerAssemblyName, bgJobType.HandlerClassName);
            BackgroundJob.Enqueue(() => bgBackgroundJobHandler.Process(bgJobParam, null));
        }

        #endregion

        #region Private Methods

        private IBackgroundJobType GetBackgroundJobType(int backgroundJobTypeID) =>
            throw new NotImplementedException("GetBackgroundJobType not implemented..!");

        #endregion

        #endregion
    }
}