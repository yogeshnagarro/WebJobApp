namespace WebJobApp
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.Operations;
    using WebJobApp.Contracts;
    using WebJobApp.Model;

    [Route("api/[controller]"), ApiController]
    public class EnqueueJobController : ControllerBase
    {
        #region Fields

        private readonly IBackgroundJobManager backgroundJobManager;

        #endregion

        #region Constructors & Destructors

        public EnqueueJobController(IBackgroundJobManager backgroundJobManager)
        {
            this.backgroundJobManager = backgroundJobManager;
        }

        #endregion

        #region Methods

        #region Public Methods

        [HttpGet("Index")]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok("Hello World!!"));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(BackgroundJobParam bgJobParam)
        {
            backgroundJobManager.EnqueueJob(bgJobParam);
            return await Task.FromResult(Ok());
        }

        #endregion

        #endregion
    }
}