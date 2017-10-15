using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Termoservis.Extensions;
using Termoservis.Models;

namespace Termoservis.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LoginModel> logger;


        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(AccountResources), ErrorMessageResourceName = "EmailRequired")]
            [EmailAddress(ErrorMessageResourceType = typeof(AccountResources), ErrorMessageResourceName = "EmailNotValid")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessageResourceType = typeof(AccountResources), ErrorMessageResourceName = "PasswordRequired")]
            [DataType(DataType.Password, ErrorMessageResourceType = typeof(AccountResources), ErrorMessageResourceName = "PasswordNotValid")]
            [Display(Name = "Zaporka")]
            public string Password { get; set; }

            [Display(Name = "Zapamti prijavu?")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
            }

            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;

            // Validate model (re-display form if model is not valid)
            if (!this.ModelState.IsValid)
                return Page();

            // Try to sign-in
            var result = await this.signInManager.PasswordSignInAsync(this.Input.Email, this.Input.Password, this.Input.RememberMe, lockoutOnFailure: true);

            // Handle success
            if (result.Succeeded)
            {
                this.logger.LogInformation("User logged in.");
                return LocalRedirect(this.Url.GetLocalUrl(returnUrl));
            }

            // Handle lockout
            if (result.IsLockedOut)
            {
                this.logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }

            // If not any of above, login attempt is invalid.
            this.ModelState.AddModelError(string.Empty, "Neispravan email ili zaporka.");
            return Page();
        }
    }
}
