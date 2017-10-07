using Termoservis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Termoservis.DAL
{
	/// <summary>
	/// The application database context.
	/// </summary>
	/// <seealso cref="ApplicationUser" />
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public DbSet<Address> Addresses { get; set; }

		/// <summary>
		/// Gets or sets the countries.
		/// </summary>
		/// <value>
		/// The countries.
		/// </value>
		public DbSet<Country> Countries { get; set; }

		/// <summary>
		/// Gets or sets the customers.
		/// </summary>
		/// <value>
		/// The customers.
		/// </value>
		public DbSet<Customer> Customers { get; set; }

		/// <summary>
		/// Gets or sets the places.
		/// </summary>
		/// <value>
		/// The places.
		/// </value>
		public DbSet<Place> Places { get; set; }

		/// <summary>
		/// Gets or sets the telephone numbers.
		/// </summary>
		/// <value>
		/// The telephone numbers.
		/// </value>
		public DbSet<TelephoneNumber> TelephoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the work items.
        /// </summary>
        /// <value>
        /// The work items.
        /// </value>
        public DbSet<WorkItem> WorkItems { get; set; }

        /// <summary>
        /// Gets or sets the workers.
        /// </summary>
        /// <value>
        /// The workers.
        /// </value>
        public DbSet<Worker> Workers { get; set; }

        /// <summary>
        /// Gets or sets the customer devices.
        /// </summary>
        /// <value>
        /// The customer devices.
        /// </value>
        public DbSet<CustomerDevice> CustomerDevices { get; set; }
	}
}