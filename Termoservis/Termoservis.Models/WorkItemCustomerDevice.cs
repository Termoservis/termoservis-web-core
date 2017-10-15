using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Termoservis.Models
{
    public class WorkItemCustomerDevice : IEntityTypeConfiguration<WorkItemCustomerDevice>
    {
        public long CustomerDeviceId { get; set; }

        public CustomerDevice CustomerDevice { get; set; }

        public long WorkItemId { get; set; }

        public WorkItem WorkItem { get; set; }


        public void Configure(EntityTypeBuilder<WorkItemCustomerDevice> builder)
        {
            builder.HasKey(r => new {r.CustomerDeviceId, r.WorkItemId});

            builder
                .HasOne(r => r.CustomerDevice)
                .WithMany(cd => cd.WorkItems)
                .HasForeignKey(r => r.CustomerDeviceId);

            builder
                .HasOne(r => r.WorkItem)
                .WithMany(wi => wi.AffectedDevices)
                .HasForeignKey(r => r.WorkItemId);
        }
    }
}