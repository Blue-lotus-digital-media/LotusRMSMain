using AutoMapper;
using LotusRMS.Models.Viewmodels.Login;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models.MapperProfiles
{
    public class AppProfile:Profile
    {
        public AppProfile()
        {
            CreateMap<ExternalLoginVM, RMSUser>();
        }
    }
}
