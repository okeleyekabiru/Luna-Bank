using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lunabank.Data.Entities;

namespace Lunabank.Data.Models
{
  public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Accounts, AccountModel>()
                .ForMember(r=> r.FirstName, o=> o.MapFrom(s=> s.User.FirstName))
                .ForMember(r=> r.LastName, o=> o.MapFrom(s=> s.User.LastName));
            
        }
    }
}
