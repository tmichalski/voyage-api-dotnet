﻿using System.Threading.Tasks;

namespace Voyage.Services.Phone
{
    public interface IPhoneService
    {
        string GenerateSecurityCode();

        void InsertSecurityCode(int phoneId, string code);

        bool IsValidPhoneNumber(string phoneNumber, out string formatedPhoneNumber);

        string GetE164Format(string phoneNumber);

        Task SendSecurityCode(string phoneNumber, string securityCode);

        Task<bool> IsValidSecurityCode(string userId, string securityCode);

        Task ClearUserPhoneSecurityCode(string userId);
    }
}