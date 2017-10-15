using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Termoservis.Controllers.Select2;
using Termoservis.DAL.Repositories;
using Termoservis.Libs.Extensions;
using Termoservis.Models;

namespace Termoservis.Controllers
{
    /// <summary>
    /// The places API controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PlacesController : Controller
    {
        private const int Select2ItemPerPage = 30;

        private readonly IPlacesRepository placesRepository;
        private readonly ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="PlacesController"/> class.
        /// </summary>
        /// <param name="placesRepository">The places repository.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// placesRepository
        /// or
        /// logger
        /// </exception>
        public PlacesController(IPlacesRepository placesRepository, ILogger<PlacesController> logger)
        {
            this.placesRepository = placesRepository ?? throw new ArgumentNullException(nameof(placesRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (!placesRepository.GetAll().Any())
            {
                this.placesRepository.AddAsync(new Place
                {
                    Name = "Zagreb",
                    Country = new Country
                    {
                        Name = "Hrvatska",
                        SearchKeywords = "hrvatska"
                    },
                    SearchKeywords = "zagreb hrvatska"
                });
            }
        }


        //
        // POST api/places/GetAllSelect2        
        /// <summary>
        /// Gets all places.
        /// This will apply request data (search term and page).
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns collection of 30 places filtered by search term and of specified page.</returns>
        [HttpPost]
        public IActionResult GetAllSelect2([FromBody]Select2RequestData request)
        {
            var response = new Select2Response();

            // Get places query
            var placesQuery = this.placesRepository.GetAll();

            // Apply term if appropriate
            var term = request.Term?.AsSearchable() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(term))
                placesQuery = placesQuery.Where(place => place.SearchKeywords.Contains(term));

            // Calcualte how many items we need to skip
            int page;
            var didParse = int.TryParse(request.Page ?? string.Empty, out page);
            var toSkip = didParse ? (page - 1) * Select2ItemPerPage : 0;

            // Execute query and transform data to Select2 items
            response.Items = placesQuery
                .OrderBy(place => place.Name)
                .Skip(toSkip)
                .Take(Select2ItemPerPage)
                .ToList()
                .Select(place => new Select2Item
                {
                    Id = place.Id.ToString(),
                    Text = place.Name
                })
                .ToList();

            return Ok(response);
        }
    }
}