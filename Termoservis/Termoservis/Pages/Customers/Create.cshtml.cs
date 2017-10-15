using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Termoservis.DAL.Repositories;
using Termoservis.Models;

namespace Termoservis.Pages.Customers
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICountriesRepository countriesRepository;


        public CreateModel(ICountriesRepository countriesRepository)
        {
            this.countriesRepository = countriesRepository ?? throw new ArgumentNullException(nameof(countriesRepository));
        }



        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;

            // Populate data
            this.AvailableCountries.AddRange(this.countriesRepository.GetAll().ToList());
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            return Page();
        }

        public List<Country> AvailableCountries { get; set; } = new List<Country>();

        [BindProperty]
        public CustomerInputModel Input { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class CustomerInputModel
    {
        [BindProperty]
        public AddressInputModel Address { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Naziv")]
        public string Name { get; set; }

        [Display(Name = "Bilješke")]
        public string Note { get; set; }

        [Display(Name = "Telefonski brojevi")]
        public List<TelephoneNumber> TelephoneNumbers { get; set; } = new List<TelephoneNumber>();

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class AddressInputModel
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Adresa")]
        public string StreetName { get; set; }

        [Required]
        [DisplayName("Mjesto")]
        public int? PlaceId { get; set; }

        [Required]
        [DisplayName("Država")]
        public int? CountryId { get; set; }
    }
}