using AutoMapper;
using GoodsAccountingSystem.Models;
using GoodsAccountingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterViewModel, UserModel>();
            CreateMap<UserModel, RegisterViewModel>();

            CreateMap<UserModel, UserViewModel>();
            CreateMap<UserViewModel, UserModel>();

            CreateMap<CreateGoodViewModel, GoodModel>();
            CreateMap<GoodModel, CreateGoodViewModel>();

            CreateMap<GoodModel, GoodViewModel>();
        }
    }
}
