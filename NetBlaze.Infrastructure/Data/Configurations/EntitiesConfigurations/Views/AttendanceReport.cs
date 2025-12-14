using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlaze.Domain.Entities.Views;
using NetBlaze.SharedKernel.Dtos.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Infrastructure.Data.Configurations.EntitiesConfigurations.Views
{
    public class AttendanceReport : IEntityTypeConfiguration<AttendanceReportDTO>
    {
        public void Configure(EntityTypeBuilder<AttendanceReportDTO> builder)
        {
            builder
                 .HasNoKey()
                 .ToView("vw_attendance_flat");
        }
    }
}
