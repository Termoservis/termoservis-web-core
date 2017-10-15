using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Termoservis.Models;

namespace Termoservis.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// signInManager
        /// or
        /// logger
        /// </exception>
        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        //
        // POST: Account/Logout
        /// <summary>
        /// Signouts the current user.
        /// </summary>
        /// <returns>Returns the redirect to index page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }
    }
}