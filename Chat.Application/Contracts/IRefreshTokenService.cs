using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Contracts
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        string ComputeSha256Hash(string value);
    }
}
