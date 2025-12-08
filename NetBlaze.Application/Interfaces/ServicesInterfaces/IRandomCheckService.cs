using Microsoft.Extensions.Logging;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.Dtos.RandomlyCheck;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IRandomCheckService
    {
        Task<ApiResponse<RandomlyCheckResponseDTO>> GenerateOtpRecord(long userId, CancellationToken cancellationToken);

        Task<ApiResponse<bool>> OtpValidation(long userId,string otp, CancellationToken cancellationToken); 
    }
}
